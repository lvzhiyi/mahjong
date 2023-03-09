/* * * * * * * * * * * * * * * * * * * * * * * *
* mqipai
* FileName:         Framework.UI
* Author:           XiNan
* Version:          14.0.1
* UnityVersion:     2018.2.21f1
* Date:             2021-01-29
* Time:             17:18:42
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.UI
{
    //using cambrian.game;
    //using UnityEditor;
    //using Tool = EditorTool;

    //[CustomEditor(typeof(SoundManager), false)]


    //public class UISoundManagerEditor : Base2DEditor
    //{
    //    private SoundManager sound;
    //    private float musicVolume, soundVolume;
    //    private bool isSoundMute, isMusicMute;

    //    protected override void init()
    //    {
    //        if (EditorApplication.isPlaying)
    //        {
    //            sound = (SoundManager)target;
    //            SoundManager.manager = sound;
    //            SoundManager.load();
    //            serObj = new SerializedObject(target);
    //            musicVolume = SoundManager.getMusicVolume();
    //            soundVolume = SoundManager.getSoundVolume();
    //            isSoundMute = SoundManager.getSoundMute();
    //            isMusicMute = SoundManager.getMusicMute();
    //        }
    //    }

    //    protected override void inspector()
    //    {
    //        if (EditorApplication.isPlaying)
    //        {
    //            Tool.BE.Vertical(() =>
    //            {
    //                Tool.AC.Label("Sound Info", GStyleTool.label);
    //                Tool.BE.Horizontal(() =>
    //                {
    //                    soundVolume = SoundManager.getSoundVolume();
    //                    soundVolume = Tool.AC.Slider("音效大小", soundVolume, 0, 1);
    //                });
    //                Tool.BE.Horizontal(() =>
    //                {
    //                    musicVolume = SoundManager.getMusicVolume();
    //                    musicVolume = Tool.AC.Slider("音乐大小", musicVolume, 0, 1);
    //                });
    //                Tool.BE.Horizontal(() =>
    //                {
    //                    SoundManager.allMute = Tool.AC.ToggleLeft("全部静音", SoundManager.allMute);
    //                });
    //                Tool.BE.Horizontal(() =>
    //                {
    //                    isMusicMute = SoundManager.getMusicMute();
    //                    isMusicMute = Tool.AC.ToggleLeft("背景音乐开关", isMusicMute);
    //                });
    //                Tool.BE.Horizontal(() =>
    //                {
    //                    isSoundMute = SoundManager.getSoundMute();
    //                    isSoundMute = Tool.AC.ToggleLeft("音效开关", isSoundMute);
    //                });
    //                Tool.BE.Horizontal(() =>
    //                {
    //                    Tool.AC.ToggleLeft("播放语音中", SoundManager.isVoice == 0);
    //                });

    //            }, GStyleTool.UIContent);
    //        }
    //        Undo.RecordObject(target, "Undo UISoundManagerEditor");
    //    }

    //    protected override void change()
    //    {
    //        EditorUtility.SetDirty(target);
    //        if (EditorApplication.isPlaying)
    //        {
    //            SoundManager.setMusicVolume(musicVolume);
    //            SoundManager.setSoundVolume(soundVolume);
    //            if (isSoundMute != SoundManager.getSoundMute())
    //            {
    //                SoundManager.setSoundMute();
    //            }
    //            if (isMusicMute != SoundManager.getMusicMute())
    //            {
    //                SoundManager.doMusicMute();
    //            }
    //            SoundManager.save();
    //        }
    //    }
    //}
}