using System;
using UnityEngine;

namespace cambrian.game
{
    /// <summary>
    /// 云娃管理
    /// </summary>
    public class YunVaImManager
    {
        public static YunVaImManager yunva=new YunVaImManager();

        private string url = Application.persistentDataPath;


        private bool initsuccess;

        public void init()
        {
           
        }

        /// <summary>
        /// 登陆
        /// </summary>
        public void onLogin(long userid,int regions,Action<bool> callback)
        {
            
        }

        private void EventListenerInit()
        {
          
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void onLogout()
        {
           
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public void startRecord()
        {
            
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void stopRecord(Action<string> recordPathBack,Action<string> uploadUrlBack)
        {
            
           

        }

        public void playRecord(string url,Action<bool> rescallback)
        {
          
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void stopPlayRecord()
        {
           
        }

    }
}
