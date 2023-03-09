using System;

namespace scene.loading
{
    [Serializable]
    public class WhNotice
    {
        /// <summary>
        /// 游戏状态 (1:维护,0:正常)
        /// </summary>
        public int status;
        /// <summary>
        /// 
        /// </summary>
        public string statusNotic;

        /// <summary>
        /// 是否是维护中
        /// </summary>
        /// <returns></returns>
        public bool isWhing()
        {
            return status == 1;
        }
    }
}
