using UnityEngine.UI;
using XLua;

/// <summary>
/// 实体显示框
/// </summary>
[Hotfix]
public class IdBar : UnrealCheckObject, IdInterface
{
    protected int id;

    protected string _name;

    /// <summary>
    /// 名字
    /// </summary>
    protected Text text;

    /// <summary>
    /// 选中
    /// </summary>
    protected UnrealCheckBox check;

    protected override void xinit()
    {
        base.xinit();
        if (this.transform.Find("text") != null)
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
        }
        if (this.transform.Find("check") != null)
        {
            this.check = this.transform.Find("check").GetComponent<UnrealCheckBox>();
        }
    }

    public virtual void setValue(int id, string name)
    {
        this.id = id;
        this._name = name;
    }

    public virtual void setId(int id)
    {
        this.id = id;
    }


    public virtual int getId()
    {
        return this.id;
    }

    public override bool getState()
    {
        if (this.check == null)
            return base.getState();
        else return this.check.getState();
    }
    public override void setState(bool state)
    {
        if (this.check == null)
            base.setState(state);
        else this.check.setState(state);
    }
    protected override void xrefresh()
    {
        base.xrefresh();
        if(this.text!=null)
        this.text.text = this._name;
    }
}