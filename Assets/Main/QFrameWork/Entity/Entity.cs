using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace QFrameWork
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 实体GameObject
        /// </summary>
        public GameObject GameObject { get; private set; }

        /// <summary>
        /// 实体Transform
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// 实体逻辑
        /// </summary>
        public EntityLogic EntityLogic { get; private set; }
        
        /// <summary>
        /// 实体信息
        /// </summary>
        public object EntityInfo { get; private set; }

        public Entity(int id, GameObject gameObject, EntityLogic entityLogic)
        {
            this.Id = id;

            this.GameObject = gameObject;

            this.Transform = gameObject.GetComponent<Transform>();

            this.EntityLogic = entityLogic;
        }

        public Entity(int id, GameObject gameObject, EntityLogic entityLogic, object entityInfo) : this(id, gameObject, entityLogic)
        {
            this.EntityInfo = entityInfo;
        }
    }
}
