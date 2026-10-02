package auth

import (
	"crypto/tls"
	"errors"
	"fmt"
	"io"
	"net"
	"net/mail"
	"net/smtp"
	"strconv"
	"strings"
	"time"
)

type smtpMailer struct {
	host     string
	address  string
	user     string
	password string
	from     *mail.Address
	mode     string
}

func NewSMTPMailer(host string, port int, user, password, from, mode string) (Mailer, error) {
	host = strings.TrimSpace(host)
	if host == "" || port < 1 || port > 65535 || strings.ContainsAny(host, "\r\n/ ") {
		return nil, errors.New("invalid SMTP server address")
	}
	fromAddr, err := mail.ParseAddress(strings.TrimSpace(from))
	if err != nil || strings.ContainsAny(from, "\r\n") {
		return nil, errors.New("invalid SMTP sender address")
	}
	if mode != "starttls" && mode != "implicit" {
		return nil, errors.New("SMTP encryption mode must be starttls or implicit")
	}
	return &smtpMailer{host: host, address: net.JoinHostPort(host, strconv.Itoa(port)), user: user, password: password, from: fromAddr, mode: mode}, nil
}

func (m *smtpMailer) Send(to, subject, body string) error {
	if strings.ContainsAny(to+subject, "\r\n") {
		return errors.New("invalid mail header")
	}
	dialer := &net.Dialer{Timeout: 8 * time.Second}
	var conn net.Conn
	var err error
	serverName := m.host
	if m.mode == "implicit" {
		tlsDialer := &tls.Dialer{NetDialer: dialer, Config: &tls.Config{ServerName: serverName, MinVersion: tls.VersionTLS12}}
		conn, err = tlsDialer.Dial("tcp", m.address)
	} else {
		conn, err = dialer.Dial("tcp", m.address)
	}
	if err != nil {
		return fmt.Errorf("connect to SMTP server: %w", err)
	}
	defer conn.Close()
	client, err := smtp.NewClient(conn, serverName)
	if err != nil {
		return fmt.Errorf("initialize SMTP client: %w", err)
	}
	defer client.Close()
	if m.mode == "starttls" {
		if ok, _ := client.Extension("STARTTLS"); !ok {
			return errors.New("SMTP server does not offer STARTTLS")
		}
		if err := client.StartTLS(&tls.Config{ServerName: serverName, MinVersion: tls.VersionTLS12}); err != nil {
			return fmt.Errorf("start SMTP TLS: %w", err)
		}
	}
	if m.user != "" {
		if err := client.Auth(smtp.PlainAuth("", m.user, m.password, serverName)); err != nil {
			return fmt.Errorf("SMTP authentication failed: %w", err)
		}
	}
	if err := client.Mail(m.from.Address); err != nil {
		return fmt.Errorf("SMTP MAIL FROM failed: %w", err)
	}
	if err := client.Rcpt(to); err != nil {
		return fmt.Errorf("SMTP RCPT TO failed: %w", err)
	}
	w, err := client.Data()
	if err != nil {
		return fmt.Errorf("SMTP DATA failed: %w", err)
	}
	message := "From: " + m.from.String() + "\r\n" +
		"To: " + (&mail.Address{Address: to}).String() + "\r\n" +
		"Subject: " + subject + "\r\n" +
		"MIME-Version: 1.0\r\n" +
		"Content-Type: text/plain; charset=utf-8\r\n" +
		"Content-Transfer-Encoding: 8bit\r\n\r\n" + body
	if _, err := io.WriteString(w, message); err != nil {
		_ = w.Close()
		return fmt.Errorf("write SMTP message: %w", err)
	}
	if err := w.Close(); err != nil {
		return fmt.Errorf("finish SMTP message: %w", err)
	}
	if err := client.Quit(); err != nil {
		return fmt.Errorf("SMTP quit failed: %w", err)
	}
	return nil
}
