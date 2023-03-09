using cambrian.common;

namespace basef.prop
{
    public class Prop : Entity
    {
        /** 相同SID前提下唯一ID，默认等于SID */
        public int uuid;
        /** 道具类型：如任务道具，消耗品，装备，材料等等 */
        public int type;
        /** 数量：非XML配置字段 */
        public int count;
        /** 每组最大数量：超过该数量，将建立新对象，并占用一个储物空间 */
        public int stackSize;
        /// <summary>
        /// 出售价格
        /// </summary>
        public int salePrice;


        public override void init()
        {
            base.init();
            if (stackSize <= 0)
                stackSize = int.MaxValue;
        }


        /** 获得此道具类型 */
        public int getType()
        {
            return type;
        }

        /**
         * 是否是指定类型的道具（注意：不限于指定类型）
         * 
         * <pre>
         *    例如： 道具可以是绑定，唯一，可以用的类型
         * </pre>
         * 
         * @param type 指定类型
         * @return TRUE 是指定类型，FALSE 不是指定类型
         */
        public bool isType(int type)
        {
            return StatusKit.hasStatus(this.type, type);
        }

        public bool isBind()
        {
            return StatusKit.hasStatus(type, PropType.BIND);
        }

        /// <summary>
        /// 是否是唯一道具
        /// </summary>
        /// <returns></returns>
        public bool isUnique()
        {
            return StatusKit.hasStatus(type, PropType.UNIQUE);
        }

        /// <summary>
        /// 是否是功能道具，可以使用的道具
        /// </summary>
        /// <returns></returns>
        public bool isFunctional()
        {
            return StatusKit.hasStatus(type, PropType.FUNCTIONAL);
        }

        /// <summary>
        /// 是否是不可摧毁的道具
        /// </summary>
        /// <returns></returns>
        public bool isIndestructible()
        {
            return StatusKit.hasStatus(type, PropType.INDESTRUCTIBLE);
        }

        /// <summary>
        /// 是否是不可回收的道具（不能出售给系统）
        /// </summary>
        /// <returns></returns>
        public bool isUnrecyclable()
        {
            return StatusKit.hasStatus(type, PropType.UNRECYCLABLE);
        }

        public override string getDescription()
        {
            if (isUnrecyclable()) return description;
            else
                return base.getDescription() + "\n出售给系统单价:" + salePrice;
        }

        /// <summary>
        /// 是否可堆叠：默认不可堆叠
        /// </summary>
        /// <returns></returns>
        public bool isStackable()
        {
            return stackSize > 1;
        }

        /// <summary>
        /// 是否已经堆叠满
        /// </summary>
        /// <returns></returns>
        public bool isStackFull()
        {
            return count >= stackSize;
        }

        /** 获得此道具堆叠数量：默认不可堆叠返回1 */
        public int getCount()
        {
            return count;
        }

        /** 设置道具数量 */
        public void setCount(int count)
        {
            this.count = count;
        }


        /** 数量增加：该组道具实际增加数 */
        public int incrCount(int value)
        {
            if (this.count >= stackSize) return 0;
            if (this.count + value > stackSize)
            {
                value = stackSize - this.count;
                this.count = stackSize;
            }
            else
            {
                this.count += value;
            }
            return value;
        }

        /** 数量减少：返回实际减少数 */
        public int decrCount(int value)
        {
            if (this.count < value)
            {
                value = this.count;
                this.count = 0;
            }
            else
            {
                this.count -= value;
            }
            return value;
        }


        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readInt();
            this.type = data.readInt();
            this.count = data.readInt();
        }
    }
}
