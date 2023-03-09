using XLua;

namespace cambrian.unreal.process
{
    [Hotfix]
    public class CheckRadioListProcess:MouseClickProcess
    {
        public override void execute()
        {
            IdBar bar = GetComponent<IdBar>();
            transform.parent.GetComponent<UnrealRadioList>().selectedIndex(bar.getId());
        }
    }
}
