using System;
using System.Collections.Generic;

namespace QFrameWork
{
    public class EventDispatcher:IEventDispatcher,IPlugin
    {
        /// <summary>
        /// 当前注册的委托。
        /// Key：消息类型；
        /// Value：该消息类型对应的委托
        /// </summary>
        private Dictionary<string, Action<object, object>> messageToDelegateMap = new Dictionary<string, Action<object, object>>();

        public void Clear()
        {
            messageToDelegateMap.Clear();
        }

        /// <summary>
        /// 添加一个消息监听
        /// </summary>
        /// <param name="message">要监听的消息类型</param>
        /// <param name="handler">处理方法委托</param>
        public void Subscribe(string message, Action<object, object> handler)
        {
            if (!messageToDelegateMap.ContainsKey(message))
            {
                messageToDelegateMap.Add(message, null);
            }
            messageToDelegateMap[message] += handler;
        }

        /// <summary>
        /// 移除一个消息监听
        /// </summary>
        /// <param name="message">要移除的消息类型</param>
        /// <param name="handler">处理方法委托</param>
        public void UnSubscribe(string message, Action<object, object> handler)
        {
            if (messageToDelegateMap.ContainsKey(message))
            {
                messageToDelegateMap[message] -= handler;
            }
            else
            {
                throw new NullReferenceException(message);
            }
        }

        public void Post(string str)
        {
            Post(str,null,null);
        }

        public void Post(string str, object sender, object args)
        {
            Action<object, object> handler = null;

            if (messageToDelegateMap.TryGetValue(str, out handler))
            {
                if (handler != null)
                {
                    handler(sender, args);
                }
            }
        }

        public void Load()
        {

        }
    }
}
