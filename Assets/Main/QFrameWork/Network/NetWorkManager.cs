using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace QFrameWork
{
    public class NetWorkManager : QBehaviour,INetworkManager,IPlugin
    {
        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void Connect(string ipAndport)
        {
            throw new NotImplementedException();
        }

        public void Connect(string ip, int port, Action<int> callback)
        {
            throw new NotImplementedException();
        }

        public new void HttpSend(string url, int timeout, Action<object> onSuccessCallback, Action<object> onFailCallback)
        {
            StartCoroutine(Send(url, timeout,null, onSuccessCallback, onFailCallback));
        }

        public void HttpSend(string url,int timeout, string header, Action<object> onSuccessCallback, Action<object> onFailCallback)
        {
            StartCoroutine(Send(url,timeout,header,onSuccessCallback,onFailCallback));
        }

        public void Load()
        {
            
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private IEnumerator Send(string url, int timeout, string headerValue, Action<object> onSuccessCallback, Action<object> onFailCallback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);

            request.chunkedTransfer = true;

            request.timeout = Mathf.Clamp(timeout,5000,timeout);

            var op = request.Send();

            yield return op;

            if (!request.isNetworkError)
            {
                onSuccessCallback(request.downloadHandler.text);
            }
            else
            {
                onFailCallback(request.error);
            }
        }
    }
}
