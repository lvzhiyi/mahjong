using cambrian.common;
using UnityEngine;
using XLua;

namespace cambrian.unreal.scroll
{
    [Hotfix]
    public class ScrollTableContainer:UnrealContainer
    {
        /// <summary>
        /// 回调函数(bar,index:索引)
        /// </summary>
        public delegate void CallBack(ScorllTableBar bar, int index);

        public enum Arrangement { Horizontal, Vertical, }
        /// <summary>
        /// 滚动模式(横，竖)
        /// </summary>
        public Arrangement _movement = Arrangement.Horizontal;

        /// <summary>
        /// 初始化的bar用于clone
        /// </summary>
        private ScorllTableBar initBar;
        /// <summary>
        /// 显示的bar
        /// </summary>
        private ArrayList<ScorllTableBar> bars;
        /// <summary>
        /// 将未显示出来的Item存入未使用队列里面，等待需要使用的时候直接取出
        /// </summary>
        System.Collections.Generic.Queue<ScorllTableBar> hideBars;
        /// <summary>
        /// 显示的个数
        /// </summary>
        private int showcount;

        /// <summary>
        /// 默认加载的Item个数，一般比可显示个数大2~3个（在预制件中选择）
        /// </summary>
        [Range(0, 30)]
        public int viewCount = 6;
        /// <summary>
        /// 总的bars数量
        /// </summary>
        int _dataCount;
        /// <summary>
        /// 多少列
        /// </summary>
        public int cols;
        /// <summary>
        /// 多少行(只针对于横向拖动)
        /// </summary>
        public int lines=1;

        /// <summary>
        /// 预制件中设定
        /// </summary>
        public RectTransform rect;

        /// <summary>
        /// RectTransform 初始x坐标
        /// </summary>
        float rect_init_x;
        /// <summary>
        /// RectTransform 初始y坐标
        /// </summary>
        float rect_init_y;
        /// <summary>
        /// 当前的bar索引
        /// </summary>
        int _index = -1;

       

        public CallBack callback;

        /// <summary>
        /// bars的父节点
        /// </summary>
        public Transform ct;

        public override void init()
        {
            this.initBar = this.transform.Find("scrollrect").Find("content").Find("bar_0").GetComponent<ScorllTableBar>();
            this.initBar.gameObject.SetActive(false);

            this.bars=new ArrayList<ScorllTableBar>();
            this.hideBars=new System.Collections.Generic.Queue<ScorllTableBar>();
            this.showcount = viewCount;

            this.rect_init_x = rect.localPosition.x;
            this.rect_init_y = rect.localPosition.y;
        }

        /// <summary>
        /// 设置回调函数
        /// </summary>
        /// <param name="callback"></param>
        public void updateData(CallBack callBack)
        {
            this.callback = callBack;
        }

        public ScorllTableBar[] getShowBar()
        {
            if (ct == null)
                ct = initBar.transform.parent;
            return ct.GetComponentsInChildren<ScorllTableBar>();
        }

        /// <summary>
        /// clone一个bar
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private ScorllTableBar cloneScrollBar(int index)
        {
            GameObject obj = this.cloneAdd(initBar.gameObject);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "bar_" + index;
            obj.transform.SetParent(rect, false);
            ScorllTableBar scorllBar = obj.GetComponent<ScorllTableBar>();
            scorllBar.init();
            scorllBar.gameObject.SetActive(true);

            scorllBar.transform.localPosition = getPosition(index);
            return scorllBar;
        }

        private Vector3 getPosition(int index)
        {
            switch (_movement)
            {
                case Arrangement.Horizontal:
                    int col = (index / lines);//第几列
                    int line = index % lines;
                    return new Vector3(col*this.initBar.getWidth(), line * -(this.initBar.getHeight()), 0f);
                case Arrangement.Vertical:
                    return new Vector3((index%cols)*this.initBar.getWidth(), index/cols*-(this.initBar.getHeight()), 0f);
            }
            return Vector3.zero;
        }

        private int getPosIndex()
        {
            switch (_movement)
            {
                case Arrangement.Horizontal:
                    return Mathf.FloorToInt(rect.anchoredPosition.x / -(this.initBar.getWidth()));
                case Arrangement.Vertical:
                    return Mathf.FloorToInt(rect.anchoredPosition.y / (this.initBar.getHeight()));
            }
            return 0;
        }

        /// <summary>
        /// shezh
        /// </summary>
        /// <param name="size"></param>
        public void incrResize(int size)
        {
            if (size < this.viewCount)
            {
                this.viewCount = size;
            }
            else
            {
                this.viewCount = this.showcount;
            }
            DataCount = size;
            OnValueChange(Vector2.zero);
        }

        public void refreshShow()
        {
            for (int j = 0; j < bars.count; j++)
            {
                if (callback != null)
                {
                    callback(bars.get(j), bars.get(j).getIndex());
                }
            }
        }

        /// <summary>
        /// 重设大小，复位
        /// </summary>
        public void resize(int size)
        {
            if (size < this.viewCount)
            {
                this.viewCount = size;
            }
            else
            {
                this.viewCount = this.showcount;
            }

            this.hideBars.Clear();
            this.bars.clear();
            ScorllTableBar[] barsss = this.transform.Find("scrollrect").Find("content").GetComponentsInChildren<ScorllTableBar>();

            for (int i = barsss.Length - 1; i >= 0; i--)
            {
                Destroy(barsss[i].gameObject);
            }

            DataCount = size;
            
            valueChange();
            

            if (bars.count == 0 && size > 0)
            {
                bars.add(cloneScrollBar(0));
            }

            this.rect.localPosition = new Vector3(rect_init_x, rect_init_y);

        }

