package gateway

const (
	ErrSucc                    int16 = 0
	ErrServerClosed            int16 = 1001
	ErrAuth                    int16 = 10000
	ErrPlayerNotFind           int16 = 12001
	ErrFriendApplyNotFind      int16 = 12006
	ErrPlayerNotFriend         int16 = 12009
	ErrInvalidParam            int16 = 10013
	ErrNotOpen                 int16 = 10015
	ErrItemEnough              int16 = 10011
	ErrRepeatedReward          int16 = 10017
	ErrRoomChatCd              int16 = 11075
	ErrRoomNotExist            int16 = 11001
	ErrRoomPlayerAlreadyJoin   int16 = 11002
	ErrRoomSlotErr             int16 = 11003
	ErrRoomSlotAlreadyOccupied int16 = 11004
	ErrRoomPlayerNotExist      int16 = 11005
	ErrRoomFull                int16 = 11007
	ErrRoomNotWait             int16 = 11008
	ErrRoomPwd                 int16 = 11009
	ErrRoomPlayerTooLittle     int16 = 11010
	ErrRoomNotReady            int16 = 11015
	ErrRoomMapNotExist         int16 = 11016
	ErrRoomHeroNotExist        int16 = 11011
	ErrRoomHeroAlreadyChoice   int16 = 11012
	ErrRoomHeroNotUse          int16 = 11014
	ErrRoomHeroNotAffirmed     int16 = 11069
	ErrRoomHeroNotChoice       int16 = 11070
	ErrRoomChooseSkinNotOwn    int16 = 11086
	ErrRoomChooseSkinMismatch  int16 = 11087
	ErrRoomChooseSkinNotExist  int16 = 11088
	ErrRoomChooseSkinNeedHero  int16 = 11089
	ErrRoomChooseSkinNeedBox   int16 = 11090
	ErrRoomActionIncorrect     int16 = 11019
	ErrRoomNotAction           int16 = 11021
	ErrRoomActionAlreadyDone   int16 = 11022
	ErrRoomActionPlayer        int16 = 11024
)
