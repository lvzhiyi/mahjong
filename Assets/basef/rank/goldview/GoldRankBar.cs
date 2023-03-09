using cambrian.common;
using cambrian.game;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rank
{
    public class GoldRankBar:ScrollBar
    {
        private RankPlayer rankPlayer;
        /// <summary>
        /// 昵称
        /// </summary>
        private Text nickname;
        /// <summary>
        /// 头像
        /// </summary>
        private Image head;
        /// <summary>
        /// 有头像
        /// </summary>
        private Image head_1;
        /// <summary>
        /// 局数
        /// </summary>
        private Text jushu;
        /// <summary>
        /// 前三名列表
        /// </summary>
        private SpritesList spritesList;
        /// <summary>
        /// 之后排名
        /// </summary>
        private UnrealTextField later;
        /// <summary>
        /// userid
        /// </summary>
        private Text id;

        protected override void xinit()
        {
            this.nickname = this.transform.Find("name").GetComponent<Text>();
            this.head = this.transform.Find("head").Find("head").GetComponent<Image>();
            this.head_1 = this.transform.Find("head").Find("head_1").GetComponent<Image>();
            this.jushu = this.transform.Find("jushu").GetComponent<Text>();
            this.spritesList = this.transform.Find("rank3").GetComponent<SpritesList>();
            this.later = this.transform.Find("later").GetComponent<UnrealTextField>();
            if (this.transform.Find("id") != null)
                this.id = this.transform.Find("id").GetComponent<Text>();

        }

        public void setData(RankPlayer rank, int index,int type)
        {
            this.rankPlayer = rank;
            this.nickname.text = rank.getNickName();
            this.jushu.text=GoldRankGroup.getGroupValuleName(type)+ rank.getMatchCount();
            if (this.id != null)
                this.id.text = "ID:" + rank.getUserId();

            showtop3icon(index);

            if (StringKit.isNullString(rank.getHead()))
            {
                this.head.gameObject.SetActive(true);
                this.head_1.gameObject.SetActive(false);
            }
            else
            {
                this.head.gameObject.SetActive(false);
                this.head_1.gameObject.SetActive(true);
                Sprite head_icon = IconManager.getHeadPic(rank.getUserId());
                if (head_icon != null)
                    this.head_1.sprite = head_icon;
                else
                {
                    Loader.getLoader().load<long, Sprite>(new Url(rank.getHead()), rank.getUserId(), this.refreshHeadIcon);
                }
            }
        }


        void refreshHeadIcon(long userid, Sprite icon)
        {
            if (userid == rankPlayer.getUserId())
            {
                IconManager.saveHeadPic(userid, icon);
                if (this.head_1 != null)
                    this.head_1.sprite = icon;
            }
        }

        private void showtop3icon(int index)
        {
            this.later.setVisible(false);
            this.spritesList.setVisible(false);
            if (index < 3)
            {
                this.spritesList.ShowIndex(index);
                this.spritesList.setVisible(true);
            }
            else
            {
                this.later.text = (index + 1) + "";
                this.later.setVisible(true);
            }

        }
    }
}