        /// <summary>
        /// 不复位重置
        /// </summary>
        public void notpostionresize(int size)
        {
            if (size < this.viewCount)
            {
                this.viewCount = size;
            }
            else
            {
                this.viewCount = this.showcount;
            }

            this.hideBars.Clear();
            this.bars.clear();

            ScorllTableBar[] barsss = this.transform.Find("scrollrect").Find("content").GetComponentsInChildren<ScorllTableBar>();

            for (int i = barsss.Length - 1; i >= 0; i--)
            {
                Destroy(barsss[i].gameObject);
            }

            if (bars.count == 0 && size > 0)
            {
                bars.add(cloneScrollBar(0));
            }

            DataCount = size;
            valueChange();

        }


        /// <summary>
        /// 不复位重置
        /// </summary>
        public void notResizeResize(int size)
        {
            if (size < this.viewCount)
            {
                this.viewCount = size;
            }
            else
            {
                this.viewCount = this.showcount;
            }
            int index = getPosIndex();
            if (index == -1)
                index = 0;

            if (size < showcount || index < 2)
            {
                this.hideBars.Clear();
                this.bars.clear();

                ScorllTableBar[] barsss = this.transform.Find("scrollrect").Find("content").GetComponentsInChildren<ScorllTableBar>();

                for (int i = barsss.Length - 1; i >= 0; i--)
                {
                    Destroy(barsss[i].gameObject);
                }

                //if (bars.count == 0 && size > 0)
                //{
                //    bars.add(cloneScrollBar(0));
                //}
            }
            DataCount = size;
            valueChange1();
        }
        private void valueChange1()
        {
            int index = getPosIndex();
            if (index < 0)
                index = 0;
            _index = index;

            if (_movement == Arrangement.Vertical)
                _index *= cols;
            else
            {
                _index *= lines;
                int c = _dataCount - viewCount;
                if (_index > c)
                    _index = c;
            }
            
            for (int i = _index; i < _index + viewCount; i++)
            {
                if (i < 0) continue;
                if (i > _dataCount - 1)
                {
                    continue;
                }
                bool isOk = false;
                if (bars.count > 0)
                {
                    for (int j = 0; j < bars.count; j++)
                    {
                        if (bars.get(j).getIndex() == i)
                        {
                            if (callback != null)
                            {
                                callback(bars.get(j), i);
                            }
                            isOk = true;
                        }
                    }
                }
                if (isOk) continue;
                CreateItem(i);
            }
        }

        private void valueChange()
        {
            int index = 0;
            _index = index;

            for (int i = _index; i < _index + viewCount; i++)
            {
                if (i < 0) continue;
                if (i > _dataCount - 1)
                {
                    continue;
                }
                bool isOk = false;

                if (bars.count > 0)
                {
                    for (int j = 0; j < bars.count; j++)
                    {
                        if (bars.get(j).getIndex() == i)
                        {
                            
                            if (callback != null)
                            {
                                callback(bars.get(j), i);
                            }
                            isOk = true;
                        }
                    }
                }

                if (isOk) continue;

                CreateItem(i);
            }
        }

        public void OnValueChange(Vector2 pos)
        {
            int index = getPosIndex();
            
            if (_index != index && index > -1)
            {
                _index = index;
                for (int i = bars.count; i > 0; i--)
                {
                    ScorllTableBar item = bars.get(i - 1);

                    int itme_index = item.getIndex()/cols;
                    if (itme_index < index || (itme_index >= index + viewCount/cols))
                    {
                        bars.remove(item);
                        hideBars.Enqueue(item);
                    }
                }

                if (_movement== Arrangement.Vertical)
                    _index *= cols;
                else
                {
                    _index *= lines;
                    //int c = _dataCount - viewCount;
                    //if (_index > c)
                    //    _index = c;
                    if (_index + viewCount > _dataCount+lines) return;
                }


                for (int k = _index; k < _index + viewCount; k++)
                {
                    if (k < 0) continue;
                    if (k > _dataCount - 1)
                    {
                        continue;
                    }
                    bool isOk = false;
                    for (int j = 0; j < bars.count; j++)
                    {
                        if (bars.get(j).getIndex() == k)
                        {
                            if (callback != null)
                            {
                                callback(bars.get(j), k);
                            }
                            isOk = true;
                        }
                    }
                    if (isOk)
                        continue;
                    
                    CreateItem(k);
                }
            }
        }

        private void CreateItem(int index)
        {
            ScorllTableBar itemBase = null;
            if (hideBars.Count > 0)
            {
                itemBase = hideBars.Dequeue();
            }
            else
            {
                if (index != 0)
                {
                    ScorllTableBar bar = cloneScrollBar(index);
                    itemBase = bar;
                }
                else
                {
                    itemBase = cloneScrollBar(0);
                }
            }

            if (callback != null) callback(itemBase, index);

            itemBase.transform.localPosition = getPosition(index);
            itemBase.gameObject.name = "bar_" + index;
            itemBase.setIndex(index);
            bars.add(itemBase);
        }

        public int DataCount
        {
            get { return _dataCount; }
            set
            {
                _dataCount = value;
                updateTotalWidth();
            }
        }

        private void updateTotalWidth()
        {
            switch (_movement)
            {
                case Arrangement.Horizontal://这个需要测试
                    rect.sizeDelta = new Vector2(this.initBar.getWidth() *cols, this.initBar.getHeight() * lines);
                    break;
                case Arrangement.Vertical:
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x, this.initBar.getHeight() * (_dataCount/cols)+ this.initBar.getHeight()*(_dataCount%cols>0?1:0));
                    break;
            }
        }
    }
}
