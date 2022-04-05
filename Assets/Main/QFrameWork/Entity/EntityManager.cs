using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFrameWork
{
    public class EntityManager : IPlugin,IEntityManager
    {
        /// <summary>
        /// 当前Id
        /// </summary>
        private int currentId = 0;

        /// <summary>
        /// 实体集合
        /// </summary>
        private Dictionary<int, Entity> entities;

        /// <summary>
        /// 获取所有实体数量
        /// </summary>
        public int EntityCount
        {
            get { return this.entities.Count; }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns></returns>
        public Entity GetEntity(int id)
        {
            if (HasEntity(id))
            {
                return this.entities[id];
            }
            return null;
        }

        /// <summary>
        /// 是否有这个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasEntity(int id)
        {
            return this.entities.ContainsKey(id);
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private Entity CreateEntity(int id, GameObject gameObject, EntityLogic entityLogic, object entityInfo)
        {
            var e = new Entity(id, gameObject, entityLogic, entityInfo);

            this.entities.Add(id, e);

            e.EntityLogic.OnInit(e, entityInfo);

            return e;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public Entity CreateEntity(GameObject gameObject, EntityLogic entityLogic, object entityInfo)
        {
            currentId++;

            return this.CreateEntity(currentId, gameObject, entityLogic, entityInfo);
        }


        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            this.entities = new Dictionary<int, Entity>();
        }
    }
}
