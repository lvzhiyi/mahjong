using cambrian.unreal.display;
using XLua;

namespace cambrian.unreal.process
{
    [Hotfix]
    public class CheckBoxsListProcess:MouseClickProcess
    {
        public override void execute()
        {
            IdBar bar = GetComponent<IdBar>();
            transform.parent.GetComponent<UnrealCheckBoxsList>().selectedIndex(bar.getId());
        }
    }
}
