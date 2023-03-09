namespace Framework.ToolWindow
{
    using cambrian.common;
    using cambrian.game;
    using CSObjectWrapEditor;
    using Framework.Extend;
    using scene.game;
    using scene.loading;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using UnityEditor;
    using UnityEngine;
    using XLua;
    using Tool = EditorTool;

    public enum BuildABTarget { Android, iOS, PC, }

    public class PackageManagerEditor : BaseWindowEditor
    {
        private const string pack_out_android = @"/ab/Package Out/Android/";
        private const string pack_out_ios = @"/ab/Package Out/IOS/";

        public PackageManagerEditor()
        {
            titleContent = new GUIContent("Package Manager");
            minSize = new Vector2(600, 500);
            maxSize = new Vector2(1600, 900);
            packageList = new ArrayList<string>();
            server_requst_URL = new ArrayList<string>();
            server_requst_URL_key = new ArrayList<string>();
            filelable = new Dictionary<string, string>();
            absoureflodelist = new ArrayList<int>();
            absoureflodelistname = new ArrayList<string>();
            PrefabArrayList = new ArrayList<PrefabPoint>();
            regions = new ArrayList<Region>();

            oneConfigView = new bool[] { true, false, false, false, false };
        }

        private static new EditorWindow window;
        [MenuItem("Tool Windows/Package Manager Window %#v")]
        public static void Open()
        {
            if (window != null)
            {
                window.Close();
                window = null;
            }
            else
            {
                window = window ?? GetWindow<PackageManagerEditor>(true, "Package Manager", true);//创建窗口
                window.wantsMouseMove = true;
                window.Show(true);//展示   
            }
        }

        private ArrayList<string> server_requst_URL;
        private ArrayList<string> server_requst_URL_key;
        private Dictionary<string, string> filelable;
        private ArrayList<string> packageList;
        private ArrayList<string> absoureflodelistname;
        private ArrayList<int> absoureflodelist;
        private BuildABTarget buildTarget;

        private bool openFilesAttribute = true;

        /// <summary> 一级开关 </summary>
        /// 1发布打包
        /// 2发布资源
        /// 3配置修改
        /// 4预制件配置
        /// 5地区配置
        private bool[] oneConfigView;
        private void hdieAllView()
        {
            for (int i = 0; i < oneConfigView.Length; i++)
                oneConfigView[i] = false;
        }

        private const int OneBtnWidth = 80;

        protected override void onGUI()
        {
            Tool.BE.Vertical(() =>
            {
                Tool.EA.Space();
                Tool.BE.Horizontal(() =>
                {
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { Generator.GenAll(); }, "Xlua 生成", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { Hotfix.HotfixInject(); }, "Xlua 注入", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { Generator.ClearAll(); }, "Xlua 清空", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                });
                Tool.EA.Space();
                Tool.BE.Horizontal(() =>
                {
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { hdieAllView(); oneConfigView[0] = true; }, " 发布打包 ", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { hdieAllView(); oneConfigView[1] = true; }, " 发布资源 ", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { hdieAllView(); oneConfigView[2] = true; }, " 配置修改 ", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { hdieAllView(); oneConfigView[3] = true; }, "预制配置表", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                    Tool.AC.Button(() => { hdieAllView(); oneConfigView[4] = true; }, "地区配置表", GStyleTool.button, GUILayout.Width(OneBtnWidth));
                    Tool.EA.Separator();
                });
                Tool.EA.Space();
            }, GStyleTool.content);
            if (oneConfigView[3]) PrefabConfigChangeTop();        //预制件修改 顶部

            vector = Tool.BE.ScrollView(() =>
            {
                if (oneConfigView[0]) AssetBundleBuild();        //资源打包
                if (oneConfigView[1]) PackageList();
                if (oneConfigView[2]) AddersConfigChange();
                if (oneConfigView[3]) PrefabConfigChange();
                if (oneConfigView[4]) RegionsConfigChange();
            }, vector, GUILayout.ExpandWidth(true));
        }

        #region 地区配置表

        private int searchindexRegions;
        private ArrayList<Region> regions;

        private void RegionsConfigChange()
        {
            Tool.BE.Horizontal(() =>
            {
                if (regions.count == 0)
                {
                    Tool.AC.Label("<color=#ffff00>文件夹路径</color>", GStyleTool.GetName("HeaderLabel"));
                    searchindexRegions = Tool.AC.Popup(searchindexRegions, prefabcfg.toArray(), GStyleTool.GetName("Popup"));
                }
                if (regions.count > 0)
                {
                    if (Tool.AC.Button("保存", GStyleTool.button))
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", prefabcfg.get(searchindexRegions).text, "/regions.json");
                        var temp = new Regions();
                        temp.region = regions.toArray();
                        SaveTextToJson(path, temp);
                    }
                    if (Tool.AC.Button("新增", GStyleTool.button)) regions.addFirst(new Region[] { new Region().Init() });
                    if (Tool.AC.Button("打开", GStyleTool.button))
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", prefabcfg.get(searchindexRegions).text, "/regions.json");
                        if (File.Exists(path)) System.Diagnostics.Process.Start(path);
                    }
                }
                if (Tool.AC.Button(regions.count > 0 ? "关闭" : "打开", GStyleTool.button))
                {
                    if (regions.count <= 0)
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", prefabcfg.get(searchindexRegions).text, "/regions.json");
                        var temp = ReadTextToJson<Regions>(path);
                        if (temp != null && temp.region != null) regions = new ArrayList<Region>(temp.region);
                        for (int i = 0; i < regions.count; i++) regions.get(i).setDataValue("open", false);
                    }
                    else regions.clear();
                }
            }, GStyleTool.UIContent, GUILayout.Height(25));

            Region region = null;
            for (int i = 0; i < regions.count; i++)
            {
                region = RegionsView(regions.get(i));
                if (region != null) regions.set(region, i);
                else regions.removeAt(i);
            }
        }

        private Region RegionsView(Region region)
        {
            //Tool.BE.Vertical(() =>
            //{
            //    Tool.AC.Label(string.Concat("<color=#FF0000>", region.name, "</color>"), GStyleTool.label);
            //    Tool.BE.Horizontal(() =>
            //    {
            //        Tool.AC.Label("<color=#6495ED>ID</color>", GStyleTool.GetName("HeaderLabel"), GUILayout.Width(180));
            //        region.id = Tool.AC.FieldInt(region.id, GStyleTool.textArea, GUILayout.Height(20));
            //        if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60))) region = null;
            //    });
            //    if (region == null) return;
            //    Tool.BE.Horizontal(() =>
            //    {
            //        Tool.AC.Label("<color=#6495ED>地区名</color>", GStyleTool.GetName("HeaderLabel"), GUILayout.Width(180));
            //        region.name = Tool.AC.TextArea(region.name, GStyleTool.textArea, GUILayout.Height(20));
            //        if (Tool.AC.Button((bool)region.getDataValue("open") ? "关闭" : "打开", GStyleTool.button, GUILayout.Width(60)))
            //            region.setDataValue("open", !(bool)region.getDataValue("open"));
            //    });
            //}, GStyleTool.UIContent);
            if (region == null || !(bool)region.getDataValue("open")) return region;
            Tool.BE.Vertical(() =>
            {
                //Tool.BE.Horizontal(() =>
                //{
                //    Tool.AC.LabelPrefix("配置表路径");
                //    region.cfgpath = Tool.AC.TextArea(region.cfgpath, GStyleTool.textArea, GUILayout.Height(20));
                //});
                //Tool.BE.Horizontal(() =>
                //{
                //    Tool.AC.LabelPrefix("规则路径");
                //    region.newrulepath = Tool.AC.TextArea(region.newrulepath, GStyleTool.textArea, GUILayout.Height(20));
                //});
                //Tool.BE.Horizontal(() =>
                //{
                //    Tool.AC.LabelPrefix("超级盾");
                //    region.openshield = Tool.AC.Toggle(region.openshield, GUILayout.Height(20));
                //});
                //Tool.BE.Horizontal(() =>
                //{
                //    Tool.AC.LabelPrefix("维护公告地址");
                //    region.noticeurl = Tool.AC.TextArea(region.noticeurl, GStyleTool.textArea, GUILayout.Height(20));
                //});
                //Tool.BE.Horizontal(() =>
                //{
                //    Tool.AC.LabelPrefix("维护公告端口");
                //    region.noticePort = Tool.AC.FieldInt(region.noticePort, GStyleTool.textArea, GUILayout.Height(20));
                //});
                //Tool.BE.Horizontal(() =>
                //{
                //    Tool.AC.LabelPrefix("维护公告后缀");
                //    region.noticeSuffix = Tool.AC.TextArea(region.noticeSuffix, GStyleTool.textArea, GUILayout.Height(20));
                //});
                //Tool.BE.Vertical(() =>
                //{
                //    Tool.BE.Horizontal(() =>
                //    {
                //        Tool.AC.Label("<color=#fff000>规则名</color>", GStyleTool.GetName("HeaderLabel"));
                //        if (Tool.AC.Button("新增", GStyleTool.button, GUILayout.Width(60)))
                //            region.rulename = region.rulename.AddFirst();
                //    });

                //    for (int i = 0; i < region.rulename.Length; i++)
                //    {
                //        Tool.BE.Horizontal(() =>
                //        {
                //            region.rulename[i] = Tool.AC.TextArea(region.rulename[i], GStyleTool.textArea, GUILayout.Height(20));
                //            if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                //                region.rulename = region.rulename.Del(i);
                //        });
                //    }
                //}, GStyleTool.UIContent);
                Tool.BE.Vertical(() =>
                {
                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.Label("<color=#fff000>配置表地址</color>", GStyleTool.GetName("HeaderLabel"));
                        if (Tool.AC.Button("新增", GStyleTool.button, GUILayout.Width(60)))
                            region.samplesurl = region.samplesurl.AddFirst();
                    });

                    for (int i = 0; i < region.samplesurl.Length; i++)
                    {
                        Tool.BE.Horizontal(() =>
                        {
                            region.samplesurl[i] = Tool.AC.TextArea(region.samplesurl[i], GStyleTool.textArea, GUILayout.Height(20));
                            if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                                region.samplesurl = region.samplesurl.Del(i);
                        });
                    }
                }, GStyleTool.UIContent);

                Tool.BE.Vertical(() =>
                {
                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.Label("<color=#fff000>游戏种类</color>", GStyleTool.GetName("HeaderLabel"));
                        if (Tool.AC.Button("新增", GStyleTool.button, GUILayout.Width(60)))
                        {
                            region.gametype = region.gametype.AddFirst();
                            region.gametype[0] = region.gametype[0].Init();
                        }
                    });
                    Tool.EA.Separator();
                    for (int i = 0; i < region.gametype.Length; i++)
                    {
                        Tool.BE.Vertical(() =>
                        {
                            region.gametype[i] = GameTypeView(region.gametype[i]);
                        }, GStyleTool.GetName("ChannelStripBg"));
                        if (region.gametype[i] == null) region.gametype = region.gametype.Del(i);
                        Tool.EA.Separator();
                    }
                }, GStyleTool.UIContent);

                Tool.BE.Vertical(() =>
                {
                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.Label("<color=#fff000>服务器组</color>", GStyleTool.GetName("HeaderLabel"));
                        if (Tool.AC.Button("新增", GStyleTool.button, GUILayout.Width(60)))
                        {
                            region.server.servers = region.server.servers.AddFirst();
                            region.server.servers[0] = region.server.servers[0].Init();
                        }
                    });
                    Tool.EA.Separator();
                    for (int i = 0; i < region.server.servers.Length; i++)
                    {
                        Tool.BE.Vertical(() =>
                        {
                            region.server.servers[i] = ServerView(region.server.servers[i]);
                        }, GStyleTool.GetName("ChannelStripBg"));
                        if (region.server.servers[i] == null) region.server.servers = region.server.servers.Del(i);
                        Tool.EA.Separator();
                    }
                }, GStyleTool.UIContent);

            }, GStyleTool.UIContent);
            return region;
        }

        private GameType GameTypeView(GameType type)
        {
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.LabelPrefix("游戏类名", GStyleTool.title);
                type.name = Tool.AC.TextArea(type.name, GStyleTool.textArea, GUILayout.Height(20));
                if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                    type = null;
                if (Tool.AC.Button(type.open ? "关闭" : "打开", GStyleTool.button, GUILayout.Width(60)))
                    type.open = !type.open;
            });
            if (type == null || !type.open) return type;
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.Label("规则ID");
                if (Tool.AC.Button("新增", GStyleTool.button, GUILayout.Width(60)))
                    type.ruleid = type.ruleid.AddFirst();
            });
            for (int i = 0; i < type.ruleid.Length; i++)
            {
                Tool.BE.Horizontal(() =>
                {
                    type.ruleid[i] = Tool.AC.FieldInt(type.ruleid[i], GStyleTool.textArea, GUILayout.Height(20));
                    if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                        type.ruleid = type.ruleid.Del(i);
                });
            }
            return type;
        }

        private Server ServerView(Server server)
        {
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.LabelPrefix("Name", GStyleTool.title);
                server.name = Tool.AC.TextArea(server.name, GStyleTool.textArea, GUILayout.Height(20));
                if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60))) server = null;
                if (Tool.AC.Button(server.open ? "关闭" : "打开", GStyleTool.button, GUILayout.Width(60)))
                    server.open = !server.open;
            });
            if (server == null || !server.open) return server;
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.LabelPrefix("ID", GStyleTool.title);
                server.id = Tool.AC.TextArea(server.id, GStyleTool.textArea, GUILayout.Height(20));
            });
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.LabelPrefix("Host", GStyleTool.title);
                server.host = Tool.AC.TextArea(server.host, GStyleTool.textArea, GUILayout.Height(20));
            });
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.LabelPrefix("Port", GStyleTool.title);
                server.port = Tool.AC.FieldInt(server.port, GStyleTool.textArea, GUILayout.Height(20));
            });
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("超级盾", GStyleTool.title);
            //    server.openshield = Tool.AC.Toggle(server.openshield, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Http Server Url", GStyleTool.title);
            //    server.httpserverurl = Tool.AC.TextArea(server.httpserverurl, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Http Server Port", GStyleTool.title);
            //    server.httpServerPort = Tool.AC.FieldInt(server.httpServerPort, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Http Server Url Suffix", GStyleTool.title);
            //    server.httpserverUrlSuffix = Tool.AC.TextArea(server.httpserverUrlSuffix, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Replay Url", GStyleTool.title);
            //    server.replayurl = Tool.AC.TextArea(server.replayurl, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Replay Port", GStyleTool.title);
            //    server.replayPort = Tool.AC.FieldInt(server.replayPort, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Share Url", GStyleTool.title);
            //    server.shareurl = Tool.AC.TextArea(server.shareurl, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Share Gold Url", GStyleTool.title);
            //    server.sharegoldurl = Tool.AC.TextArea(server.sharegoldurl, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Share Gold Port", GStyleTool.title);
            //    server.sharegoldPort = Tool.AC.FieldInt(server.sharegoldPort, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("Share Gold Url Suffix", GStyleTool.title);
            //    server.sharegoldUrlSuffix = Tool.AC.TextArea(server.sharegoldUrlSuffix, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("DS Url", GStyleTool.title);
            //    server.dsurl = Tool.AC.TextArea(server.dsurl, GStyleTool.textArea, GUILayout.Height(20));
            //});
            //Tool.BE.Horizontal(() =>
            //{
            //    Tool.AC.LabelPrefix("DS Url Port", GStyleTool.title);
            //    server.dsurlPort = Tool.AC.FieldInt(server.dsurlPort, GStyleTool.textArea, GUILayout.Height(20));
            //});
            return server;
        }

        #endregion

        #region 预制件配置表

        private string searchPrefab = "";
        private int searchindexPrefab;
        private bool openPrefabConfig;
        private ArrayList<PrefabPoint> PrefabArrayList;

        private void PrefabConfigChangeTop()
        {
            Tool.BE.Horizontal(() =>
            {
                if (PrefabArrayList.count == 0)
                {
                    Tool.AC.LabelPrefix("文件夹路径");
                    searchindexPrefab = Tool.AC.Popup(searchindexPrefab, prefabcfg.toArray(), GStyleTool.GetName("Popup"));
                }
                if (Tool.AC.Button(openPrefabConfig ? "  关闭  " : "  打开  ", GStyleTool.button))
                {
                    openPrefabConfig = !openPrefabConfig;
                    if (openPrefabConfig)
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", prefabcfg.get(searchindexPrefab).text, "/prefabpoint.json");
                        var temp = ReadTextToJson<PrefabPointData>(path);
                        if (temp != null && temp.data != null) PrefabArrayList = new ArrayList<PrefabPoint>(temp.data);
                    }
                    else PrefabArrayList.clear();
                }
                if (Tool.AC.Button("打开文件", GStyleTool.button))
                {
                    var path = string.Concat(Application.persistentDataPath, "/ab/", prefabcfg.get(searchindexPrefab).text, "/prefabpoint.json");
                    if (File.Exists(path)) System.Diagnostics.Process.Start(path);
                }

            }, GStyleTool.UIContent, GUILayout.Height(25));
            if (PrefabArrayList.count != 0)
            {
                Tool.EA.Separator();
                Tool.BE.Horizontal(() =>
                {
                    GUILayout.Space(30);
                    searchPrefab = Tool.AC.TextArea(searchPrefab, GStyleTool.GetName("SearchTextField"), GUILayout.Width(position.width - 180));
                    Tool.AC.Label("", GStyleTool.GetName("SearchCancelButtonEmpty"));
                    if (Tool.AC.Button("新增", GStyleTool.button, GUILayout.Width(60)))
                    {
                        PrefabArrayList.addFirst(new PrefabPoint[] { new PrefabPoint() });
                        PrefabArrayList.get(0).abname = "";
                        PrefabArrayList.get(0).name = "";
                    }
                    if (Tool.AC.Button("保存", GStyleTool.button, GUILayout.Width(60)))
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", prefabcfg.get(searchindexPrefab).text, "/prefabpoint.json");
                        var temp = new PrefabPointData();
                        temp.data = PrefabArrayList.toArray();
                        SaveTextToJson(path, temp);
                    }
                });
                Tool.EA.Separator();
            }
        }

        private void PrefabConfigChange()
        {
            if (PrefabArrayList.count != 0)
            {
                Tool.BE.Vertical(() =>
                {
                again:
                    PrefabPoint temp = null;
                    for (int i = 0; i < PrefabArrayList.count; i++)
                    {
                        if (PrefabArrayList.get(i).name.ToLower().Contains(searchPrefab.ToLower()))
                        {
                            Tool.BE.Vertical(() => { temp = PrefabPointView(PrefabArrayList.get(i), i); }, GStyleTool.content);
                            if (temp == null)
                            {
                                PrefabArrayList.removeAt(i);
                                goto again;
                            }
                            else PrefabArrayList.set(temp, i);
                        }
                    }
                }, GStyleTool.content);
            }
        }

        private PrefabPoint PrefabPointView(PrefabPoint point, int i)
        {
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.Label(string.Concat("<color=#FFFF00> NO.", i + 1, "</color>"), GStyleTool.helpBox);
                if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60))) point = null;
            });
            if (point != null)
            {
                Tool.BE.Horizontal(() =>
                {
                    Tool.AC.LabelPrefix("Name");
                    point.name = Tool.AC.TextArea(point.name, GStyleTool.textArea, GUILayout.Height(20));
                    Tool.EA.ButtonCopyText("复制", -1, 60, point.name);
                });
                Tool.BE.Horizontal(() =>
                {
                    Tool.AC.LabelPrefix("Package");
                    point.abname = Tool.AC.TextArea(point.abname, GStyleTool.textArea, GUILayout.Height(20));
                    Tool.EA.ButtonCopyText("复制", -1, 60, point.abname);
                });
            }
            return point;
        }

        #endregion

        #region 配置表

        private GameVersion gameVersion;
        private ResUpdateList resUpdateList;
        private string resUpdatePath;
        private LoadingRoot.UrlAddress address = LoadingRoot.UrlAddress.Develop;
        private void AddersConfigChange()
        {
            Tool.BE.Vertical(() =>
            {
                Tool.AC.Label("Version", GStyleTool.title);
                Tool.EA.Separator();
                Tool.BE.Horizontal(() =>
                {
                    address = (LoadingRoot.UrlAddress)EditorGUILayout.EnumPopup("资源配置路径", address, GStyleTool.GetName("Popup"));
                    if (gameVersion == null)
                    {
                        if (Tool.AC.Button("读取内容", GStyleTool.button, GUILayout.Width(60)))
                        {
                            gameVersion = ReadTextToJson<GameVersion>(GetVersionPath(EnumHelper.GetDescription(address)));
                        }
                    }

                    if (gameVersion != null)
                    {
                        if (Tool.AC.Button("刷新", GStyleTool.button, GUILayout.Width(60)))
                        {
                            gameVersion = ReadTextToJson<GameVersion>(GetVersionPath(EnumHelper.GetDescription(address)));
                        }
                        if (Tool.AC.Button("保存", GStyleTool.button, GUILayout.Width(60)))
                        {
                            SaveTextToJson(GetVersionPath(EnumHelper.GetDescription(address)), gameVersion);
                        }

                        if (Tool.AC.Button("关闭", GStyleTool.button, GUILayout.Width(60))) gameVersion = null;
                    }
                });
            }, GStyleTool.content);

            if (gameVersion != null)
            {
                Tool.BE.Vertical(() =>
                {
                    Tool.BE.Vertical(() =>
                    {
                        Tool.AC.Label("Version", GStyleTool.title);
                        Tool.BE.Horizontal(() =>
                        {
                            //gameVersion.downInfoPort = Tool.AC.FieldInt("下载端口", gameVersion.downInfoPort, GStyleTool.textArea, GUILayout.Height(20));
                            //gameVersion.openshield = Tool.AC.Toggle("超级盾", gameVersion.openshield);
                        });
                        Tool.BE.Horizontal(() =>
                        {
                            Tool.AC.LabelPrefix("下载信息", GStyleTool.title);
                            gameVersion.downInfoUrl = Tool.AC.TextArea(gameVersion.downInfoUrl, GStyleTool.textArea, GUILayout.Height(20));
                        });
                        Tool.BE.Horizontal(() =>
                        {
                            //Tool.AC.LabelPrefix("下载公告", GStyleTool.title);
                            //gameVersion.downInfoUrlSuffix = Tool.AC.FieldText(gameVersion.downInfoUrlSuffix, GStyleTool.textArea, GUILayout.Height(20));
                        });
                    }, GStyleTool.content);

                    gameVersion.android = ShowInfoPVersion("安卓", gameVersion.android);
                    gameVersion.ios = ShowInfoPVersion("苹果", gameVersion.ios);
                }, GStyleTool.content);

                if (resUpdateList != null) resUpdateList = ShowInfoPVersion("资源配置文件", resUpdateList);
            }
        }

        private PlatformVersion ShowInfoPVersion(string title, PlatformVersion platform)
        {
            if (platform != null)
            {
                Tool.BE.Vertical(() =>
                {
                    Tool.AC.Label(title, GStyleTool.title);

                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.LabelPrefix("版本号", GStyleTool.title);
                        platform.version = Tool.AC.TextArea(platform.version, GStyleTool.textArea, GUILayout.Height(20));
                    });
                    Tool.BE.Horizontal(() =>
                    {
                        //platform.urlPort = Tool.AC.FieldInt("端口", platform.urlPort, GStyleTool.textArea, GUILayout.Height(20));
                        //platform.openshield = Tool.AC.Toggle("超级盾", platform.openshield);
                    });
                    Tool.BE.Horizontal(() =>
                    {
                        //Tool.AC.LabelPrefix("分享地址", GStyleTool.title);
                        //platform.urlSuffix = Tool.AC.TextArea(platform.urlSuffix, GStyleTool.textArea, GUILayout.Height(20));
                    });
                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.LabelPrefix("整包下载地址", GStyleTool.title);
                        platform.downurl = Tool.AC.TextArea(platform.downurl, GStyleTool.textArea, GUILayout.Height(20));
                    });
                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.LabelPrefix("配置文件路径", GStyleTool.title);
                        platform.cfgurl = Tool.AC.TextArea(platform.cfgurl, GStyleTool.textArea, GUILayout.Height(20));
                    });
                    Tool.BE.Horizontal(() =>
                    {
                        Tool.AC.LabelPrefix("更新地址", GStyleTool.title);
                        platform.url = Tool.AC.TextArea(platform.url, GStyleTool.textArea, GUILayout.Height(20));
                        if (Tool.AC.Button("修改", GStyleTool.button, GUILayout.Width(60), GUILayout.Height(20)))
                        {
                            resUpdatePath = GetVersionPath(platform.url);
                            resUpdateList = ReadTextToJson<ResUpdateList>(resUpdatePath);
                        }
                    });
                }, GStyleTool.content);
            }
            return platform;
        }

        private ResUpdateList ShowInfoPVersion(string title, ResUpdateList res)
        {
            if (res != null)
            {
                Tool.BE.Vertical(() =>
                {
                    Tool.AC.Label(title, GStyleTool.title);
                    Tool.BE.Horizontal(() =>
                    {
                        if (Tool.AC.Button(" 新增Del ", GStyleTool.button))
                        {
                            var temp = new string[res.delteresdata.Length + 1];
                            for (int i = 0; i < res.delteresdata.Length; i++)
                                temp[i] = res.delteresdata[i];
                            temp[temp.Length - 1] = "";
                            res.delteresdata = temp; temp = null;
                        }
                        if (Tool.AC.Button("新增AB包", GStyleTool.button))
                        {
                            var temp = new ResData[res.resdata.Length + 1];
                            temp[0] = new ResData();
                            for (int i = 0; i < res.resdata.Length; i++)
                                temp[i + 1] = res.resdata[i];
                            res.resdata = temp; temp = null;
                        }
                        if (Tool.AC.Button(" 保存文件 ", GStyleTool.button))
                        {
                            SaveTextToJson(GetVersionPath(resUpdatePath), res);
                            resUpdatePath = null;
                        }
                        if (Tool.AC.Button(" 关闭文件 ", GStyleTool.button)) res = null;
                    });

                    if (res != null)
                    {
                        if (res.delteresdata.Length != 0)
                        {
                            Tool.BE.Vertical(() =>
                            {
                            delteresdata:
                                bool del = false;
                                for (int i = 0; i < res.delteresdata.Length; i++)
                                {
                                    Tool.BE.Horizontal(() =>
                                    {
                                        Tool.AC.LabelPrefix(string.Concat("NO.", i + 1), GStyleTool.title);
                                        res.delteresdata[i] = Tool.AC.TextArea(res.delteresdata[i], GStyleTool.textArea, GUILayout.Height(20));
                                        if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60), GUILayout.Height(20)))
                                        {
                                            var temp = res.delteresdata;
                                            res.delteresdata = new string[res.delteresdata.Length - 1];
                                            for (int j = 0; j < temp.Length; j++)
                                            {
                                                if (j < i) res.delteresdata[j] = temp[j];
                                                if (j > i) res.delteresdata[j - 1] = temp[j];
                                            }
                                            temp = null; del = true;
                                        }
                                    });
                                    if (del) goto delteresdata;
                                }
                            }, GStyleTool.content);
                        }

                        if (res.resdata.Length != 0)
                        {
                            for (int i = 0; i < res.resdata.Length; i++)
                            {
                                res.resdata[i] = ShowInfoPVersion(res.resdata[i]);
                                if (res.resdata[i] == null)
                                {
                                    var temp = res.resdata;
                                    res.resdata = new ResData[res.resdata.Length - 1];
                                    for (int j = 0; j < temp.Length; j++)
                                    {
                                        if (j < i) res.resdata[j] = temp[j];
                                        if (j > i) res.resdata[j - 1] = temp[j];
                                    }
                                    temp = null; return;
                                }
                            }
                        }
                    }
                }, GStyleTool.content);
                return res;
            }
            else return null;
        }

        private ResData ShowInfoPVersion(ResData data)
        {
            if (data != null)
            {
                //Tool.BE.Vertical(() =>
                //{
                //    Tool.AC.Label(data.packname, GStyleTool.title);
                //    Tool.BE.Horizontal(() =>
                //    {
                //        Tool.AC.LabelPrefix("包名", GStyleTool.title);
                //        data.packname = Tool.AC.TextArea(data.packname, GStyleTool.textArea);
                //    }, GUILayout.Height(20));
                //    Tool.BE.Horizontal(() =>
                //    {
                //        Tool.AC.LabelPrefix("分享包路径", GStyleTool.title);
                //        data.urlSuffix = Tool.AC.TextArea(data.urlSuffix, GStyleTool.textArea);
                //    }, GUILayout.Height(20));
                //    Tool.BE.Horizontal(() =>
                //    {
                //        Tool.AC.LabelPrefix("资源包路径", GStyleTool.title);
                //        data.url = Tool.AC.TextArea(data.url, GStyleTool.textArea);
                //    }, GUILayout.Height(30));
                //    Tool.BE.Horizontal(() =>
                //    {
                //        data.urlPort = Tool.AC.FieldInt("端口", data.urlPort, GStyleTool.textArea, GUILayout.Height(20));
                //        data.openshield = Tool.AC.Toggle("超级盾", data.openshield);
                //        if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60))) data = null;
                //    });
                //}, GStyleTool.content);
            }
            return data;
        }

        #endregion

        #region 发布打包

        private BuildABTarget BundlePlatform;
        private string androidBundlePath, iosBundlePath, pcBundlePath;
        private bool isShowABResFloder = true;
        private string UpdateAssetsPath = "UpdateAssets";
        private string[] keys;

        private void AssetBundleBuild()
        {
            /* ----------------------------------------------------- 设置属性*/
            Tool.BE.Vertical(() =>
            {
                Tool.EA.Separator();
                Tool.AC.Label("设置属性", GStyleTool.title);
                PlayerSettings.companyName = Tool.AC.FieldText("项目名称", PlayerSettings.companyName, GStyleTool.GetName("LargeTextField"));
                PlayerSettings.productName = Tool.AC.FieldText("本地名", PlayerSettings.productName, GStyleTool.GetName("LargeTextField"));
                PlayerSettings.bundleVersion = Tool.AC.FieldText("版本号", PlayerSettings.bundleVersion, GStyleTool.GetName("LargeTextField"));
            }, GStyleTool.content);
            /* ----------------------------------------------------- 资源打包*/
            Tool.BE.Vertical(() =>
            {
                Tool.EA.Separator();
                Tool.AC.Label("资源打包", GStyleTool.title);
                Tool.BE.Horizontal(() =>
                {
                    BundlePlatform = (BuildABTarget)EditorGUILayout.EnumPopup("打包平台", BundlePlatform, GStyleTool.GetName("Popup"));
                    if (Tool.AC.Button("打包", GStyleTool.button, GUILayout.Width(60)))
                    {
                        switch (BundlePlatform)
                        {
                            case BuildABTarget.Android: BuildAllAssetBundlesAndroid(androidBundlePath); break;
                            case BuildABTarget.iOS: BuildAllAssetBundlesIOS(iosBundlePath); break;
                            case BuildABTarget.PC: BuildAllAssetBundlesPC(pcBundlePath); break;
                        }
                    }
                });
                Tool.BE.Horizontal(() =>
                {
                    switch (BundlePlatform)
                    {
                        case BuildABTarget.Android:
                            androidBundlePath = Tool.AC.FieldText("安卓文件夹", androidBundlePath, GStyleTool.GetName("LargeTextField"));
                            break;
                        case BuildABTarget.iOS:
                            iosBundlePath = Tool.AC.FieldText("苹果文件夹", iosBundlePath, GStyleTool.GetName("LargeTextField"));
                            break;
                        case BuildABTarget.PC:
                            pcBundlePath = Tool.AC.FieldText("PC文件夹", pcBundlePath, GStyleTool.GetName("LargeTextField"));
                            break;
                    }
                });
            }, GStyleTool.content);
            /* ----------------------------------------------------- 设置文件标签属性*/
            Tool.BE.Vertical(() =>
            {
                Tool.AC.Label("设置文件标签属性", GStyleTool.title);
                Tool.BE.Horizontal(() =>
                {
                    UpdateAssetsPath = Tool.AC.FieldText("更新资源路径", UpdateAssetsPath, GStyleTool.GetName("LargeTextField"));
                });
                Tool.EA.Separator();
                Tool.BE.Horizontal(() =>
                {
                    Tool.EA.Separator();
                    if (Tool.AC.Button(" 设置全部 ", GStyleTool.button))
                    {
                        foreach (var item in filelable)
                            SetVersionDirAssetName(string.Concat(UpdateAssetsPath, '/', item.Key), item.Value);
                        EditorUtility.DisplayDialog("提示", "设置成功", "确定");
                    }
                    Tool.EA.Separator();
                    if (Tool.AC.Button(" 清空标签 ", GStyleTool.button))
                    {
                        AssetDatabase.RemoveUnusedAssetBundleNames();
                        AssetDatabase.Refresh();
                    }
                    Tool.EA.Separator();
                    if (Tool.AC.Button(openFilesAttribute ? " 显示全部 " : " 隐藏显示 ", GStyleTool.button))
                    {
                        openFilesAttribute = !openFilesAttribute;
                    }
                    Tool.EA.Separator();
                    if (Tool.AC.Button(" 重新加载 ", GStyleTool.button))
                    {
                        var UpdateAssets = new DirectoryInfo(string.Concat(Application.dataPath, '/', UpdateAssetsPath));
                        var temps = new Dictionary<string, string>();
                        foreach (var item in UpdateAssets.GetDirectories())
                        {
                            if (filelable.ContainsKey(item.Name))
                                temps.Add(item.Name, filelable[item.Name]);
                            else temps.Add(item.Name, "");
                        }
                        filelable = new Dictionary<string, string>(temps);
                        keys = new string[filelable.Count];
                        filelable.Keys.CopyTo(keys, 0);
                        temps = null;
                        UpdateAssets = null;
                        EditorUtility.DisplayDialog("提示", "重新加载成功", "确定");
                    }
                    Tool.EA.Separator();
                });
            }, GStyleTool.content);
            /* ----------------------------------------------------- 打开设置标签*/
            if (openFilesAttribute)
            {
                Tool.BE.Vertical(() =>
                {
                    foreach (var key in keys)
                    {
                        Tool.BE.Horizontal(() =>
                        {
                            Tool.AC.Label(string.Concat("<color=#fff000>", key, "</color>"), GStyleTool.helpBox, GUILayout.Width(120));
                            Tool.AC.Label("标签", GUILayout.Width(30)); filelable[key] = Tool.AC.FieldText(filelable[key]);
                            if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                            {
                                filelable.Remove(key);
                                keys = new string[filelable.Count];
                                filelable.Keys.CopyTo(keys, 0);
                            }
                            if (Tool.AC.Button("设置", GStyleTool.button, GUILayout.Width(60)))
                            {
                                SetVersionDirAssetName(string.Concat(UpdateAssetsPath, '/', key), filelable[key]);
                                EditorUtility.DisplayDialog("提示", string.Concat("设置成功: ", key), "确定");
                            }
                        }, GUILayout.Height(20));
                    }
                }, GStyleTool.content);
            }
            /* ----------------------------------------------------- 快捷移动AB文件*/
            Tool.BE.Vertical(() =>
            {
                Tool.EA.Separator();
                Tool.BE.Horizontal(() =>
                {
                    if (Tool.AC.Button("删除全部资源", GStyleTool.button))
                    {
                        string path = "";
                        switch (BundlePlatform)
                        {
                            case BuildABTarget.Android:
                                path = Path.Combine(string.Concat(Application.persistentDataPath, "/ab/"), androidBundlePath);
                                break;
                            case BuildABTarget.iOS:
                                path = Path.Combine(string.Concat(Application.persistentDataPath, "/ab/"), iosBundlePath);
                                break;
                            case BuildABTarget.PC:
                                path = Path.Combine(string.Concat(Application.persistentDataPath, "/ab/"), pcBundlePath);
                                break;
                        }
                        EditorKit.FileDelete(path);
                        EditorUtility.DisplayDialog("提示", "删除成功", "确定");
                    }
                    if (Tool.AC.Button(isShowABResFloder ? "关闭源文件夹" : "显示源文件夹", GStyleTool.button)) isShowABResFloder = !isShowABResFloder;
                });
                if (isShowABResFloder)
                {
                    string path = "";
                    switch (BundlePlatform)
                    {
                        case BuildABTarget.Android:
                            path = Path.Combine(string.Concat(Application.persistentDataPath, "/ab/"), androidBundlePath);
                            break;
                        case BuildABTarget.iOS:
                            path = Path.Combine(string.Concat(Application.persistentDataPath, "/ab/"), iosBundlePath);
                            break;
                        case BuildABTarget.PC:
                            path = Path.Combine(string.Concat(Application.persistentDataPath, "/ab/"), pcBundlePath);
                            break;
                    }
                    if (!Directory.Exists(path)) return;
                    var num = 0;
                    foreach (var item in new DirectoryInfo(path).GetFiles())
                    {
                        if (item.Extension == ".manifest") continue;
                        Tool.BE.Horizontal(() =>
                        {
                            if (absoureflodelist.count <= num) absoureflodelist.add(0);
                            if (absoureflodelistname.count <= num) absoureflodelistname.add(item.Name);

                            if (absoureflodelistname.get(num) != item.Name)
                            {
                                absoureflodelistname.set(item.Name, num);
                                absoureflodelist.set(0, num);
                            }
                            Tool.AC.Label(string.Concat("NO.", num + 1), GUILayout.Width(50));
                            Tool.AC.Label(item.Name);

                            absoureflodelist.set(Tool.AC.Popup(absoureflodelist.get(num), floders, GUILayout.Width(200)), num);
                            if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                            {
                                File.Delete(@item.FullName);
                                if (File.Exists(string.Concat(@item.FullName, ".manifest")))
                                {
                                    File.Delete(string.Concat(@item.FullName, ".manifest"));
                                }
                                absoureflodelist.removeAt(num);
                                absoureflodelistname.removeAt(num);
                            }
                            if (Tool.AC.Button("复制", GStyleTool.button, GUILayout.Width(60)))
                            {
                                if (floders[absoureflodelist.get(num)].text != "NULL")
                                {
                                    var dest = string.Concat(Application.persistentDataPath, "/ab/", floders[absoureflodelist.get(num)].text);
                                    File.Copy(@item.FullName, Path.Combine(dest, item.Name), true);
                                    EditorUtility.DisplayDialog("提示", "复制成功", "确定");
                                    Debug.Log(string.Concat("复制 : <<color=#FF5B00>", item.Name, "</color>> 完成! 目标文件夹 : <color=#FF5B00>", dest, "</color>"));
                                }
                                else EditorUtility.DisplayDialog("提示", "目标文件夹为空", "确定");
                            }
                            num++;
                        }, GStyleTool.helpBox);
                    }
                }
            }, GStyleTool.content);
        }

        #endregion

        #region 发布资源

        private string sourceABFloder;
        private bool isShowABFloder = true;
        private bool isShowSourceABFloder = true;
        private void PackageList()
        {
            Tool.BE.Vertical(() =>
            {
                Tool.AC.Label("源文件夹资源", GStyleTool.title);
                Tool.BE.Horizontal(() => { buildTarget = (BuildABTarget)EditorGUILayout.EnumPopup("目标平台", buildTarget, GStyleTool.GetName("Popup")); });

                Tool.EA.Separator();
                Tool.BE.Horizontal(() =>
                {
                    sourceABFloder = Tool.AC.FieldText("源文件夹", sourceABFloder, "LargeTextField");
                    var path = string.Concat(Application.persistentDataPath, "/ab/", sourceABFloder);
                    if (Tool.AC.Button("创建", GStyleTool.button, GUILayout.Width(60)))
                    {
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    }
                    if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                    {
                        if (Directory.Exists(path)) Directory.Delete(path);
                    }
                    if (Tool.AC.Button("打开", GStyleTool.button, GUILayout.Width(60)))
                    {
                        if (Directory.Exists(path)) System.Diagnostics.Process.Start(path);
                    }
                });

            }, GStyleTool.content);

            /* ----------------------------------------------------- */
            if (sourceABFloder.Length != 0 && buildTarget != BuildABTarget.PC)   //显示文件夹
            {
                Tool.BE.Vertical(() =>
                {
                    Tool.EA.Separator();
                    Tool.BE.Horizontal(() =>
                    {
                        if (Tool.AC.Button(" 一键打包 ", GStyleTool.button))
                        {
                            string @out = "";
                            switch (buildTarget)
                            {
                                case BuildABTarget.iOS:
                                    @out = string.Concat(Application.persistentDataPath, pack_out_ios, sourceABFloder, "/");
                                    break;
                                case BuildABTarget.Android:
                                    @out = string.Concat(Application.persistentDataPath, pack_out_android, sourceABFloder, "/");
                                    break;
                                default: return;
                            }
                            PackageFunc(sourceABFloder, @out, packageList.toArray());
                        }
                        if (!isShowABFloder) { if (Tool.AC.Button("显示源文件夹", GStyleTool.button)) isShowABFloder = true; }
                        else { if (Tool.AC.Button("关闭源文件夹", GStyleTool.button)) isShowABFloder = false; }

                    });
                    if (isShowABFloder)
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", sourceABFloder);
                        if (!Directory.Exists(path)) return;
                        var num = 0;
                        foreach (var item in new DirectoryInfo(path).GetDirectories())
                        {
                            Tool.BE.Horizontal(() =>
                            {
                                Tool.AC.Label(string.Concat("NO.", ++num), GUILayout.Width(100));
                                Tool.AC.Label(item.Name);
                                Tool.EA.ButtonCopyText("复制", -1, 60, item.Name);
                                if (Tool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
                                {
                                    Directory.Delete(item.FullName, true);
                                }
                                if (Tool.AC.Button("打包", GStyleTool.button, GUILayout.Width(60)))
                                {
                                    string @out = "";
                                    switch (buildTarget)
                                    {
                                        case BuildABTarget.iOS:
                                            @out = string.Concat(Application.persistentDataPath, pack_out_ios, sourceABFloder, "/");
                                            break;
                                        case BuildABTarget.Android:
                                            @out = string.Concat(Application.persistentDataPath, pack_out_android, sourceABFloder, "/");
                                            break;
                                        default: return;
                                    }
                                    PackageFunc(sourceABFloder, @out, new string[] { item.Name });
                                }
                            }, GStyleTool.helpBox);
                        }
                    }
                }, GStyleTool.content);
            }
            /* ----------------------------------------------------- */
            Tool.BE.Vertical(() =>
            {
                Tool.BE.Horizontal(() =>
                {
                    if (Tool.AC.Button(" 新增路径 ", GStyleTool.button)) packageList.add("");
                    if (Tool.AC.Button("备份源文件", GStyleTool.button))    //备份源文件到新的文件夹下
                    {
                        var path = string.Concat(Application.persistentDataPath, "/ab/", sourceABFloder);
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        var root = new DirectoryInfo(string.Concat(Application.persistentDataPath, "/ab/"));
                        foreach (var item in packageList.toArray())
                        {
                            foreach (var floder in root.GetDirectories())
                            {
                                if (item == floder.Name) EditorKit.CopyFolderAll(floder.FullName, string.Concat(path, '/', item), true);
                            }
                        }
                    }
                    if (Tool.AC.Button(isShowSourceABFloder ? " 关闭列表 " : " 显示列表 ", GStyleTool.button)) isShowSourceABFloder = !isShowSourceABFloder;
                });


                if (isShowSourceABFloder)
                {
                    Tool.BE.Vertical(() =>
                    {
                        Tool.EA.Separator();
                        for (int i = 0; i < packageList.count; i++)
                        {
                            Tool.BE.Horizontal(() =>
                            {
                                Tool.AC.Label(string.Concat("NO.", i + 1), GUILayout.Width(40));
                                packageList.set(Tool.AC.FieldText(packageList.get(i), GStyleTool.GetName("LargeTextField")), i);

                                if (packageList.get(i).Length != 0 && Tool.AC.Button("备份", GStyleTool.button, GUILayout.Width(60)))
                                {
                                    var s = string.Concat(Application.persistentDataPath, "/ab/", packageList.get(i));
                                    var d = string.Concat(Application.persistentDataPath, "/ab/", sourceABFloder, "/", packageList.get(i));
                                    EditorKit.CopyFolderAll(s, d, true);
                                    EditorUtility.DisplayDialog("提示", "覆盖该文件夹成功", "确定");
                                }
                                if (Tool.AC.Button("移除", GStyleTool.button, GUILayout.Width(60))) { packageList.remove(packageList.get(i)); }
                            });
                        }
                    });
                }
            }, GStyleTool.content);

            /* ----------------------------------------------------- */
            Tool.BE.Vertical(() =>
            {
                Tool.EA.Separator();
                Tool.BE.Horizontal(() =>
                {
                    if (Tool.AC.Button("解包本地资源", GStyleTool.button))
                    {
                        switch (buildTarget)
                        {
                            case BuildABTarget.iOS: UnCommpressFile(@pack_out_ios); break;
                            case BuildABTarget.Android: UnCommpressFile(@pack_out_android); break;
                        }
                    }
                    if (Tool.AC.Button("新增资源路径", GStyleTool.button))
                    {
                        server_requst_URL.add("");
                        server_requst_URL_key.add("");
                    }
                });

                Tool.EA.Separator();
                Tool.BE.Vertical(() =>
                {
                    for (int i = 0; i < server_requst_URL.count; i++)
                    {
                        server_requst_URL.set(Tool.AC.FieldText(server_requst_URL.get(i), GStyleTool.textArea, GUILayout.Height(40)), i);
                        Tool.BE.Horizontal(() =>
                        {
                            Tool.EA.Separator();
                            if (server_requst_URL_key.count - 1 < i) server_requst_URL_key.add("");
                            server_requst_URL_key.set(Tool.AC.FieldText(server_requst_URL_key.get(i), GStyleTool.title, GUILayout.Height(20), GUILayout.ExpandWidth(true)), i);
                            if (Tool.AC.Button("删除路径", GStyleTool.button, GUILayout.Height(20), GUILayout.Width(80))) server_requst_URL.removeAt(i);
                            if (Tool.AC.Button("覆盖资源", GStyleTool.button, GUILayout.Height(20), GUILayout.Width(80)))
                            {
                                string @source = "", @dest = "";
                                switch (buildTarget)
                                {
                                    case BuildABTarget.iOS:
                                        @source = string.Concat(Application.persistentDataPath, pack_out_ios, sourceABFloder);
                                        @dest = string.Concat(server_requst_URL.get(i), @"\ios\");
                                        break;
                                    case BuildABTarget.Android:
                                        @source = string.Concat(Application.persistentDataPath, pack_out_android, sourceABFloder);
                                        @dest = string.Concat(server_requst_URL.get(i), @"\android\");
                                        break;
                                    default: return;
                                }
                                ReplacePackage(@source, @dest);
                            }
                            if (Tool.AC.Button("打开文件夹", GStyleTool.button, GUILayout.Height(20), GUILayout.Width(80)))
                                if (Directory.Exists(server_requst_URL.get(i))) System.Diagnostics.Process.Start(server_requst_URL.get(i));
                        });
                        if (i < server_requst_URL.count - 1) Tool.EA.Separator(2);
                    }
                }, GUILayout.ExpandWidth(true));
            }, GStyleTool.content);
        }

        #endregion

        #region Func

        /// <summary> 读取JSON文件 </summary>
        private T ReadTextToJson<T>(string path)
        {
            T type = default(T);
        again:
            if (File.Exists(@path))
            {
                var bytes = File.ReadAllBytes(@path);
                if (bytes != null)
                {
                    string str = Encoding.UTF8.GetString(bytes);
                    type = JsonUtility.FromJson<T>(str);
                    JsonUtility.FromJsonOverwrite(str, type);
                }
            }
            else switch (Path.GetExtension(@path))
                {
                    case ".json":
                        @path = Path.ChangeExtension(@path, ".txt");
                        goto again;
                    default:
                        EditorUtility.DisplayDialog("提示", "读取文件失败", "确定");
                        return type;
                }
            return type;
        }

        /// <summary> 保存为JSON文件 </summary>
        private void SaveTextToJson<T>(string path, T obj)
        {
            if (obj != null)
            {
                File.WriteAllText(path, JsonUtility.ToJson(obj, true), new UTF8Encoding(false));
                EditorUtility.DisplayDialog("提示", "保存成功", "确定");
            }
            else EditorUtility.DisplayDialog("提示", "保存信息为空", "确定");
        }

        /// <summary> 获取地址 </summary>
        private string GetVersionPath(string https)
        {
            var path = https.Replace("http://", "");
            var index = path.IndexOf('/', 0);
            var rep = path.Substring(0, index);
            return string.Concat(@"D:\Static Programs\Server-modules\www\", @path.Replace(rep, ""));
        }

        /// <summary> 打包 </summary>
        public void BuildAllAssetBundlesPC(string flodername)
        {
            if (flodername.Length == 0) return;
            var @path = string.Concat(Application.dataPath, "/AssetBundles/", flodername);
            BuildPipeline.BuildAssetBundles(@path, BuildAssetBundleOptions.None, UnityEditor.BuildTarget.StandaloneWindows64);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("提示", "打包完成", "确定");
        }

        /// <summary> 打包 </summary>
        public void BuildAllAssetBundlesAndroid(string flodername)
        {
            if (flodername.Length == 0) return;
            var @path = string.Concat(Application.persistentDataPath, "/ab/", flodername);
            if (!Directory.Exists(@path)) Directory.CreateDirectory(@path);
            BuildPipeline.BuildAssetBundles(@path, BuildAssetBundleOptions.ChunkBasedCompression, UnityEditor.BuildTarget.Android);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("提示", "打包完成", "确定");
        }

        /// <summary> 打包 </summary>
        public void BuildAllAssetBundlesIOS(string flodername)
        {
            if (flodername.Length == 0) return;
            var @path = string.Concat(Application.persistentDataPath, "/ab/", flodername);
            BuildPipeline.BuildAssetBundles(@path, BuildAssetBundleOptions.ChunkBasedCompression, UnityEditor.BuildTarget.iOS);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("提示", "打包完成", "确定");
        }

        /// <summary> 解包 </summary>
        private void UnCommpressFile(string @out)
        {
            UnCompress unCommpress; DateTime time;
            foreach (var folder in new DirectoryInfo(@out).GetFiles())
            {
                unCommpress = new UnCompress(@out + folder.Name, @out);
                unCommpress.unCompressThread.Start();
                time = DateTime.Now;
                while (unCommpress.unCompressThread.ThreadState == ThreadState.Running)
                {
                    EditorUtility.DisplayProgressBar(
                        string.Concat("UnCommpressFile ", PlayerSettings.applicationIdentifier, ' ', folder.Name),
                        string.Concat(Mathf.CeilToInt(unCommpress.getPerCent() * 100), '%'),
                        unCommpress.getPerCent());
                }
                EditorUtility.ClearProgressBar();
                Debug.Log(string.Concat("解包 : <<color=#FF5B00>", folder.Name, "</color>> 完成! 一共用时 : <color=#FF5B00>", EditorKit.ExecDateDiff(time, DateTime.Now), "</color>"));
            }
            EditorUtility.DisplayDialog("提示", "解包完成", "确定");
        }

        /// <summary> 替换 </summary>
        private void ReplacePackage(string source, string dest)
        {
            if (!Directory.Exists(source)) return;
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
            var names = "";
            foreach (var file in new DirectoryInfo(source).GetFiles())
            {
                File.Copy(@file.FullName, Path.Combine(@dest, file.Name), true);
                names += string.Concat("<<color=#FF5B00>", file.Name, "</color>> ");
            }
            Debug.Log(string.Concat("更换 : <<color=#FF5B00>", new DirectoryInfo(source).GetFiles().Length, "</color>> 个文件 : " + names));
            EditorUtility.DisplayDialog("提示", "覆盖资源完成", "确定");
        }

        /// <summary> 打包方法 </summary>
        /// <param name="out">输出路径</param>
        private void PackageFunc(string @source, string @out, string[] floders)
        {
            if (!Directory.Exists(@out)) { Directory.CreateDirectory(@out); }

            var root = new DirectoryInfo(Application.persistentDataPath + @"/ab/" + @source + @"/");
            string fullname = "", outPutPath = ""; CompressZip compressZip; DirectoryInfo temp; DateTime time;
            foreach (var v in floders)
            {
                if (v.Length == 0 || v.Contains("/") || v.Contains(@"\")) continue;
                foreach (var folder in root.GetDirectories())
                {
                    if (v == folder.Name)     //获取比较名 cfg res update gameui qycfg等
                    {
                        time = DateTime.Now;
                        temp = root.CreateSubdirectory("temp");
                        Directory.Move(folder.FullName, string.Format(@"{0}\{1}", temp.FullName, folder.Name));
                        Directory.Move(temp.FullName, folder.FullName);

                        fullname = string.Concat(root.FullName, '/', folder.Name, '/');
                        outPutPath = string.Concat(@out, folder.Name, ".upk");
                        compressZip = new CompressZip(fullname, outPutPath, folder.Name);
                        compressZip.compressThread.Start();

                        while (compressZip.compressThread.ThreadState == ThreadState.Running)
                        {
                            EditorUtility.DisplayProgressBar(string.Concat("Package ", PlayerSettings.applicationIdentifier, ' ', folder.Name), string.Concat(Mathf.CeilToInt(compressZip.getPercent() * 100), '%'), compressZip.getPercent());
                        }

                        Directory.Move(string.Format(@"{0}\{1}", folder.FullName, folder.Name), temp.FullName);
                        Directory.Delete(folder.FullName);
                        Directory.Move(temp.FullName, folder.FullName);

                        folder.Refresh();
                        EditorUtility.ClearProgressBar();
                        Debug.Log(string.Concat("打包 : <<color=#FF5B00>", folder.Name, "</color>> 完成! 一共用时 : <color=#FF5B00>", EditorKit.ExecDateDiff(time, DateTime.Now), "</color>"));
                        break;
                    }
                }
            }
            root.Refresh();
            EditorUtility.DisplayDialog("提示", "打包结束", "确定");
        }

        private void SetVersionDirAssetName(string fullPath, string abName = "")
        {
            var relativeLen = fullPath.Length + 8; // Assets 长度  
            fullPath = string.Concat(Application.dataPath, '/', fullPath, '/');
            if (Directory.Exists(fullPath))
            {
                var title = string.Concat("修改 <", abName, "> 名称");
                var content = string.Concat("设置标签 ", abName, " ...");
                EditorUtility.DisplayProgressBar(title, content, 0f);
                var dir = new DirectoryInfo(fullPath);
                var files = dir.GetFiles("*", SearchOption.AllDirectories);
                //var folders = dir.GetDirectories("*", SearchOption.AllDirectories);
                //var len = files.Length + folders.Length;
                for (var i = 0; i < files.Length; ++i)
                {
                    EditorUtility.DisplayProgressBar(title, content, 1f * i / files.Length);
                    ChangeFileInfo(files[i], files[i].FullName.Substring(fullPath.Length - relativeLen), abName);
                }
                //title = string.Concat("修改文件夹 <", fullPath, "> 名称");
                //len -= files.Length;
                //for (int i = 0; i < folders.Length; i++)
                //{
                //    EditorUtility.DisplayProgressBar(title, content, 1f * i / len);
                //    ChangeFileInfo(folders[i], folders[i].FullName.Substring(fullPath.Length - relativeLen), abName);
                //}
                EditorUtility.ClearProgressBar();
            }
            Debug.Log(string.Concat("Full Path = <<color=#FF5B00>", fullPath, "</color>> | <<color=#FF5B00>Exists = ", Directory.Exists(fullPath), "</color>>"));
        }

        private void ChangeFileInfo(FileInfo info, string basePath, string abName)
        {
            if (!info.Name.EndsWith(".meta"))
            {
                var importer = AssetImporter.GetAtPath(basePath);
                if (!importer || importer.assetBundleName == abName) return;
                importer.assetBundleName = abName;
                //importer.SetAssetBundleNameAndVariant(abName, "");
                //importer.SaveAndReimport();
            }
        }


        private void ChangeFileInfo(DirectoryInfo info, string basePath, string abName)
        {
            if (!info.Name.EndsWith(".meta"))
            {
                var importer = AssetImporter.GetAtPath(basePath.Replace("/", @"\").ToString());
                if (!importer) return;
                if (importer.assetBundleName == abName) return;
                importer.SetAssetBundleNameAndVariant(abName, "");
                importer.SaveAndReimport();
            }
        }

        #endregion

        private ArrayList<GUIContent> prefabcfg;
        private GUIContent[] floders;

        protected override void onEnable()
        {
            buildTarget = (BuildABTarget)EditorPrefs.GetInt("BuildTarget");
            for (int i = 0; i < EditorPrefs.GetInt("packageList"); i++)
            {
                packageList.add(EditorPrefs.GetString(string.Concat("packageList", i)));
            }

            for (int i = 0; i < EditorPrefs.GetInt("server_requst_URL"); i++)
            {
                server_requst_URL.add(EditorPrefs.GetString(string.Concat("server_requst_URL", i)));
                server_requst_URL_key.add(EditorPrefs.GetString(string.Concat("server_requst_URL_key", i)));
            }

            string combie = "";
            for (int i = 0, end; i < EditorPrefs.GetInt("filelable"); i++)
            {
                combie = EditorPrefs.GetString(string.Concat("filelable", i));
                end = combie.LastIndexOf('|');
                filelable.Add(combie.Substring(0, end), combie.Substring(end + 1, combie.Length - end - 1));
            }
            keys = new string[filelable.Count];
            filelable.Keys.CopyTo(keys, 0);

            for (int i = 0, end; i < EditorPrefs.GetInt("absoureflodelist"); i++)
            {
                combie = EditorPrefs.GetString(string.Concat("absoureflodelist", i));
                end = combie.LastIndexOf('|');
                absoureflodelistname.add(combie.Substring(0, end));
                absoureflodelist.add(int.Parse(combie.Substring(end + 1, combie.Length - end - 1)));
            }

            UpdateAssetsPath = EditorPrefs.GetString("UpdateAssetsPath");    //更新资源路径
            sourceABFloder = EditorPrefs.GetString("sourceABFloder");        //源AB文件夹路径
            androidBundlePath = EditorPrefs.GetString("androidBundlePath");
            iosBundlePath = EditorPrefs.GetString("iosBundlePath");
            pcBundlePath = EditorPrefs.GetString("pcBundlePath");
            if (androidBundlePath.Length == 0 || androidBundlePath == null) androidBundlePath = "Build AssetBundles Android";
            if (iosBundlePath.Length == 0 || iosBundlePath == null) iosBundlePath = "Build AssetBundles IOS";
            if (pcBundlePath.Length == 0 || pcBundlePath == null) pcBundlePath = "Build AssetBundles PC";

            var fd = new DirectoryInfo(string.Concat(Application.persistentDataPath, "/ab/")).GetDirectories();
            prefabcfg = new ArrayList<GUIContent>();
            floders = new GUIContent[fd.Length];
            floders[0] = new GUIContent("NULL");
            var num = 0;
            var type = "";
            switch (BundlePlatform)
            {
                case BuildABTarget.Android: type = androidBundlePath; break;
                case BuildABTarget.iOS: type = iosBundlePath; break;
                case BuildABTarget.PC: type = pcBundlePath; break;
            }
            foreach (var item in fd)
            {
                if (item.Name != type)
                {
                    floders[++num] = new GUIContent(item.Name);
                }
                if (item.Name.Contains("cfg"))
                {
                    prefabcfg.add(new GUIContent(item.Name));
                }
            }
        }

        protected override void change()
        {
            EditorPrefs.SetInt("BuildTarget", (int)buildTarget);
            EditorPrefs.SetInt("packageList", packageList.count);
            int i = 0;
            for (i = 0; i < packageList.count; i++)                       //资源包路径 文件名
            {
                EditorPrefs.SetString(string.Concat("packageList", i), packageList.get(i));
            }

            EditorPrefs.SetInt("server_requst_URL", server_requst_URL.count); //服务器路径 项目名
            for (i = 0; i < server_requst_URL.count; i++)
            {
                EditorPrefs.SetString(string.Concat("server_requst_URL_key", i), server_requst_URL_key.get(i));
                EditorPrefs.SetString(string.Concat("server_requst_URL", i), server_requst_URL.get(i));
            }

            EditorPrefs.SetInt("filelable", filelable.Count);             //标签设置
            i = 0;
            foreach (var item in filelable)
            {
                EditorPrefs.SetString(string.Concat("filelable", i++), string.Concat(item.Key, '|', item.Value));
            }

            EditorPrefs.SetInt("absoureflodelist", absoureflodelistname.count);      //AB文件名
            for (i = 0; i < absoureflodelistname.count; i++)
            {
                EditorPrefs.SetString(string.Concat("absoureflodelist", i), string.Concat(absoureflodelistname.get(i), '|', absoureflodelist.get(i)));
            }

            EditorPrefs.SetString("UpdateAssetsPath", UpdateAssetsPath);      //更新资源路径
            EditorPrefs.SetString("sourceABFloder", sourceABFloder);          //源AB文件夹路径
            EditorPrefs.SetString("androidBundlePath", androidBundlePath);    //AB Bunlder Aseets Android 包文件夹路径
            EditorPrefs.SetString("iosBundlePath", iosBundlePath);            //AB Bunlder Aseets IOS 包文件夹路径      
            EditorPrefs.SetString("pcBundlePath", pcBundlePath);              //AB Bunlder Aseets PC 包文件夹路径
        }

        protected override void close()
        {
            window = null;
        }
    }
}