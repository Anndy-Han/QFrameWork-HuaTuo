using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace QFrameWork
{
    public class TabWidget : Selectable, IPointerClickHandler
    {
        [SerializeField]
        bool m_isOn;

        [SerializeField]
        bool m_isGroupable = true;

        internal int index;

        private TabGroupWidget tabGroup;

        TabAdapter m_adapter;
        public TabAdapter adapter
        {
            get
            {
                if (m_adapter == null)
                {
                    m_adapter = GetComponent<TabAdapter>() ?? gameObject.AddComponent<TabAdapter>();
                }
                return m_adapter;
            }
        }

        public Action<bool> OnValueChanged;

        protected override void Awake()
        {
            adapter.OnToggle(m_isOn);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.tabGroup.OnTabChanged(this, !m_isOn);
        }

        internal void BindGroup(TabGroupWidget groupWidget)
        {
            this.tabGroup = groupWidget;
        }

        internal void DoChanged(bool value)
        {
            if (OnValueChanged != null)
                OnValueChanged(value);
            adapter.OnToggle(value);
            m_isOn = value;
        }
    }
}