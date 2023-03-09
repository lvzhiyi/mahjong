using System;
using System.IO;
using System.Threading;
using UnityEngine;

public class CompressZip
{
    /// <summary>
    /// 进度
    /// </summary>
    CodeProgress codeProgress = null; //进度;

    public Thread compressThread = null; //压缩线程;

    /// <summary>
    /// 打包路径
    /// </summary>
    string packPath;

    /// <summary>
    /// 输出路径
    /// </summary>
    private string outPutPath;

    private float percent = 0f;


    public CompressZip(string packPath, string outPutPath)
    {
        this.packPath = packPath;
        this.outPutPath = outPutPath;
        this.codeProgress = new CodeProgress(setProgressPercent);

        compressThread = new Thread(new ThreadStart(compress));
    }

    private string name;
    public CompressZip(string packPath, string outPutPath,string name)
    {
        this.packPath = packPath;
        this.outPutPath = outPutPath;
        this.codeProgress = new CodeProgress(setProgressPercent);
        this.name = name;

        compressThread = new Thread(new ThreadStart(compress));
    }

    /// <summary>
    /// 获取解压进度
    /// </summary> 
    public float getPercent()
    {
        return this.percent;
    }

    void compress()
    {
        try
        {
            new PackResource().PackFolder(packPath, packPath + name + "xdcpres.upk", codeProgress);
            string opp = outPutPath.Substring(0, outPutPath.LastIndexOf('/'));
            if (!Directory.Exists(opp))
                Directory.CreateDirectory(opp);
            LZMAHelper.Compress(packPath + name + "xdcpres.upk", outPutPath, codeProgress);
            File.Delete(packPath + name + "xdcpres.upk");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
        finally
        {
            compressThread.Interrupt();
        }
    }

    void setProgressPercent(long fileSize, long processSize)
    {
        percent = (float) processSize/fileSize;

        if (percent >= 1)
        {

        }
    }
}