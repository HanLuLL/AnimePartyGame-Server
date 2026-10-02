package gateway

import (
	"bytes"
	"encoding/base64"
	"fmt"
	"io"
	"net/http"
	"path"
	"strings"
	"time"

	traceevent "astralparty-server/internal/trace"
)

const maxRawHTTPBody = 16 << 20

// CaptureHTTP records every non-management HTTP transaction during an active
// session, including unknown paths that the server rejects. Admin and health
// endpoints are excluded so management credentials and probes are not captured.
func (s *Server) CaptureHTTP(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		if next == nil {
			return
		}
		sessionID, capturing := s.activeCaptureID()
		if !capturing || !captureHTTPPath(r.URL.Path) {
			if next != nil {
				next.ServeHTTP(w, r)
			}
			return
		}
		if !s.tryCaptureWork() {
			s.captures.MarkSessionIncomplete(sessionID, "producer_limit_reached")
			next.ServeHTTP(w, r)
			return
		}
		producer, ok := s.captures.BeginProducer(sessionID)
		if !ok {
			s.releaseCaptureWork()
			next.ServeHTTP(w, r)
			return
		}
		defer s.releaseCaptureWork()
		defer producer.Close()
		captureCompleted := false
		defer func() {
			if !captureCompleted {
				s.captures.MarkSessionIncomplete(sessionID, "http_capture_interrupted")
			}
		}()

		started := time.Now().UTC()
		method := r.Method
		requestTarget := r.URL.RequestURI()
		remoteAddr := r.RemoteAddr
		requestHeaders := cloneHTTPHeader(r.Header)
		if r.Host != "" {
			if requestHeaders == nil {
				requestHeaders = make(map[string][]string)
			}
			requestHeaders["Host"] = []string{r.Host}
		}
		message := method + " " + requestTarget
		originalBody := r.Body
		if originalBody == nil {
			originalBody = http.NoBody
		}
		requestBody, readErr := io.ReadAll(io.LimitReader(originalBody, maxRawHTTPBody+1))
		r.Body = &captureReadCloser{Reader: io.MultiReader(bytes.NewReader(requestBody), originalBody), Closer: originalBody}
		requestBytes := len(requestBody)
		if r.ContentLength > int64(requestBytes) {
			requestBytes = int(r.ContentLength)
		}
		requestOmitted := ""
		if readErr != nil {
			requestOmitted = "request_body_read_failed"
		} else if len(requestBody) > maxRawHTTPBody {
			requestBody = requestBody[:maxRawHTTPBody]
			requestOmitted = "body_over_16MiB"
		}

		connectionID := fmt.Sprintf("http-%x-%x", time.Now().UnixNano(), s.connectionSN.Add(1))
		requestEvent := traceevent.Event{
			Timestamp: started, Transport: "http", Direction: "request", ConnectionID: connectionID,
			RemoteAddr: remoteAddr, Message: message, HTTPMethod: method,
			RequestTarget: requestTarget, RequestHeaders: requestHeaders,
			PayloadBytes: requestBytes, RawBodyBase64: base64.StdEncoding.EncodeToString(requestBody),
			RawBodyOmitted: requestOmitted,
		}
		requestRecorded := producer.Append(requestEvent)
		response := &rawCaptureResponseWriter{ResponseWriter: w, limit: maxRawHTTPBody}
		next.ServeHTTP(response, r)
		status := response.status
		if status == 0 {
			status = http.StatusOK
		}
		responseOmitted := ""
		if response.overflow {
			responseOmitted = "body_over_16MiB"
		}
		if response.writeFailed {
			if responseOmitted != "" {
				responseOmitted += ";"
			}
			responseOmitted += "response_write_failed"
		}
		responseEvent := traceevent.Event{
			Timestamp: time.Now().UTC(), Transport: "http", Direction: "response", ConnectionID: connectionID,
			RemoteAddr: remoteAddr, Message: message, HTTPMethod: method, RequestTarget: requestTarget,
			HTTPStatus: status, ResponseHeaders: cloneHTTPHeader(response.Header()),
			PayloadBytes: response.bytesWritten, RawBodyBase64: base64.StdEncoding.EncodeToString(response.body.Bytes()),
			RawBodyOmitted: responseOmitted,
		}
		responseRecorded := producer.Append(responseEvent)
		captureCompleted = requestRecorded && responseRecorded
	})
}

func captureHTTPPath(requestPath string) bool {
	cleanPath := path.Clean("/" + strings.TrimLeft(requestPath, "/"))
	for _, candidate := range []string{requestPath, cleanPath} {
		lowerPath := strings.ToLower(candidate)
		for _, protectedPrefix := range []string{"/healthz"} {
			if lowerPath == protectedPrefix || strings.HasPrefix(lowerPath, protectedPrefix+"/") ||
				strings.HasPrefix(lowerPath, protectedPrefix+";") {
				return false
			}
		}
	}
	return true
}

func cloneHTTPHeader(header http.Header) map[string][]string {
	if len(header) == 0 {
		return nil
	}
	clone := make(map[string][]string, len(header))
	for key, values := range header {
		clone[key] = append([]string(nil), values...)
	}
	return clone
}

type rawCaptureResponseWriter struct {
	http.ResponseWriter
	status       int
	bytesWritten int
	limit        int
	overflow     bool
	writeFailed  bool
	body         bytes.Buffer
}

type captureReadCloser struct {
	io.Reader
	io.Closer
}

func (w *rawCaptureResponseWriter) WriteHeader(status int) {
	if w.status != 0 {
		return
	}
	w.status = status
	w.ResponseWriter.WriteHeader(status)
}

func (w *rawCaptureResponseWriter) Write(body []byte) (int, error) {
	if w.status == 0 {
		w.WriteHeader(http.StatusOK)
	}
	n, err := w.ResponseWriter.Write(body)
	if n < 0 {
		n = 0
	}
	if n > len(body) {
		n = len(body)
	}
	w.bytesWritten += n
	remaining := w.limit - w.body.Len()
	if remaining > 0 {
		if n > remaining {
			_, _ = w.body.Write(body[:remaining])
			w.overflow = true
		} else {
			_, _ = w.body.Write(body[:n])
		}
	} else if n > 0 {
		w.overflow = true
	}
	if err != nil || n != len(body) {
		w.writeFailed = true
	}
	return n, err
}

func (w *rawCaptureResponseWriter) Unwrap() http.ResponseWriter { return w.ResponseWriter }
