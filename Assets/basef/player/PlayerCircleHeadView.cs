using cambrian.common;
using cambrian.game;
using cambrian.unreal.display;
using cambrian.uui;
using scene.game;
using System;
using UnityEngine;

namespace basef.player
{
    public class PlayerCircleHeadView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 默认头像
        /// </summary>
        private UnrealUICircle head;
        /// <summary>
        /// 头像
        /// </summary>
        private UnrealUICircle player_head;

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
            this.head = this.transform.Find("head").GetComponent<UnrealUICircle>();
            this.player_head = this.transform.Find("head_1").GetComponent<UnrealUICircle>();
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
                UIGray.SetUIGray1(head);
                UIGray.SetUIGray1(player_head);
            }
            else
            {
                UIGray.Recovery1(head);
                UIGray.Recovery1(player_head);
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
                    this.player_head.texture = head_icon.texture;
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
                    this.player_head.texture = icon.texture;
            }
        }

        void robothead()
        {
            try {
                string head = headurl.Split(new string[] { "//" }, StringSplitOptions.None)[1];
                string[] h = head.Split(new string[] { "?" }, StringSplitOptions.None);

                if (sex == 1)
                    player_head.texture = CacheManager.boyRobotHead[StringKit.parseInt(h[0])].texture;
                else
                    player_head.texture = CacheManager.girlRobotHead[StringKit.parseInt(h[0])].texture;
            }
            catch (Exception e)
            {
                Debug.Log("e="+e);
            }
            
        }
    }
}
