//go:build !linux && !windows

package trace

import (
	"errors"
	"os"
)

func lockCaptureDirectory(string) (*os.File, error) {
	return nil, errors.New("persistent capture locking is supported only on Linux and Windows")
}

func unlockCaptureDirectory(file *os.File) error {
	if file == nil {
		return nil
	}
	return file.Close()
}
