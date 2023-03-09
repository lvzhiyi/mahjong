using UnityEngine;
using XLua;

namespace cambrian.unreal.scroll
{
    [Hotfix]
    public class ScrollBar: UnrealLuaSpaceObject
    {
        private ScrollContainer _scroller;

        private int _index;

        public RectTransform rectTransform;

        protected override void xinit()
        {
            base.xinit();
        }

        public void setIndex(int index)
        {
            this._index = index;
        }

        public int getIndex()
        {
            return this._index;
        }

        public ScrollContainer Scroller
        {
            set { _scroller = value; }
        }
    }
}
