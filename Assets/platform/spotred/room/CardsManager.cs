using cambrian.common;
using platform.spotred;
using System.Collections;

namespace platform.spotred.room
{
    /// <summary>
    /// 牌管理(对牌处理后,显示)
    /// </summary>
    public class CardsManager
    {
        /// <summary>
        /// 获取手牌
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static ArrayList<int[]> getHand(int[] cards, bool singleblack7)
        {
            ArrayList<int>[] ll = new ArrayList<int>[13];
            for (int i = 0; i < 13; i++)
            {
                ll[i] = new ArrayList<int>();
            }
            ///将面值一样的牌放在一起
            for (int i = 0; i < cards.Length; i++)
            {
                int value = Card.getValue(cards[i]);
                ll[value - 1].add(cards[i]);
            }

            Hashtable table = new Hashtable();

            for (int i = 0; i < ll.Length; i++)
            {
                ArrayList<int> list_1 = ll[i];
                if (list_1.count == 0) continue;
                table.Add(Card.getValue(list_1.get(0)), list_1.toArray());
            }

            ArrayList keys = new ArrayList(table.Keys);
            object[] keys_arra = keys.ToArray(); //keys 面值2,3,4,5


            //same格式 {"key":{{1,1,1,1},{1,1,1,1}}} 里面的1代表面值一样，但具体内容不同（7点有3种）
            Hashtable same = new Hashtable();
            ArrayList<int[]> list = null;
            for (int i = 0; i < keys_arra.Length; i++)
            {
                ArrayList<int> l = new ArrayList<int>((int[]) table[keys_arra[i]]);

                list = new ArrayList<int[]>();
                while (l.count != 0)
                {
                    int value = l.removeAt(l.count - 1);
                    int count = 1;
                    for (int j = l.count - 1; j >= 0; j--)
                    {
                        if (l.get(j) == value)
                        {
                            l.removeAt(j);
                            count++;
                        }
                    }

                    int[] a = new int[count];

                    for (int j = 0; j < count; j++)
                    {
                        a[j] = value;
                    }

                    list.add(a);
                }

                same.Add(keys_arra[i], list);
            }

            ArrayList<int[]> return_value = new ArrayList<int[]>();


            ///牌的面值
            int[] keyss = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};

            if (singleblack7)
            {
                ArrayList<int[]> ty = (ArrayList<int[]>) same[keyss[6]];
                if (ty != null)
                {
                    int[] cd = null;
                    for (int i = 0; i < ty.count; i++)
                    {
                        if (ty.get(i)[0] == Card.C25)
                        {
                            cd = ty.get(i);
                            ty.removeAt(i);
                            break;
                        }
                    }

                    if (cd != null)
                    {
                        ArrayList<int[]> l = new ArrayList<int[]>();
                        l.add(cd);
                        arrayAdd(return_value, getShowColumnCards(l));
                    }

                    if (ty.count == 0)
                    {
                        same.Remove(7);
                    }
                }
            }


            for (int i = 0; i < keyss.Length; i++)
            {
                ArrayList<int[]> min = (ArrayList<int[]>) same[keyss[i]];
                same.Remove(i);

                if (min != null && Card.getValue(min.get(0)[0]) == 7)
                {
                    min = null;
                }

                int index = 14 - keyss[i];

                ArrayList<int[]> max = null;
                if (same.ContainsKey(index))
                {
                    max = (ArrayList<int[]>) same[index];
                    same.Remove(index);
                }

                if (min != null && max != null)
                {
                    arrayAdd(return_value, getColumnCards(min, max));
                }
                else if (min != null && max == null)
                {
                    arrayAdd(return_value, getShowColumnCards(min));
                }
                else if (max != null && min == null)
                {
                    arrayAdd(return_value, getShowColumnCards(max));
                }
            }
            return return_value;
        }


        public static void arrayAdd(ArrayList<int[]> dest,ArrayList<int[]> src)
        {
            for (int i = 0; i < src.count; i++)
            {
                dest.add(src.get(i));
            }
        }

