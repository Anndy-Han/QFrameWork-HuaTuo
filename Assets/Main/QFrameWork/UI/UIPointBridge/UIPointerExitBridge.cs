using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace QFrameWork
{

    public class UIPointerExitBridge : MonoBehaviour,IPointerExitHandler
    {
        public Action<PointerEventData> onPointerExit;
        public Action onExit;

        public void OnPointerExit(PointerEventData eventData)
        {
            if (onPointerExit != null) onPointerExit(eventData);
            if (onExit != null)        onExit();
        }
    }
    
}