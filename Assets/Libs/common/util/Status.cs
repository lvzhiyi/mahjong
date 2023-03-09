using System.Timers;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 帧频和内存使用情况
    /// </summary>
    public class Status : MonoBehaviour
    {
        /** 内存 */
        public string memory;
        /** 帧频 */
        public int fps;
        /** 帧频 */
        public string sfps;

        void Start()
        {
            Timer heartTimer = new Timer();
            heartTimer.Interval = 1000;
            heartTimer.Elapsed += new ElapsedEventHandler(this.heart);
            heartTimer.Enabled = true;
        }

        void Update()
        {
            this.fps++;
        }


        /** 显示帧频 */
        /** 心跳 */

        public void heart(object obj, ElapsedEventArgs e)
        {
            this.sfps = " fps=" + this.fps;
            this.fps = 0;
            //showMemory();
        }

        void OnGUI()
        {
            GUI.depth = -100;
            GUI.skin.label.fontSize = 20;
            GUI.skin.label.normal.textColor = Color.red;
            GUI.Label(new Rect(100, 0, 640, 500), this.sfps + this.memory);
        }
    }
}