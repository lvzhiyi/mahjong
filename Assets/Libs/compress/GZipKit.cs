using cambrian.common;
using System.IO;
using Unity.IO.Compression;

namespace Libs
{
    public class GZipKit
    {
        public static ByteBuffer unDecompress(byte[] bytes)
        {
            // 解压
            ByteBuffer data = new ByteBuffer();
            using (MemoryStream memory = new MemoryStream(bytes))
            {
                using (GZipStream stream = new GZipStream(memory, CompressionMode.Decompress))
                {
                    byte[] buf = new byte[1024];
                    int l = 0;
                    while ((l = stream.Read(buf, 0, buf.Length)) > 0)
                    {
                        data.write(buf, 0, l);
                    }
                    stream.Flush();
                    stream.Close();

                }
                memory.Flush();
                memory.Close();
            }
            return data;

        }
    }
}
