namespace platform.poker
{
    public class PPDKAnimationPlayView : PKAnimationPlayView
    {
        protected override void xinit()
        {
            lefts = transform.Find("left").GetComponent<PKAnimationPlayControl>();
            lefts.init();

            rights = transform.Find("right").GetComponent<PKAnimationPlayControl>();
            rights.init();

            downs = transform.Find("down").GetComponent<PKAnimationPlayControl>();
            downs.init();

            if (transform.Find("top") != null)
            {
                top = transform.Find("top").GetComponent<PKAnimationPlayControl>();
                top.init();
            }

        }

        protected override void xrefresh()
        {
            setVisible(false);
            downs.refresh();
            rights.refresh();
            lefts.refresh();
            if (top != null)
                top.refresh();
        }

        public override void animationPlay(int type, int[] cards, int pos)
        {
            if (!gameObject.activeSelf) setVisible(true);
            int sex = Room.room.players[pos].player.sex;
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    downs.Play(type, cards[0], sex, getTypeName(type), index(PKGAME.DOWN, type));
                    break;
                case PKGAME.RIGHT:
                    rights.Play(type, cards[0], sex, getTypeName(type), index(PKGAME.RIGHT, type));
                    break;
                case PKGAME.LEFT:
                    lefts.Play(type, cards[0], sex, getTypeName(type), index(PKGAME.LEFT, type));
                    break;
                case PKGAME.TOP:
                    top.Play(type, cards[0], sex, getTypeName(type), index(PKGAME.TOP, type));
                    break;
            }
        }

        protected override string getTypeName(int type)
        {
            switch (type)
            {
                case PKCardType.TYPE_3_1: return "";
                case PKCardType.TYPE_3_2: return "";
                case PKCardType.TYPE_CARDS_CONNECT: return "shunzi";
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT: return "liandui";
                case PKCardType.TYPE_CARDS_THREE_CONNECT: return "feiji";
                case PKCardType.TYPE_FEIJI_1: return "feiji";
                case PKCardType.TYPE_FEIJI_2: return "feiji";
                case PKCardType.TYPE_CARDS_PLANE: return "feiji";
                case PKCardType.TYPE_4_1_1_1: return "";
                case PKCardType.TYPE_CARDS_BOMB: return "zadan";
                case PKCardType.TYPE_4_1_1: return "";
                case PKCardType.TYPE_4_2_2: return "";
                case PKCardType.TYPE_4_1: return "";
                case PKCardType.TYPE_ZHA_WANG: return "wangzha";
                case PKCardType.TYPE_ZHA_LIAN: return "";
                default: return "";
            }
        }


        protected virtual string index(int pos, int type)
        {
            string str = "";
            switch (pos)
            {
                case PKGAME.DOWN:
                    downs.gameObject.SetActive(true);
                    str = "down";
                    break;
                case PKGAME.LEFT:
                    lefts.gameObject.SetActive(true);
                    str = "left";
                    break;
                case PKGAME.RIGHT:
                    rights.gameObject.SetActive(true);
                    str = "right";
                    break;
                case PKGAME.TOP:
                    top.gameObject.SetActive(true);
                    str = "top";
                    break;
            }
            switch (type)
            {
                case PKCardType.TYPE_4_1_1:
                case PKCardType.TYPE_4_2_2:
                case PKCardType.TYPE_4_1_1_1:
                case PKCardType.TYPE_4_1:
                case PKCardType.TYPE_ZHA_WANG:
                case PKCardType.TYPE_ZHA_LIAN:
                case PKCardType.TYPE_3_1:
                case PKCardType.TYPE_3_2:
                case PKCardType.TYPE_CARDS_CONNECT:
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT:
                    str = "center"; break;
            }
            return str;
        }
    }
}
