using System;
using UnityEngine;

namespace QFrameWork
{
    public interface IAudioManager
    {
        void PlayBgm(string name,bool loop = true);

        void PlaySfx(string name);

        void PlaySfx(string name,float volume);

        void CacheAudioClip(string name,AudioClip audioClip);

        AudioClip FindAudioClip(string name);

        void SetBgmVolume(float volume);

        void SetSfxVolume(float volume);

        float GetBgmVolume();

        float GetSfxVolume();

        void Mute();

        void Resume();

        void SaveConfig();
    }
}
