using System;
using UnityEngine;
using QFrameWork;
using UnityEngine.UI;

namespace HotFix.Module
{
    public class LoginView : BaseView
    {
        public LoginView(Widget widget) : base(widget)
        {

        }

        public Action onClickButton;

        public override void Init()
        {
            base.Init();

            SetButtonListener("Image/Button", () => { onClickButton(); });
        }
    }
}
