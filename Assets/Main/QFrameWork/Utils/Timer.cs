using System;
using UnityEngine;
using System.Collections;

namespace QFrameWork
{
    public static class Timer
    {
        public static IEnumerator StartTimer(float sceonds,Action<float> onUpdate, Action onComplete)
        {
            float time = sceonds;
            while(true)
            {
                if (time < 0)
                {
                    onComplete();
                }
                else
                {
                    onUpdate(time);
                }
                time = time - UnityEngine.Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
