using System.Collections;
using UnityEngine;

namespace QFrameWork
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class Global
    {
        /// <summary>
        /// 如果开启更新模式，前提必须启动框架自带服务器端。
        /// 否则就需要自己将StreamingAssets里面的所有内容
        /// 复制到自己的Webserver上面，并修改下面的WebUrl。
        /// </summary>
        public const bool UpdateMode = false;                       //更新模式-默认关闭 

        public static string AppName = "demo";
        public const string LuaTempDir = "Lua/";                    //临时目录
        public const string ExtName = ".unity3d";                   //素材扩展名
        public const string AssetDir = "StreamingAssets";           //素材目录 
        public const string WebUrl = "http://localhost:6688/";      //测试更新地址

        public static IApp app;

        public static IUIManager uiManager;

        public static IEventDispatcher eventDispatcher;

        public static IResourcesManager resourcesManager;

        public static IProcedureManager proceduceManager;

        public static IAudioManager audioManager;

        public static INetworkManager networkManager;

        public static IEntityManager entityManager;
    }
}
