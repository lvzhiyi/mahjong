using cambrian.common;
using cambrian.uui;
using platform;
using platform.mahjong;
using platform.poker;
using scene.game;
using System;
using UnityEngine;
using XLua;

namespace cambrian.game
{

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager manager;

        private const string MUSIC = "sound/bgm1";

        public const int MAN = 1;

        public const int WOMAN = 2;

        /// <summary>
        /// 背景音乐开关
        /// </summary>
        private static bool isMusicMute = false;
        /// <summary>
        /// 音效开关
        /// </summary>
        private static bool isSoundMute = false;

        /// <summary>
        /// 当前是否在播放语音 0表示播放 1表示没播放
        /// </summary>
        private static int isVoice = 0;

        private static float musicVolume = 1.0f;
        private static float soundVolume = 1.0f;

        private static AudioSource music;

        private static long lasttime;
        /// <summary>
        /// 录音时间
        /// </summary>
        public static int maxRecordTime=10;

        public static bool allMute = false;

        /* fields */

        /// <summary>
        /// 声音
        /// </summary>
        private ArrayList<AudioSource> audios = new ArrayList<AudioSource>();

        /// <summary>
        /// 语音信息列表
        /// </summary>
        public ArrayList<SoundIndex> voices = new ArrayList<SoundIndex>();

        public static void byteRead(ByteBuffer data)
        {
            isMusicMute = data.readBoolean();
            isSoundMute = data.readBoolean();
            musicVolume = data.readFloat();
            soundVolume = data.readFloat();
        }

        public static void byteWrite(ByteBuffer data)
        {
            data.writeBoolean(isMusicMute);
            data.writeBoolean(isSoundMute);
            data.writeFloat(musicVolume);
            data.writeFloat(soundVolume);
        }

        public static void setAllMute(bool b)
        {
            allMute = b;
        }

