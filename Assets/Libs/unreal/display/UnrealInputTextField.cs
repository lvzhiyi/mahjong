using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 输入框
/// </summary>
[Hotfix]
public class UnrealInputTextField : UnrealLuaSpaceObject
{
    /// <summary>
    /// 输入文本框
    /// </summary>
    InputField textField;
    /// <summary>
    /// 可输入提示图标
    /// </summary>
    Transform editicon;
    /// <summary>
    /// 按钮
    /// </summary>
    Transform submit;
    /// <summary>
    /// 结束输入时调用
    /// </summary>
    [HideInInspector] public ProxyProcess endEdit;
    /// <summary>
    /// 输入变更时调用
    /// </summary>
    [HideInInspector] public Action<string> valueChange;
    /// <summary>
    /// 失去焦点调用
    /// </summary>
    [HideInInspector] public Action<string> endChange;

    /// <summary>
    /// 是否可输入的
    /// </summary>
    public bool interactive
    {
        get
        {
            this.ensureInit();
            return this.textField.interactable;
        }
        set
        {
            this.ensureInit();
            this.textField.interactable = value;
            if (this.editicon == null) return;
            this.editicon.gameObject.SetActive(value);
        }
    }
    /// <summary>
    /// 设置"提交内容的触发显示对象"是否显示
    /// </summary>
    public bool submitable
    {
        get
        {
            this.ensureInit();
            return this.submit.gameObject.activeSelf;
        }
        set
        {
            this.ensureInit();
            this.submit.gameObject.SetActive(value);
        }
    }
    /// <summary>
    /// 最大长度
    /// </summary>
    public int maxlength
    {
        get
        {
            this.ensureInit();
            return this.textField.characterLimit;
        }
        set
        {
            this.ensureInit();
            this.textField.characterLimit = value;
        }
    }
    /// <summary>
    /// 键盘类型
    /// </summary>
    public InputField.ContentType keyboard
    {
        get
        {
            this.ensureInit();
            return this.textField.contentType;
        }
        set
        {
            this.ensureInit();
            this.textField.contentType = value;
        }
    }
    /// <summary>
    /// 换行类型
    /// </summary>
    public InputField.LineType warp
    {
        get
        {
            this.ensureInit();
            return this.textField.lineType;
        }
        set
        {
            this.ensureInit();
            this.textField.lineType = value;
        }
    }
    /// <summary>
    /// 内容为空时的提示文字
    /// </summary>
    public string tips
    {
        get
        {
            this.ensureInit();
            return ((Text) textField.placeholder).text;
        }
        set
        {
            this.ensureInit();
            ((Text) textField.placeholder).text = value;
        }
    }
    /// <summary>
    /// 显示的文字
    /// </summary>
    public string text
    {
        get
        {
            this.ensureInit();
            return this.textField.text;
        }
        set
        {
            this.ensureInit();
            this.textField.text = value;
        }
    }
    private void ensureInit()
    {
        if (this.textField == null) this.init();
    }
    protected override void xinit()
    {
        base.xinit();
        this.textField = this.transform.Find("text").GetComponent<InputField>();
        this.editicon = this.transform.Find("edit_icon");
        this.submit = this.transform.Find("submit");
    }
    /// <summary>
    /// 输入变化
    /// </summary>
    public void onValueChange()
    {
        if (valueChange == null) return;
        valueChange(this.text);
    }
    /// <summary>
    /// 输入结束(点提交或点其它位置时,即失去焦点后)
    /// </summary>
    public void onEndEdit()
    {
        if (endChange != null)
            endChange(text);
        if (endEdit == null) return;
        endEdit.execute();
    }
}