        /// <summary>
        /// 对单个面值一样的进行组合成显示的那样
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ArrayList<int[]> getShowColumnCards(ArrayList<int[]> list)
        {
            ArrayList<int[]> ll = new ArrayList<int[]>();

            sort(list);

            ArrayList<int> l = getTwoPairsCard(list);
            if (l.count>0)
            {
                ll.add(l.toArray());
            }



            while (list.count != 0)
            {
                for (int i = list.count - 1; i >= 0;)
                {
                    int[] arr = list.removeAt(i);
                    int len = arr.Length;
                    int[] arr1 = null;
                    switch (len)
                    {
                        case 4:
                            ll.add(arr);
                            break;
                        case 3:
                            arr1 = removeSize(list, 1);
                            if (arr1 != null)
                            {
                                ll.add(arrayAdd(arr, arr1));
                            }
                            else
                            {
                                arr1 = removeSize(list, 2);
                                if (arr1 != null)
                                {
                                    ll.add(arrayAdd(arr, new[] { arr1[0] }));
                                    list.add(new[] { arr1[0] });
                                    sort(list);
                                }
                                else
                                    ll.add(arr);
                            }
                            break;
                        case 2:
                            arr1 = removeSize(list, 2);
                            if (arr1 != null)
                            {
                                ll.add(arrayAdd(arr, arr1));
                            }
                            else
                            {
                                ArrayList<int[]> ls = removeSizes(list, 1);

                                int[] array = new int[0];

                                for (int j = 0; j < ls.count; j++)
                                {
                                    array = arrayAdd(array, ls.get(j));
                                }

                                ll.add(arrayAdd(arr, array));
                            }
                            break;
                        case 1:
                            ArrayList<int[]> lss = removeSizes(list, 1);

                            int[] arrays = new int[0];

                            for (int j = 0; j < lss.count; j++)
                            {
                                arrays = arrayAdd(arrays, lss.get(j));
                            }

                            ll.add(arrayAdd(arr, arrays));
                            break;
                    }
                    break;
                }
            }
            return ll;
        }



