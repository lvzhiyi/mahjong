using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 所有成员
    /// </summary>
    public class SearchArenaMemberCommand:UserCommand
    {
        long userid;

        long arenaid;


        bool b;

        public SearchArenaMemberCommand(long arenaid,long userid,bool b)
        {
            id = CommandConst.PORT_ARENA_DEST_MEMBER;
            this.userid = userid;
            this.arenaid = arenaid;
            this.b = b;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(userid);
            data.writeBoolean(b);
        }

        public override void bytesRead(ByteBuffer data)
        {
            ArenaMember member=new ArenaMember();
            member.bytesRead(data);
            callbackobj = member;
        }
    }
}
