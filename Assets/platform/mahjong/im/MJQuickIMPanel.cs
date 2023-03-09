using basef.im;
using cambrian.unreal.display;
using scene.game;
using UnityEngine;

namespace platform.mahjong
{
    public class MJQuickIMPanel: UnrealViewPanel
    {
        public const int TEXT = 1, ICON = 2;

        UnrealSwtichTextButton tab0;

        UnrealSwtichTextButton tab1;

        int index;

        /// <summary>
        /// 文字
        /// </summary>
        UnrealTableContainer texts;
        /// <summary>
        /// 图片
        /// </summary>
        UnrealTableContainer icons;
        /// <summary>
        /// 输入文本
        /// </summary>
        [HideInInspector]
        public UnrealInputTextField inputText;

        /// <summary>
        /// 麻将
        /// </summary>
        [HideInInspector] public static string[] textInfo = new string[] {  "别吵了，别吵了，专心玩游戏吧。", "催什么，我在想打哪张。", "快点啊，等得我花儿都谢了。", "美女，你缺哪张？我打给你。",
            "土豪，我们做朋友吧。", "喂，你这么牛叉你家人知道吗？", "我是菜鸟，各位手下留情啊。", "怎么又断线了，网络也忒差了。" ,"啥子牌哦，这么烂。","手气还可以，感觉人生已经达到了巅峰。" };


        protected override void xinit()
        {
            base.xinit();
            tab0 = this.content_conainer.transform.Find("tab_0").GetComponent<UnrealSwtichTextButton>();
            tab0.init();
            tab1 = this.content_conainer.transform.Find("tab_1").GetComponent<UnrealSwtichTextButton>();
            tab1.init();
            texts = this.content_conainer.transform.Find("center").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            texts.init();
            icons = this.content_conainer.transform.Find("icons").GetComponent<UnrealTableContainer>();
            icons.init();
            inputText = this.content_conainer.transform.Find("text").GetComponent<UnrealInputTextField>();

            resizeDelta(new Margin());
        }

        protected override void xrefresh()
        {
            base.xrefresh();

            int len = textInfo.Length;

            texts.resize(len);

            for (int i = 0; i < len; i++)
            {
                IMQuickMsgBar bar=(IMQuickMsgBar)texts.bars[i];
                bar.index_space = i;
                bar.setIMQuickMsg(new IMQuickMsg(IMQuickMsg.TYPE_TEXT,i, textInfo[i]));
                bar.refresh();
            }

            len = CacheManager.emojis.Length;
            icons.resize(len);
            for (int i = 0; i < len; i++)
            {
                IMQuickMsgBar bar=(IMQuickMsgBar)icons.bars[i];
                bar.index_space = i;
                bar.setIMQuickMsg(new IMQuickMsg(IMQuickMsg.TYPE_ICON, i));
                bar.refresh();
            }
            
            inputText.text = "";
            texts.resizeDelta();
        }

        /// <summary>
        /// 切换表情界面和常用语界面
        /// </summary>
        public void switchTextOrIcon(int type)
        {
            tab0.setState(UnrealButton.NORMAL);
            tab1.setState(UnrealButton.NORMAL);

            texts.setVisible(false);
            icons.setVisible(false);

            switch (type)
            {
                case TEXT:
                    tab0.setState(UnrealButton.DISABLE);
                    texts.setVisible(true);
                    break;
                case ICON:
                    tab1.setState(UnrealButton.DISABLE);
                    icons.setVisible(true);
                    break;
            }
        }
    }
}
