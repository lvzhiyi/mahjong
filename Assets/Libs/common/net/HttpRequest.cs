using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace cambrian.common
{
    /**
	 * 类说明：Http请求
	 * 
	 * @version 1.0
	 * @author maxw<woldits@qq.com>
	 */
    public class HttpRequester
    {
        /** 日志记录 */
        protected static Logger log = LogFactory.getLogger<HttpRequester>();

        /** http通信 */
        public static string getResponse(string url)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage(url));
            try
            {
                // Create a request for the URL. 		
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                // Display the status.
                Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                if (log.isDebug)
                    Debug.LogWarning(log.getMessage(e));
                //throw e;
                return null;
            }
        }
        /** http通信 */
        public static void getResponse(string url, Action<string> func)
        {
            string str = getResponse(url);
            func(str);
        }
        /** 下载 */
        public static void downLoad(string url, string savePath)
        {
            //eg:
            //string url = "http://192.168.1.203/";
            //string file = "cambrian.1.0.0.1.dll";
            //string savePath = Application.persistentDataPath + "/";
            //downLoad(url + file, savePath + file);
            WebClient myWebClient = new WebClient();
            if (log.isDebug)
                Debug.Log(log.getMessage("Downloading File \"{0}\" from \"{1}\" .......\n\n" + savePath + "," + File.Exists(savePath) + "," + url));
            myWebClient.DownloadFile(url, savePath);
            if (log.isDebug)
                Debug.Log(log.getMessage("Successfully Downloaded File \"{0}\" from \"{1}\"" + savePath + "," + File.Exists(savePath)));
        }
    }
}