        /// <summary>
        /// 组合牌分列
        /// </summary>
        /// <returns></returns>
         static ArrayList<int[]> getColumnCards(ArrayList<int[]> min,ArrayList<int[]> max)
        {

            ArrayList<int[]> list=new ArrayList<int[]>();

            //将四张一样的牌分出来
            int[] card = null;

            for (int i = max.count - 1; i >= 0; i--)
            {
                card = max.get(i);
                if (card.Length == 4)
                {
                    list.add(card);
                    max.removeAt(i);
                }
            }

            for (int i = min.count - 1; i >= 0; i--)
            {
                card = min.get(i);
                if (card.Length == 4)
                {
                    list.add(card);
                    min.removeAt(i);
                }
            }

            //判断是否大于4张
            int max_num=getCardNum(max);
            int min_num = getCardNum(min);

            int[][] cards = null;
            ArrayList<int> l=new ArrayList<int>();
            if (max_num + min_num > 4)
            {
                //无成对的牌：-1，0:小牌成对，1：大牌成对
                int pairs = -1;
                l.clear();
                //成对的组合在一起
                l = getTwoPairsCard(max);
                if (l.count > 0)
                {
                    list.add(l.toArray());
                    pairs = 1;
                }
                l = getTwoPairsCard(min);
                if (l.count > 0)
                {
                    list.add(l.toArray());
                    pairs = 0;
                }


                max_num = getCardNum(max);
                min_num = getCardNum(min);

                //4张一样的牌组合在一起
                l.clear();
                if (max_num == 4)
                {
                    cards = max.toArray();
                    for (int i = 0; i < cards.Length; i++)
                    {
                        l.add(cards[i]);
                    }
                    list.add(l.toArray());
                    max.clear();
                }

                l.clear();

                //四张牌小牌
                int[] min_cards = null;
                if (min_num == 4)
                {
                    cards = min.toArray();
                    for (int i = 0; i < cards.Length; i++)
                    {
                        l.add(cards[i]);
                    }
                    list.add(l.toArray());
                 //   min_cards = l.toArray();
                    min.clear();
                }


                max_num = getCardNum(max);
                min_num = getCardNum(min);
                l.clear();

                //max_num+min_num=5
                if ((max_num + min_num >= 5&& max_num + min_num <= 6 )&& max_num != 0 && min_num != 0)
                {

                    if (pairs == -1 || pairs == 1)
                    {
                        cards = max.toArray();
                        for (int i = 0; i < cards.Length; i++)
                        {
                            l.add(cards[i]);
                        }
                        list.add(l.toArray());
                        max.clear();

                        l.clear();

                        cards = min.toArray();
                        for (int i = 0; i < cards.Length; i++)
                        {
                            l.add(cards[i]);
                        }
                        list.add(l.toArray());
                        min.clear();
                    }
                    else 
                    {
                        cards = min.toArray();
                        for (int i = 0; i < cards.Length; i++)
                        {
                            l.add(cards[i]);
                        }
                        list.add(l.toArray());
                        min.clear();

                        l.clear();

                        cards = max.toArray();
                        for (int i = 0; i < cards.Length; i++)
                        {
                            l.add(cards[i]);
                        }
                        list.add(l.toArray());
                        max.clear();
                    }
                }
                else  if (min_cards != null)
                    list.add(min_cards);
            }

            sort(max);

            ArrayList<int> less_than4=new ArrayList<int>();

            //把大牌首先组成4张一列。
            while (max.count != 0)
            {
                for (int i = max.count - 1; i >= 0;)
                {
                    int[] arr = max.removeAt(i);
                    int len = arr.Length;

                    int[] arr1 = null;

                    ArrayList<int[]> ls = null; //牌的数组
                    switch (len)
                    {
                        case 3:
                            arr1 = removeSize(max, 1);
                            if (arr1 != null)
                            {
                                list.add(arrayAdd(arr, arr1));
                            }
                            else
                            {
                                arr1 = removeSize(max, 2);
                                if (arr1 != null)
                                {
                                    list.add(arrayAdd(arr, new[] {arr1[0]}));
                                    max.add(new[] {arr1[0]});
                                    sort(max);
                                }
                                else
                                {
                                    arr1 = removeSize(min, 1);
                                    if (arr1 != null)
                                    {
                                        list.add(arrayAdd(arr1, arr));
                                    }
                                    else
                                        list.add(arr);
                                }

                            }
                            break;
                        case 2:
                            arr1 = removeSize(max, 2);
                            if (arr1 != null)
                            {
                                list.add(arrayAdd(arr, arr1));
                            }
                            else
                            {
                                ls = removeSizes(max, 1);
                                if (ls.count > 0)
                                {
                                    if (ls.count == 2)
                                    {
                                        list.add(arrayAdd(arr, arrayAdd(ls.get(0), ls.get(1))));
                                    }
                                    else
                                    {
                                        less_than4.add(arrayAdd(arr, ls.get(0)));
                                    }
                                }
                                else
                                {
                                    less_than4.add(arr);
                                }
                            }
                            break;
                        case 1:
                            ls = removeSizes(max, 1);

                            ArrayList<int> b=new ArrayList<int>();
                            for (int j = 0; j < ls.count; j++)
                            {
                                b.add(ls.get(j)[0]);
                            }
                            less_than4.add(arrayAdd(arr, b.toArray()));
                            break;
                    }
                    //停止循环
                    break;
                }
            }


            //对未满四张的牌进行组合
            if (less_than4.count != 0)
            {
                int len = less_than4.count;

                int[] arr1 = null;
                switch (len)
                {
                    case 3:
                        arr1 = removeSize(min, 1);
                        if (arr1 != null)
                        {
                            list.add(arrayAdd( arr1, less_than4.toArray()));
                        }
                        else
                        {
                            arr1 = removeSize(min, 2);
                            if (arr1 != null)
                            {
                                list.add(arrayAdd( new[] {arr1[0]}, less_than4.toArray()));
                                min.add(new[] {arr1[0]});
                            }
                            else
                            {
                                arr1=removeSize(min, 3);
                                if (arr1 != null)
                                {
                                    list.add(arrayAdd( new[] { arr1[0] }, less_than4.toArray()));
                                    min.add(new[] { arr1[0],arr1[0]});
                                }
                                else
                                {
                                    list.add(less_than4.toArray());
                                }
                            }
                        }
                        break;
                    case 2:
                        arr1 = removeSize(min, 2);
                        if (arr1 != null)
                        {
                            list.add(arrayAdd( arr1, less_than4.toArray()));
                        }
                        else
                        {
                            ArrayList<int[]> bb= removeSizes(min, 1);

                            if (bb.count > 0)
                            {
                                if (bb.count == 2)
                                {
                                    list.add(arrayAdd(arrayAdd(bb.get(0), bb.get(1)), less_than4.toArray()));
                                }
                                else if (bb.count == 3)
                                {
                                    list.add(arrayAdd(arrayAdd(bb.get(0), bb.get(1)), less_than4.toArray()));
                                    min.add(bb.get(2));
                                }
                                else if (bb.count==1)
                                {
                                    list.add(arrayAdd(bb.get(0), less_than4.toArray()));
                                }
                            }
                            else
                            {
                                    arr1=removeSize(min, 3);
                                    if (arr1 != null)
                                    {
                                        list.add(arrayAdd(
                                            arrayAdd(new[] {arr1[0]}, new[] {arr1[0]}), less_than4.toArray()));
                                        min.add(new []{arr1[0]});
                                    }
                                    else
                                    {
                                         list.add(less_than4.toArray());
                                    }
                            }
                        }
                        break;
                    case 1:
                        arr1 = removeSize(min, 3);
                        if (arr1 != null)
                            list.add(arrayAdd( arr1, less_than4.toArray()));
                        else
                        {
                            ArrayList<int[]> bb = removeSizes(min, 2);
                            if (bb.count >= 2)
                            {
                                list.add(arrayAdd(arrayAdd(bb.get(0), new int[] { bb.get(1)[0] }),less_than4.toArray()));
                                min.add(new[] {bb.get(1)[0]});

                                if (bb.count == 3)
                                {
                                    min.add(bb.get(2));
                                }

                            }
                            else if (bb.count == 1)
                            {
                                arr1 = arrayAdd( bb.get(0), less_than4.toArray());
                                int[] c= removeSize(min, 1);
                                if (c != null)
                                {
                                    list.add(arrayAdd(c,arr1));
                                }
                                else
                                {
                                    list.add(arr1);
                                }
                            }
                            else
                            {
                                bb = removeSizes(min, 1);

                                if (bb.count > 0)
                                {
                                    int[] a = new int[0];
                                    for (int i = 0; i < bb.count; i++)
                                        a = arrayAdd(a, bb.get(i));
                                    list.add(arrayAdd( a, less_than4.toArray()));
                                }
                                else
                                    list.add(less_than4.toArray());
                            }
                        }
                        break;
                }
            }


            sort(min);
            //对小牌进行组合
            while (min.count!=0)
            {
                for (int i = min.count - 1; i >= 0;)
                {
                    int[] arr = min.removeAt(i);

                    int len = arr.Length;
                    int[] arr1 = null;
                    switch (len)
                    {
                        case 3:
                            arr1 = removeSize(min, 1);
                            if (arr1 != null)
                            {
                                list.add(arrayAdd(arr, arr1));
                            }
                            else
                            {
                                arr1 = removeSize(min, 2);
                                if (arr1 != null)
                                {
                                    list.add(arrayAdd(arr, new[] { arr1[0] }));
                                    min.add(new[] { arr1[0] });
                                    sort(min);
                                }
                                else
                                    list.add(arr);
                            }
                            break;
                        case 2:
                            arr1 = removeSize(min, 2);
                            if (arr1 != null)
                            {
                                list.add(arrayAdd(arr, arr1));
                            }
                            else
                            {
                                ArrayList<int[]> ls = removeSizes(min, 1);

                                int[] array = new int[0];

                                for (int j = 0; j < ls.count; j++)
                                {
                                    array = arrayAdd(array, ls.get(j));
                                }

                                 list.add(arrayAdd(arr, array));

                            }
                            break;
                        case 1:
                            ArrayList<int[]> lss = removeSizes(min, 1);

                            int[] arrays = new int[0];

                            for (int j = 0; j < lss.count; j++)
                            {
                                arrays = arrayAdd(arrays, lss.get(j));
                            }

                            list.add(arrayAdd(arr, arrays));
                            break;

                    }
                    break;
                }
            }
            return list;
        }

