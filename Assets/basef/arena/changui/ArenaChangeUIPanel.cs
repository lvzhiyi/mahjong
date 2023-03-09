using cambrian.common;
using cambrian.game;
using cambrian.unreal.scroll;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    public class ArenaChangeUIPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 选择类型：0场景，1桌子，2玩法
        /// </summary>
        public const int TYPE_BG = 0, TYPE_DESK = 1, TYPE_RULE = 2;

        string[] typeName = new string[]
        {
            "场景","桌子","玩法/桌面"
        };

        Transform bgview;
        Transform deskview;
        ChangUIRuleView ruleview;

        ScrollContainer selectContainer;

        ScrollTableContainer bgContainer;

        ScrollTableContainer deskContainer;

        /// <summary>
        /// 选中标题
        /// </summary>
        int selectType;
        /// <summary>
        /// 选中背景
        /// </summary>
        int selectBg;
        /// <summary>
        /// 选中桌子
        /// </summary>
        int selectDesk;
        
        string[] bgnames = new string[]
        {
            "旭日东升","清凉夏日","落日余晖","当风秉烛","荷塘月色","高楼大厦"
        };

        string[] desknames = new string[]
        {
           "天地四方","八面圆通"
        };
       
        protected override void xinit()
        {
            base.xinit();
            bgview= this.content.Find("content").Find("view").Find("bgview");
            deskview = this.content.Find("content").Find("view").Find("deskview");
            ruleview = this.content.Find("content").Find("view").Find("ruleview").GetComponent<ChangUIRuleView>();
            ruleview.init();

            selectContainer = this.content.Find("content").Find("leftType").GetComponent<ScrollContainer>();
            selectContainer.init();

            bgContainer = bgview.Find("container").GetComponent<ScrollTableContainer>();
            bgContainer.init();

            deskContainer = deskview.Find("container").GetComponent<ScrollTableContainer>();
            deskContainer.init();
        }

        protected override void xrefresh()
        {
            setSelect(0);
        }


        public void setSelect(int type)
        {
            this.selectType = type;
            refreshSelect();
            bgview.gameObject.SetActive(false);
            deskview.gameObject.SetActive(false);
            ruleview.gameObject.SetActive(false);
            ByteBuffer data = null;
            if (type == TYPE_BG)
            {
                data = FileCachesManager.loadPath(CacheLocalPath.APPLICATION_PATH + "/lobbyskin" + "/bgskin" + Arena.arena.getId(), true);
                if (data != null) selectBg = data.readInt();
                refreshBg();
                bgview.gameObject.SetActive(true);
            }
            else if (type == TYPE_DESK) 
            {
                data = FileCachesManager.loadPath(CacheLocalPath.APPLICATION_PATH + "/lobbyskin" + "/desk" + Arena.arena.getId(), true);
                if (data != null) selectDesk = data.readInt();
                refreshDesk();
                deskview.gameObject.SetActive(true);
            }
            else
            {
                data = FileCachesManager.loadPath(CacheLocalPath.APPLICATION_PATH + "/lobbyskin" + "/ruleskin" + Arena.arena.getId(), true);
                RuleSkin ruleSkin = new RuleSkin();
                if (data != null) ruleSkin.bytesRead(data);
                ruleview.setData(ruleSkin);
                ruleview.refresh();
                ruleview.gameObject.SetActive(true);
            }
        }

        // ==========================选择=============================
        public void refreshSelect()
        {
            selectContainer.updateData(selectCallback);
            selectContainer.resize(typeName.Length);
            selectContainer.resizeDelta();
        }
        
        private void selectCallback(ScrollBar bar, int index)
        {
            ArenaChangUITitleTypeBar typeBar = (ArenaChangUITitleTypeBar)bar;
            typeBar.index_space = index;
            typeBar.setData(typeName[index], selectType);
            typeBar.refresh();
        }

        public void  selectedBar(int type)
        {
            if (selectType == TYPE_BG)
            {
                this.selectBg = type;
                bgContainer.refreshShow();
            }
            else if (selectType == TYPE_DESK)
            {
                this.selectDesk = type;
                refreshDesk();
            }
        }
        // ==========================保存=============================
        public void saveSetting()
        {
            ByteBuffer data = new ByteBuffer();
            string saveName;
            if (selectType == TYPE_BG)
            {
                saveName = "/bgskin";
                data.writeInt(selectBg);
            }
            else if (selectType == TYPE_DESK)
            {
                saveName = "/desk";
                data.writeInt(selectDesk);
            }
            else
            {
                saveName = "/ruleskin";
                ruleview.saveSetting(data);
            }
            FileCachesManager.savePath(CacheLocalPath.APPLICATION_PATH + "/lobbyskin" + saveName + Arena.arena.getId(), true, data);
            Alert.autoShow("设置成功！");
        }

        // ==========================背景=============================

        public void refreshBg()
        {
            bgContainer.updateData(bgCallback);
            bgContainer.resize(bgnames.Length);
            bgContainer.resizeDelta();
        }

        private void bgCallback(ScorllTableBar bar, int index)
        {
            ChangUIBgBar temp = (ChangUIBgBar)bar;
            temp.index_space = index;
            temp.setData(selectBg, bgnames[index]);
            temp.refresh();
        }

        // ==========================桌子=============================
        public void refreshDesk()
        {
            deskContainer.updateData(deskCallback);
            deskContainer.resize(desknames.Length);
            deskContainer.resizeDelta();
        }

        private void deskCallback(ScorllTableBar bar, int index)
        {
            ChangUIDeskBar temp = (ChangUIDeskBar)bar;
            temp.index_space = index;
            temp.setData(selectDesk, desknames[index]);
            temp.refresh();
        }
    }
}
