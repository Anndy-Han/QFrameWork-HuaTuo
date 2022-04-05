using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace QFrameWork
{
    public class UIPointerUpBridge : MonoBehaviour,IPointerUpHandler
    {
        public Action<PointerEventData> onPointerUp;
        public Action onUp;

        public void OnPointerUp(PointerEventData eventData)
        {
            if (onPointerUp != null) onPointerUp(eventData);
            if (onUp != null)        onUp();
        }
    }
    
}