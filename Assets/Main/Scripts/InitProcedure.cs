using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFrameWork;
using System.IO;

public class InitProcedure : BaseProcedure
{
    public override void OnAwake()
    {
        base.OnAwake();
        LoadGameDll();
        RunMain();
    }

    public static System.Reflection.Assembly gameAss;

    private void LoadGameDll()
    {
#if UNITY_EDITOR
        string gameDll = Application.dataPath + "/../Library/ScriptAssemblies/HotFix.dll";
        // ʹ��File.ReadAllBytes��Ϊ�˱���Editor��gameDll�ļ���ռ�õ��º���������޷�����
#else
        string gameDll = Application.streamingAssetsPath + "/HotFix.dll";
#endif
        gameAss = System.Reflection.Assembly.Load(File.ReadAllBytes(gameDll));
    }

    public void RunMain()
    {
        if (gameAss == null)
        {
            UnityEngine.Debug.LogError("dllδ����");
            return;
        }
        var appType = gameAss.GetType("App");
        var mainMethod = appType.GetMethod("Main");
        mainMethod.Invoke(null, null);

        // �����Update֮��ĺ������Ƽ���ת��Delegate�ٵ��ã���
        //var updateMethod = appType.GetMethod("Update");
        //var updateDel = System.Delegate.CreateDelegate(typeof(Action<float>), null, updateMethod);
        //updateMethod(deltaTime);
    }
}
