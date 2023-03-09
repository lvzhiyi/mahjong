using System;
using System.IO;
using System.Threading;
using UnityEngine;
using XLua;

/// <summary>
/// 解压
/// </summary>
[Hotfix]
public class UnCompress
{
    /// <summary>
    /// 进度
    /// </summary>
    CodeProgress codeProgress = null; //进度;

    public Thread unCompressThread = null; //压缩线程;
    /// <summary>
    /// 目标路径
    /// </summary>
    private string destPath;
    /// <summary>
    /// 解压路径
    /// </summary>
    private string unCompressPath;

    private float percent = 0f;

    /// <summary>
    /// 是否解压(true:解压，false:拆包)
    /// </summary>
    private bool isUnCompress;

    public UnCompress(string destPath,string unCompressPath)
    {
        this.destPath = destPath;
        this.unCompressPath = unCompressPath;
        this.codeProgress = new CodeProgress(setProgressPercent);

        unCompressThread = new Thread(new ThreadStart(deCompress));
    }

    void setProgressPercent(long fileSize, long processSize)
    {
        this.percent = (float)processSize / fileSize;
    }

    public float getPerCent()
    {
        if (isUnCompress)
        {
            if (this.percent >= 1)
                this.percent = 0.99f;
        }
        return this.percent;
    }

    void deCompress()
    {
        try
        {
            string[] ss= destPath.Split(new[] {'/'});
            string name = destPath.Substring(destPath.LastIndexOf('/')+1,ss[ss.Length-1].Length);
            string opp = destPath.Substring(0, destPath.LastIndexOf('/'))+ "xdcp"+name;
            this.isUnCompress = true;
            LZMAHelper.DeCompress(destPath, opp, codeProgress);
            this.isUnCompress = false;
            new ZipExtra().ExtraZIP(opp, unCompressPath, codeProgress);
            if(File.Exists(opp)) File.Delete(opp);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
}

