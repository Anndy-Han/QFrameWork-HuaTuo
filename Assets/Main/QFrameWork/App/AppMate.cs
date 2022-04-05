using UnityEngine;
using System.Collections.Generic;
using System;

namespace QFrameWork
{
    [CreateAssetMenu]
    public class AppMate : QScriptableObject
    {
        [Header("-- Base Property")]
        public string AppName;

        public string AppVerision;

        public string ResourceVersion;

        public string EngineVersion;

        [Space(10)]
        [Header("-- App Property")]
        public int targetFrameRate;
        [Space(10)]
        public string startProcedure;

        public List<Msg> procedureMates;
    }
}