        public static void load()
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.SOUND_INFO_PATH, true);
            if (data != null)
                byteRead(data);
            init();
        }

        public static void init()
        {
            music = manager.GetComponent<AudioSource>();
            Url url = new Url(Url.RES, MUSIC, null);
            Loader.getLoader().load<AudioSource, AudioClip>(url, music, (param, s) =>
            {
                param.clip = s;
                param.loop = true;
                param.volume = musicVolume;
                param.mute = true;
            });

            
        }

        public static void playBackMusic()
        {
            music.mute = false;
        }

        /// <summary> 保存用户数据 </summary>
        public static void save()
        {
            ByteBuffer data = new ByteBuffer();
            byteWrite(data);
            FileCachesManager.savePath(CacheLocalPath.SOUND_INFO_PATH, true, data);
        }

        public static void setMusicVolume(float volume)
        {
            musicVolume = volume;
            music.volume = musicVolume;
        }

        public static void setSoundVolume(float volume)
        {
            soundVolume = volume;
        }

        public static float getMusicVolume()
        {
            return musicVolume;
        }

        public static float getSoundVolume()
        {
            return soundVolume;
        }

        public static void setSoundMute()
        {
            isSoundMute = !isSoundMute;
        }


        public static void doMusicMute()
        {
            isMusicMute = !isMusicMute;
            setMusicMute();
        }

        private static void setMusicMute()
        {
            if (isMusicMute ||allMute)
                music.Pause();
            else
            {
                music.volume = musicVolume*isVoice;
                if (music.isPlaying)
                    return;
                music.Play();
            }
        }

        public static bool getSoundMute()
        {
            return isSoundMute;
        }

        public static bool getMusicMute()
        {
            return isMusicMute;
        }

        private void Awake()
        {
            manager = this.GetComponent<SoundManager>();
        }


        #region 长牌

        public static void playChangPai(int sex, string card, int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_" + card + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_" + card + (soundType != 0 ? "_1" : ""));
            }
        }

        /// <summary>
        /// 播放短语
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="card"></param>
        public static void playPhrase(int sex, int index)
        {
            index++;
            playSound("sound/man/fix_msg_" + index);
        }

        public static void playHu(int sex, int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_hu" + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_hu" + (soundType != 0 ? "_1" : ""));
            }
        }

        /// <summary>
        /// 播放垮
        /// </summary>
        /// <param name="sex"></param>
        public static void playKua(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_kua");
            }
            else
            {
                playSound("sound/woman/w_kua");
            }
        }

        /// <summary>
        /// 播放挡
        /// </summary>
        /// <param name="sex"></param>
        public static void playDang(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_dang");
            }
            else
            {
                playSound("sound/woman/w_dang");
            }
        }

        /// <summary>
        /// 播放偷
        /// </summary>
        /// <param name="sex"></param>
        public static void playTou(int sex,int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_tou" + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_tou" + (soundType != 0 ? "_1" : ""));
            }
        }

        /// <summary>
        /// 播放暗
        /// </summary>
        /// <param name="sex"></param>
        public static void playAn(int sex, int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_an" + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_an" + (soundType != 0 ? "_1" : ""));
            }
        }


        /// <summary>
        /// 碰
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="soundType"></param>
        public static void playPeng(int sex, int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_peng" + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_peng" + (soundType != 0 ? "_1" : ""));
            }
        }

        /// <summary>
        /// 扒
        /// </summary>
        public static void playBa(int sex,int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_ba" + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_ba" + (soundType != 0 ? "_1" : ""));
            }
        }

        /// <summary>
        /// 报牌
        /// </summary>
        /// <param name="sex"></param>
        public static void playBaoPai(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_baopai");
            }
            else
            {
                playSound("sound/woman/w_baopai");
            }
        }

        /// <summary>
        /// 吃成坎
        /// </summary>
        /// <param name="sex"></param>
        public static void playChiChengKan(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_chichengkan");
            }
            else
            {
                playSound("sound/woman/w_chichengkan");
            }
        }

        /// <summary>
        /// 吃吐
        /// </summary>
        /// <param name="sex"></param>
        public static void playChiTu(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_chitu");
            }
            else
            {
                playSound("sound/woman/w_chitu");
            }
        }

        /// <summary>
        /// 掉坎
        /// </summary>
        /// <param name="sex"></param>
        public static void playDiaoKan(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_diaokan");
            }
            else
            {
                playSound("sound/woman/w_diaokan");
            }
        }

        /// <summary>
        /// 撕对
        /// </summary>
        /// <param name="sex"></param>
        public static void playShiDui(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_shidui");
            }
            else
            {
                playSound("sound/woman/w_shidui");
            }
        }

        /// <summary>
        /// 偷成4根
        /// </summary>
        /// <param name="sex"></param>
        public static void playTouCheng4Gen(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_toucheng4gen");
            }
            else
            {
                playSound("sound/woman/w_toucheng4gen");
            }
        }

        /// <summary>
        /// 招吃
        /// </summary>
        /// <param name="sex"></param>
        public static void playZhaoChi(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_zhaochi");
            }
            else
            {
                playSound("sound/woman/w_zhaochi");
            }
        }
        ///<summary>
        /// 招碰
        /// </summary>
        /// <param name="sex"></param>
        public static void playZhaoPeng(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/man/m_zhaopeng");
            }
            else
            {
                playSound("sound/woman/w_zhaopeng");
            }
        }

        public static void playChi(int sex,int soundType)
        {
            if (sex == MAN)
            {
                playSound("sound/man/" + soundType + "/m_chi" + (soundType != 0 ? "_1" : ""));
            }
            else
            {
                playSound("sound/woman/" + soundType + "/w_chi" + (soundType != 0 ? "_1" : ""));
            }
        }

        #endregion

        #region 麻将
        /// <summary>
        /// 播放麻将声音
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="card"></param>
        public static void playMJCard(int sex,int card)
        {
            int type = MJCard.getType(card);
            int index = MJCard.getIndex(card);
            if (sex == MAN)
            {
                playSound("sound/mj/man/mj_"+type+"_"+index);
            }
            else
            {
                playSound("sound/mj/woman/mj_" + type + "_" + index);
            }
        }

        /// <summary>
        /// 播放麻将声音
        /// </summary>
        /// <param name="name">麻将名称简写</param>
        /// <param name="sex"></param>
        /// <param name="card"></param>
        public static void playNormalMJCard(string name,int sex,int card,int count=1)
        {
            int type = MJCard.getType(card);
            int index = MJCard.getIndex(card);
            if (count > 1)
                count = MathKit.random(0, count);
            string path = "sound/" + name;
            if (sex == MAN)
            {
                path += "/man/" + name + "_" + type + "_" + index;
                if (count > 0)
                    path += "_" + (count-1);
            }
            else
            {
                path += "/woman/" + name + "_" + type + "_" + index;
                if (count > 0)
                    path += "_" + (count - 1);
            }
            playSound(path);
        }

        /// <summary>
        /// 播放碰
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="name"></param>
        public static void playMJNormalChi(int sex, string name, int count = 1)
        {
            if (count > 1)
                count = MathKit.random(1, count + 1);
            if (sex == MAN)
            {
                playSound("sound/" + name + "/man/" + name + "_chi" + count);
            }
            else
            {
                playSound("sound/" + name + "/women/" + name + "_chi" + count);
            }
        }

        /// <summary>
        /// 播放碰
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="name"></param>
        public static void playMJNormal(int sex, string name,string sufffix)
        {
            if (sex == MAN)
            {
                playSound("sound/" + name + "/man/" + name + "_"+ sufffix);
            }
            else
            {
                playSound("sound/" + name + "/women/" + name + "_"+ sufffix);
            }
        }

        /// <summary>
        /// 播放碰
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="name"></param>
        public static void playMJNormalPong(int sex,string name, int count = 1)
        {
            if (count > 1)
                count = MathKit.random(1,count+1);
            if (sex == MAN)
            {
                playSound("sound/"+name+"/man/"+name+"_peng"+count);
            }
            else
            {
                playSound("sound/" + name + "/women/" + name + "_peng"+count);
            }
        }

        /// <summary>
        /// 播放gang
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="name"></param>
        public static void playMJNormalKong(int sex, string name,int count= 1)
        {
            if (count > 1)
                count = MathKit.random(1, count + 1);
            if (sex == MAN)
            {
                playSound("sound/" + name + "/man/" + name + "_gang"+count);
            }
            else
            {
                playSound("sound/" + name + "/women/" + name + "_gang"+count);
            }
        }

        /// <summary>
        /// 播放胡
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="name"></param>
        public static void playMJNormalHu(int sex, string name,int count=1)
        {
            if (count > 1)
                count = MathKit.random(1, count + 1);
            if (sex == MAN)
            {
                playSound("sound/" + name + "/man/" + name + "_hu"+count);
            }
            else
            {
                playSound("sound/" + name + "/women/" + name + "_hu"+count);
            }
        }

        /// <summary>
        /// 播放自摸
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="name"></param>
        public static void playMJNormalSelfHu(int sex, string name, int count = 1)
        {
            if (count > 1)
                count = MathKit.random(1, count + 1);
            if (sex == MAN)
            {
                playSound("sound/" + name + "/man/" + name + "_zimo"+count);
            }
            else
            {
                playSound("sound/" + name + "/women/" + name + "_zimo"+count);
            }
        }

        /// <summary>
        /// 播放碰
        /// </summary>
        /// <param name="sex"></param>
        public static void playMJPong(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/mj/man/peng");
            }
            else
            {
                playSound("sound/mj/woman/peng");
            }
        }



        /// <summary>
        /// 播放杠
        /// </summary>
        /// <param name="sex"></param>
        public static void playMJGang(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/mj/man/gang");
            }
            else
            {
                playSound("sound/mj/woman/gang");
            }
        }

        /// <summary>
        /// 播放点炮胡
        /// </summary>
        /// <param name="sex"></param>
        public static void playMJHu(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/mj/man/hu");
            }
            else
            {
                playSound("sound/mj/woman/hu");
            }
        }

        /// <summary>
        /// 播放自摸
        /// </summary>
        /// <param name="sex"></param>
        public static void playMJSelfHu(int sex)
        {
            if (sex == MAN)
            {
                playSound("sound/mj/man/zimo");
            }
            else
            {
                playSound("sound/mj/woman/zimo");
            }
        }

        /// <summary>
        /// 播放麻将效果音效
        /// </summary>
        /// <param name="name"></param>
        public static void playMJEffect(string name)
        {
            playSound("sound/mj/common/audio_"+name);
        }

        /// <summary>
        /// 播放短语
        /// </summary>
        /// <param name="sex"></param>
        /// <param name="card"></param>
        public static void playMJPhrase(int sex, int index)
        {
            index++;
            if (sex == MAN)
            {
                playSound("sound/mj/man/mj_fix_msg_" + index);
            }
            else
            {
                playSound("sound/mj/woman/mj_fix_msg_" + index);
            }
        }

        #endregion

        #region 扑克

        public const int cardselect = 0;
        public const int showcard = 1;
        public const int baojing1 = 2;
        public const int baojing2 = 3;
        public const int buyao = 4;
        public const int call_no = 5;
        public const int call_yes = 6;
        public const int grab_no = 7;
        public const int grab_yes = 8;
        public const int dani = 9;
        public const int warning = 10;
        public const int jiaofen_1 = 11;
        public const int jiaofen_2 = 12;
        public const int jiaofen_3 = 13;
        public const int fapai = 14;
        /// <summary>
        /// 陵水13张警报
        /// </summary>
        public const int ls13jingbao = 15;

        public static bool pkSpecial = true;

        /// <summary> 播放其他声音 </summary>
        public static void playPKOther(int type, int sex)
        {
            string str = "";
            switch (type)
            {
                case cardselect:
                    str = string.Concat("cardselect");
                    break;
                case showcard:
                    str = string.Concat("showcard");
                    break;
                case baojing1:
                    WXManager.getInstance().vibrator(1000);//震动
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "baojing_1");
                    playSound(string.Concat("sound/pk/other/warning"));
                    break;
                case baojing2:
                    WXManager.getInstance().vibrator(1000);//震动
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "baojing_2");
                    playSound(string.Concat("sound/pk/other/warning"));
                    break;
                case buyao:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "buyao_", UnityEngine.Random.Range(0, 5));
                    break;
                case call_no:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "call_no");
                    break;
                case call_yes:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "call_yes");
                    break;
                case grab_no:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "grab_no");
                    break;
                case grab_yes:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "grab_yes");
                    break;
                case dani:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "dani_", UnityEngine.Random.Range(1, 4));
                    break;
                case jiaofen_1:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "fen_", 1);
                    break;
                case jiaofen_2:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "fen_", 2);
                    break;
                case jiaofen_3:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "fen_", 3);
                    break;
                case fapai:
                    str = string.Concat("fapai");
                    break;
                case ls13jingbao:
                    str = string.Concat((sex == MAN ? "man/m_" : "woman/w_"), "ls_baojing1");
                    WXManager.getInstance().vibrator(1000);//震动
                    break;
            }
            if (str.Length != 0) playSound(string.Concat("sound/pk/other/", str));
            
        }

        public static void playPKSpecial(bool firstShowCard, int sex)
        {
            if (!firstShowCard)
            {
                int range = UnityEngine.Random.Range(0, 3);
                if (range == 0)
                {
                    playPKOther(dani, sex);
                    pkSpecial = false; return;
                }
            }
            pkSpecial = true;
        }

        /// <summary> 播放牌声音 </summary>
        public static void playPKCard(int sex, int type, int cards, int rulesid)
        {
            if (!pkSpecial) return;
            string str = "";
            switch (type)
            {
                case PKCardType.TYPE_CARDS_SINGLE:
                    str = string.Concat("pk_", Poker.getCountIndex(cards));
                    break;
                case PKCardType.TYPE_CARDS_DOUBLE:
                    str = string.Concat("dui_", Poker.getCountIndex(cards));
                    break;
                case PKCardType.TYPE_CARDS_THREE:
                    str = string.Concat("tuple_", Poker.getCountIndex(cards));
                    break;
            }
            if (str.Length != 0) playSound(string.Concat("sound/pk/card/", (sex == MAN ? "man/m_" : "woman/w_"), str));
        }

        /// <summary> 播放牌声音 </summary>
        public static void playPKEffect(int type, int sex,int rulesid)
        {
            string str = "";
            switch (type)
            {
                case PKCardType.TYPE_CARDS_BOMB:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_boom_", UnityEngine.Random.Range(1, 3)); break;
                }
                case PKCardType.TYPE_ZHA_WANG:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_boom_king");
                    playSound(string.Concat("sound/pk/effect/", (sex == MAN ? "man/m_" : "woman/w_"), "pk_wangzha"));
                    break;
                }
                case PKCardType.TYPE_4_1: //缺少四带一
                {
                    if (!pkSpecial) return;
                    break;
                }
                case PKCardType.TYPE_4_1_1:
                case PKCardType.TYPE_4_2_2:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_boom_er"); break;
                }
                case PKCardType.TYPE_3_1:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_sandai_one"); break;
                }
                case PKCardType.TYPE_3_2:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_sandai_two"); break;
                }
                case PKCardType.TYPE_CARDS_CONNECT:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_shunzi_one"); break;
                }
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT:
                {
                    if (!pkSpecial) return;
                    str = string.Concat("pk_shunzi_two"); break;
                }
                case PKCardType.TYPE_FEIJI_1:
                case PKCardType.TYPE_FEIJI_2:
                case PKCardType.TYPE_CARDS_THREE_CONNECT:
                case PKCardType.TYPE_CARDS_PLANE:
                {
                    str = string.Concat("pk_plane"); break;
                }
            }
            if (str.Length != 0) playSound(string.Concat("sound/pk/effect/", (sex == MAN ? "man/m_" : "woman/w_"), str));
        }


        #endregion

        public static void playButton()
        {
            if (isSoundMute || allMute) return;
            Url url = new Url(Url.RES, "sound/common/audio_button_click", null);
            Loader.getLoader().load<AudioSource, AudioClip>(url, getAudioSource(), (param, s) =>
            {
                param.clip = s;
                param.volume = soundVolume*isVoice;
                param.Play();
            });
        }

        //音效时效性很短  静音处理 为下一个音效开始不播放
        public static void playSound(string key)
        {
            if (isSoundMute || allMute) return;
            string[] str=StringKit.split(key, '/');

            AudioClip clip= ModuleManager.manager.playAudio(str[str.Length-1]);
            if (clip == null)
            {
                Debug.LogError("未找到指定音频,path=" + str[str.Length - 1]);
                return;
            }
            AudioSource source= getAudioSource();
            source.clip = clip;
            source.volume = soundVolume*isVoice;
            source.Play();
        }

        public static void playClickSound()
        {
            string key = "sound/common/tick";
            if (isSoundMute || allMute) return;
            if (clock == null)
            {
                clock = manager.gameObject.AddComponent<AudioSource>();
                AudioClip t = (AudioClip) Resources.Load(key, typeof (AudioClip));
                clock.clip = t;
            }
            clock.Play();
            clock.volume = soundVolume*isVoice;
        }

        private static AudioSource clock;
        /// <summary>
        /// 落子声音
        /// </summary>
        private static AudioSource point;
        /// <summary>
        /// 播放落子声音
        /// </summary>
        public static void playPointSound()
        {
            string key = "sound/common/point";
            if (isSoundMute || allMute) return;
            if (point == null)
            {
                point = manager.gameObject.AddComponent<AudioSource>();
                AudioClip t = (AudioClip)Resources.Load(key, typeof(AudioClip));
                point.clip = t;
            }
            point.Play();
            point.volume = soundVolume * isVoice;
        }

        #region 道具播放音效

        /// <summary>
        /// 播放鸡蛋道具声音
        /// </summary>
        public static void playEggSound()
        {
            playSound("sound/common/egg");
        }

        /// <summary>
        /// 播放鲜花道具声音
        /// </summary>
        public static void playRoseSound()
        {
            playSound("sound/common/rose");
        }

        /// <summary>
        /// 播放亲吻道具音效
        /// </summary>
        public static void playKissSound()
        {
            playSound("sound/common/kiss");
        }

        /// <summary>
        /// 播放炸弹道具音效
        /// </summary>
        public static void playBoomSound()
        {
            playSound("sound/common/boom");
        }
        #endregion


        public static void stopClickSound()
        {
//            if (clock!=null)
//            {
//                clock.Stop();
//            }
        }

        public static void playVoice(long userid, string url)
        {  
            SoundIndex sound = new SoundIndex();
            sound.userid = userid;
            sound.url = url; 

            manager.voices.add(sound);
        }

        void Update()
        {
            if (music == null) return;
            if (TimeKit.currentTimeMillis < 0) return;
            if (TimeKit.currentTimeMillis - lasttime >= 1000)
            {
                lasttime = TimeKit.currentTimeMillis;
                playVoice();
            }
        }
        private static void playVoice()
        {
            if (manager.voices.count < 1)
            {
                isVoice = 1;
                setMusicMute();
                return;
            }

            long time = TimeKit.currentTimeMillis;

            SoundIndex audio = manager.voices.get(0);
            if (audio.isPlaying)
            {
                if (time >= audio.time)
                {
                    manager.voices.remove(audio);
                    YunVaImManager.yunva.stopPlayRecord();
                    playvoice(audio.userid, false);
                }
                return;
            }

            audio.isPlaying = true;
            audio.time = time + 10000;

            YunVaImManager.yunva.playRecord(audio.url, playOkBack);
            setMusicMute();
            playvoice(audio.userid, true);
        }

        static void playOkBack(bool b)
        {
            SoundIndex audio = manager.voices.get(0);
            setMusicMute();
            playvoice(audio.userid, false);

            if (manager.voices.count>0)
                manager.voices.removeAt(0);

            int count = manager.voices.count;
            if (count > 1)
                isVoice = 0;
            else if (count <= 1)
                isVoice = 1;
        }


        /// <summary>
        /// 接收快捷方式回调
        /// </summary>
        [CSharpCallLua]
        [LuaCallCSharp]
        public static Action<long,bool> voiceBack;

        static void playvoice(long userid, bool b)
        {
            if (voiceBack != null)
            {
                voiceBack(userid, b);
            }
        }
        private static AudioSource getAudioSource()
        {
            for (int i = 0; i < manager.audios.count; i++)
            {
                AudioSource audio = manager.audios.get(i);
                if (audio != null && !audio.isPlaying)
                    return audio;
            }
            return createAudio();
        }
        private static AudioSource createAudio()
        {
            AudioSource audio = manager.gameObject.AddComponent<AudioSource>();
            manager.audios.add(audio);
            return audio;
        }
        private static void clearEmptyAudio()
        {
            for (int i = manager.audios.count - 1; i > -1; i--)
            {
                AudioSource audio = manager.audios.get(i);
                if (!audio.isPlaying)
                {
                    manager.audios.removeLast(); 
                }
            }
        }
    }
}
