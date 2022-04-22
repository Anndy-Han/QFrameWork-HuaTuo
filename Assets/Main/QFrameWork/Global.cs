using System.Collections;
using UnityEngine;

namespace QFrameWork
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class Global
    {
        public static IApp app;

        public static IUIManager uiManager;

        public static IEventDispatcher eventDispatcher;

        public static IProcedureManager proceduceManager;

        public static IAudioManager audioManager;

        public static INetworkManager networkManager;

        public static IEntityManager entityManager;

        public static IAssetManager assetManager;
    }
}
