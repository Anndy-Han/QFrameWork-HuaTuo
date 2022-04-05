using System.Collections.Generic;

namespace QFrameWork
{
    [System.Serializable]
    public class Msg:Dictionary<string,object>
    {
        [UnityEngine.SerializeField]
        public int intValue;
        [UnityEngine.SerializeField]
        public string stringValue;

        public Msg():base()
        {

        }
    }
}
