using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFrameWork
{
    public class AudioManager : IAudioManager,IPlugin
    {
        private AudioSource bgmAudioSource;

        private AudioSource sfxAudioSource;

        private AudioListener audioListener;

        private Transform qFrameWork;

        private float bgmVolume ;

        private float sfxVolume ;

        public void Load()
        {
            qFrameWork = GameObject.Find("QFrameWork").transform;

            bgmAudioSource = new GameObject("BgmAudioSource").AddComponent<AudioSource>();

            sfxAudioSource = new GameObject("SfxAudioSource").AddComponent<AudioSource>();

            audioListener = new GameObject("AudioListener").AddComponent<AudioListener>();

            bgmAudioSource.transform.SetParent(qFrameWork);

            sfxAudioSource.transform.SetParent(qFrameWork);

            audioListener.transform.SetParent(qFrameWork);

            LoadConfig();

            SetBgmVolume(bgmVolume);

            SetSfxVolume(sfxVolume);
        }

        private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

        public void SaveConfig()
        {
            PlayerPrefs.SetFloat("sfxVolume",sfxVolume);

            PlayerPrefs.SetFloat("bgmVolume",bgmVolume);
        }

        public void LoadConfig()
        {
            sfxVolume = PlayerPrefs.HasKey("sfxVolume") ? PlayerPrefs.GetFloat("sfxVolume") : 1f;

            bgmVolume = PlayerPrefs.HasKey("bgmVolume") ? PlayerPrefs.GetFloat("bgmVolume") : 1f;
        }

        public void CacheAudioClip(string name, AudioClip audioClip)
        {
            audioClips.AddOrReplace(name,audioClip);
        }

        public AudioClip FindAudioClip(string name)
        {
            if (!audioClips.ContainsKey(name))
            {
                throw new SystemException(string.Format("{0} is not exit", name));
            }
            return audioClips[name];
        }

        public float GetBgmVolume()
        {
            return this.bgmVolume;
        }

        public float GetSfxVolume()
        {
            return this.sfxVolume;
        }

        public void Mute()
        {
            this.bgmAudioSource.volume = 0f;

            this.sfxAudioSource.volume = 0f;
        }

        public void PlayBgm(string name, bool loop = true)
        {
            AudioClip audioClip;

            if (!audioClips.ContainsKey(name))
            {
                throw new SystemException(string.Format("{0} is not exits",name));
            }

            audioClip = audioClips[name];

            bgmAudioSource.clip = audioClip;

            bgmAudioSource.volume = bgmVolume;

            bgmAudioSource.loop = loop;

            bgmAudioSource.Play();
        }

        public void PlaySfx(string name)
        {
            PlaySfx(name,this.sfxVolume);
        }

        public void PlaySfx(string name, float volume)
        {
            AudioClip audioClip;

            if (!audioClips.ContainsKey(name))
            {
                throw new SystemException(string.Format("{0} is not exits", name));
            }

            audioClip = audioClips[name];

            sfxAudioSource.PlayOneShot(audioClip,volume);
        }

        public void Resume()
        {
            this.bgmAudioSource.volume = this.bgmVolume;

            this.sfxAudioSource.volume = this.sfxVolume;
        }

        public void SetBgmVolume(float volume)
        {
            bgmVolume = volume;
            bgmAudioSource.volume = volume;
        }

        public void SetSfxVolume(float volume)
        {
            sfxVolume = volume;
            sfxAudioSource.volume = volume;
        }
    }
}
