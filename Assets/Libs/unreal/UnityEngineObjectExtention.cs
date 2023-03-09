using DragonBones;
using System;
using XLua;

[LuaCallCSharp]
[ReflectionUse]
public static class UnityEngineObjectExtention
{
    public static bool IsNull(this UnityEngine.Object o) // 或者名字叫IsDestroyed等等
    {
        return o == null;
    }


    public static ListenerDelegate<DragonBones.EventObject> getDragonBones(Action<string,EventObject> action)
    {
        return new ListenerDelegate<EventObject>(action);
    }
}

