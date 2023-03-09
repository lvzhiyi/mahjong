using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 根据状态设置所指定的文本的颜色
/// </summary>
[Hotfix]
public class TextColorCheckBoxTool : UnrealCheckBox
{

    public Transform[] trans;

    public Color32 normal = Color.white;

    public Color32 actived = Color.white;

    public override void setState(bool state)
    {
        base.setState(state);
        for (int i = 0; i < this.trans.Length; i++)
        {
            Text text = this.trans[i].GetComponent<Text>();
            if (text == null) continue;
            if (state)
                text.color = this.actived;
            else
                text.color = this.normal;
        }
    }
}