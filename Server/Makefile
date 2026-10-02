.PHONY: test vet build run smoke codegen proto-map handler-status

vet:
	CGO_ENABLED=1 go vet -mod=vendor ./...

test:
	CGO_ENABLED=1 go test -mod=vendor ./...

build:
	mkdir -p bin
	CGO_ENABLED=1 go build -mod=vendor -o bin/server .
	CGO_ENABLED=1 go build -mod=vendor -o bin/smokeclient ./internal/smokeclient

run:
	CGO_ENABLED=1 go run -mod=vendor .

smoke:
	CGO_ENABLED=1 go run -mod=vendor ./internal/smokeclient -addr 127.0.0.1:8800 -nick player-a

codegen:
	gh workflow run codegen.yml --repo HanLuLL/AstralParty --ref main

proto-map handler-status: codegen
