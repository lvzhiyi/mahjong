using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    public class MJMatchAllPiaoOverOperate : MJBaseOperate
    {
        /// <summary>
        /// 飘模式信息
        /// 四个字节用法：[是否是甩飘，甩飘第一个值，甩飘第二个值，冗余]
        /// </summary>
        public int mode;

        /** 所有人飘情况（4个字节）：[第一家，第二家，第三家，第四家] */
        public int piaos;

        public MJMatchAllPiaoOverOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex,
            isreplay)
        {

        }

        /** 是否是甩飘 */
        public bool isShuaiPiao()
        {
            return mode != 0;
        }

        /** 获取第一个骰子点数 */
        public int getFistDice()
        {
            return mode & 0xFF;
        }

        /** 获取第二个骰子点数 */
        public int getSecondDice()
        {
            return (mode >> 8) & 0xFF;
        }

        /** 获取两个骰子总计点数 */
        public int getDicePoint()
        {
            return getFistDice() + getSecondDice();
        }

        /** 指定索引的玩家是否飘 */
        public bool hadPiao(int index)
        {
            return (piaos & (1 << index)) != 0;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            mode = data.readInt();
            piaos = data.readInt();

        }
    }
}
