/* * * * * * * * * * * * * * * * * * * * * * * *
* mqipai
* FileName:         Framework.Extend
* Author:           XiNan
* Version:          14.4.1
* UnityVersion:     2018.2.21f1
* Date:             2021-03-01
* Time:             14:37:25
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Extend
{
    using cambrian.game;
    using scene.game;

    public static class Extend
    {
        public static Region Init(this Region region)
        {
            if (region == null) region = new Region();
            //region.name = "";
            //region.newrulepath = "newMyRule";
            //region.noticeurl = "http://www.huangyz.net/qipai/xdcp/address/hnnotice.txt";
            //region.noticeSuffix = "/qipai/xdcp/address/hnnotice.txt";
            //region.cfgpath = "nccfg14.0.30/xdcpasset1.0.24.cfg";

            //region.openshield = false;

            //region.id = 10000;
            //region.noticePort = 80;

            //region.samplesurl = new string[] {
            //                "nccfg14.0.30/sample.award.xml",
            //                "nccfg14.0.30/sample.turnplateitem.xml",
            //                "nccfg14.0.30/sample.prop.xml",
            //                "nccfg14.0.30/sample.mission.xml",
            //                "nccfg14.0.30/sample.gamepaneldata.xml"};
            //region.rulename = new string[] { "游戏规则描述" };

            //region.gametype = new GameType[] { new GameType().Init() };
            //region.server = new ServerInfos().Init();

            //region.setDataValue("open", false);
            return region;
        }

        public static GameType Init(this GameType gameType)
        {
            if (gameType == null) gameType = new GameType();
            gameType.name = "";
            gameType.goldid = new int[] { };
            gameType.ruleid = new int[] { };
            return gameType;
        }

        public static ServerInfos Init(this ServerInfos serverInfos)
        {
            if (serverInfos == null) serverInfos = new ServerInfos();
            serverInfos.servers = new Server[] { new Server().Init() };
            return serverInfos;
        }

        public static Server Init(this Server server)
        {
            if (server == null) server = new Server();
            //server.name = "服务器名";
            //server.id = "服务器ID";
            //server.host = "192.168.2.207";
            //server.port = 11015;
            //server.openshield = false;
            //server.httpserverurl = "http://192.168.2.207/qipai/xdcp";
            //server.httpServerPort = 80;
            //server.httpserverUrlSuffix = "/qipai/xdcp";
            //server.replayurl = "http://192.168.2.207";
            //server.replayPort = 11026;
            //server.shareurl = "http://share.xdcp.wemywin.com/share";
            //server.sharegoldurl = "http://192.168.2.207:11016";
            //server.sharegoldPort = 11026;
            //server.sharegoldUrlSuffix = "/player/share?type=1";
            //server.dsurl = "http://192.168.2.207";
            //server.dsurlPort = 11016;
            return server;
        }

        public static T[] Del<T>(this T[] array, int index)
        {
            if (array == null) return array;
            var temp = array;
            array = new T[array.Length - 1];
            for (int i = 0; i < temp.Length; i++)
            {
                if (i < index)
                {
                    array[i] = temp[i];
                }
                else if (i > index)
                {
                    array[i - 1] = temp[i];
                }
            }
            temp = null;
            return array;
        }

        public static T[] AddLast<T>(this T[] array)
        {
            if (array == null) return array;
            var temp = (T[])array.Clone();
            array = new T[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
            {
                array[i] = temp[i];
            }
            array[temp.Length] = default(T);
            temp = null;
            return array;
        }

        public static T[] AddFirst<T>(this T[] array)
        {
            if (array == null) return array;
            var temp = (T[])array.Clone();
            array = new T[temp.Length + 1];
            array[0] = default(T);
            for (int i = 0; i < temp.Length; i++)
            {
                array[i + 1] = temp[i];
            }
            temp = null;
            return array;
        }

        public static T[] AddInsert<T>(this T[] array, int index)
        {
            if (array == null) return array;
            if (index < 0 || index > array.Length) return array;
            var temp = (T[])array.Clone();
            array = new T[temp.Length + 1];
            array[0] = default(T);
            for (int i = 0; i < temp.Length; i++)
            {
                if (i < index)
                    array[i] = temp[i];
                else if (i > index)
                    array[i + 1] = temp[i];
                else
                    array[index] = default(T);
            }
            temp = null;
            return array;
        }
    }
}