
using cambrian.common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace cambrian.unreal.scroll
{
    [Hotfix]
    public class ChatScrollContainer : UnrealContainer
    {
        public delegate void Callback(ScrollBar bar, int index);

        public enum Arrangement { Horizontal, Vertical, }

        public Arrangement _movement = Arrangement.Horizontal;
        //Item之间的距离
        [Range(0, 20)]
        public int cellPadiding = 5;

        //默认加载的Item个数，一般比可显示个数大2~3个

        [Range(0, 20)]
        public int viewCount = 6;

        /// <summary>
        /// 实际显示的个数
        /// </summary>
        private int viewCount_1 = 0;


        public RectTransform _content;

        float content_x;
        float content_y;

        int _index = -1;
        /// <summary>
        /// 所有的bar
        /// </summary>
        public ArrayList<ScrollBar> bars;

        int _dataCount;
        /// <summary>
        /// 将未显示出来的Item存入未使用队列里面，等待需要使用的时候直接取出
        /// </summary>
        System.Collections.Generic.Queue<ScrollBar> hideBars;

        ScrollBar initBar;

        public Callback callback;
        /// <summary>
        /// 无数据图标
        /// </summary>
        private Image noData;


        /// <summary>
        /// 初始化高度
        /// </summary>
        float initheight;
        /// <summary>
        /// 上方指示图标(预制件中赋值)
        /// </summary>
        public Image topDirection;
        /// <summary>
        /// 下方指示图标（预制件中赋值)
        /// </summary>
        public Image bottomDirection;

        public CustomSrcollRect srcollRect;

        public override void init()
        {
            this.initBar = this.transform.Find("scrollrect").Find("content").Find("bar_0").GetComponent<ScrollBar>();
            this.initBar.gameObject.SetActive(false);
            if (this.transform.Find("nodata") != null)
                this.noData = this.transform.Find("nodata").GetComponent<Image>();
            bars = new ArrayList<ScrollBar>();
            hideBars = new System.Collections.Generic.Queue<ScrollBar>();
            this.viewCount_1 = this.viewCount;

            this.content_x = this._content.localPosition.x;
            this.content_y = this._content.localPosition.y;

            initheight = this._content.rect.height;

            if (topDirection != null)
            {
                topDirection.DOFade(0.2f, 1.2f).SetLoops(-1, LoopType.Yoyo);
            }

            if (bottomDirection != null)
                bottomDirection.DOFade(0.2f, 1.2f).SetLoops(-1, LoopType.Yoyo);
        }

        /// <summary>
        /// 预制件中绑定该方法，topDirection,bottomDirection 必然存在
        /// </summary>
        public void onValueDrag()
        {
            if (this.drag.vertical)
            {
                if (this._content.rect.height <= initheight)
                {
                    topDirection.gameObject.SetActive(false);
                    bottomDirection.gameObject.SetActive(false);
                    return;
                }
                float value = drag.verticalNormalizedPosition;
                topDirection.gameObject.SetActive(false);
                bottomDirection.gameObject.SetActive(false);
                if (value > 0 && value < 1)
                {
                    topDirection.gameObject.SetActive(true);
                    bottomDirection.gameObject.SetActive(true);

                }
                else if (value >= 1)
                {
                    topDirection.gameObject.SetActive(false);
                    bottomDirection.gameObject.SetActive(true);
                }
                else
                {
                    topDirection.gameObject.SetActive(true);
                    bottomDirection.gameObject.SetActive(false);
                }
            }
        }



        [CSharpCallLua]
        public void updateData(Callback callback)
        {
            this.callback = callback;
        }

        private ScrollBar cloneScrollBar(int index)
        {
            GameObject obj = this.cloneAdd(initBar.gameObject);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "bar_" + index;
            obj.transform.SetParent(_content, false);
            ScrollBar scorllBar = obj.GetComponent<ScrollBar>();
            scorllBar.init();
            scorllBar.gameObject.SetActive(true);

            scorllBar.transform.localPosition = GetPosition(index);
            return scorllBar;
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
                this.viewCount = this.viewCount_1;
            }


            float verticalNormalizedPosition = srcollRect.verticalNormalizedPosition;
            DataCount = size;
            OnValueChange(Vector2.zero);
            if (verticalNormalizedPosition <= 0.03f)
                srcollRect.verticalNormalizedPosition = 0;
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
                this.viewCount = this.viewCount_1;
            }

            this.hideBars.Clear();
            this.bars.clear();

            ScrollBar[] barsss = this.transform.Find("scrollrect").Find("content").GetComponentsInChildren<ScrollBar>();

            for (int i = barsss.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(barsss[i].gameObject);
            }

            if (bars.count == 0 && size > 0)
            {
                bars.add(cloneScrollBar(0));
            }

            this._content.localPosition = new Vector3(content_x, content_y);
            DataCount = size;
            valueChange();



            showNoDataImage();

            if (drag != null)
            {
                onValueDrag();
            }
        }

        /// <summary>
        /// 显示无数据图标
        /// </summary>
        private void showNoDataImage()
        {
            if (noData != null)
            {
                bool b = false;

                if (DataCount == 0)
                    b = true;
                noData.gameObject.SetActive(b);
            }
        }

        /// <summary>
        /// 移除后不重置位置
        /// </summary>
        /// <param name="size"></param>
        public void removeresize(int size)
        {
            if (size < this.viewCount)
            {
                this.viewCount = size;
            }
            else
            {
                this.viewCount = this.viewCount_1;
            }

            this.hideBars.Clear();
            this.bars.clear();

            ScrollBar[] barsss = this.transform.Find("scrollrect").Find("content").GetComponentsInChildren<ScrollBar>();

            for (int i = barsss.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(barsss[i].gameObject);
            }

            if (bars.count == 0 && size > 0)
            {
                bars.add(cloneScrollBar(0));
            }

            DataCount = size;
            valueChange();

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
            int index = GetPosIndex();

            if (_index != index && index > -1)
            {
                _index = index;
                for (int i = bars.count; i > 0; i--)
                {
                    ScrollBar item = bars.get(i - 1);
                    if (item.getIndex() < index || (item.getIndex() >= index + viewCount))
                    {
                        bars.remove(item);
                        hideBars.Enqueue(item);
                    }
                }
                for (int i = _index; i < _index + viewCount; i++)
                {
                    if (i < 0) continue;
                    if (i > _dataCount - 1)
                    {
                        continue;
                    }
                    bool isOk = false;
                    for (int j = 0; j < bars.count; j++)
                    {
                        if (bars.get(j).getIndex() == i)
                        {
                            isOk = true;
                        }
                    }
                    if (isOk)
                    {
                        continue;
                    }
                    CreateItem(i);
                }
            }
        }

        private void CreateItem(int index)
        {
            ScrollBar itemBase = null;
            if (hideBars.Count > 0)
            {
                itemBase = hideBars.Dequeue();
            }
            else
            {
                if (index != 0)
                {
                    ScrollBar bar = cloneScrollBar(index);
                    itemBase = bar;
                }
                else
                {
                    itemBase = cloneScrollBar(0);
                }
            }

            if (callback != null) callback(itemBase, index);

            itemBase.transform.localPosition = GetPosition(index);
            itemBase.gameObject.name = "bar_" + index;
            itemBase.setIndex(index);
            bars.add(itemBase);
        }

        private int GetPosIndex()
        {
            switch (_movement)
            {
                case Arrangement.Horizontal:
                    return Mathf.FloorToInt(_content.anchoredPosition.x / -(this.initBar.getWidth() + cellPadiding));
                case Arrangement.Vertical:
                    return Mathf.FloorToInt(_content.anchoredPosition.y / (this.initBar.getHeight() + cellPadiding));
            }
            return 0;
        }

        public Vector3 GetPosition(int i)
        {
            switch (_movement)
            {
                case Arrangement.Horizontal:
                    return new Vector3(i * (this.initBar.getWidth() + cellPadiding), 0f, 0f);
                case Arrangement.Vertical:
                    return new Vector3(0f, i * -(this.initBar.getHeight() + cellPadiding), 0f);
            }
            return Vector3.zero;
        }

        public int DataCount
        {
            get { return _dataCount; }
            set
            {
                _dataCount = value;
                UpdateTotalWidth();
            }
        }

        private void UpdateTotalWidth()
        {
            switch (_movement)
            {
                case Arrangement.Horizontal:
                    _content.sizeDelta = new Vector2(this.initBar.getWidth() * _dataCount + cellPadiding * (_dataCount - 1), _content.sizeDelta.y);
                    break;
                case Arrangement.Vertical:
                    _content.sizeDelta = new Vector2(_content.sizeDelta.x, this.initBar.getHeight() * _dataCount + cellPadiding * (_dataCount - 1));
                    break;
            }
        }
    }
}
