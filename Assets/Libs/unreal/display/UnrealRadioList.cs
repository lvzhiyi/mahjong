using XLua;
/// <summary>
/// 单选框列表
/// </summary>
[Hotfix]
public class UnrealRadioList : UnrealTableContainer
{
    private int selected_index;

    public void selectedIndex(int index)
    {
        for (int i = 0; i < this.size; i++)
        {
            if (this.bars[i] == null) continue;
            if (((IdInterface) this.bars[i]).getId() == index)
            {
                ((UnrealCheckObject) this.bars[i]).setState(UnrealRadio.ACTIVED);
                this.selected_index = index;
            }
            else
                ((UnrealCheckObject) this.bars[i]).setState(UnrealRadio.NORMAL);
        }
    }


    public int getSelectedIndex()
    {
        return selected_index;
    }

}