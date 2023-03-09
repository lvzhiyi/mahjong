using scene.game;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.over
{
    public class AllOverPointBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 当分数为0时，传入该值
        /// </summary>
        public static int Zero = -1;

        [HideInInspector] public Image bg;

        [HideInInspector] public int number;
        /// <summary>
        /// 是否是正数
        /// </summary>
        [HideInInspector] public bool isPositive;

        protected override void xinit()
        {
            this.bg = this.transform.Find("bg").GetComponent<Image>();
        }

        /// <summary>
        /// 数字，是否大于0
        /// </summary>
        /// <param name="number"></param>
        /// <param name="isPositive"></param>
        public void setNumber(int number,bool isPositive)
        {
            this.number = number;
            this.isPositive = isPositive;
        }

        /// <summary>
        /// 说明：正数0-9对应的索引(1-10，+：11)，负数0-9对应的索引(12-21，-：22)
        /// </summary>
        protected override void xrefresh()
        {
            this.bg.sprite=getSprite(number);
        }

        public Sprite getSprite(int number)
        {
            if (isPositive)
            {
                switch (number)
                {
                    case 0:
                        return CacheManager.common[1];
                    case 1:
                        return CacheManager.common[2];
                    case 2:
                        return CacheManager.common[3];
                    case 3:
                        return CacheManager.common[4];
                    case 4:
                        return CacheManager.common[5];
                    case 5:
                        return CacheManager.common[6];
                    case 6:
                        return CacheManager.common[7];
                    case 7:
                        return CacheManager.common[8];
                    case 8:
                        return CacheManager.common[9];
                    case 9:
                        return CacheManager.common[10];
                    case 10://+图片
                        return CacheManager.common[11];
                }
            }
            else
            {
                switch (number)
                {
                    case 0:
                        return CacheManager.common[12];
                    case 1:
                        return CacheManager.common[13];
                    case 2:
                        return CacheManager.common[14];
                    case 3:
                        return CacheManager.common[15];
                    case 4:
                        return CacheManager.common[16];
                    case 5:
                        return CacheManager.common[17];
                    case 6:
                        return CacheManager.common[18];
                    case 7:
                        return CacheManager.common[19];
                    case 8:
                        return CacheManager.common[20];
                    case 9:
                        return CacheManager.common[21];
                    case 10://-图片
                        return CacheManager.common[22];
                }
            }

            return CacheManager.common[1];//如果总分数为0，number==-1
        }
    }
}
