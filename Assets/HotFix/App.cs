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
        QFrameWork.Global.proceduceManager.ChangeProcedure(QFrameWork.Global.proceduceManager.CreateProcedureEnter("HotFix.Module.Login.LoginProcedure"));
        return 0;
    }
}
