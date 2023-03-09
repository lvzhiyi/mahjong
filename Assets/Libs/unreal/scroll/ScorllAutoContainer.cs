using cambrian.common;
using UnityEngine;
using XLua;

namespace cambrian.unreal.scroll
{
    /// <summary>
    /// 自动重复滚动容器
    /// </summary>
    [Hotfix]
    public class ScorllAutoContainer: UnrealContainer
    {
        /// <summary>
        /// 所有的bar
        /// </summary>
        private ArrayList<ScrollAutoBar> bars;
        /// <summary>
        /// 隐藏的bars
        /// </summary>
        private Queue<object> hideData;
        /// <summary>
        /// 记录列表
        /// </summary>
        private ArrayList<object> list;
        /// <summary>
        /// 间隔时间(单位毫秒)
        /// </summary>
        public long intervalTime=50;
        /// <summary>
        /// 移动的距离(像素)
        /// </summary>
        public float moveDistance = 10;

        //实际加载的Item个数，一般比可显示个数大2~3个
        [Range(0, 20)]
        public int viewCount = 6;
        /// <summary>
        ///  默认加载的Item个数
        /// </summary>
        public int defaultCount = 6;

        /// <summary>
        /// 初始的bar
        /// </summary>
        ScrollAutoBar initBar;

        public RectTransform content;
        /// <summary>
        /// 当前索引
        /// </summary>
        private int cur_index;
        /// <summary>
        /// 是否需要增加
        /// </summary>
        private bool isNeedAdd;
        /// <summary>
        /// 滚动的数量
        /// </summary>
        int scorllcount;


        public override void init()
        {
            this.initBar = this.transform.Find("scrollrect").Find("content").Find("bar_0").GetComponent<ScrollAutoBar>();
            this.initBar.gameObject.SetActive(false);

            bars = new ArrayList<ScrollAutoBar>();
            list=new ArrayList<object>();
        }


        public void setListData(object[] objs,int scrollcount)
        {
            list.clear();
            this.scorllcount = scrollcount;
            list.add(objs);
        }

        public override void refresh()
        {
            if (list.count == 0)
                return;

            if (list.count <= defaultCount)
                viewCount = list.count;
            
            hideData = new Queue<object>(list.count);

            cur_index = viewCount - 1;

            bars.clear();
            ScrollAutoBar[] barsss = this.transform.Find("scrollrect").Find("content").GetComponentsInChildren<ScrollAutoBar>();

            for (int i = barsss.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(barsss[i].gameObject);
            }

            isNeedAdd = false;

            time = 0;
            initShowData();
        }

        /// <summary>
        /// 初始化显示数据
        /// </summary>
        private void initShowData()
        {
            for (int i = 0; i < viewCount; i++)
            {
                ScrollAutoBar bar = cloneScrollBar(i);
                bar.ObjectData = list.get(i);
                bar.refresh();
                bars.add(bar);
            }
        }

        private ScrollAutoBar cloneScrollBar(int index)
        {
            GameObject obj = this.cloneAdd(initBar.gameObject);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "bar_" + index;
            obj.transform.SetParent(content, false);
            ScrollAutoBar scorllBar = obj.GetComponent<ScrollAutoBar>();
            scorllBar.init();
            scorllBar.gameObject.SetActive(true);

            scorllBar.transform.localPosition = getPosition(index);
            return scorllBar;
        }

        private Vector3 getPosition(int i)
        {
            return new Vector3(0f, i*-getBarHeight(), 0f);
        }

        private float getBarHeight()
        {
            return this.initBar.getHeight();
        }

        private long time;
        void Update()
        {
            if (bars.count == 0||bars.count<scorllcount) return;

            long cur_time = TimeKit.currentTimeMillis;
            if (time == 0)
            {
                time = cur_time;
                return;
            }

            if (cur_time - time > intervalTime)
            {
                for (int i = 0; i < bars.count;i++)
                {
                    ScrollAutoBar bar = bars.get(i);
                    DisplayKit.setLocalY(bar.gameObject, bar.transform.localPosition.y + moveDistance);
                }

                ScrollAutoBar bar1 = bars.get(0);
                if (bar1.transform.localPosition.y>=getBarHeight())
                {
                    hideData.add(bar1.ObjectData);
                    if (cur_index < list.count - 1)
                    {
                        bar1.ObjectData = list.get(++cur_index);
                        bar1.name = "bar_" + cur_index;
                        float y = bars.getLast().transform.localPosition.y - getBarHeight();
                        bar1.transform.localPosition = new Vector3(0f, y, 0f);
                        bar1.refresh();
                        //移动到末尾
                        bars.add(bars.removeAt(0));
                    }
                    else if (isNeedAdd)
                    {
                        cur_index++;
                        bar1.ObjectData = list.get(list.count - 1);
                        bar1.name = "bar_" + cur_index;
                        float y = bars.getLast().transform.localPosition.y - getBarHeight();
                        bar1.transform.localPosition = new Vector3(0f, y, 0f);
                        bar1.refresh();
                        //移动到末尾
                        bars.add(bars.removeAt(0));
                        isNeedAdd = false;
                    }
                    else
                    {
                        cur_index++;
                        bar1.ObjectData = hideData.remove();
                        bar1.name = "bar_" + cur_index;
                        float y = bars.getLast().transform.localPosition.y - getBarHeight();
                        bar1.transform.localPosition = new Vector3(0f, y, 0f);
                        bar1.refresh();
                        //移动到末尾
                        bars.add(bars.removeAt(0));
                    }
                }
                time = cur_time;
            }
        }
    }
}
