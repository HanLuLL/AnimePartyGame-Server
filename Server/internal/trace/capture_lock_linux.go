//go:build linux

package trace

import (
	"fmt"
	"os"
	"path/filepath"
	"syscall"
)

func lockCaptureDirectory(dir string) (*os.File, error) {
	path := filepath.Join(dir, ".capture.lock")
	file, err := openOrCreateCaptureLock(path)
	if err != nil {
		return nil, fmt.Errorf("open capture directory lock: %w", err)
	}
	if err = syscall.Flock(int(file.Fd()), syscall.LOCK_EX|syscall.LOCK_NB); err != nil {
		_ = file.Close()
		return nil, fmt.Errorf("capture directory is already in use: %w", err)
	}
	return file, nil
}

func unlockCaptureDirectory(file *os.File) error {
	unlockErr := syscall.Flock(int(file.Fd()), syscall.LOCK_UN)
	closeErr := file.Close()
	if unlockErr != nil {
		return unlockErr
	}
	return closeErr
}
