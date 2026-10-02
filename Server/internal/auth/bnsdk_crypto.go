package auth

import (
	"crypto/rc4"
	"encoding/base64"
	"errors"
)

// The target v3.2.0 APK reads this value from its BnSDK manifest metadata
// (YH_SIGN_KEY fallback / BN_PAYSIGN). It is client-distributed compatibility
// data, not a server credential. A newer client can override it via config.
const defaultBNSDKPaySign = "239e6d210b4070bd2fd160540a615c6e"

// encodeBNSDKUserID mirrors the bundled BnSDK response parser: RC4 over the
// UID bytes, then Android Base64.NO_WRAP (which retains standard padding).
func encodeBNSDKUserID(userID, paySign string) (string, error) {
	if userID == "" {
		return "", errors.New("empty BnSDK user ID")
	}
	if paySign == "" {
		paySign = defaultBNSDKPaySign
	}
	cipher, err := rc4.NewCipher([]byte(paySign))
	if err != nil {
		return "", err
	}
	plain := []byte(userID)
	encoded := make([]byte, len(plain))
	cipher.XORKeyStream(encoded, plain)
	return base64.StdEncoding.EncodeToString(encoded), nil
}
