using cambrian.common;
using scene.game;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace scene.loading
{
    public class LoadingPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 资源下载队列
        /// </summary>
        Queue<ResData> queue;

        private string suffixName = ".upk";

        private Action action;

        private Action netWorkError;

        /// <summary>
        /// 已经下载的资源
        /// </summary>
        ArrayList<string> Downloaded = new ArrayList<string>();

        int downloadlength = 0;

        private ResData[] resdatas;

        private LoadingUpdateView updateView;

        protected override void xinit()
        {
            base.xinit();
            this.updateView = this.content.Find("load").GetComponent<LoadingUpdateView>();
            this.updateView.init();
        }

        protected override void xrefresh()
        {
            updateView.setVisible(false);
            Downloaded.clear();
        }

        public void downPack(Action netWorkError,string url)
        {
            this.netWorkError = netWorkError;
            updateView.refresh();
            updateView.setVisible(true);
           
            StartCoroutine(down(url));
         }

        IEnumerator down(string url)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                www.SendWebRequest();
                while (!www.isDone)
                {
                    updateView.setProgress(www.downloadProgress);
                    updateView.setText((int)(www.downloadProgress*100)+"%");
                    yield return 0;
                }

                if (www.isDone&&www.error == null)
                {
                    byte[] bytes = www.downloadHandler.data;
                    ModuleManager.save("mahjong.apk", bytes);
                    netWorkError();
                }
                else
                {
                    
                }
            }
        }

        /// <summary>
        /// 更新资源
        /// </summary>
        public void updateRes(ResData[] resData,Action action,Action netWorkError)
        {
            this.isStartUnComPress = false;
            this.updateView.refresh();
            this.updateView.setVisible(true);
            this.resdatas = resData;
            this.action = action;
            this.netWorkError = netWorkError;


            queue = new Queue<ResData>(resData.Length);
            for (int i = 0; i < resData.Length; i++)
                queue.add(resData[i]);

            downloadlength = resData.Length;

            ResData downLoad = queue.remove();
            this.updateView.setText("[正在努力下载中......]");
            
            StartCoroutine(send(callback, downLoad.getUrl(), downLoad.packname));
        }


        IEnumerator send(Action<byte[], string> obj, string url, string name)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                www.SendWebRequest();
                while (!www.isDone)
                {
                    this.updateView.setProgress(www.downloadProgress);
                    yield return 1;
                }

                if (www.isDone && www.error == null)
                {
                    byte[] bytes = www.downloadHandler.data;

                    obj(bytes, name);
                }
                else
                {
                    xrefresh();
                    netWorkError();
                }
            }
        }

        public void callback(byte[] bytes, string name)
        {
            if (bytes != null)
            {
                ModuleManager.save(name+suffixName, bytes);
                Downloaded.add(name);
            }

            if (!queue.isEmpty())
            {
                ResData downLoad = queue.remove();
                this.updateView.setText("[" + (downloadlength - queue.size) + "/" + downloadlength + "]");
                StartCoroutine(send(callback, downLoad.getUrl(), downLoad.packname));
            }
            else
            {
                if (Downloaded.count == downloadlength) //资源下载完成
                {
                    action();
                }
                else
                {
                    netWorkError();
                }
            }
        }


        //-----------------------解压------------------------------
        private bool isStartUnComPress = false;

        private UnCompress unCommpress;

        private int maxsum;
       
        /// <summary>
        /// 解压
        /// </summary>
        public void unCompress(ResData[] resData,Action action)
        {
            this.maxsum = resData.Length;
            this.action = action;
            xrefresh();
            this.updateView.setVisible(true);

            for (int i = 0; i < resData.Length; i++)
                this.Downloaded.add(resData[i].packname + suffixName);


            this.updateView.setText("正在解压中......");
            this.updateView.setProgress(0);
            string destPath = CacheLocalPath.AB_RESROUCE_PATH + this.Downloaded.getLast();
            startCompress(destPath);
            isStartUnComPress = true;
        }

        private void startCompress(string destPath)
        {
            unCommpress = new UnCompress(destPath, CacheLocalPath.AB_RESROUCE_PATH);
            unCommpress.unCompressThread.Start();
        }

        /// <summary>
        /// 解压完成一个
        /// </summary>
        private void oneUnCompressOver()
        {
            string path = CacheLocalPath.AB_RESROUCE_PATH + this.Downloaded.getLast();
            if (File.Exists(path))
            {
                File.Delete(path);
                this.Downloaded.removeLast();
            }

            if (this.Downloaded.count > 0)
            {
                path = CacheLocalPath.AB_RESROUCE_PATH + this.Downloaded.getLast();
                this.updateView.setText("[正在解压中......]");
                startCompress(path);
            }
            else
            {
                Invoke("allUnCommpressOver", 1f);
            }
        }

        /// <summary>
        /// 解压完成，跳转到游戏场景
        /// </summary>
        private void allUnCommpressOver()
        {
            action();
        }

        protected override void xupdate()
        {
            if ((!isStartUnComPress) || unCommpress == null) return;

            float percent = unCommpress.getPerCent();
            this.updateView.setProgress(percent);
            if (percent >= 1)
            {
                try
                {
                    unCommpress.unCompressThread.Interrupt();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
                finally
                {
                    unCommpress = null;
                    oneUnCompressOver();
                }
            }
        }
    }
}
