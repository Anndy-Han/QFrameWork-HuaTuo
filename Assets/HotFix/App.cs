using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using QFrameWork;

public class App
{
    public static int Main()
    {
        Debug.Log("hello,huatuo");
        Global.proceduceManager.ChangeProcedure(QFrameWork.Global.proceduceManager.CreateProcedureEnter("HotFix.Module.LoginProcedure"));
        return 0;
    }
}
