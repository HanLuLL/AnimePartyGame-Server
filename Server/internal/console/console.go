// Package console implements the interactive server console: a stdin command
// loop. Operators type commands at the "> " prompt;
// "/" prefixes are accepted but optional. Console callers always have full
// permission.
package console

import (
	"bufio"
	"context"
	"fmt"
	"os"
	"sort"
	"strings"
)

// Commander is the server surface the console needs. *gateway.Server
// implements it.
type Commander interface {
	// ConsoleExec runs one operator command line and returns the reply text.
	ConsoleExec(ctx context.Context, line string) (string, error)
	// ConsoleUsages lists the command usage strings for /help.
	ConsoleUsages() []string
	// Stop gracefully shuts the server down (used by /stop).
	Stop()
}

// Run reads stdin lines forever and executes them. It blocks until stdin
// closes or the /stop command runs; both terminate the process the same way
// the signal handler does.
func Run(ctx context.Context, srv Commander, logf func(format string, args ...any)) {
	reader := bufio.NewReader(os.Stdin)
	for {
		fmt.Print("> ")
		line, err := reader.ReadString('\n')
		if err != nil {
			// stdin closed (EOF): exit like other terminal-first servers do on Ctrl+D.
			logf("console closed, shutting down")
			srv.Stop()
			return
		}
		line = strings.TrimSpace(line)
		if line == "" {
			continue
		}
		name := strings.Fields(line)[0]
		if strings.HasPrefix(name, "/") || strings.HasPrefix(name, "!") {
			name = name[1:]
		}
		if name == "stop" || name == "exit" {
			logf("stop requested from console")
			srv.Stop()
			return
		}
		reply, err := srv.ConsoleExec(ctx, line)
		if err != nil {
			logf("[ERROR] %v", err)
			continue
		}
		fmt.Println(reply)
	}
}

// UsageTable returns the sorted usage strings with the command name first.
func UsageTable(usages []string) []string {
	out := append([]string(nil), usages...)
	sort.Strings(out)
	return out
}
