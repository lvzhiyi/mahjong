using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.bag
{
    public class BagUsePropCommand : UserCommand
    {
        int propsid;

        int count;

        public BagUsePropCommand(int propsid,int count)
        {
            this.id = CommandConst.PORT_PROP_USE;
            this.propsid = propsid;
            this.count = count;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(propsid);
            data.writeInt(count);
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (!data.readBoolean())
            {
                Alert.show(data.readUTF());
            }
        }
    }
}
