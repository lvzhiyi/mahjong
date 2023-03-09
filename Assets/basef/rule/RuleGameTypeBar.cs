using scene.game;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    public class RuleGameTypeBar:UnrealLuaSpaceObject
    {
        [HideInInspector] public UnrealCheckBox checkbox;

        protected Text txtchecked;

        protected Text txtnormal;

        GameType gameType;

        protected override void xinit()
        {
            checkbox = transform.Find("checkbox").GetComponent<UnrealCheckBox>();
            txtchecked = transform.Find("text_1").GetComponent<Text>();
            txtnormal = transform.Find("text").GetComponent<Text>();
        }


        public void setGameType(GameType gameType)
        {
            this.gameType = gameType;
        }

        public GameType getGameType()
        {
            return gameType;
        }

        /// <summary>
        /// 选中
        /// </summary>
        public void selected()
        {
            this.checkbox.setState(UnrealCheckObject.ACTIVED);
            this.txtchecked.gameObject.SetActive(true);
            this.txtnormal.gameObject.SetActive(false);
        }

        public void selectNormal()
        {
            this.checkbox.setState(UnrealCheckObject.NORMAL);
            this.txtchecked.gameObject.SetActive(false);
            this.txtnormal.gameObject.SetActive(true);
        }

        protected override void xrefresh()
        {
            txtchecked.text = gameType.name;
            txtnormal.text = gameType.name;

            txtnormal.gameObject.SetActive(true);
            txtchecked.gameObject.SetActive(false);

            checkbox.setState(UnrealCheckObject.NORMAL);
        }
    }
}
