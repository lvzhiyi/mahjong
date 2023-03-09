using platform.spotred;

namespace basef.arena
{
    public class RoomEvent: StatusAble
    {
        /** 房间事件：0房间初始化（新建房间） */
        public const int INIT = 0;
        /** 房间事件：1房间比赛开始（比赛中） */
        public const int START = 1;
        /** 房间事件：2房间一局比赛结束（准备下一局比赛或结束） */
        public const int OVER = 2;
        /** 房间事件：3房间解散（任何阶段都可能出现） */
        public const int DISBAND = 3;
        /** 房间事件：4房间结束（比赛完成，传递比赛总计结果） */
        public const int FINISH = 4;
        /** 房间事件：5房间超时（房间长时间无人操作） */
        public const int TIMEOUT = 5;
        /** 房间事件：6房间等待（所有人都准备，但人数未满等待其他人加入） */
        public const int WAIT = 6;
    }
}
