using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFrameWork
{
    public class ProcedureManager : IProcedureManager,IPlugin
    {
        private Dictionary<string, BaseProcedure> procedureDic = new Dictionary<string, BaseProcedure>();

        List<BaseProcedure> awakeProcedures = new List<BaseProcedure>();

        public void ChangeProcedure(Msg msg)
        {
            string msgStringValue = msg.stringValue;

            if (procedureDic.ContainsKey(msgStringValue))
            {
                OnProceduceEvent(procedureDic[msgStringValue],msg);
            }
            else
            {
                BaseProcedure baseProcedure = LoadProcedure(msg);

                OnProceduceEvent(baseProcedure,msg);

                procedureDic.Add(msgStringValue,baseProcedure);
            }
        }
        
        /// <summary>
        /// 加载所有的流程
        /// </summary>
        /// <param name="msgs"></param>
        public void LoadProceduces(List<Msg> msgs)
        {
            for (int i = 0; i < msgs.Count; i++)
            {
                BaseProcedure baseProcedure = LoadProcedure(msgs[i]);

                baseProcedure.OnLoad();

                procedureDic.Add(msgs[i].stringValue, baseProcedure);
            }
        }

        /// <summary>
        /// 加载流程
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private BaseProcedure LoadProcedure(Msg msg)
        {
            BaseProcedure baseProcedure = null;

            if (String.IsNullOrEmpty(msg.stringValue))
            {
                throw new SystemException("Argument cannot be null.");
            }

            if (msg.stringValue.StartsWith("HotFix"))
            {
                baseProcedure = InitProcedure.gameAss.CreateInstance(msg.stringValue) as BaseProcedure;
            }
            else
            {
                baseProcedure = Activator.CreateInstance(Type.GetType(msg.stringValue)) as BaseProcedure;
            }
            return baseProcedure;
        }

        private void OnProceduceEvent(BaseProcedure baseProcedure,Msg msg)
        {
            if (!awakeProcedures.Contains(baseProcedure))
            {
                awakeProcedures.Add(baseProcedure);

                baseProcedure.OnAwake();
            }
            switch (msg.intValue)
            {
                case 0:
                    baseProcedure.OnEnter(msg);
                    break;
                case 1:
                    baseProcedure.OnLeave(msg);
                    break;
                case 2:
                    baseProcedure.OnUnload();
                    awakeProcedures.Remove(baseProcedure);
                    break;
            }
        }

        private void Clear()
        {
            procedureDic.Clear();
        }

        public Msg CreateProcedureEnter(string str)
        {
            Msg msg = new Msg();

            msg.stringValue = str;

            msg.intValue = 0;

            return msg;
        }

        public Msg CreateProcedureLeave(string str)
        {
            Msg msg = new Msg();

            msg.stringValue = str;

            msg.intValue = 1;

            return msg;
        }

        public void Load()
        {
            
        }
    }
}
