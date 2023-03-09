using basef.lobby;
using cambrian.common;
using cambrian.game;
using cambrian.uui;
using System;
using UnityEngine;

namespace basef.notice
{
    /// <summary>
    /// 获取滚动公告内容
    /// </summary>
    public class OpenScrollNoticeListPanelProcess : IProcess
    {
        public void execute()
        {
            //CommandManager.addCommand(new ScrollNoticeCommand(), onCommand);

            Loader.getLoader().loadBytes(ServerInfos.server.getScorllNotice(), scrollnoticback);
        }
        public void scrollnoticback(byte[] b)
        {
            if (b == null) return;
            ByteBuffer buff = new ByteBuffer(b);
            int status = buff.readShort();
            if (status != 0) return;
            int len = buff.readInt();
            ArrayList<ScrollNotice> list = new ArrayList<ScrollNotice>();
            for (int i = 0; i < len; i++)
            {
                ScrollNotice scrollnotice = new ScrollNotice();
                scrollnotice.bytesRead(buff);
                list.add(scrollnotice);
            }
            ShowLobbyPanel.refreshScrollNotice(list);
        }

        //public void onCommand(object obj)
        //{

        //    if (obj == null) return;
        //    ArrayList<ScrollNotice> list = (ArrayList<ScrollNotice>)obj;

        //    ArrayList<ScrollNotice> scrollnotices = new ArrayList<ScrollNotice>();
        //    for (int i = 0; i < list.count; i++)
        //    {
        //        if (list.get(i).isEffective())
        //            scrollnotices.add(list.get(i));
        //    }

        //    ShowLobbyPanel.refreshScrollNotice(scrollnotices);
        //}


        public void setVisible(bool b)
        {
            throw new NotImplementedException();
        }

        public Sprite getSprite()
        {
            throw new NotImplementedException();
        }

        public string getTitle()
        {
            throw new NotImplementedException();
        }
    }
}
