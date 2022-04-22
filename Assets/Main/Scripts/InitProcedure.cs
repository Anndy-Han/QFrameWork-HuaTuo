using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFrameWork;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using UnityEngine.Networking;

public class InitProcedure : BaseProcedure
{
    public static System.Reflection.Assembly gameAss;

    public override void OnAwake()
    {
#if !UNITY_EDITOR
        PlayerPrefs.DeleteKey(Addressables.kAddressablesRuntimeDataPath);
#endif

        base.OnAwake();
        StartCoroutine(Start());
    }

    private IEnumerator Start()
    {
        var InitAddressablesAsync = Addressables.InitializeAsync();
        yield return InitAddressablesAsync;
        StartCoroutine(UpdateCatalogCoro());
    }

    private IEnumerator UpdateCatalogCoro()
    {
        List<string> catalogsToUpdate = new List<string>();
        var checkCatalogHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkCatalogHandle;

        if (checkCatalogHandle.Status == AsyncOperationStatus.Succeeded)
        {
            catalogsToUpdate = checkCatalogHandle.Result;
        }

        if (catalogsToUpdate.Count > 0)
        {
            foreach (var catalog in catalogsToUpdate)
            {
                Debug.Log(catalog);
            }
            var updateCatalogHandle = Addressables.UpdateCatalogs(catalogsToUpdate, false);
            yield return updateCatalogHandle;
        }
        StartCoroutine(UpdateAllGroupsCoro());
    }

    IEnumerator UpdateAllGroupsCoro()
    {
        Debug.Log("开始更新资源");
        foreach (var loc in Addressables.ResourceLocators)
        {
            Debug.Log("loc:" + loc);
            foreach (var key in loc.Keys)
            {
                Debug.Log("key:" + key);
                var sizeAsync = Addressables.GetDownloadSizeAsync(key);
                yield return sizeAsync;
                long totalDownloadSize = sizeAsync.Result;
                Debug.Log("sizeAsync.Result:" + sizeAsync.Result);
                if (sizeAsync.Result > 0)
                {
                    var downloadAsync = Addressables.DownloadDependenciesAsync(key);
                    while (!downloadAsync.IsDone)
                    {
                        float percent = downloadAsync.PercentComplete;
                        Debug.Log($"{key} = percent {(int)(totalDownloadSize * percent)}/{totalDownloadSize}");
                        yield return new WaitForEndOfFrame();
                    }
                    Addressables.Release(downloadAsync);
                }
                Addressables.Release(sizeAsync);
            }
        }
        LoadGameDll();
    }

    private IEnumerator ClearAllAssetCoro()
    {
        foreach (var locats in Addressables.ResourceLocators)
        {
            var async = Addressables.ClearDependencyCacheAsync(locats.Keys, false);
            yield return async;
            Addressables.Release(async);
        }
        Caching.ClearCache();
    }

    private void LoadGameDll()
    {
        if (this.app.GetAppMode() == AppMode.Developing)
        {
            string gameDll = Application.dataPath + "/../Library/ScriptAssemblies/HotFix.dll";

            gameAss = System.Reflection.Assembly.Load(File.ReadAllBytes(gameDll));

            RunMain();
        }
        else if (this.app.GetAppMode() == AppMode.Release)
        {
            string path = string.Format("https://anndy.oss-cn-beijing.aliyuncs.com/huatuo-test/{0}/huatuo", Util.GetPlatformName());
            StartCoroutine(LoadAssetBundle(path,
                (_assetBundle) =>
                    {
                        gameAss = System.Reflection.Assembly.Load(_assetBundle.LoadAsset<TextAsset>("HotFix").bytes);
                        RunMain();
                }));
        }
    }

    public void RunMain()
    {
        if (gameAss == null)
        {
            UnityEngine.Debug.LogError("dll未加载");
            return;
        }
        var appType = gameAss.GetType("App");
        var mainMethod = appType.GetMethod("Main");
        mainMethod.Invoke(null, null);

        // 如果是Update之类的函数，推荐先转成Delegate再调用，如
        //var updateMethod = appType.GetMethod("Update");
        //var updateDel = System.Delegate.CreateDelegate(typeof(Action<float>), null, updateMethod);
        //updateMethod(deltaTime);
    }

    private IEnumerator LoadAssetBundle(string _path, Action<AssetBundle> _callback)
    {
        UnityWebRequest _request = UnityWebRequestAssetBundle.GetAssetBundle(_path);
        yield return _request.SendWebRequest();

        if (_request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(_request.error);
        }
        else
        {
            AssetBundle _bundle = DownloadHandlerAssetBundle.GetContent(_request);

            if (_callback != null)
            {
                _callback(_bundle);
            }
        }
    }
}
