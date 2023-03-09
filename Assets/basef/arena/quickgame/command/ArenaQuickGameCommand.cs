using cambrian.common;
using cambrian.game;
using platform;
using scene.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace basef.arena
{
    public class ArenaQuickGameCommand : UserCommand
    {
        long arenaid;
        int lockid;
        public ArenaQuickGameCommand(long arenaid, int lockid)
        {
            this.id = CommandConst.PORT_ARENA_QUICK_GAME;
            this.arenaid = arenaid;
            this.lockid = lockid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeInt(AccessPlatformMessage.n);
            data.writeInt(AccessPlatformMessage.e);
            data.writeInt(lockid);
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