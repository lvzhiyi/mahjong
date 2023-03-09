using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    public class ArenaStatistcsBarData : BytesSerializable
    {
        public int[] value = new int[6];

        public override void bytesRead(ByteBuffer data)
        {
            for (int i = 0; i < value.Length; i++)
            {
                value[i] = data.readInt();

               // Debug.Log(value[i]);
            }
        }
    }
}
