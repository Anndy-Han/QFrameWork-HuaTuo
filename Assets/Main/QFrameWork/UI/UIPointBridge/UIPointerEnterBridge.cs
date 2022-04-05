using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace QFrameWork
{
    public class UIPointerEnterBridge : MonoBehaviour,IPointerEnterHandler
    {
        public Action<PointerEventData> onPointerEnter;
        public Action onEnter;
   
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onPointerEnter != null) onPointerEnter(eventData);
            if (onEnter != null)        onEnter();
        }
    }
    
}