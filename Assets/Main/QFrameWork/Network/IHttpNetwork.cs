using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public interface IHttpNetwork
    {
        void HttpSend(string url, int timeout,Action<object> onSuccessCallback,Action<object> onFailCallback);

        void HttpSend(string url, int timeout,string header, Action<object> onSuccessCallback, Action<object> onFailCallback);
    }
}
