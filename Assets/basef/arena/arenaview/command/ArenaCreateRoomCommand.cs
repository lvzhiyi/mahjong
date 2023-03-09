using basef.rule;
using cambrian.common;
using cambrian.game;
using platform;
using scene.game;

namespace basef.arena
{
    public class ArenaCreateRoomCommand:UserCommand
    {
        private long clubid;

        private ArenaLockRule rule;
        /// <summary>
        /// 桌子编号
        /// </summary>
        private int deskIndex;

        public ArenaCreateRoomCommand(long clubid, int deskIndex,ArenaLockRule rule)
        {
            id = CommandConst.PORT_ARENA_ROOM_FKC_CREATE;
            this.clubid = clubid;
            this.deskIndex = deskIndex;
            this.rule = rule;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(clubid);
            data.writeInt(AccessPlatformMessage.n);
            data.writeInt(AccessPlatformMessage.e);
            data.writeInt(rule.uuid);
            data.writeInt(deskIndex);
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;
            Room.instance();
            Room.room.bytesRead(data);
            this.callbackobj = Room.room;
        }

    }
}
