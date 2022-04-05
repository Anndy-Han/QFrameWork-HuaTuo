using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace QFrameWork
{
    public class TabGroupWidget : UIBehaviour
    {

        [SerializeField]
        private int defaultIndex;
        [SerializeField]
        private TabWidget[] tabs;

        public Action<int, bool> OnValueChanged;

        protected override void Start()
        {
            DoInit();
        }

        void DoInit()
        {
            List<TabWidget> widgets = new List<TabWidget>();
            for (int i = 0; i < tabs.Length; i++)
            {
                var temp = tabs[i];
                temp.index = i;
                temp.BindGroup(this);
                widgets.Add(temp);
            }
            defaultIndex = widgets.Count > defaultIndex ? defaultIndex : widgets.Count - 1;
            for (int i = 0; i < widgets.Count; i++)
            {
                widgets[i].DoChanged(i==defaultIndex);
            }
        }

        internal void OnTabChanged(TabWidget tab, bool value)
        {
            OnValueChanged(tab.index, value);
            if (value)
            {
                DoTabOn(tab);
            }
        }

        void DoTabOn(TabWidget tab)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                var temp = tabs[i];
                if (temp != tab)
                {
                    temp.DoChanged(false);
                }
                else
                {
                    temp.DoChanged(true);
                }
            }
        }
    }
}