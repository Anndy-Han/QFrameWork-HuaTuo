using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QFrameWork
{
    public abstract class BaseView:Object
    {
        public Widget widget;

        private GameObject m_gameObject;

        private Transform m_transform;

        public BaseView()
        {

        }

        public BaseView(Widget widget)
        {
            this.widget = widget;

            Init();
        }

        public virtual void BindWidget(Widget widget)
        {
            this.widget = widget;
        }

        public virtual void Init()
        {
            this.m_gameObject = widget.gameObject;

            this.m_transform = widget.transform;
        }

        public Widget GetChildWidget(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(path);
            }
            return this.transform.Find(path).GetComponent<Widget>();
        }

        public T GetChildView<T>(string path) where T: BaseView,new ()
        {
            BaseView baseView = null;

            Widget widget = GetChildWidget(path);

            baseView = new T();

            baseView.BindWidget(widget);

            baseView.Init();

            return baseView as T;
        }

        /// <summary>
        /// 设置按钮的监听事件，仅存在当前事件
        /// </summary>
        protected Button SetButtonListener(string componentPath, UnityAction callback)
        {
            var tmp = FindButton(componentPath);
            tmp.onClick.RemoveAllListeners();
            tmp.onClick.AddListener(callback);
            return tmp;
        }

        /// <summary>
        /// 设置文本内容，此方法会调用获取脚本的方法，如果是频繁需要赋值的对象，建议FindText缓存对象之后继续进行操作
        /// </summary>
        protected Text SetText(string componentPath, string text)
        {
            var tmp = FindText(componentPath);
            CheckComponent(tmp, componentPath);
            tmp.text = text;
            return tmp;
        }

        void CheckComponent(object target, string componentPath)
        {
            if (target == null)
                throw new NullReferenceException(string.Format("Component: {0} is not exists!", componentPath));
        }

        protected Button FindButton(string componentPath)
        {
            var tmp = FindChild<Button>(componentPath);
            CheckComponent(tmp, componentPath);
            return tmp;
        }

        protected Text FindText(string componentPath)
        {
            var tmp = FindChild<Text>(componentPath);
            CheckComponent(tmp, componentPath);
            return tmp;
        }

        protected T FindChild<T>(string componentName) where T : Component
        {
            return transform.Find(componentName).GetComponent<T>();
        }

        public GameObject gameObject
        {
            get { return this.m_gameObject; }
        }

        public Transform transform
        {
            get { return this.m_transform; }
        }

        public virtual void Show()
        {
            this.uiManager.Show(this);
        }

        public virtual void Close()
        {
            this.uiManager.Close(this);
        }

        public virtual void OnEnter()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void OnExit()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void OnPause()
        {

        }

        public virtual void OnResume()
        {

        }
    }
}
