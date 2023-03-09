using System;
using UnityEngine;
using XLua;

/// <summary>
/// 弹出提示框
/// </summary>
[Hotfix]
public class Alert : UnrealViewPanel
{
    /** 单例 */
    public static Alert alert;

    /// <summary>
    /// 只能点确认的提示框
    /// </summary>
    public static FixedAlert fixedalert;

    /// <summary>
    /// 只能点确认的提示框
    /// </summary>
    public static FixedAlertShow fixedshow;

    /// <summary>
    /// 有取消按钮提示框
    /// </summary>
    public static Confirm confirmAlert;

    /* static methods */

    /// <summary>
    /// 刷新
    /// </summary>
    public static void updateAlert()
    {
        if (Alert.alert == null) return;
        if (Alert.alert.getVisible()) return;
        if (Alert.alert.messsage == null) return;
        Alert.alert.update();
    }
    /// <summary>
    /// 刷新
    /// </summary>
    public static void updateFixedAlert()
    {
        if (Alert.fixedalert == null) return;
        if (Alert.fixedalert.getVisible()) return;
        if (Alert.fixedalert.messsage == null) return;
        Alert.fixedalert.update();
    }

    /// <summary>
    /// 刷新
    /// </summary>
    public static void updateFixedAlertShow()
    {
        if (Alert.fixedshow == null) return;
        if (Alert.fixedshow.getVisible()) return;
        if (Alert.fixedshow.messsage == null) return;
        Alert.fixedshow.update();
    }

    /// <summary>
    /// 刷新
    /// </summary>
    public static void updateConfirmAlert()
    {
        if (Alert.confirmAlert == null) return;
        if (Alert.confirmAlert.getVisible()) return;
        if (Alert.confirmAlert.messsage == null) return;
        Alert.confirmAlert.update();
    }


    /// <summary>
    /// 需要手动关闭的提示框
    /// </summary>
    /// <param name="str"></param>
    public static void show(string str)
    {
        alert.showAlert(str, "确认", null);
    }
    /// <summary>
    /// 选择确定时调用给定函数的提示框
    /// </summary>
    /// <param name="str"></param>
    /// <param name="func"></param>
    public static void show(string str, Action<Transform> func)
    {
        alert.showAlert(str, "确认", func);
    }
    public static void show(string str, string sstr, Action<Transform> sfunc)
    {
        alert.showAlert(str, sstr, sfunc);
    }

    /// <summary>
    /// 需要手动关闭的提示框
    /// </summary>
    /// <param name="str"></param>
    public static void fixedShow(string str)
    {
        fixedalert.showAlert(str, "确认", null);
    }

    /// <summary>
    /// 需要手动关闭的提示框
    /// </summary>
    /// <param name="str"></param>
    public static void fixedContinueShow(string str)
    {
        fixedshow.showAlert(str, "确认", null);
    }

    [LuaCallCSharp]
    public static void confirmShow(string str, Action<Transform> func)
    {
        Alert.confirmAlert.showAlert(str,"确认", func);
    }

    /// <summary>
    /// 选择确定时调用给定函数的提示框
    /// </summary>
    /// <param name="str"></param>
    /// <param name="func"></param>
    [LuaCallCSharp]
    public static void fixedShow(string str, Action<Transform> func)
    {
        fixedalert.showAlert(str, "确认", func);
    }
    public static void fixedShow(string str, string sstr, Action<Transform> sfunc)
    {
        fixedalert.showAlert(str, sstr, sfunc);
    }


    /// <summary>
    /// 选择确定时调用给定函数的提示框
    /// </summary>
    /// <param name="str"></param>
    /// <param name="func"></param>
    [LuaCallCSharp]
    public static void fixedContinueShow(string str, Action<Transform> func)
    {
        fixedshow.showAlert(str, "确认", func);
    }
    public static void fixedContinueShow(string str, string sstr, Action<Transform> sfunc)
    {
        fixedshow.showAlert(str, sstr, sfunc);
    }


    /// <summary>
    /// 自动消失的提示框
    /// </summary>
    /// <param name="str"></param>
    public static void autoShow(string str)
    {
        alert.autoShowAlert(str);
    }

    /* fields */
    /// <summary>
    /// 将要显示的消息
    /// </summary>
    string messsage;
    /// <summary>
    /// 自动消失
    /// </summary>
    bool auto;
    /// <summary>
    /// 
    /// </summary>
    float lasttime;
    /// <summary>
    /// 确认按钮显示文字
    /// </summary>
    string sstr;

    /// <summary>
    /// 确认
    /// </summary>
    UnrealTextScaleButton confirm;

    ProxyProcess confirmAction;
    /// <summary>
    /// 提示内容
    /// </summary>
    UnrealTextField text;

    protected override void xinit()
    {
        base.xinit();
        this.confirm = this.center.Find("confirm").GetComponent<UnrealTextScaleButton>();
        this.confirmAction = this.confirm.GetComponent<ProxyProcess>();
        this.text = this.center.Find("text").GetComponent<UnrealTextField>();
        this.resizeDelta();
        this.relayout();
    }
    public void showAlert(string str, string sstr, Action<Transform> sfunc)
    {
        this.messsage = str;
        this.auto = false;
        this.lasttime = 0;
        this.sstr = sstr;
        this.confirmAction.callback = sfunc;
    }
    public void autoShowAlert(string str)
    {
        this.messsage = str;
        this.auto = true;
        this.lasttime=Time.time;
        this.confirmAction.callback = null;
    }

    protected override void xupdate()
    {
        if (this.messsage != null)
        {
            this.text.text = this.messsage;
            this.messsage = null;
            this.confirm.text = this.sstr;
            DisplayKit.setVisible(this.confirm, !this.auto);
            this.setVisible(true);
        }
        if (this.auto)
        {
            if (this.lasttime + 2 > Time.time) return;
            this.setVisible(false);
        }
    }

    public override void setVisible(bool b)
    {
        base.setVisible(b);
        if (b) return;
        this.text.text = "";
        this.confirm.text = "";
        this.confirmAction.callback = null;
    }
}