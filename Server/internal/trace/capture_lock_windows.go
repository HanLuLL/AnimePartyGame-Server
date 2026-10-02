//go:build windows

package trace

import (
	"fmt"
	"os"
	"path/filepath"
	"syscall"
	"unsafe"
)

var (
	kernel32     = syscall.NewLazyDLL("kernel32.dll")
	lockFileEx   = kernel32.NewProc("LockFileEx")
	unlockFileEx = kernel32.NewProc("UnlockFileEx")
)

func lockCaptureDirectory(dir string) (*os.File, error) {
	file, err := openOrCreateCaptureLock(filepath.Join(dir, ".capture.lock"))
	if err != nil {
		return nil, fmt.Errorf("open capture directory lock: %w", err)
	}
	var overlapped syscall.Overlapped
	const failImmediately = 0x1
	const exclusiveLock = 0x2
	r1, _, callErr := lockFileEx.Call(file.Fd(), failImmediately|exclusiveLock, 0, 1, 0, uintptr(unsafe.Pointer(&overlapped)))
	if r1 == 0 {
		_ = file.Close()
		return nil, fmt.Errorf("capture directory is already in use: %w", callErr)
	}
	return file, nil
}

func unlockCaptureDirectory(file *os.File) error {
	var overlapped syscall.Overlapped
	r1, _, callErr := unlockFileEx.Call(file.Fd(), 0, 1, 0, uintptr(unsafe.Pointer(&overlapped)))
	closeErr := file.Close()
	if r1 == 0 {
		return callErr
	}
	return closeErr
}
