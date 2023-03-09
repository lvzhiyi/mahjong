using XLua;

namespace cambrian.unreal.scroll
{
    [Hotfix]
    public class ScorllTableBar: UnrealLuaSpaceObject
    {
        private ScrollTableContainer _scroller;
        private int _index;


        public void setIndex(int index)
        {
            this._index = index;
        }

        public int getIndex()
        {
            return this._index;
        }

        public ScrollTableContainer Scroller
        {
            set { _scroller = value; }
        }
    }
}
