using System;
using System.Collections;
using UnityEngine;

namespace QFrameWork
{
    public abstract class BaseProcedure:Object
    {
        public static QBehaviour qBehaviour;

        public Msg CreateProcedureEnter(string str)
        {
            return this.proceduceManager.CreateProcedureEnter(str);
        }

        public Msg CreateProcedureLeave(string str)
        {
            return this.proceduceManager.CreateProcedureLeave(str);
        }

        public Widget LoadWidget(string widget)
        {
            return this.uiManager.LoadWidget(widget);
        }

        public Coroutine StartCoroutine(IEnumerator coutine)
        {
            return qBehaviour.StartCoroutine(coutine);
        }

        public void StopAllCoroutines()
        {
            qBehaviour.StopAllCoroutines();
        }

        public void StopCoroutine(IEnumerator routine)
        {
            qBehaviour.StopCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            qBehaviour.StopCoroutine(routine);
        }

        public void StopCoroutine(string methodName)
        {
            qBehaviour.StopCoroutine(methodName);
        }

        public virtual void OnLoad() { }

        public virtual void OnAwake() { }

        public virtual void OnEnter(Msg msg) { }

        public virtual void OnLeave(Msg msg) { }

        public virtual void OnUnload() { }
    }
}
