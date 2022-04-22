using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QFrameWork
{
    [StructLayout(LayoutKind.Auto)]
    public struct LoadAssetInfo
    {
        private readonly string m_AssetName;

        private Action<UnityEngine.Object> m_onSuccessLoadAssetCallback;

        private Action m_onFailLoadAssetCallback;

        public LoadAssetInfo(string assetName, Action<UnityEngine.Object> onSuccessLoadAssetCallback, Action onFailLoadAssetCallback)
        {
            m_AssetName = assetName;

            m_onSuccessLoadAssetCallback = onSuccessLoadAssetCallback;

            m_onFailLoadAssetCallback = onFailLoadAssetCallback;
        }

        public string AssetName
        {
            get
            {
                return m_AssetName;
            }
        }

        public Action<UnityEngine.Object> OnSuccessLoadAssetCallback
        {
            get
            {
                return m_onSuccessLoadAssetCallback;
            }
        }

        public Action OnFailLoadAssetCallback
        {
            get
            {
                return m_onFailLoadAssetCallback;
            }
        }
    }
}
