using cambrian.common;
using cambrian.uui;
using UnityEngine;
using XLua;

/// <summary>
/// 截屏
/// </summary>
[Hotfix]
public class CaptureScreenshot
{
    /// <summary>
    /// 分解成多个文件
    /// </summary>
    /// <param name="name"></param>
    public static void splitTexture2D(string name)
    {
        Url url=new Url(Url.RES, name, null);
        Loader.getLoader().loads<object, Sprite>(url, null, (obj, sprites) =>
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                Sprite sprite = sprites[i];
                Debug.Log(sprite.name + "," + sprite.rect + "," + sprite.texture.name);
                newTableTexture2D(name + '/' + sprite.name, sprite.rect, sprite.texture);
            }
        }); 
    }
    public static void newTableTexture2D(string name, Rect rect, Texture2D src)
    {
        Texture2D texture = new Texture2D((int) rect.width, (int) rect.height);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = src.GetPixel(x + (int) rect.x, y + (int) rect.y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        save(texture, name + ".png");
    }
    public static void newTableTexture2D(int w, int h, int space)
    {
        Texture2D texture = new Texture2D(w, h);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                if (x%space == 0 || y%space == 0)
                    texture.SetPixel(x, y, Color.black);
                else
                    texture.SetPixel(x, y, Color.clear);
            }
        }
        texture.Apply();
        save(texture, "table_" + Time.time + ".png");
    }
    /// <summary>
    /// 需在OnPostRender()中调用
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public static Texture2D capture(Rect rect=default(Rect))
    {
        if (rect == default(Rect))
         rect = new Rect(0, 0, Screen.width, Screen.height);
        // 创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int) rect.width, (int) rect.height, TextureFormat.ARGB32, false);
        
        // 读取屏幕像素信息并存储为纹理数据
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        save(screenShot, Time.time + ".png");
        return screenShot;
    }
    /// <summary>
    /// 保存图片到指定文件
    /// </summary>
    /// <param name="screenShot"></param>
    /// <param name="filename"></param>
    public static void save(Texture2D screenShot, string filename)
    {
        byte[] bytes = screenShot.EncodeToPNG();
        string path = Application.persistentDataPath + "/" + filename;
        FileKit.write(path, bytes);
    }

    public delegate void Back(byte[] bytes);

    public static Back back;

    public static void captures(Back b)
    {
        back = b;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        // 创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int) rect.width, (int) rect.height, TextureFormat.RGBA32, false);

        // 读取屏幕像素信息并存储为纹理数据
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        back(screenShot.EncodeToJPG());
    }
}