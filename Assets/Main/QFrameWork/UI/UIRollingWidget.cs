using UnityEngine;
using UnityEngine.EventSystems;

namespace QFrameWork
{
    public class UIRollingWidget : UIBehaviour
    {
        [SerializeField]
        RectTransform m_transform;
        [SerializeField]
        RectTransform m_transformParent;

        [SerializeField]
        Vector3 m_speed;
        float m_startX;
        float m_endX;
        float m_checkX;

        bool m_isActive;

        public void ResetSize()
        {
            if (m_transformParent == null)
            {
                m_transformParent = m_transform.parent as RectTransform;
            }

            if (m_speed.x < 0)
            {
                m_endX   = m_transformParent.offsetMin.x;
                m_startX = m_transformParent.offsetMax.x;
            }
            else
            {
                m_startX = m_transformParent.offsetMax.x;
                m_endX   = m_transformParent.offsetMin.x;
            }
            Vector3 pos = m_transform.anchoredPosition3D;
            pos.x = m_startX;
            m_transform.anchoredPosition3D = pos;
        }

        protected override void OnEnable()
        {
            ResetSize();
            m_isActive = true;
        }

        protected override void OnDisable()
        {
            m_isActive = false;
        }

        void LateUpdate()
        {
            if (!m_isActive)
                return;
            m_transform.anchoredPosition3D += m_speed * Time.deltaTime;
            if (m_transform.offsetMax.x <= m_endX)
            {
                Vector3 pos = m_transform.anchoredPosition3D;
                pos.x = m_startX;
                m_transform.anchoredPosition3D = pos;
            }
        }
    }
}