using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public interface IUIManager
    {
        void Show(BaseView baseView);

        void Close(BaseView baseView);

        Widget LoadWidget(string widget);

        void DestroyWidget(string widget);
    }
}
