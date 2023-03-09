using cambrian.common;
using scene.game;
using basef.prop;
using UnityEngine;
using UnityEngine.UI;

namespace basef.bag
{
    public class PropUseSuccessBar : UnrealLuaSpaceObject
    {
        /// <summary> bar的宽度</summary>
        float width;

        /// <summary> 高度 </summary>
        float height;

        /// <summary> 每行显示的个数 </summary>
        public int count = 5;

        /// <summary> 总共的个数 </summary>
        int total;

        Image img;

        Text num;

        Prop prop;

        protected override void xinit()
        {
            Rect rect = this.transform.GetComponent<RectTransform>().rect;
            width = rect.width;
            height = rect.height;
            img = this.transform.Find("img").GetComponent<Image>();
            num = this.transform.Find("num").GetComponent<Text>();
        }

        public void setProp(Prop prop)
        {
            this.prop = prop;
        }

        public void setTotal(int total)
        {
            this.total = total;
        }

        protected override void xrefresh()
        {
            float x = 0;
            int cols = index_space % count;
            if (total >= count)//多行
            {
                if (cols == 2)
                    x = 0;
                else if (cols < 2)
                {
                    x = -(2 - cols) * width;
                }
                else
                {
                    x = (cols - 2) * width;
                }
            }
            else
            {
                if (total == 1 || (total == 3 && index_space == 1))
                    x = 0;
                else if (total == 2 || total == 4)
                {
                    x = (width / 2 + width * (cols >= total / 2 ? cols - total / 2 : cols)) * (cols >= (total / 2) ? 1 : -1);
                }
                else if (total == 3)
                {
                    x = (cols - 1) * width;
                }
            }

            float y = 0;
            if (total > count)//多行
            {
                int line = index_space / count;
                if (line == 0)
                {
                    y = height / 2;
                }
                else
                {
                    y = -height * line + height / 2;
                }
            }
            else
            {
                y = 0;
            }

            DisplayKit.setLocalXY(this.transform,x,y);

            img.sprite = CacheManager.shopimg[prop.img];
            num.text = "X" + MathKit.getDecimal(prop.count);
        }
    }
}
