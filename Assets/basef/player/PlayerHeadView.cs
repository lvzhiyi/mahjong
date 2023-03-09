using System;
using cambrian.common;
using cambrian.game;
using scene.game;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.player
{
    public class PlayerHeadView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 默认头像
        /// </summary>
        private Image head;
        /// <summary>
        /// 头像
        /// </summary>
        private Image player_head;

        /// <summary>
        /// 头像地址
        /// </summary>
        private string headurl;
        /// <summary>
        /// 用户id，方面在缓存中调用
        /// </summary>
        private long userid;
        /// <summary>
        /// 性别
        /// </summary>
        private int sex;

        protected override void xinit()
        {
            base.xinit();
            this.head = this.transform.Find("head").GetComponent<Image>();
            this.player_head = this.transform.Find("head_1").GetComponent<Image>();
        }

        public void setData(string headurl,long userid)
        {
            this.headurl = headurl;
            this.userid = userid;
        }

        public void setDatas(string headurl,long userid,int sex)
        {
            this.headurl = headurl;
            this.userid = userid;
            this.sex = sex;
        }

        /// <summary>
        /// 置灰
        /// </summary>
        /// <param name="b">true:置灰,false:取消</param>
        public void setHeadGray(bool b)
        {
            if (b)
            {
                UIGray.SetUIGray(head);
                UIGray.SetUIGray(player_head);
            }
            else
            {
                UIGray.Recovery(head);
                UIGray.Recovery(player_head);
            }
        }

        protected override void xrefresh()
        {
            if (!IconManager.checkHeadUrl(headurl))
            {
                this.head.gameObject.SetActive(true);
                this.player_head.gameObject.SetActive(false);
            }
            else
            {
                this.head.gameObject.SetActive(false);
                this.player_head.gameObject.SetActive(true);
                Sprite head_icon = IconManager.getHeadPic(userid);
                if (head_icon != null)
                    this.player_head.sprite = head_icon;
                else
                {
                    Loader.getLoader().load<long, Sprite>(new Url(headurl), userid, this.refreshHeadIcon);
                }
            }
        }
        void refreshHeadIcon(long userid, Sprite icon)
        {
            if (icon == null)
            {
                robothead();
                return;
            }

            if (userid == this.userid)
            {
                IconManager.saveHeadPic(userid, icon);
                if (this.player_head != null)
                    this.player_head.sprite = icon;
            }
        }

        void robothead()
        {
            string head=headurl.Split(new string[] { "//" }, StringSplitOptions.None)[1];
            string[] h=head.Split(new string[]{"?"}, StringSplitOptions.None);

            if (sex == 1)
                player_head.sprite = CacheManager.boyRobotHead[StringKit.parseInt(h[0])];
            else
                player_head.sprite = CacheManager.girlRobotHead[StringKit.parseInt(h[0])];
        }
    }
}
