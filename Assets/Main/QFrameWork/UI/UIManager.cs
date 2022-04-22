using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using System.Text;

namespace QFrameWork
{
    [DisallowMultipleComponent]
    public class UIManager:QBehaviour,IUIManager,IPlugin
    {
        public Dictionary<string, Widget> _UIDict = new Dictionary<string,Widget>();

        public List<Widget> widgets = new List<Widget>();

        private Transform normalCanvas;

        private Transform modalCanvas;

        public void Load()
        {
            normalCanvas = transform.Find("UI/NormalRoot").transform;

            modalCanvas = transform.Find("UI/ModalRoot").transform;
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="baseView"></param>
        public void Show(BaseView baseView)
        {
            baseView.OnEnter();

            baseView.widget.transform.SetAsLastSibling();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close(BaseView baseView)
        {
            baseView.OnExit();

            baseView.widget.transform.SetAsFirstSibling();
        }

        /// <summary>
        /// 加载一个Widget
        /// </summary>
        /// <param name="widget"></param>
        /// <returns></returns>
        public Widget LoadWidget(string widget)
        {
            if (!_UIDict.ContainsKey(widget))
            {
                var asset = assetManager.LoadAssetAsync<GameObject>(widget);
                if (asset != null)
                {
                    Widget go = GameObject.Instantiate(asset).GetComponent<Widget>();
                    WidgetType widgetType = go.WidgetType;
                    switch (widgetType)
                    {
                        case WidgetType.Modal:
                            go.transform.SetParent(modalCanvas, false);
                            break;
                        case WidgetType.Normal:
                            go.transform.SetParent(normalCanvas, false);
                            break;
                        case WidgetType.Window:
                            break;
                    }
                    _UIDict.AddOrReplace(widget, go);
                    go.gameObject.name = widget;
                    go.transform.SetAsLastSibling();
                    go.gameObject.SetActive(false);
                    return go;
                }
                else
                {
                    Debug.Log(asset + "is null");
                }
            }
            return _UIDict[widget];
        }

        private void OnFailLoadAssetCallback()
        {
            throw new NotImplementedException();
        }

        private void OnSuccessLoadAssetCallback(UnityEngine.Object obj)
        {

        }

        /// <summary>
        /// 销毁一个Widget
        /// </summary>
        /// <param name="widget"></param>
        public void DestroyWidget(string widget)
        {
            if (!_UIDict.ContainsKey(widget))
            {
                return;
            }
            if (_UIDict[widget] == null)
            {
                _UIDict.Remove(widget);
                return;
            }
            GameObject.Destroy(_UIDict[widget]);
            _UIDict.Remove(widget);
        }
	}
}
