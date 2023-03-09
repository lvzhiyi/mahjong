using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJTangView:UnrealLuaSpaceObject
    {
        Image down;

        Image right;

        Image top;

        Image left;


        protected override void xinit()
        {
            down = transform.Find("tang0").GetComponent<Image>();
            right = transform.Find("tang1").GetComponent<Image>();
            top = transform.Find("tang2").GetComponent<Image>();
            left = transform.Find("tang3").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            down.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
        }

        public void showTang(int index,bool b)
        {
            switch (index)
            {
                case MahjongRoomPanel.LOC_DOWN:
                    down.gameObject.SetActive(b);
                    break;
                case MahjongRoomPanel.LOC_RIGHT:
                   
                    right.gameObject.SetActive(b);
                    break;
                case MahjongRoomPanel.LOC_TOP:
                    top.gameObject.SetActive(b);
                    break;
                case MahjongRoomPanel.LOC_LEFT:
                    left.gameObject.SetActive(b);
                    break;
            }
        }
    }
}
