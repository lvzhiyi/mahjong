using UnityEngine.UI;

public class NumberKey: UnrealLuaSpaceObject
{
    /// <summary>
    /// 键
    /// </summary>
    public string key;

    Text text;

    protected override void xinit()
    {
        base.xinit();
        this.text = this.transform.Find("text").GetComponent<Text>();
    }
    public void setKey(string key)
    {
        this.key = key;
    }
    protected override void xrefresh()
    {
        base.refresh();
        this.text.text = this.key;
    }
}
