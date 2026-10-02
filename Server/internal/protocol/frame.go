package protocol

import (
	"encoding/binary"
	"errors"
	"fmt"
	"io"
)

const HeaderSize = 35
const MaxPayload = 16 << 20 // defensive cap; not a client-proven protocol limit

type Frame struct {
	PayloadLength int32
	SessionID     int64
	CmdID         uint16
	Version       [3]byte
	UPSN          int64
	DOWNSN        int64
	Err           int16
	Payload       []byte
	Raw           []byte // Exact captured wire bytes; ignored by Bytes.
}

func ReadFrame(r io.Reader) (Frame, error) {
	return readFrame(r, false)
}

// ReadFrameWithRaw preserves the exact header and payload bytes consumed from
// the stream. Call it only while a packet capture session is active so normal
// gameplay does not allocate a second copy of every frame.
func ReadFrameWithRaw(r io.Reader) (Frame, error) {
	return readFrame(r, true)
}

// ReadFrameWithRawOnStart preserves the bytes in one frame only when onStart
// returns true after the reader first supplies bytes. This lets a capture
// session begin while a connection is already blocked waiting for its next
// frame, without allocating a second copy of ordinary gameplay traffic.
func ReadFrameWithRawOnStart(r io.Reader, onStart func() bool) (Frame, error) {
	observed := &frameCaptureReader{reader: r, onStart: onStart}
	h := make([]byte, HeaderSize)
	_, err := io.ReadFull(observed, h)
	if err != nil {
		f := Frame{}
		if observed.capturing {
			f.Raw = observed.raw
		}
		return f, err
	}
	length := int32(binary.BigEndian.Uint32(h[0:4]))
	if length < 0 || length > MaxPayload {
		f := Frame{}
		if observed.capturing {
			f.Raw = observed.raw
		}
		return f, fmt.Errorf("invalid payload LENGTH=%d", length)
	}
	f := Frame{
		PayloadLength: length,
		SessionID:     int64(binary.BigEndian.Uint64(h[4:12])),
		CmdID:         binary.BigEndian.Uint16(h[12:14]),
		UPSN:          int64(binary.BigEndian.Uint64(h[17:25])),
		DOWNSN:        int64(binary.BigEndian.Uint64(h[25:33])),
		Err:           int16(binary.BigEndian.Uint16(h[33:35])),
	}
	copy(f.Version[:], h[14:17])
	if observed.capturing {
		f.Raw = make([]byte, HeaderSize+int(length))
		copy(f.Raw, h)
		f.Payload = f.Raw[HeaderSize:]
	} else {
		f.Payload = make([]byte, int(length))
	}
	if length > 0 {
		payloadBytes, readErr := io.ReadFull(r, f.Payload)
		if readErr != nil {
			if observed.capturing {
				f.Payload = f.Payload[:payloadBytes]
				f.Raw = f.Raw[:HeaderSize+payloadBytes]
				return f, readErr
			}
			return Frame{}, readErr
		}
	}
	return f, nil
}

type frameCaptureReader struct {
	reader    io.Reader
	onStart   func() bool
	started   bool
	capturing bool
	raw       []byte
}

func (r *frameCaptureReader) Read(p []byte) (int, error) {
	n, err := r.reader.Read(p)
	if n > 0 {
		if !r.started {
			r.started = true
			r.capturing = r.onStart != nil && r.onStart()
		}
		if r.capturing {
			r.raw = append(r.raw, p[:n]...)
		}
	}
	return n, err
}

func readFrame(r io.Reader, preserveRaw bool) (Frame, error) {
	var f Frame
	h := make([]byte, HeaderSize)
	headerBytes, err := io.ReadFull(r, h)
	if err != nil {
		if preserveRaw && headerBytes > 0 {
			f.Raw = append([]byte(nil), h[:headerBytes]...)
		}
		return f, err
	}
	length := int32(binary.BigEndian.Uint32(h[0:4]))
	if length < 0 || length > MaxPayload {
		if preserveRaw {
			f.Raw = append([]byte(nil), h...)
			return f, fmt.Errorf("invalid payload LENGTH=%d", length)
		}
		return Frame{}, fmt.Errorf("invalid payload LENGTH=%d", length)
	}
	f.PayloadLength = length
	f.SessionID = int64(binary.BigEndian.Uint64(h[4:12]))
	f.CmdID = binary.BigEndian.Uint16(h[12:14])
	copy(f.Version[:], h[14:17])
	f.UPSN = int64(binary.BigEndian.Uint64(h[17:25]))
	f.DOWNSN = int64(binary.BigEndian.Uint64(h[25:33]))
	f.Err = int16(binary.BigEndian.Uint16(h[33:35]))
	if preserveRaw {
		f.Raw = make([]byte, HeaderSize+int(length))
		copy(f.Raw, h)
		f.Payload = f.Raw[HeaderSize:]
	} else {
		f.Payload = make([]byte, int(length))
	}
	if length > 0 {
		payloadBytes, readErr := io.ReadFull(r, f.Payload)
		if readErr != nil {
			if preserveRaw {
				f.Payload = f.Payload[:payloadBytes]
				f.Raw = f.Raw[:HeaderSize+payloadBytes]
				return f, readErr
			}
			return Frame{}, readErr
		}
	}
	return f, nil
}

func (f Frame) Bytes() ([]byte, error) {
	if len(f.Payload) > MaxPayload {
		return nil, fmt.Errorf("payload too large: %d", len(f.Payload))
	}
	if f.CmdID == 0 {
		return nil, errors.New("CMDID cannot be zero for application frame")
	}
	buf := make([]byte, HeaderSize+len(f.Payload))
	binary.BigEndian.PutUint32(buf[0:4], uint32(len(f.Payload))) // LENGTH excludes the header
	binary.BigEndian.PutUint64(buf[4:12], uint64(f.SessionID))
	binary.BigEndian.PutUint16(buf[12:14], f.CmdID)
	copy(buf[14:17], f.Version[:])
	binary.BigEndian.PutUint64(buf[17:25], uint64(f.UPSN))
	binary.BigEndian.PutUint64(buf[25:33], uint64(f.DOWNSN))
	binary.BigEndian.PutUint16(buf[33:35], uint16(f.Err))
	copy(buf[HeaderSize:], f.Payload)
	return buf, nil
}

func WriteFrame(w io.Writer, f Frame) error {
	b, err := f.Bytes()
	if err != nil {
		return err
	}
	for len(b) > 0 {
		n, e := w.Write(b)
		if e != nil {
			return e
		}
		if n == 0 {
			return io.ErrShortWrite
		}
		b = b[n:]
	}
	return nil
}