        /// <summary>
        /// 从小到大。(只针对与数组的数量)
        /// </summary>
         static void sort(ArrayList<int[]> list)
        {
            for (int i = 0; i < list.count; i++)
            {
                for (int j = i; j < list.count; j++)
                {
                    if (list.get(i).Length > list.get(j).Length)
                    {
                        int[] temp = list.get(i);
                        list.set(list.get(j), i);
                        list.set(temp, j);
                    }
                }
            }
        }

         static int[] arrayAdd(int[] a,int[] b)
        {
            int count = a.Length + b.Length;

            int[] bb=new int[count];
            for (int i = 0; i < a.Length; i++)
            {
                bb[i] = a[i];
            }
            
            for (int i = a.Length; i < count; i++)
            {
                bb[i] = b[i-a.Length];
            }

            return bb;

        }


         static int[] removeSize(ArrayList<int[]> list,int size)
        {
            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).Length == size)
                {
                    return list.removeAt(i);
                }
            }
            return null;
        }

         static ArrayList<int[]> removeSizes(ArrayList<int[]> list,int size)
        {
            ArrayList<int[]> ll=new ArrayList<int[]>();

            for (int i = list.count-1; i >=0; i--)
            {
                if (list.get(i).Length == size)
                {
                    ll.add(list.removeAt(i));
                }
            }
            return ll;
        }

        /// <summary>
        /// 获取相同面值牌的数量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static int getCardNum(ArrayList<int[]> list)
        {
            int num = 0;
            for (int i = 0; i < list.count; i++)
            {
                num += list.get(i).Length;
            }

            return num;
        }

        /// <summary>
        /// 获取有两对牌
        /// </summary>
        /// <returns></returns>
        static ArrayList<int> getTwoPairsCard(ArrayList<int[]> list)
        {
            ArrayList<int> ll = new ArrayList<int>();
            int index = 0;
            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).Length != 2) continue;
                index++;
            }

            if (index < 2) return ll;
            index = 0;
            for (int i = list.count-1; i >=0; i--)
            {
                if(index==2) break;
                if (list.get(i).Length != 2) continue;
                ll.add(list.removeAt(i));
                index++;
            }

            return ll;
        }

    }
}
