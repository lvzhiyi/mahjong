using cambrian.game;
using basef.player;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rank
{
    public class GoldBottomView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 昵称
        /// </summary>
        private Text nickname;
        /// <summary>
        /// 局数或者金币
        /// </summary>
        private Text jushu;
        /// <summary>
        /// 排名
        /// </summary>
        private Text rank;
        /// <summary>
        /// 暂未上榜
        /// </summary>
        private Text noinfo;
        /// <summary>
        /// 头像
        /// </summary>
        private Image head;

        private int index;
        /// <summary>
        /// 用于判断是显示局数，还是金币数
        /// </summary>
        private int type;
        /// <summary>
        /// 数量
        /// </summary>
        private long num;

        protected override void xinit()
        {
            base.xinit();
            this.nickname = this.transform.Find("name").GetComponent<Text>();
            this.jushu = this.transform.Find("jushu").GetComponent<Text>();
            this.rank = this.transform.Find("info").GetComponent<Text>();
            this.head = this.transform.Find("head").GetComponent<Image>();
            this.noinfo = this.transform.Find("noinfo").GetComponent<Text>();
        }

        public void setData(long num,int index,int type)
        {
            this.index = index;
            this.type = type;
            this.num = num;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            Sprite sprite = IconManager.getHeadPic(Player.player.userid);
            if (sprite != null)
                this.head.sprite = sprite;
            this.nickname.text = Player.player.name;
            this.noinfo.gameObject.SetActive(false);
            this.rank.gameObject.SetActive(false);
            if (index == -1)
                this.noinfo.gameObject.SetActive(true);
            else
            {
                this.rank.gameObject.SetActive(true);
                this.rank.text = (index+1)+"";
            }
            if (type == GoldRankGroup.RANK_GROUP_WIN && num < 0) 
            {
                this.jushu.text = GoldRankGroup.getGroupValuleName(type)+0;
            }
            else if(type == GoldRankGroup.RANK_GROUP_LOSE )
            {
                if(num >= 0)
                {
                    this.jushu.text = GoldRankGroup.getGroupValuleName(type) + 0;
                }
                else
                {
                    this.jushu.text= GoldRankGroup.getGroupValuleName(type) + (-num);
                }
            }
            else
            {
                this.jushu.text = GoldRankGroup.getGroupValuleName(type)+num;
            }

        }
    }
}
