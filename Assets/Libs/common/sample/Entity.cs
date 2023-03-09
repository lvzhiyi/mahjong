using System.Text.RegularExpressions;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Entity : Sample
    {

        /// <summary>
        /// 名字，请使用getName()
        /// </summary>
        public string name;

        /// <summary>
        /// 介绍，请使用getInfo();
        /// </summary>
        public string info;

         /// <summary>
        /// 描述
        /// </summary>
        public string description;

        /// <summary>
        /// 图片路径，请使用方法getSprite()
        /// </summary>
        public int img;
        /// <summary> 注释信息: 只是为了方便查看，不是具体数据，也不参与显示 </summary>
        public string notes;

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <returns></returns>
        public virtual string getName()
        {
            return LangKit.trans(this.name);
        }

        public virtual string getDescription()
        {
            return description;
        }

        /// <summary>
        /// 获取介绍
        /// </summary>
        /// <returns></returns>
        public virtual string getInfo()
        {
            return LangKit.trans(this.info);
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <returns></returns>
        public virtual Sprite getSprite()
        {
            return null;
        }

        /// <summary>
        /// 获取背景图标
        /// </summary>
        /// <returns></returns>
        public virtual Sprite getBgSprite()
        {
            return null;
        }

        public virtual Color32 getColor32()
        {
            return Color.white;
        }

        public virtual string getColorStr()
        {
            return ColorKit.toHex(255, 255, 255, 1);
        }

        public virtual long getValue()
        {
            throw new DataAccessException("override in subclass");
        }

        public virtual void setValue(long value)
        {
            throw new DataAccessException("override in subclass");
        }

        public override void init()
        {
            base.init();
            if (this.name != null && (!this.name.Equals("")))
                this.name = Regex.Split(this.name, "!#")[0];
            if (this.info != null && (!this.info.Equals("")))
                this.info = Regex.Split(this.info, "!#")[0];
        }

        public virtual string getToString()
        {
            return "";
        }
    }
}
