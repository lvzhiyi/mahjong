using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using cambrian.common;
using cambrian.unreal;
using cambrian.unreal.scroll;
using DragonBones;
using platform.spotred.room;
using scene.game;
using UnityEngine;
using XLua;

public static class HotfixCfg
{
    [Hotfix]
    public static List<Type> by_property
    {
        get
        {
            Type[] types = Assembly.Load("Assembly-CSharp").GetTypes();
            ArrayList<Type> list = new ArrayList<Type>();
            for (int i = 0; i < types.Length; i++)
            {
                //工具 功能
                if (types[i].Namespace == "cambrian.game")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "cambrian.common")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "Libs")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.poker")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "DragonBones")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "cambrian.uui")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "cambrian.unreal.scroll")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "cambrian.unreal.display")
                {
                    list.add(types[i]);
                }
                //scene
                else if (types[i].Namespace == "scene")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "scene.start")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "scene.game")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "scene.loading")
                {
                    list.add(types[i]);
                }
                //平台
                else if (types[i].Namespace == "platform")
                {
                    list.add(types[i]);
                }
                //平台 - 长牌
                else if (types[i].Namespace == "platform.spotred")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.spotred.waitroom")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.spotred.over")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.spotred.playback")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.spotred.room")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.spotred.voice")
                {
                    list.add(types[i]);
                }
                //平台 - 五子棋
                else if (types[i].Namespace == "platform.wuziqi")
                {
                    list.add(types[i]);
                }
                //功能
                else if (types[i].Namespace == "basef")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.newsrun")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.util")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.teahouse")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.task")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.shop")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.share")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.setting")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.server")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.rule")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.regions")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.recruit")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.rank")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.proxybind")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.prop")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.player")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.notice")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.lxqd")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.luckturn")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.lobby")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.joinroom")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.im")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.help")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.gold")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.getgoldeffect")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.firstloginactive")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.duelgame")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.chat")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.bin")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.bag")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.award")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.authname")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.arena")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "basef.activity")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.mahjong")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.mahjong.guizhou")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.mahjong.hn")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "platform.poker.zpy")
                {
                    list.add(types[i]);
                }
                else if (types[i].Namespace == "Unity.IO.Compression")
                {
                    list.add(types[i]);
                }
            }
            return list.toArray().ToList();
        }

    }



    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>()
    {
        typeof (Action),
        typeof (Func<double, double, double>),
        typeof (Action<string>),
        typeof (Action<string[]>),
        typeof (Action<double>),
        typeof (Action<object>),
        typeof (Action<bool>),
        typeof (Action<int>),
        typeof (Action<ScrollBar, int>),
        typeof (Action<object, object>),
        typeof (Action<byte[]>),
        typeof (Action<string,EventObject>),
        typeof (EventTriggerListener.VoidDelegate),
        typeof (ScrollContainer.Callback),
        typeof (ScrollTableContainer.CallBack),
        typeof (RoomPanel.TouCardOverAction),
        typeof (RoomPanel.TouCardOverAction),
        typeof (PositionTweener.CallBack),
        typeof (PositionTweener.CallObjs),
        typeof (PositionTweener.Back2),
        typeof (Action<string,EventObject>),
        typeof (Action<UnityEngine.EventSystems.PointerEventData>),
        typeof (DragonBones.ListenerDelegate<DragonBones.EventObject>),

        typeof (UnityEngine.Events.UnityAction),
        typeof (System.Collections.IEnumerator),
    };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()
    {
        new List<string>() {"UnityEngine.WWW", "movie"},
#if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
#endif

        new List<string>() {"UnityEngine.Texture2D", "alphaIsTransparency"},
        new List<string>() {"UnityEngine.Security", "GetChainOfTrustValue"},
        new List<string>() {"UnityEngine.CanvasRenderer", "onRequestRebuild"},
        new List<string>() {"UnityEngine.Light", "areaSize"},
        new List<string>() {"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},

#if !UNITY_WEBPLAYER
        new List<string>() {"UnityEngine.Application", "ExternalEval"},
#endif

        new List<string>() {"UnityEngine.Light", "lightmapBakeType"},
        new List<string>() {"UnityEngine.WWW", "MovieTexture"},
        new List<string>() {"UnityEngine.WWW", "GetMovieTexture"},

        new List<string>() {"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
        new List<string>() {"UnityEngine.Component", "networkView"}, //4.6.2 not support
        new List<string>()
        {
            "System.IO.FileInfo",
            "GetAccessControl",
            "System.Security.AccessControl.AccessControlSections"
        },
        new List<string>()
        {
            "System.IO.FileInfo",
            "SetAccessControl",
            "System.Security.AccessControl.FileSecurity"
        },
        new List<string>()
        {
            "System.IO.DirectoryInfo",
            "GetAccessControl",
            "System.Security.AccessControl.AccessControlSections"
        },
        new List<string>()
        {
            "System.IO.DirectoryInfo",
            "SetAccessControl",
            "System.Security.AccessControl.DirectorySecurity"
        },
        new List<string>()
        {
            "System.IO.DirectoryInfo",
            "CreateSubdirectory",
            "System.String",
            "System.Security.AccessControl.DirectorySecurity"
        },
        new List<string>()
        {
            "System.IO.DirectoryInfo",
            "Create",
            "System.Security.AccessControl.DirectorySecurity"
        },
        new List<string>()
        {
            "UnityEngine.MonoBehaviour",
            "runInEditMode"
        },
        new List<string>()
        {
            "System.IO.Directory",
            "CreateDirectory",
            "System.String",
            "System.Security.AccessControl.DirectorySecurity"
        },
    };

    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>()
    {
        typeof (System.Object),
        typeof (UnityEngine.Object),
        typeof (Vector2),
        typeof (Vector3),
        typeof (Vector4),
        typeof (Quaternion),
        typeof (Color),
        typeof (Ray),
        typeof (Bounds),
        typeof (Ray2D),
        typeof (Time),
        typeof (GameObject),
        typeof (Component),
        typeof (Behaviour),
        typeof (UnityEngine.Transform),
        typeof (Resources),
        typeof (TextAsset),
        typeof (Keyframe),
        typeof (AnimationCurve),
        typeof (AnimationClip),
        typeof (MonoBehaviour),
        typeof (ParticleSystem),
        typeof (SkinnedMeshRenderer),
        typeof (Renderer),
        typeof (WWW),
        typeof (WaitForSeconds),
        typeof(GZipStream),
        typeof(Stream),
        typeof(MemoryStream),
        typeof(CompressionMode),
        // typeof(Light),
        typeof (Mathf),
        typeof (List<int>),
        typeof (Action<string>),
        typeof (Action<string[]>),
        typeof (Action<string,EventObject>),
        typeof (Debug),
        typeof (DragonBones.ListenerDelegate<DragonBones.EventObject>),
        typeof (Action<string,EventObject>),

        typeof(DG.Tweening.AutoPlay),
        typeof(DG.Tweening.AxisConstraint),
        typeof(DG.Tweening.Ease),
        typeof(DG.Tweening.LogBehaviour),
        typeof(DG.Tweening.LoopType),
        typeof(DG.Tweening.PathMode),
        typeof(DG.Tweening.PathType),
        typeof(DG.Tweening.RotateMode),
        typeof(DG.Tweening.ScrambleMode),
        typeof(DG.Tweening.TweenType),
        typeof(DG.Tweening.UpdateType),

        typeof(DG.Tweening.DOTween),
        typeof(DG.Tweening.DOVirtual),
        typeof(DG.Tweening.EaseFactory),
        typeof(DG.Tweening.Tweener),
        typeof(DG.Tweening.Tween),
        typeof(DG.Tweening.Sequence),
        typeof(DG.Tweening.TweenParams),
        typeof(DG.Tweening.Core.ABSSequentiable),

        typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),

        typeof(DG.Tweening.TweenCallback),
        typeof(DG.Tweening.TweenExtensions),
        typeof(DG.Tweening.TweenSettingsExtensions),
        typeof(DG.Tweening.ShortcutExtensions),
    };

}

