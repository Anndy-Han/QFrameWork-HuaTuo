using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public abstract class EntityLogic
    {
        public Entity Entity { get; private set; }

        public object EntityInfo { get; private set; }

        /// <summary>
        /// 实体初始化
        /// </summary>
        /// <param name="e"></param>
        /// <param name="entityInfo"></param>
        public virtual void OnInit(Entity e, object entityInfo)
        {
            this.Entity = e;

            this.EntityInfo = entityInfo;
        }

        /// <summary>
        /// 展示实体
        /// </summary>
        /// <returns></returns>
        public virtual bool OnShow()
        {
            if (this.Entity != null)
            {
                this.Entity.GameObject.SetActive(true);

                return true;
            }
            return false;
        }

        /// <summary>
        /// 隐藏实体
        /// </summary>
        /// <returns></returns>
        public virtual bool OnHide()
        {
            if (this.Entity != null)
            {
                this.Entity.GameObject.SetActive(false);

                return true;
            }
            return false;
        }
    }
}
