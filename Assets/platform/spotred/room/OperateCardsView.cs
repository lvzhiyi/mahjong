using System;
using DG.Tweening;
using UnityEngine;

namespace platform.spotred.room
{
    public class OperateCardsView:UnrealLuaSpaceObject
    {
        private UnrealListContainer container;
        /// <summary>
        /// 初始位置
        /// </summary>
        private Vector2 initPos;
        /// <summary>
        /// 移动时间(秒单位)
        /// </summary>
        public float moveTime= 0.3f;
        /// <summary>
        /// 变大速度
        /// </summary>
        public float scaleBigSpeed=0.04f;
        /// <summary>
        /// 旋转速度
        /// </summary>
        public int roationSpeed = 600;

        public Vector3 roateVector=new Vector3(0,0,90);
        /// <summary>
        /// 方向
        /// </summary>
        public int direction;

        

        protected override void xinit()
        {
            this.container = this.transform.Find("container").GetComponent<UnrealListContainer>();
            this.container.init();
            this.container.resize(3);
            this.initPos = this.transform.localPosition;
        }

        public void setCards(int[] cards)
        {
            this.container.resize(cards.Length);
            for (int i = 0; i < container.size; i++)
            {
                OperateCardsBar bar = (OperateCardsBar) this.container.bars[i];
                bar.setCard(cards[i]);
            }
        }

        private Action callback;

        private Vector2 pos;

        /// <summary>
        /// 开始动画
        /// </summary>
        public void startAnimation(Vector2 pos,Action callback)
        {
            if (direction == RoomPanel.LOC_DOWN)
            {
                this.pos = new Vector2(pos.x + 48, pos.y);
            }
            else if(direction == RoomPanel.LOC_TOP)
            {
                this.pos = new Vector2(pos.x-48, pos.y);
            }
            else if (direction== RoomPanel.LOC_LEFT)
            {
                this.pos = new Vector2(pos.x, pos.y-48);
            }
            else
            {
                this.pos = new Vector2(pos.x, pos.y+48);
            }

            this.callback = callback;
        }

        public void moveOver()
        {
            callback();
            setVisible(false);
        }

        public void scaleOver()
        {
            Sequence s = DOTween.Sequence();
            s.Append(this.transform.DOLocalMove(pos, moveTime).SetEase(Ease.Linear));
            s.Insert(0, this.transform.DOLocalRotate(roateVector, moveTime));
            s.Insert(0, this.transform.DOScaleX(0.65f, moveTime));
            s.Insert(0, this.transform.DOScaleY(0.58f, moveTime));

            s.OnComplete(() =>
            {
                moveOver();
            });
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            
            if (b)
            {
                this.transform.DOScale(new Vector3(1.2f,1.2f),0.2f).OnComplete(() => { scaleOver();});
            }
            else
            {
                DisplayKit.setLocalScaleXY(this.transform, 1f);
                DisplayKit.setLocalVector2(this.gameObject, initPos);
                DisplayKit.setLocalRoateXYZ(transform,0,0,0);
            }
        }
    }
}
