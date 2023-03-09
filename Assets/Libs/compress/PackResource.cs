using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


class PackResource
{
    /// <summary>
    /// 文件id
    /// </summary>
	private int m_id=0;
    /// <summary>
    /// 文件总大小
    /// </summary>
	private int m_totalSize = 0;
	
	private Dictionary<int,SingleFileInfo> m_allFileInfoDic = new Dictionary<int,SingleFileInfo>();


	/** 遍历文件夹获取所有文件信息 **/
	private void SearchFolder(string path)
	{
        DirectoryInfo tmpDirectoryInfo = new DirectoryInfo(path);
        path = tmpDirectoryInfo.FullName.Replace("\\","/");

		string sourceDirpath= path.Substring(0, path.LastIndexOf('/'));
		
		/** 读取文件夹下面所有文件的信息 **/
		DirectoryInfo dirInfo = new DirectoryInfo(path);
		
		foreach (FileInfo fileinfo in dirInfo.GetFiles("*.*", SearchOption.AllDirectories))
		{
			if (fileinfo.Extension == ".meta")
			{
				continue;
			}

			string filename= fileinfo.FullName.Replace("\\","/");
			filename= filename.Replace(sourceDirpath + "/","");
			int filesize = (int)fileinfo.Length;

            SingleFileInfo info = new SingleFileInfo();
			info.file_id = m_id;
			info.file_size = filesize;
			info.file_Path = filename;
			info.file_PathLength = new UTF8Encoding().GetBytes(filename).Length;

            /**  读取这个文件  **/
            FileStream fileStreamRead = new FileStream(fileinfo.FullName, FileMode.Open, FileAccess.Read);
			if (fileStreamRead == null)
			{
                Debug.LogError("读取文件失败 ： "+ fileinfo.FullName);
				return;
			}
			else
			{
				byte[] filedata = new byte[filesize];
				fileStreamRead.Read(filedata, 0, filesize);
				info.file_data = filedata;
			}

			fileStreamRead.Close();
            m_allFileInfoDic.Add(m_id, info);
            m_id++;
            m_totalSize += filesize;
		}
	}
	
	
	/**  打包一个文件夹  **/
	public void PackFolder(string srcpath,string destpath,CodeProgress progress)
	{
        SearchFolder(srcpath);
        Debug.Log(string.Concat("数量 : <<color=#FF5B00>", m_id, "</color>> 大小 : <<color=#FF5B00>", m_totalSize, "</color>>"));
        /**  更新文件在ZIP中的起始点  **/
        int firstfilestartpos = 0 + 4;
        for (int index = 0; index < m_allFileInfoDic.Count; index++)
        {
            firstfilestartpos += 4 + 4 + 4 + 4 + m_allFileInfoDic[index].file_PathLength;
        }

        int startpos = 0;
        for (int index = 0; index < m_allFileInfoDic.Count; index++)
        {
            if (index == 0)
            {
                startpos = firstfilestartpos;
            }
            else
            {
                startpos = m_allFileInfoDic[index - 1].file_StartPos + m_allFileInfoDic[index - 1].file_size;//上一个文件的开始+文件大小;
            }

            m_allFileInfoDic[index].file_StartPos = startpos;
        }

        /**  写文件  **/
        FileStream fileStream = new FileStream(destpath, FileMode.Create);

        /**  文件总数量  **/
        byte[] totaliddata = System.BitConverter.GetBytes(m_id);
        fileStream.Write(totaliddata, 0, totaliddata.Length);

        for (int index = 0; index < m_allFileInfoDic.Count; index++)
        {
            /** 写入ID **/
            byte[] iddata = System.BitConverter.GetBytes(m_allFileInfoDic[index].file_id);
            fileStream.Write(iddata, 0, iddata.Length);

            /**  写入StartPos  **/
            byte[] startposdata = System.BitConverter.GetBytes(m_allFileInfoDic[index].file_StartPos);
            fileStream.Write(startposdata, 0, startposdata.Length);

            /**  写入size  **/
            byte[] sizedata = System.BitConverter.GetBytes(m_allFileInfoDic[index].file_size);
            fileStream.Write(sizedata, 0, sizedata.Length);

            /**  写入pathLength  **/
            byte[] pathLengthdata = System.BitConverter.GetBytes(m_allFileInfoDic[index].file_PathLength);
            fileStream.Write(pathLengthdata, 0, pathLengthdata.Length);

            /**  写入path  **/
            byte[] mypathdata = new UTF8Encoding().GetBytes(m_allFileInfoDic[index].file_Path);

            fileStream.Write(mypathdata, 0, mypathdata.Length);
        }

        int totalprocessSize = 0;
        foreach (var infopair in m_allFileInfoDic)
        {
            SingleFileInfo info = infopair.Value;
            int size = info.file_size;
            byte[] tmpdata = null;
            int processSize = 0;
            while (processSize < size)
            {
                if (size - processSize < 1024)
                {
                    tmpdata = new byte[size - processSize];
                }
                else
                {
                    tmpdata = new byte[1024];
                }
                fileStream.Write(info.file_data, processSize, tmpdata.Length);

				processSize+=tmpdata.Length;
				totalprocessSize+=tmpdata.Length;

				progress.SetProgressPercent(m_totalSize,totalprocessSize);
			}
		}
		
		fileStream.Flush();
		fileStream.Close();
		/** 重置数据 **/
		m_id = 0;
		m_totalSize = 0;
		m_allFileInfoDic.Clear();
		
	}
}
