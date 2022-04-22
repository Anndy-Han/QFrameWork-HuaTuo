using System;
using UnityEngine;
using QFrameWork;

namespace HotFix.Module
{
    public class HomeProcedure : BaseProcedure
    {
        private HomeView view;

        public override void OnAwake()
        {
            base.OnAwake();

            view = new HomeView(LoadWidget("HomeView"));

            Debug.Log("HomeProcedure-OnAwake");
        }

        public override void OnEnter(Msg msg)
        {
            base.OnEnter(msg);

            view.Show();

            Debug.Log("HomeProcedure-OnEnter");
        }

        public override void OnLeave(Msg msg)
        {
            base.OnLeave(msg);

            view.Close();

            Debug.Log("HomeProcedure-OnLeave");
        }
    }
}