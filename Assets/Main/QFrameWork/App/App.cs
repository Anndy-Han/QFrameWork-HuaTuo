using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QFrameWork
{
    public enum AppMode
    {
        Developing,
        Release
    }
    [DisallowMultipleComponent]
    public class App:QBehaviour,IApp
    {
        public AppMode appMode = AppMode.Developing;

        public AppMate appMate;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            StartCoroutine(ApplicationDidFinishLaunching());
        }

        public AppMode GetAppMode()
        {
            return this.appMode;
        }

        private void LoadRuntimeManager()
        {
            Global.app = this;

            Global.eventDispatcher = GetRuntimeManager<EventDispatcher>() as EventDispatcher;

            Global.resourcesManager = GetRuntimeManager<ResourcesManager>() as ResourcesManager;

            Global.uiManager = GetRuntimeManager<UIManager>() as UIManager;

            Global.proceduceManager = GetRuntimeManager<ProcedureManager>() as ProcedureManager;

            Global.audioManager = GetRuntimeManager<AudioManager>() as AudioManager;

            Global.networkManager = GetRuntimeManager<NetWorkManager>() as NetWorkManager;

            Global.entityManager = GetRuntimeManager<EntityManager>() as EntityManager;
        }

        public object GetRuntimeManager<T>()
        {
            object instance = null;

            Type type = typeof(T);

            if (type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                instance = GetComponent(type) ?? gameObject.AddComponent(type);
            }
            else if (type.IsInterface)
            {
                instance = gameObject.GetComponent(type);
            }
            else
            {
                instance = Activator.CreateInstance<T>();
            }
            IPlugin plugin = instance as IPlugin;

            plugin.Load();

            Debug.Log("LoadRuntimeManager  "+instance.GetType()+" Success");

            return instance;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplicationDidFinishLaunching()
        {
            Debug.Log("=======App is launching=======");

            Application.targetFrameRate = this.appMate.targetFrameRate;

            LoadRuntimeManager();

            BaseProcedure.qBehaviour = this;

            LoadProceduces(this.appMate.procedureMates);

            ChangeProcedure(CreateProcedureEnter(this.appMate.startProcedure));

            if (appMode == AppMode.Developing)
            {
               
            }
            else
            {
                
            }
            yield return null;
        }

        public AppMate GetAppMate()
        {
            return this.appMate;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Debug.LogFormat("Engine - Engine is resumed.");
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Debug.LogFormat("Engine - Engine is paused.");
            }
        }

        # region ITicker

        private static List<ITicker> m_tickers = new List<ITicker>();

        public static void AddTicker(ITicker ticker)
        {
            if (!m_tickers.Contains(ticker))
            {
                m_tickers.Add(ticker);
            }
        }

        public static void RemoveTicker(ITicker ticker)
        {
            if (m_tickers.Contains(ticker))
            {
                m_tickers.Remove(ticker);
            }
        }

        private void Update()
        {
            var count = m_tickers.Count;
            if (count <= 0)
                return;
            for (var i = 0; i < count; i++)
            {
                var evt = m_tickers[i];
                if (!evt.OnUpdate(Time.deltaTime)) continue;
                m_tickers.RemoveAt(i);
                count--;
            }
        }
        # endregion 
    }
}
