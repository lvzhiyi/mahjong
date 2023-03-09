using cambrian.common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class PlayCardProcess_1 : MouseEventProcess
    {
        public const int CHOW = 1, PLAY = 2;

        public RectTransform canvas;         //得到canvas的ugui坐标
        private RectTransform imgRect;       //得到图片的ugui坐标
        Vector2 offset = new Vector3();      //用来得到鼠标和图片的差值

        private Vector2 init_postion;
        /// <summary>
        /// 按下的位置
        /// </summary>
        private Vector2 mouseDown;

        /// <summary>
        /// 图片
        /// </summary>
        private Image imge;


        private HandCardBar bar;

        /// <summary>
        /// 每列
        /// </summary>
        private HandColumBar columBar;

        private HandView handView;

        private int point_num;

        /// <summary>
        /// 出牌y坐标
        /// </summary>
        private float chupaiy = 262.5f;

        void Start()
        {
            this.canvas =this.transform.parent.parent.parent.parent.parent.parent.GetComponent<RectTransform>();
            chupaiy*=Screen.height/2f/320f;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (bar == null || !bar.check) return;
            if (columBar == null) return;
            if (this.getRoot<SpotRoomPanel>().operate == 0 || this.getRoot<SpotRoomPanel>().stauts == -1||!bar.getDiableChu()) return;
            bar.check.gameObject.SetActive(false);
            columBar.setSlibingIndex(handView.column.size - 1);

            Vector2 mouseDrag = eventData.position; //当鼠标拖动时的屏幕坐标
            Vector2 uguiPos = new Vector2(); //用来接收转换后的拖动坐标
            //和上面类似
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag,
                eventData.enterEventCamera, out uguiPos);

            if (isRect)
                //设置图片的ugui坐标与鼠标的ugui坐标保持不变
                imgRect.anchoredPosition = offset + uguiPos;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if(this.imgRect!=null) this.imgRect.anchoredPosition = this.init_postion;
            point_num = 0;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            bar = this.transform.GetComponent<HandCardBar>();

            if (this.getRoot<SpotRoomPanel>().operate == 0 || this.getRoot<SpotRoomPanel>().stauts == -1||!bar.getDiableChu()) return;
            handView = this.getRoot<SpotRoomPanel>().allHandView.selfView.getHandView();
            this.imgRect = this.transform.Find("card").GetComponent<RectTransform>();
            this.init_postion = this.imgRect.anchoredPosition;
            this.imge = this.transform.Find("card").GetComponent<Image>();


            columBar = bar.transform.parent.parent.GetComponent<HandColumBar>();
            columBar.setSlibingIndex(handView.column.size-1);


            if (!bar.isUp) //未展开
            {
                point_num = 0;
                handView.UpColumnCard(bar.column);
                bar.showCheck();
                this.getRoot<SpotRoomPanel>().allHandView.selfView.getHandView().hideCheck(bar.column, bar.index_space);
            }
            else
            {
               
                if (bar.ischecked && bar.getDiableChu())
                {

                }
                else
                {
                    bar.showCheck();
                    point_num = 0;
                    this.getRoot<SpotRoomPanel>().allHandView.selfView.getHandView().hideCheck(bar.column, bar.index_space);
                }
            }
            if (bar==null||!bar.ischecked) return;

            point_num++;

            mouseDown = eventData.position; //记录鼠标按下时的屏幕坐标
            Vector2 mouseUguiPos = new Vector2(); //定义一个接收返回的ugui坐标
            //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
            //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
            //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
            //out mouseUguiPos：返回转换后的ugui坐标
            //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown,
                eventData.enterEventCamera, out mouseUguiPos);
            if (isRect) //如果在
            {
                //计算图片中心和鼠标点的差值
                offset = imgRect.anchoredPosition - mouseUguiPos;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (bar == null || !bar.ischecked||!bar.getDiableChu())
            {
                if(this.imgRect!=null)
                this.imgRect.anchoredPosition = this.init_postion;
                point_num = 0;
                bar = null;
                this.imgRect = null;
                return;
            }

            if (eventData.position.y < chupaiy&& eventData.dragging)
            {
                this.imgRect.anchoredPosition = this.init_postion;
                //整理手牌
                handView.UpColumnCard(bar.column);
                bar.showCheck();

                this.getRoot<SpotRoomPanel>().allHandView.selfView.getHandView().hideCheck(bar.column, bar.index_space);
                return;
            }

            if (!eventData.dragging)//未拖动
            {
                if (point_num !=2) return;//代表点击了两次，针对与点击出牌
            }

            if (bar.ischecked && bar.getDiableChu())
            {
                int step = CPMatch.match.step;
                if (this.getRoot<SpotRoomPanel>().stauts == CHOW)
                {
                    CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.CHOW, step, bar.card_value,0,null));
                    handView.UpColumnCard(-1);
                    this.getRoot<SpotRoomPanel>().showTextinfo(true);
                    this.getRoot<SpotRoomPanel>().stauts = -1;
                    this.getRoot<SpotRoomPanel>().allHandView.selfView.hideHuaDong();
                }
                else if (this.getRoot<SpotRoomPanel>().stauts == PLAY)
                {
                    CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.DISCARD, step, bar.card_value,0,null));
                    handView.UpColumnCard(-1);
                    this.getRoot<SpotRoomPanel>().stauts = -1;
                    this.getRoot<SpotRoomPanel>().allHandView.selfView.hideHuaDong();
                }
                else
                {
                    this.getRoot<SpotRoomPanel>().allHandView.selfView.getHandView().hideCheck(bar.column, bar.index_space);
                }
            }
            this.imgRect.anchoredPosition = this.init_postion;
            bar = null;
            point_num = 0;
            this.imgRect = null;
        }
    }
}
