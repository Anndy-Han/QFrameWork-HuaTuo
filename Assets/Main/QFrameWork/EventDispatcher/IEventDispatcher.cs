using System;

namespace QFrameWork
{
    public interface IEventDispatcher
    {
        void Subscribe(string str ,Action<object,object> handler);

        void UnSubscribe(string str, Action<object, object> handler);

        void Post(string str);

        void Post(string str , object sender ,object args);

    }
}
