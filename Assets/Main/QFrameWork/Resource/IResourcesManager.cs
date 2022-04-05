
namespace QFrameWork
{
    public interface IResourcesManager
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="assetName"></param>
        T LoadAsset<T>(string assetName) where T : UnityEngine.Object;

        /// <summary>
        /// 释放资源
        /// </summary>
        void ReleaseResource();
    }
}
