using cambrian.common;
using XLua;

namespace cambrian.unreal.display
{
    /// <summary>
    /// 复选框列表
    /// </summary>
    [Hotfix]
    public class UnrealCheckBoxsList:UnrealTableContainer
    {
        private bool[] states;
        public override void resize(int size)
        {
            base.resize(size);
            states = new bool[size];
        }

        public void selectedIndex(int index)
        {
            for (int i = 0; i < this.size; i++)
            {
                if (this.bars[i] == null) continue;
                if (((IdInterface)this.bars[i]).getId() == index)
                {
                    UnrealCheckObject unrealCheckObject=(UnrealCheckObject) this.bars[i];
                    bool state = unrealCheckObject.getState();
                    states[index] = !state;
                    unrealCheckObject.setState(!state);
                }
            }
        }

        /// <summary>
        /// 获取所有的状态
        /// </summary>
        /// <returns></returns>
        public bool[] getStates()
        {
            return states;
        }

        /// <summary>
        /// 获取选中的索引
        /// </summary>
        /// <returns></returns>
        public int[] getSelectsIndexs()
        {
            ArrayList<int> indexs=new ArrayList<int>();
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i])  indexs.add(i);
            }
            return indexs.toArray();
        }

    }
}
