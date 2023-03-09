using System;
using UnityEngine;

namespace cambrian.common
{
    public class AudioClipKit
    {
        /// <summary>
        /// AudioClip转换成byte[]
        /// </summary>
        /// <param name="clip"></param>
        /// <returns>压缩过的byte </returns>
        public static byte[] audioToBytes(AudioClip clip,int lastpostion)
        {

            //float[] data = new float[lastpostion * clip.channels];
            float[] data = new float[lastpostion * clip.channels*4];
            

            clip.GetData(data, 0);

            byte[] bytes = new byte[data.Length * 4];

            Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

            //return GZipKit.Compress(bytes);

            return bytes;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static AudioClip bytesToAudio(AudioClip clip, byte[] bytes)
        {
           // Alert.show("abc");
          //  bytes= GZipKit.Decompress(bytes);

            float[] data = new float[bytes.Length / 4];
            Buffer.BlockCopy(bytes, 0, data, 0, data.Length);

            clip.SetData(data, 0);

            return clip;
        }
    }
}