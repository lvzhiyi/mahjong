using cambrian.common;
using cambrian.uui;
using DG.Tweening;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 胡牌视图
    /// </summary>
    public class MJHuView : UnrealLuaSpaceObject
    {
        SpritesList down;

        SpritesList right;

        SpritesList top;

        SpritesList left;

        protected override void xinit()
        {
            down = transform.Find("hu0").GetComponent<SpritesList>();
            right = transform.Find("hu1").GetComponent<SpritesList>();
            top = transform.Find("hu2").GetComponent<SpritesList>();
            left = transform.Find("hu3").GetComponent<SpritesList>();
        }

        protected override void xrefresh()
        {
            down.setVisible(false);
            right.setVisible(false);
            top.setVisible(false);
            left.setVisible(false);
        }

        public void showHu(int index, int order, int huType, bool isPlaying)
        {
            bool b = huType > -1;
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    if (b)
                    {
                        down.ShowIndex(getIndex(order, huType));
                        if (isPlaying)
                            playEffect(down);
                        else
                        {
                            normalShow(down);
                        }
                    }

                    down.setVisible(b);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    if (b)
                    {
                        right.ShowIndex(getIndex(order, huType));
                        if (isPlaying)
                            playEffect(right);
                        else
                        {
                            normalShow(right);
                        }
                    }

                    right.setVisible(b);
                    break;
                case MahJongPanel.LOC_TOP:
                    if (b)
                    {
                        top.ShowIndex(getIndex(order, huType));
                        if (isPlaying)
                            playEffect(top);
                        else
                        {
                            normalShow(top);
                        }
                    }

                    top.setVisible(b);
                    break;
                case MahJongPanel.LOC_LEFT:
                    if (b)
                    {
                        left.ShowIndex(getIndex(order, huType));
                        if (isPlaying)
                            playEffect(left);
                        else
                            normalShow(left);
                    }
                    left.setVisible(b);
                    break;
            }
        }

        public void playEffect(SpritesList sprites)
        {
            float scale = 1.5f;

            DisplayKit.setLocalScaleXY(sprites.transform, 1);
            sprites.resetFade();

            Sequence s = DOTween.Sequence();
            s.Append(sprites.transform.DOScale(new Vector3(scale, scale, scale), 0.5f).SetEase(Ease.Linear));
            s.Append(sprites.transform.DOScale(new Vector3(scale, scale, scale), 1).SetEase(Ease.Linear));
            s.Append(sprites.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.Linear));
            s.Append(sprites.getSource().DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
        }

        public void normalShow(SpritesList sprites)
        {
            DisplayKit.setLocalScaleXY(sprites.transform, 0.8f);
            sprites.getSource().DOFade(0.5f, 0).SetEase(Ease.Linear);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order">从1开始</param>
        /// <param name="huType"></param>
        /// <returns></returns>
        public static int getIndex(int order, int huType)
        {
            if (StatusKit.hasStatus(huType, MJMatchHuOperate.HU_NORMAL) ||
                StatusKit.hasStatus(huType, MJMatchHuOperate.HU_HAIDI) ||
                StatusKit.hasStatus(huType, MJMatchHuOperate.HU_KONG) ||
                StatusKit.hasStatus(huType, MJMatchHuOperate.HU_DI) ||
                StatusKit.hasStatus(huType, MJMatchHuOperate.HU_ROB))
            {
                return order - 1 + 3;
            }
            return order - 1;
        }
    }
}
