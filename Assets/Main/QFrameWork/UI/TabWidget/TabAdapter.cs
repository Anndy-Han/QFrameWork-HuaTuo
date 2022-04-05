using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace QFrameWork
{
    public class TabAdapter:UIBehaviour
    {
        [SerializeField]
        private GameObject m_targetOff;
        [SerializeField]
        private GameObject m_targetOn;

        internal protected virtual void OnToggle(bool value)
        {
            if (m_targetOn != null)
                m_targetOn.SetActive(value);
            if (m_targetOff != null)
                m_targetOff.SetActive(!value);
        }
    }
}