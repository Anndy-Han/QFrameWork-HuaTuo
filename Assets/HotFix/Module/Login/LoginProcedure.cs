using System;
using QFrameWork;
using UnityEngine;

namespace HotFix.Module.Login
{
    public class LoginProcedure : BaseProcedure
    {
        private LoginView view;

        public override void OnLoad()
        {
            base.OnLoad();
        }

        public override void OnAwake()
        {
            base.OnAwake();

            view = new LoginView(LoadWidget("LoginView"));

            Debug.Log("LoginProcedure-OnAwake");
        }


        public override void OnEnter(Msg msg)
        {
            base.OnEnter(msg);

            view.Show();

            Debug.Log("LoginProcedure-OnEnter");
        }

        public override void OnLeave(Msg msg)
        {
            base.OnLeave(msg);

            view.Close();

            Debug.Log("LoginProcedure-OnLeave");
        }
    }
}
