// Command smokeclient exercises the prototype server end to end over real TCP.
// It sends one frame at a time and waits for the matching S2C, collecting
// interleaved server pushes (UPSN==0) which are expected during room play.
package main

import (
	"flag"
	"fmt"
	"net"
	"os"
	"time"

	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

type client struct {
	conn   net.Conn
	sid    int64
	up     int64
	pushes []wire.Frame
}

func (c *client) wait(want uint16, timeout time.Duration) (wire.Frame, error) {
	deadline := time.Now().Add(timeout)
	for {
		if err := c.conn.SetReadDeadline(deadline); err != nil {
			return wire.Frame{}, err
		}
		f, err := wire.ReadFrame(c.conn)
		if err != nil {
			return wire.Frame{}, err
		}
		if f.SessionID != 0 {
			c.sid = f.SessionID
		}
		if f.CmdID == want {
			return f, nil
		}
		if f.UPSN == 0 {
			c.pushes = append(c.pushes, f)
			continue
		}
		return wire.Frame{}, fmt.Errorf("unexpected CMDID %d while waiting for %d", f.CmdID, want)
	}
}

func (c *client) call(cmd, want uint16, m proto.Message) (wire.Frame, error) {
	c.up++
	var body []byte
	var err error
	if m != nil {
		if body, err = proto.Marshal(m); err != nil {
			return wire.Frame{}, err
		}
	}
	if err := wire.WriteFrame(c.conn, wire.Frame{SessionID: c.sid, CmdID: cmd, Version: [3]byte{1, 0, 0}, UPSN: c.up, Payload: body}); err != nil {
		return wire.Frame{}, err
	}
	f, err := c.wait(want, 10*time.Second)
	if err != nil {
		return wire.Frame{}, err
	}
	fmt.Printf("C2S %-5d -> S2C %-5d err=%-4d up=%-3d %d bytes\n", cmd, f.CmdID, f.Err, f.UPSN, len(f.Payload))
	return f, nil
}

func main() {
	addr := flag.String("addr", "127.0.0.1:8800", "server address")
	nick := flag.String("nick", "player-a", "dev nickname")
	flag.Parse()

	conn, err := net.DialTimeout("tcp", *addr, 5*time.Second)
	if err != nil {
		fmt.Fprintln(os.Stderr, "dial:", err)
		os.Exit(1)
	}
	defer conn.Close()
	c := &client{conn: conn}

	// 5001 Connect
	f, err := c.call(5001, 5002, &protocolpb.ConnectC2S{
		Auth: protocolpb.AuthType_Dev, ClientVer: "3.2.0",
		AuthInfo: &protocolpb.ConnectC2S_Dev{Dev: &protocolpb.DevInfo{Nick: *nick}},
	})
	if err != nil {
		fmt.Fprintln(os.Stderr, "connect:", err)
		os.Exit(1)
	}
	var connected protocolpb.ConnectS2C
	if err := proto.Unmarshal(f.Payload, &connected); err != nil {
		fmt.Fprintln(os.Stderr, "decode ConnectS2C:", err)
		os.Exit(1)
	}
	fmt.Printf("  account=%s player=%d session=%d\n", connected.GetAccount().GetNick(), connected.GetPlayer().GetId(), connected.SessionId)

	// 5003 Heartbeat
	if _, err = c.call(5003, 5004, &protocolpb.HeartbeatC2S{Client: time.Now().Unix()}); err != nil {
		fmt.Fprintln(os.Stderr, "heartbeat:", err)
		os.Exit(1)
	}
	// 5005 CreateRoom with the first real map resolved by the server
	f, err = c.call(5005, 5006, &protocolpb.CreateRoomC2S{Name: "smoke room", MaxTime: 30})
	if err != nil {
		fmt.Fprintln(os.Stderr, "create room:", err)
		os.Exit(1)
	}
	var created protocolpb.CreateRoomS2C
	_ = proto.Unmarshal(f.Payload, &created)
	roomID := created.GetRoom().GetId()
	fmt.Printf("  room=%d map=%d mapEvents=%v\n", roomID, created.GetRoom().GetMapId(), created.GetRoom().GetMapEventIds())

	// 5131 Ready, 5019 Start
	if _, err = c.call(5131, 5132, &protocolpb.RoomReadyC2S{IsReady: true}); err != nil {
		fmt.Fprintln(os.Stderr, "ready:", err)
		os.Exit(1)
	}
	if _, err = c.call(5019, 5020, &protocolpb.StartGameC2S{}); err != nil {
		fmt.Fprintln(os.Stderr, "start:", err)
		os.Exit(1)
	}
	// 5021 ThrowDice, 5027 Move
	f, err = c.call(5021, 5022, &protocolpb.ThrowDiceC2S{})
	if err != nil {
		fmt.Fprintln(os.Stderr, "dice:", err)
		os.Exit(1)
	}
	var dice protocolpb.ThrowDiceS2C
	_ = proto.Unmarshal(f.Payload, &dice)
	fmt.Printf("  dice=%v movePoint=%d\n", dice.GetVals(), dice.GetMovePoint())

	if _, err = c.call(5027, 5028, &protocolpb.MoveC2S{Path: []int32{1, 2}}); err != nil {
		fmt.Fprintln(os.Stderr, "move:", err)
		os.Exit(1)
	}
	// Drain pushes for a moment so room/round notifications are visible.
	fmt.Println("--- pushes ---")
	_ = conn.SetReadDeadline(time.Now().Add(1200 * time.Millisecond))
	for {
		p, err := wire.ReadFrame(conn)
		if err != nil {
			break
		}
		fmt.Printf("PUSH cmd=%-5d err=%-4d %d bytes\n", p.CmdID, p.Err, len(p.Payload))
	}
	fmt.Printf("--- %d push frames collected while waiting for responses ---\n", len(c.pushes))
	for _, p := range c.pushes {
		fmt.Printf("  queued push cmd=%-5d %d bytes\n", p.CmdID, len(p.Payload))
	}
}
