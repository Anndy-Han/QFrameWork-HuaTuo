using UnityEngine;

namespace QFrameWork
{
    public interface IEntityManager
    {
        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        Entity CreateEntity(GameObject gameObject, EntityLogic entityLogic, object entityInfo);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Entity GetEntity(int id);

        /// <summary>
        /// 是否有这个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool HasEntity(int id);

        /// <summary>
        /// 获取实体数量
        /// </summary>
        /// <returns></returns>
        int EntityCount { get; }
    }
}
