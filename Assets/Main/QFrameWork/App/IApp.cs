using System;
using System.Collections;

namespace QFrameWork
{
    public interface IApp
    {
        object GetRuntimeManager<T>();

        AppMode GetAppMode();

        AppMate GetAppMate();
    }
}
