using basef.getgoldeffect;
using cambrian.common;
using scene.game;
using basef.prop;

namespace basef.bag
{
    /// <summary> 接收增加道具的消息 </summary>
    public class RecvPropUpdateCommand : RecvPort
    {
        public RecvPropUpdateCommand()
        {
            this.id = RecvConst.PORT_CLIENT_PROP_UPDATE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            if (len > 1)
            {
                Prop[] props = new Prop[len];
                for (int i = 0; i < len; i++)
                {
                    Prop prop = (Prop)Sample.factory.newSample(data.readInt());
                    prop.bytesRead(data);
                    props[i] = prop;
                }
                PropUseSuccessPanel panel = UnrealRoot.root.getDisplayObject<PropUseSuccessPanel>();
                panel.setProps(props);
                panel.refresh();
                panel.setCVisible(true);
            }
            else if (len == 1)
            {
                Prop prop = (Prop)Sample.factory.newSample(data.readInt());
                prop.bytesRead(data);
                ShowGoldEffectPanel panel = UnrealRoot.root.getDisplayObject<ShowGoldEffectPanel>();
                panel.showProp(CacheManager.shopimg[prop.img],"X" + prop.count);
                panel.refresh();
                panel.setCVisible(true);
            }
        }
    }
}
