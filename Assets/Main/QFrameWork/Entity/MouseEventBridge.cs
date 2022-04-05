using UnityEngine;
using System.Collections;
using System;

namespace QFrameWork
{
    public class MouseEventBridge : MonoBehaviour
    {
        public Action<GameObject> onMouseDown;
        public Action onDown;

        public Action<GameObject> onMouseUp;
        public Action onUp;

        public Action<GameObject> onMouseDrag;
        public Action onDrag;

        public Action<GameObject> onMouseEnter;
        public Action onEnter;

        public Action<GameObject> onMouseExit;
        public Action onExit;

        public Action<GameObject> onMouseOver;
        public Action onOver;

        private void OnMouseDown()
        {
            if (onMouseDown != null) onMouseDown(this.gameObject);
            if (onUp != null) onDown();
        }

        private void OnMouseDrag()
        {
            if (onMouseDrag != null) onMouseDrag(this.gameObject);
            if (onDrag != null) onDrag();
        }

        private void OnMouseEnter()
        {
            if (onMouseEnter != null) onMouseEnter(this.gameObject);
            if (onEnter != null) onEnter();
        }

        private void OnMouseExit()
        {
            if (onMouseExit != null) onMouseExit(this.gameObject);
            if (onExit != null) onExit();
        }

        private void OnMouseOver()
        {
            if (onMouseOver != null) onMouseOver(this.gameObject);
            if (onOver != null) onOver();
        }

        private void OnMouseUp()
        {
            if (onMouseUp != null) onMouseUp(this.gameObject);
            if (onUp != null) onUp();
        }
    }
}
