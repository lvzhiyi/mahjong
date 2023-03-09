using cambrian.common;
using cambrian.game;
using scene.game;
using cambrian.uui;
using System;
using UnityEngine;

namespace basef.lobby
{
    /// <summary>
    /// 大厅界面滚动公告面板管理器(这里是左边的公告栏，因为这里会根据内容来显示不同的图片)
    /// </summary>
    public class LobbyScrollNoticeManager
    {
        static LobbyScrollNoticeManager manager;

        private ArrayList<Sprite> sprites;

        /// <summary>
        /// 当前数量
        /// </summary>
        private int curr_count;

        private int maxcount;


        public LobbyScrollNoticeManager()
        {
            sprites=new ArrayList<Sprite>();
        }


        public static LobbyScrollNoticeManager getInstance()
        {
            if(manager==null)
                manager=new LobbyScrollNoticeManager();
            return manager;
        }

        public ArrayList<Sprite> getSprites()
        {
            return sprites;
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        public int getSaveVersion()
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.SNOTICE_SCROLL_PATH, true);
            if (data == null) return 0;
            return data.readInt();
        }

        public void saveVersion(int version)
        {
            ByteBuffer data=new ByteBuffer();
            data.writeInt(version);
            FileCachesManager.savePath(CacheLocalPath.SNOTICE_SCROLL_PATH, true,data);
        }
        /// <summary>
        /// 获取web上txt内容
        /// </summary>
        public void getWebFileTxt()
        {
            if(getSprites().count==0)
                Loader.getLoader().loadText(ServerInfos.server.getHttPServerUrl() + "/snotices/info.txt?"+MathKit.random(1,10000), -1000, callback);
        }

        public void callback(object id,object content)
        {
            if (id == null) return;
            if ((int) id == -1000)
            {
                string txt = (string) content;
                string[] strs = txt.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                if (strs.Length == 1)
                {
                    sprites.clear();
                   // UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().refresh();
                    return;
                }

                try
                {
                    if (getSaveVersion() == StringKit.parseInt(strs[strs.Length - 1]) && sprites.count!=0) return;

                    sprites.clear();
                    saveVersion(StringKit.parseInt(strs[strs.Length - 1]));

                    maxcount = strs.Length - 1;
                    for (int i = 0; i < strs.Length - 1; i++)
                    {
                        
                        Loader.getLoader()
                            .load<int, Sprite>(new Url(ServerInfos.server.getHttPServerUrl() + "/snotices/" + strs[i]+"?"+MathKit.random(1,10000)), i,
                                loadImageBack);
                    }
                }
                catch (Exception)
                {
                    Debug.Log("error!");
                }
            }
        }

        public void loadImageBack(int id,Sprite img)
        {
            if (img == null) return;

            sprites.add(img);

            curr_count++;

//            if(curr_count==maxcount)
//                UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().refresh();
        }

    }
}
