using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QFrameWork
{
    [DisallowMultipleComponent]
    public class Widget: UIBehaviour
    {
        [SerializeField]
        private WidgetType widgetType;

        [SerializeField]
        private Transform content;

        public WidgetType WidgetType
        {
            get { return this.widgetType; }
        }

        public Transform Content
        {
            get
            {
                if (this.content != null)
                {
                    return content;
                }
                return this.transform;
            }
            set
            {
                this.content = value;
            }
        }
    }
}
