using cambrian.uui;
using UnityEngine;

namespace cambrian.common
{
    /**
	 * 类说明：
	 * 
	 * @version 1.0
	 * @author maxw<woldits@qq.com>
	 */
    public class AudioManager : MonoBehaviour
    {

        /* static fields */
        /** 日志记录 */
        private static Logger log = LogFactory.getLogger<AudioManager>();

        private static AudioManager manager;

        /// <summary>声音状态</summary>
        public static bool musicState = false;

        /* static methods */

        /* fields */
        /// <summary>
        /// 
        /// </summary>
        ArrayList<string> keys = new ArrayList<string>();
        /// <summary>
        /// 声音
        /// </summary>
        ArrayList<AudioSource> audios = new ArrayList<AudioSource>();

        /* constructors */

        /* properties */

        /* init start */
        void Start()
        {
            manager = this;
        }


        /* methods */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loop"></param>
        private static AudioSource put(string key, bool loop)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage("put,key=" + key + ",loop=" + loop));
            AudioSource audioSource = put(key);
            audioSource.loop = loop;
            return audioSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static AudioSource put(string key)
        {
            AudioSource audioSource = get(key);
            if (audioSource == null)
            {
                audioSource = manager.gameObject.AddComponent<AudioSource>();
                Url url = new Url(Url.RES, key,null);
                Loader.getLoader().load<string, AudioClip>(url, key, (k, s) =>
                {
                    manager.keys.add(k);
                    manager.audios.add(audioSource);
                });
            }
            return audioSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static AudioSource get(string key)
        {
            for (int i = 0; i < manager.keys.count; i++)
            {
                if (Equals(key, manager.keys.get(i)))
                    return (AudioSource) manager.audios.get(i);
            }
            return null;
        }
        /// <summary>
        /// 播放一次
        /// </summary>
        /// <param name="key"></param>
        public static void play(string key)
        {
            play(key, false);
        }
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loop">是否循环</param>
        public static void play(string key, bool loop)
        {
            put(key, loop);
            if (musicState)
                return;
            AudioSource audioSource = get(key);
            audioSource.Play();
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="key"></param>
        public static void stop(string key)
        {
            AudioSource audioSource = get(key);
            if (audioSource == null)
                return;
            audioSource.Stop();
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="key"></param>
        public static void pause(string key)
        {
            AudioSource audioSource = get(key);
            audioSource.Pause();
        }
        /// <summary>
        /// 静音
        /// </summary>
        /// <param name="b"></param>
        public static void muteMusic(bool b)
        {
            for (int i = 0; i < manager.keys.count; i++)
            {
                if (((string) manager.keys.get(i)).StartsWith("music"))
                {
                    ((AudioSource) manager.audios.get(i)).mute = b;
                }
            }
            musicState = b;
        }

        public static void getMuteMusic()
        {

        }

        public static void muteSound(bool b)
        {
            for (int i = 0; i < manager.keys.count; i++)
            {
                if (((string) manager.keys.get(i)).StartsWith("sound"))
                {
                    ((AudioSource) manager.audios.get(i)).mute = b;
                }
            }
            musicState = b;
        }

        public static void setSoundVolume(float value)
        {
            for (int i = 0; i < manager.keys.count; i++)
            {
                if (((string) manager.keys.get(i)).StartsWith("sound"))
                {
                    ((AudioSource) manager.audios.get(i)).volume = value;
                }
            }
        }

        public static void setMusicVolume(float value)
        {
            for (int i = 0; i < manager.keys.count; i++)
            {
                if (((string) manager.keys.get(i)).StartsWith("music"))
                {
                    ((AudioSource) manager.audios.get(i)).volume = value;
                }
            }
        }
        /* common methods */
    }
}