using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFrameWork
{
    public class Object : IObject
    {
        public void Subscribe(string str, Action<object, object> handler)
        {
            this.eventDispatcher.Subscribe(str, handler);
        }

        public void UnSubscribe(string str, Action<object, object> handler)
        {
            this.eventDispatcher.UnSubscribe(str, handler);
        }

        public void Post(string str)
        {
            this.Post(str, null, null);
        }

        public void Post(string str, object sender, object args)
        {
            this.eventDispatcher.Post(str, sender, args);
        }

        public void ChangeProcedure(Msg msg)
        {
            this.proceduceManager.ChangeProcedure(msg);
        }

        public void LoadProceduces(List<Msg> msgs)
        {
            this.proceduceManager.LoadProceduces(msgs);
        }

        public void CacheAudioClip(string name,AudioClip audioClip)
        {
            this.audioManager.CacheAudioClip(name ,audioClip);
        }

        public void PlayBgm(string name, bool loop = true)
        {
            this.audioManager.PlayBgm(name, loop);
        }

        public void PlaySfx(string name)
        {
            this.audioManager.PlaySfx(name);
        }

        public void SaveConfig()
        {
            this.audioManager.SaveConfig();
        }

        public void Mute()
        {
            this.audioManager.Mute();
        }

        public void Resume()
        {
            this.audioManager.Resume();
        }

        public void SetBgmVolume(float volume)
        {
            this.audioManager.SetBgmVolume(volume);
        }

        public void SetSfxVolume(float volume)
        {
            this.audioManager.SetSfxVolume(volume);
        }

        public float GetBgmVolume()
        {
            return this.audioManager.GetBgmVolume();
        }

        public float GetSfxVolume()
        {
            return this.audioManager.GetSfxVolume();
        }

        public Entity CreateEntity(GameObject gameObject, EntityLogic entityLogic, object entityInfo)
        {
            return this.entityManager.CreateEntity(gameObject, entityLogic, entityInfo);
        }

        public Entity GetEntity(int id)
        {
            return this.entityManager.GetEntity(id);
        }

        public bool HasEntity(int id)
        {
            return this.entityManager.HasEntity(id);
        }

        public int EntityCount {
            get {
                return this.entityManager.EntityCount;
            }
        }

        public void ReleaseResource()
        {
            this.resourcesManager.ReleaseResource();
        }

        public void HttpSend(string url, int timeout, Action<object> onSuccessCallback, Action<object> onFailCallback)
        {
            this.networkManager.HttpSend(url, timeout, onSuccessCallback, onFailCallback);
        }

        public IApp app
        {
            get
            {
                return Global.app;
            }
        }

        public IEventDispatcher eventDispatcher
        {
            get { return Global.eventDispatcher; }
        }

        public IResourcesManager resourcesManager
        {
            get { return Global.resourcesManager; }
        }

        public IUIManager uiManager
        {
            get { return Global.uiManager; }
        }

        public IProcedureManager proceduceManager
        {
            get { return Global.proceduceManager; }
        }

        public IAudioManager audioManager
        {
            get { return Global.audioManager; }
        }

        public INetworkManager networkManager
        {
            get { return Global.networkManager; }
        }

        public IEntityManager entityManager 
        {
            get { return Global.entityManager; }
        }
    }
}
