using cambrian.common;
using cambrian.unreal.display;

namespace platform.spotred.room
{
    public class SelectSlipCardPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 牌
        /// </summary>
        int[][] card;
        /// <summary>
        /// 剩余牌的总数
        /// </summary>
        public int cardnum;

        private UnrealTableContainer container;

        /// <summary>
        /// 选中的牌
        /// </summary>
        public ArrayList<int[]> selectCards=new ArrayList<int[]>();

        protected override void xinit()
        {
            this.container = transform.Find("Canvas").Find("group").GetComponent<UnrealTableContainer>();
            this.container.init();
        }

        public void setData(int[][] card,int cardnum)
        {
            this.card = card;
            this.cardnum = cardnum;
        }

        private ArrayList<int[]> list;
        protected override void xrefresh()
        {
            list = count();
            this.container.resize(list.count);

            selectCards.clear();

            for (int i = 0; i < list.count; i++)
            {
                SlipCardBar bar = (SlipCardBar) this.container.bars[i];

                int[] c = list.get(i);
                bar.setData(c[0], c[1]);
                bar.index_space = i;
                bar.refresh();
            }
            this.container.resizeDelta();
            this.container.relayout();
        }


        public void setSelected(int index,int card,int count,bool isSelected)
        {
            SlipCardBar bar = null;
            if (!isSelected)
            {
                bar = (SlipCardBar)this.container.bars[index];
                bar.setSelected(false);
                removeSelected(card,count);
                return;
            }


            int key=-1;
            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i)[0] == card&&list.get(i)[1]!=count)
                {
                    key = i;
                    break;
                }
            }

            if (key != -1)//有相同的牌
            {
                bar = (SlipCardBar)this.container.bars[key];
                bar.setSelected(false);
                removeSelected(bar.card,bar.count);
            }

            bar = (SlipCardBar)this.container.bars[index];
            bar.setSelected(true);
            selectCards.add(new int[2] { card , count });
        }

        void removeSelected(int card,int count)
        {
            for (int i = 0; i < selectCards.count; i++)
            {
                int[] cards = selectCards.get(i);
                if (cards[0] == card && cards[1] == count)
                {
                    selectCards.removeAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 是否可以选择
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool isCanSelect(int card,int count)
        {
            int num=0;

            int[] cards = null;//检查是否有相同的牌

            for (int i = 0; i < selectCards.count; i++)
            {

                int[] c = selectCards.get(i);

                if (c[0]==card&&c[1]!=count)
                    cards = c;

                if (c[1] == 3)
                    num++;
                else if (c[1] == 4)
                    num += 2;
            }

            num += (count == 4 ? 2 : 1);

            if (cards != null)
            {
                num-= (cards[1] == 4 ? 2 : 1);
            }
            return cardnum >= num;
        }


        ArrayList<int[]> count()
        {
            ArrayList<int[]> list=new ArrayList<int[]>();
            

            for (int i = 0; i < card.Length; i++)
            {
                if (card[i][1]==4)
                {
                    list.add(card[i]);
                    list.add(new int[2] {card[i][0],3});
                }
                else
                {
                    list.add(card[i]);
                }
            }

            return list;
        }

    }
}
