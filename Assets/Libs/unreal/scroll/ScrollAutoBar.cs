using System;
using XLua;

namespace cambrian.unreal.scroll
{
    [Hotfix]
    public class ScrollAutoBar: UnrealLuaSpaceObject
    {
        private Object obj;

        protected override void xinit()
        {
            
        }

        protected override void xrefresh()
        {
            
        }

        public Object ObjectData
        {
            get{
                return obj;
            }
            set { obj = value; }
        }
    }
}
