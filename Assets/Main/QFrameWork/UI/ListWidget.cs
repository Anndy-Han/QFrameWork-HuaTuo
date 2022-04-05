using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

namespace QFrameWork
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class ListWidget : Widget
    {
        [SerializeField]
        private RectTransform m_item;

        public ObjectPool<RectTransform> m_itemPool;

        Action<RectTransform> m_onItemInstantiated;
        Action<RectTransform, int> m_onItemRefresh;

        public void Init(Action<RectTransform> onItemInstantiated, Action<RectTransform, int> onItemRefresh)
        {
            this.m_onItemInstantiated = onItemInstantiated;
            this.m_onItemRefresh = onItemRefresh;
            InitPool();
        }

        void InitPool()
        {
            if (m_itemPool != null)
                return;

            m_itemPool = new ObjectPool<RectTransform>(() =>
            {
                RectTransform nextItem = Instantiate(m_item);
                nextItem.transform.SetParent(Content, false);
                InstantiatedItem(nextItem);
                return nextItem;

            }, (RectTransform trans) =>
             {
                 trans.SetParent(transform);
                 trans.gameObject.SetActive(false);
             });

            m_itemPool.Release(m_item);
            InstantiatedItem(m_item);
        }

        private void ReturnObjectAndSendMessage(RectTransform go)
        {
            m_itemPool.Release(go);
        }

        public void ClearCells()
        {
            for (int i = Content.childCount - 1; i >= 0; i--)
            {
                ReturnObjectAndSendMessage(Content.GetChild(i) as RectTransform);
            }
        }

        void InstantiatedItem(RectTransform transform)
        {
            if (m_onItemInstantiated != null)
                m_onItemInstantiated(transform);

        }

        public RectTransform InstantiateNextItem(int itemIdx)
        {
            RectTransform nextItem = m_itemPool.Get();
            nextItem.transform.SetParent(Content, false);
            nextItem.gameObject.SetActive(true);
            ProvideData(nextItem, itemIdx);
            return nextItem;
        }

        void ProvideData(RectTransform trans, int itemIdx)
        {
            if (m_onItemRefresh != null)
            {
                m_onItemRefresh(trans, itemIdx);
            }
        }
    }
}