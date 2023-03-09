using System;
using System.Collections;
using UnityEngine;
using XLua;

namespace scene.game
{
    /// <summary>
    /// xlua用的
    /// </summary>
    [LuaCallCSharp]
    public class Coroutine_Runner:MonoBehaviour
    {
        public void YieldAndCallback(object to_yield, Action callback)
        {
            StartCoroutine(CoBody(to_yield, callback));
        }

        private IEnumerator CoBody(object to_yield, Action callback)
        {
            if (to_yield is IEnumerator)
                yield return StartCoroutine((IEnumerator)to_yield);
            else
                yield return to_yield;
            callback();
        }
    }
}
