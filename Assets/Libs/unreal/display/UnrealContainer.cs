using cambrian.uui;
using System;
using System.Collections;
using scene.game;
using UnityEngine;
using UnityEngine.UI;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 容器
/// 1.添加、移除子gameObject
/// 2.动态加载并添加prefab
/// 3.动态加载并添加图片
/// </summary>
[Hotfix]
public class UnrealContainer : UnrealIndexSpaceObject
{
    /// <summary>
    /// 
    /// </summary>
    protected Hashtable displays = new Hashtable();

    public ScrollRect drag;

    /// <summary>
    /// 最小宽高
    /// </summary>
    public float minWidth, minHeight;

    public override void init()
    {
        base.init();
        this.drag = this.transform.parent.GetComponent<ScrollRect>();
        if (this.drag == null) this.drag = this.transform.parent.parent.GetComponent<ScrollRect>();
        if (this.drag != null)
        {
            this.drag.content.GetComponent<UnrealContainer>().drag = this.drag;
            this.drag.verticalNormalizedPosition = 1;
            this.drag.horizontalNormalizedPosition = 0;
        }
    }

    public int count
    {
        get { return this.gameObject.transform.childCount; }
    }

    public GameObject add(Type clazz)
    {
        string prefabName = "prefab/" + clazz.FullName.Replace('.', '/');
        if (log.isDebug)
            Debug.Log(log.getMessage(clazz.FullName + "," + prefabName));
        GameObject obj = add(prefabName);
        obj.AddComponent(clazz);
        return obj;
    }

    public GameObject add(string prefab)
    {
        GameObject res= (GameObject)Resources.Load(prefab);
        if (res == null) return null;
        GameObject obj = Instantiate(res);
        return add(obj);
    }

    public GameObject addABbundle(string prefab)
    {
        string path = "";
        int index = prefab.LastIndexOf('/');
        if (index >= 0) path = prefab.Substring(index + 1);
        if (this.displays[path]!=null) return null;

        GameObject gobj = null;
        if (SpotRedRoot.roots != null)
        {
            gobj = SpotRedRoot.roots.regionModule.region.module.loadPrefab(path);
        }

        if (gobj == null)
        {
            gobj = ModuleManager.manager.loadPrefab(path);
        }

        
        if (gobj == null)
        {
            GameObject o=add(prefab);

#if UNITY_EDITOR
            if (o == null)
            {
                o = AssetDatabase.LoadAssetAtPath("Assets/" + prefab + ".prefab", typeof(GameObject)) as GameObject;
                o = add(Instantiate(o));
            }
#endif

            this.displays[path] = o;
            return o;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(gobj);
            this.displays[path] = obj;
            return add(obj);
        }
       
    }

    public GameObject add(string prefab, Vector3 point)
    {
        GameObject obj = (GameObject) Instantiate(Resources.Load(prefab), point, Quaternion.identity);
        return add(obj, point);
    }

    /// <summary>
    /// 克隆prefab并添加
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public GameObject cloneAdd(GameObject prefab, Vector3 point)
    {
        GameObject obj =Instantiate(prefab, point, Quaternion.identity);
        return add(obj, point);
    }
    public GameObject cloneAdd(GameObject prefab, string name)
    {
        GameObject obj = Instantiate(prefab);
        obj.name = name;
        return add(obj);
    }
    public GameObject cloneAdd(GameObject prefab)
    {
        GameObject obj =Instantiate(prefab);
        return add(obj);
    }

    public GameObject add(GameObject obj)
    {
        Vector3 point = obj.transform.position;
        return add(obj, point);
    }

    public virtual GameObject add(GameObject obj, Vector3 point)
    {
        if (obj.transform.parent != null)
            throw new UnityException("add ,obj.transform.parent.gameObject.name=" + obj.transform.parent.gameObject.name);
        if (obj.transform is RectTransform)
        {
            ((RectTransform) obj.transform).SetParent(this.transform);
        }
        else
        {
            obj.transform.parent = this.transform;
        }
        obj.transform.localPosition = point;
        return obj;
    }

    public GameObject addSprite(string path, string name, Vector3 point)
    {
        GameObject obj;
        if (name == null)
            obj = new GameObject();
        else
            obj = new GameObject(name);
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        Url url = new Url(Url.RES, path, null);
        Loader.getLoader().load<SpriteRenderer, Sprite>(url, renderer, (parm, sprite) => parm.sprite = sprite);
        return add(obj, point);
    }

    public GameObject addSprite(string path, string name)
    {
        return this.addSprite(path, name, Vector3.zero);
    }

    public GameObject addSprite(Sprite sprite, string name)
    {
        GameObject obj;
        if (name == null)
            obj = new GameObject();
        else
            obj = new GameObject(name);
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        return add(obj);
    }

    public void remove(string name)
    {
        Transform obj = this.transform.Find(name);
        if (obj == null)
            throw new UnityException("remove " + name + ",null");
        Destroy(obj.gameObject);
    }

    public virtual void resizeDelta()
    {
        if (this.transform is RectTransform)
        {
            float h = this.getHeight();
            if (this.drag != null)
            {
                float sh = this.drag.GetComponent<RectTransform>().rect.height;
                if (sh > h) h = sh;
            }
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            Vector2 vector = rectTransform.sizeDelta;
            float offset = (vector.y - h)/2;
            vector.y = h;
            rectTransform.sizeDelta = vector;

            float y = this.transform.localPosition.y;
            DisplayKit.setLocalY(this.gameObject, y + offset);
        }
    }


    public virtual void resizeDelta(SizeDeltaLayer layer)
    {
        if (layer == SizeDeltaLayer.Null) return;
        if (this.transform is RectTransform)
        {
            float w = this.getWidth(layer);
            float h = this.getHeight(layer);
            if (this.drag != null)
            {
                if (this.drag.vertical)
                {
                    float sh = this.drag.GetComponent<RectTransform>().rect.height;
                    if (sh > h) h = sh;
                }
                if (this.drag.horizontal)
                {
                    float sw = this.drag.GetComponent<RectTransform>().rect.width;
                    if (sw > w) w = sw;
                }
            }
            if (minWidth > 0 && minWidth > w) w = minWidth;
            if (minHeight > 0 && minHeight > h) h = minHeight;
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            Vector2 vector = rectTransform.sizeDelta;
            float offsetX = (vector.x - w) / 2;
            float offsetY = (vector.y - h) / 2;
            vector.x = w;
            vector.y = h;
            rectTransform.sizeDelta = vector;

            if (this.drag != null)
            {
                if (this.drag.vertical)
                {
                    float y = this.transform.localPosition.y;
                    DisplayKit.setLocalY(this.gameObject, y + offsetY);
                }
                if (this.drag.horizontal)
                {
                    float x = this.transform.localPosition.x;
                    DisplayKit.setLocalX(this.gameObject, x - offsetX);
                }
            }
        }
    }

    /// <summary>
    /// 重置滚动横向
    /// </summary>
    public void resizeScrollHorizontal()
    {
        if (this.drag == null) return;
        this.drag.horizontalNormalizedPosition = 0;
    }
    /// <summary>
    /// 重置滚动纵向
    /// </summary>
    public void resizeScrollVertical()
    {
        if (this.drag == null) return;
        this.drag.verticalNormalizedPosition = 1;
    }
    /// <summary>
    /// 排版，拉到滚动面板的最下方
    /// </summary>
    public void scrollToBottom()
    {
        if (this.drag == null) return;
        this.drag.verticalNormalizedPosition = 0;
    }


    /// <summary>
    /// 清空所有子对象
    /// </summary>
    public void clear()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}