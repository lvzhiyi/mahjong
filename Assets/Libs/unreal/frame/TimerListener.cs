using cambrian.common;
using UnityEngine;
using XLua;

[Hotfix]
public class TimerListener<T> : MonoBehaviour
    where T : UnrealLuaPanel
{

    T _root;
    /// <summary>
    /// 所属面板
    /// </summary>
    public T root
    {
        get
        {
            if (this._root == null)
            {
                Transform tran = this.transform;
                this._root = tran.GetComponent<T>();
                while (this._root == null)
                {
                    tran = tran.parent;
                    this._root = tran.GetComponent<T>();
                }
            }
            return this._root;
        }
        set { this._root = value; }
    }
    void Awake()
    {
        this.init();
    }
    public virtual void init()
    {
        
    }
    void Update()
    {
        this.update(TimeKit.currentTimeMillis);
    }
    /// <summary>
    /// 帧驱动
    /// </summary>
    public virtual void update(long time)
    {

    }

    public virtual void setVisible(bool b)
    {
        this.gameObject.SetActive(b);
    }
}