using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace QFrameWork
{

    public class UIPointerDragBridge : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Action<PointerEventData> onDrag;
        public Action<PointerEventData> onBeginDrag;
        public Action<PointerEventData> onEndDrag;

        public void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null) onDrag(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (onBeginDrag != null) onBeginDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null) onEndDrag(eventData);
        }
    }
}