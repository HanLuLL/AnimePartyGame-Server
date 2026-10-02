package protocol

import (
	_ "embed"
	"encoding/json"
	"fmt"
	"strings"

	_ "astralparty-server/internal/protocol/gen/errcode"
	_ "astralparty-server/internal/protocol/gen/model"
	_ "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
	"google.golang.org/protobuf/reflect/protoreflect"
	"google.golang.org/protobuf/reflect/protoregistry"
)

//go:embed command-map.json
var commandMapJSON []byte

type Command struct {
	CmdID           uint32  `json:"cmdId"`
	WrapperField    string  `json:"wrapperField"`
	Message         string  `json:"message"`
	MessageName     string  `json:"messageName"`
	Direction       string  `json:"direction"`
	Scope           string  `json:"scope"`
	PayloadField    string  `json:"payloadField"`
	ResponseCmdID   *uint32 `json:"responseCmdId"`
	ResponseMessage *string `json:"responseMessage"`
}

type Registry struct {
	byID   map[uint16]Command
	byName map[string]Command
}

func NewRegistry() (*Registry, error) {
	var rows []Command
	if err := json.Unmarshal(commandMapJSON, &rows); err != nil {
		return nil, err
	}
	r := &Registry{byID: make(map[uint16]Command, len(rows)), byName: make(map[string]Command, len(rows))}
	for _, row := range rows {
		if row.CmdID > 65535 {
			continue
		}
		if _, err := protoregistry.GlobalTypes.FindMessageByName(protoreflect.FullName(row.Message)); err != nil {
			return nil, fmt.Errorf("protobuf type %s for CMDID %d not registered: %w", row.Message, row.CmdID, err)
		}
		r.byID[uint16(row.CmdID)] = row
		r.byName[row.MessageName] = row
	}
	return r, nil
}
func (r *Registry) Lookup(id uint16) (Command, bool)      { c, ok := r.byID[id]; return c, ok }
func (r *Registry) ByMessage(name string) (Command, bool) { c, ok := r.byName[name]; return c, ok }
func (r *Registry) NewMessage(fullName string) (proto.Message, error) {
	fullName = strings.TrimPrefix(fullName, ".")
	mt, err := protoregistry.GlobalTypes.FindMessageByName(protoreflect.FullName(fullName))
	if err != nil {
		return nil, err
	}
	return mt.New().Interface(), nil
}
func (r *Registry) NewMessageForID(id uint16) (proto.Message, error) {
	c, ok := r.Lookup(id)
	if !ok {
		return nil, fmt.Errorf("unknown CMDID %d", id)
	}
	return r.NewMessage(c.Message)
}
func (r *Registry) ResponseFor(id uint16) (Command, bool) {
	c, ok := r.Lookup(id)
	if !ok || c.ResponseCmdID == nil {
		return Command{}, false
	}
	return r.Lookup(uint16(*c.ResponseCmdID))
}
