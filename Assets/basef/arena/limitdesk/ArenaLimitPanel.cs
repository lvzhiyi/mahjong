using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{/// <summary>
/// 竞赛场同桌限制界面  擂主可见
/// </summary>
    public class ArenaLimitPanel : UnrealLuaPanel
    {
        ToggleGroup limit;
        public List<Toggle> toggleList;
        public int limitindex = -1;
        ArenaMember member;
        protected override void xinit()
        {
            limit = transform.Find("Canvas").Find("limit").GetComponent<ToggleGroup>();
            toggleList = new List<Toggle>(transform.GetComponentsInChildren<Toggle>());
            for (int i = 0; i < toggleList.Count; i++)
            {
                toggleList[i].onValueChanged.AddListener(limitlistener);
            }
        }
        protected override void xrefresh()
        {
            refreshlimit();
        }
        private void limitlistener(bool b)
        {
            if (b)
            {
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].isOn)
                    {
                        limitindex = i;
                    }
                }
            }
        }

        public void setData(ArenaMember member)
        {
            this.member = member;
        }

        private void refreshlimit()
        {
            toggleList[0].isOn = false;
            toggleList[1].isOn = false;
            toggleList[2].isOn = false;
            toggleList[3].isOn = false;
            if (member.mutexStatus == 0)
                toggleList[0].isOn = true;
            else if (member.mutexStatus == 1)
                toggleList[1].isOn = true;
            else if (member.mutexStatus == 2)
                toggleList[2].isOn = true;
            else if (member.mutexStatus == 3)
                toggleList[3].isOn = true;
        }
    }
}