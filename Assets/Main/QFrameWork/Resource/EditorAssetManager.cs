using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QFrameWork
{
    public class EditorAssetManager : IAssetManager, IPlugin
    {
        public void Load()
        {

        }

        public T LoadAssetAsync<T>(string assetName) where T:UnityEngine.Object
        {
            var assetPath = Util.GetPrefabPath(assetName);
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.LoadMainAssetAtPath(assetPath) as T;
#else
            return null;
#endif
        }
    }
}
