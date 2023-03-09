using System;
using scene.game;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class LDisableBar:UnrealLuaSpaceObject
    {
        private Image card_img;

        public int card;

        private int line = 7;

        public double angle = 35;

        public float distance = 30;

        protected override void xinit()
        {
            this.card_img = transform.Find("card").GetComponent<Image>();
            DisplayKit.setLocalScaleXY(transform,0.9f);
        }

        protected override void xrefresh()
        {
            this.card_img.sprite = WidgetManager.getInstance().getShowCard(card);
            double angles = angle*Math.PI / 180;
            float dis = distance * (index_space%line);
            float y = (float) (Math.Sin(angles) * dis);
            float x = (float)Math.Sqrt(Math.Pow(dis, 2)-Math.Pow(y,2));
            y += ((index_space / line) * 48);
            DisplayKit.setLocalXY(this.transform, x, y);
        }
    }
}
