using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public interface ISocketNetwork
    {
        void Connect(string ip , int port);

        void Connect(string ipAndport);

        void Connect(string ip ,int port,Action<int> callback);

        void Close();

        void Stop();
    }
}
