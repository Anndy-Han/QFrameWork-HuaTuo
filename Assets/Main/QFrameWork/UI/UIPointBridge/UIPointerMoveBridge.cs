using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace QFrameWork
{

    public class UIPointerMoveBridge : MonoBehaviour,IMoveHandler
    {
        public Action<AxisEventData> onPointerMove;

        public void OnMove(AxisEventData eventData)
        {
            if (onPointerMove != null) onPointerMove(eventData);
        }
    }
    
}