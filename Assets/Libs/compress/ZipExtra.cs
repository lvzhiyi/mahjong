using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ZipExtra
{
	private  Dictionary<int,SingleFileInfo> m_allFileInfoDic = new Dictionary<int,SingleFileInfo>();

	private  System.Text.UTF8Encoding m_UTF8Encoding=new System.Text.UTF8Encoding();

	public  void ExtraZIP(string zipfilepath,string outputpath,CodeProgress progress)
	{
        m_allFileInfoDic.Clear();
        int totalsize=0;
		
		FileStream zipFilestream=new FileStream(zipfilepath,FileMode.Open);
		zipFilestream.Seek(0,SeekOrigin.Begin);
		
		int offset=0;
		
		//读取文件数量;
		byte[] totaliddata=new byte[4];
		zipFilestream.Read(totaliddata,0,4);
		int filecount=BitConverter.ToInt32(totaliddata,0);
		offset+=4;
		
		//读取所有文件信息;
		for(int index=0;index<filecount;index++)
		{
			//读取id;
			byte[] iddata=new byte[4];
			zipFilestream.Seek(offset,SeekOrigin.Begin);
			zipFilestream.Read(iddata,0,4);
			int id=BitConverter.ToInt32(iddata,0);
			offset+=4;
			
			//读取StartPos;
			byte[] startposdata=new byte[4];
			zipFilestream.Seek(offset,SeekOrigin.Begin);
			zipFilestream.Read(startposdata,0,4);
			int startpos=BitConverter.ToInt32(startposdata,0);
			offset+=4;
			
			//读取size;
			byte[] sizedata=new byte[4];
			zipFilestream.Seek(offset,SeekOrigin.Begin);
			zipFilestream.Read(sizedata,0,4);
			int size=BitConverter.ToInt32(sizedata,0);
			offset+=4;
			
			//读取pathLength;
			byte[] pathLengthdata=new byte[4];
			zipFilestream.Seek(offset,SeekOrigin.Begin);
			zipFilestream.Read(pathLengthdata,0,4);
			int pathLength=BitConverter.ToInt32(pathLengthdata,0);
			offset+=4;
			
			//读取path;
			byte[] pathdata=new byte[pathLength];
			zipFilestream.Seek(offset,SeekOrigin.Begin);
			zipFilestream.Read(pathdata,0,pathLength);
			string path=m_UTF8Encoding.GetString(pathdata);
			offset+=pathLength;
			
			
			//添加到Dic;
			SingleFileInfo info=new SingleFileInfo();
			info.file_id = id;
			info.file_size = size;
			info.file_PathLength=pathLength;
			info.file_Path=path;
			info.file_StartPos=startpos;
			m_allFileInfoDic.Add(id,info);
			
			totalsize+=size;
		}
		
		//释放文件;
		int totalprocesssize=0;
		foreach(var infopair in m_allFileInfoDic)
		{
			SingleFileInfo info=infopair.Value;
			
			int startPos=info.file_StartPos;
			int size=info.file_size;
			string path=info.file_Path;

			//创建文件;
            string dirpath = "";
            try
            {
                int index = path.LastIndexOf('/');
                if (index > 0) dirpath = outputpath + path.Substring(0, index);
            }
            catch
            {
                Debug.Log("未创建文件夹");
            }
			
			string filepath=outputpath+path;
			if(Directory.Exists(dirpath)==false&&dirpath!="")
			{
				Directory.CreateDirectory(dirpath);
			}


			if(File.Exists(filepath))
			{
				File.Delete(filepath);

                Debug.Log("deltepath="+filepath);
			}
			
			FileStream fileStream = new FileStream(filepath, FileMode.Create);
			
			byte[] tmpfiledata;
			int processSize=0;
			while(processSize<size)
			{
				if(size-processSize<1024)
				{
					tmpfiledata=new byte[size-processSize];
				}
				else
				{
					tmpfiledata=new byte[1024];
				}
				
				//读取;
				zipFilestream.Seek(startPos+processSize,SeekOrigin.Begin);
				zipFilestream.Read(tmpfiledata,0,tmpfiledata.Length);
				
				//写入;
				fileStream.Write(tmpfiledata,0,tmpfiledata.Length);
				
				processSize+=tmpfiledata.Length;
				totalprocesssize+=tmpfiledata.Length;
				
				progress.SetProgressPercent((long)totalsize,(long)totalprocesssize);
			}
            
            fileStream.Flush();
			fileStream.Close();
		}

        zipFilestream.Flush();
        zipFilestream.Close();
	}
}
