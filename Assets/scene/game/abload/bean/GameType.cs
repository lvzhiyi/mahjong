using basef.arena;
using cambrian.common;
using System;

namespace scene.game
{
    /// <summary>
    /// 游戏种类
    /// </summary>
    [Serializable]
    public class GameType:BytesSerializable
    {
        /// <summary>
        /// 种类名称
        /// </summary>
        public string name;
        /// <summary>
        /// 规则id
        /// </summary>
        public int[] ruleid;
        /// <summary>
        /// 金币场id
        /// </summary>
        public int[] goldid;
        /// <summary>
        /// 竞赛场锁定规则
        /// </summary>
        private ArrayList<ArenaLockRule> arenLockRule;

#if UNITY_EDITOR
        [NonSerialized]
        /// <summary> 编辑器使用 </summary>
        public bool open;
#endif

        public void addArenaLockRule(ArenaLockRule[] lockRule)
        {
            if(arenLockRule==null)
                arenLockRule=new ArrayList<ArenaLockRule>();
            arenLockRule.add(lockRule);
        }

        public void addArenaLockRule(ArenaLockRule lockRule)
        {
            if (arenLockRule == null)
                arenLockRule = new ArrayList<ArenaLockRule>();
            arenLockRule.add(lockRule);
        }

        public ArrayList<ArenaLockRule> getArenaLockRule()
        {
            return arenLockRule;
        }

        public bool isExistRule(int rule)
        {
            for (int i = 0; i < ruleid.Length; i++)
            {
                if (ruleid[i] == rule) return true;
            }
            return false;
        }

        public GameType clone()
        {
            GameType type=new GameType();
            type.name = name;
            type.ruleid = ruleid;
            type.goldid =ruleid;
            return type;
        }
    }
}
