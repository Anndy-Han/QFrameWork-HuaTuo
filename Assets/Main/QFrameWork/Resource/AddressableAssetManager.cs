using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using System.Threading.Tasks;

namespace QFrameWork
{
    public class AddressableAssetManager : IAssetManager, IPlugin
    {
        public void Load()
        {

        }

        T IAssetManager.LoadAssetAsync<T>(string assetName)
        {
            var op = Addressables.LoadAssetAsync<T>(assetName);
            T gameObject = op.WaitForCompletion();
            return gameObject;
        }
    }
}

