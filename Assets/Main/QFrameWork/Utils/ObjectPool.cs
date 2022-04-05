using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QFrameWork
{
    public class ObjectPool<T>
    {
        readonly Stack<T> m_Stack = new Stack<T>();
        readonly Func<T>  m_ActionOnGet;
        readonly Action<T> m_ActionOnRelease;

        public int countAll      { get; private set; }
        public int countActive   { get { return countAll - countInactive; } }
        public int countInactive { get { return m_Stack.Count; } }

        public Stack<T> GetStack()
        {
            return m_Stack;
        }

        public ObjectPool(Func<T> actionOnGet, Action<T> actionOnRelease)
        {
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }

        public T Get()
        {
            T element;
            if (m_Stack.Count == 0)
            {
                element = m_ActionOnGet();
                countAll++;
            }
            else
            {
                element = m_Stack.Pop();
            }
            return element;
        }

        public void Release(T element)
        {
            if (m_Stack.Count > 0 && ReferenceEquals(m_Stack.Peek(), element))
            {
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
                if (m_ActionOnRelease != null)  m_ActionOnRelease(element);
                return;
            }
            if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);
            m_Stack.Push(element);
        }

        public void Clean()
        {
            m_Stack.Clear();
        }
    }
    
}