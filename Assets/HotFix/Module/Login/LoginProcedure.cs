using System;
using QFrameWork;
using UnityEngine;

namespace HotFix.Module
{
    public class LoginProcedure : BaseProcedure
    {
        private LoginView view;

        public override void OnAwake()
        {
            base.OnAwake();

            view = new LoginView(LoadWidget("LoginView"));

            view.onClickButton = OnClickButton;

            Debug.Log("LoginProcedure-OnAwake");
        }

        private void OnClickButton()
        {
            ChangeProcedure(CreateProcedureEnter("HotFix.Module.HomeProcedure"));

            ChangeProcedure(CreateProcedureLeave("HotFix.Module.LoginProcedure"));
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
