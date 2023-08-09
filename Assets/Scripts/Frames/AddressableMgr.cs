using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableMgr 
{
    private static AddressableMgr _instance=new AddressableMgr();
    public static AddressableMgr Instance => _instance;
    private AddressableMgr() {}
    
    public Dictionary<string,IEnumerator> resDic=new Dictionary<string, IEnumerator>();

    public void LoadRes<T>(string name, Action<AsyncOperationHandle<T>> callback)
    {
        string keyName = name + "_"+typeof(T).Name;
        AsyncOperationHandle<T> handle;
        //if the resource has been loaded
        if (resDic.ContainsKey(keyName))
        {
            handle=(AsyncOperationHandle<T>)resDic[keyName];
            //if the resource has been loaded and completed
            if (handle.IsDone)
            {
                //invoke the callback immediately
                callback?.Invoke(handle);
            }
            //if the resource has been loaded but not completed
            else
            {
                //invoke the callback when the resource is loaded
                handle.Completed += (obj) =>
                {
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                        callback?.Invoke(obj);
                };
            }
            return;
        }
        
        //if the resource has not been loaded
        handle=Addressables.LoadAssetAsync<T>(name);//load the resource
        handle.Completed+= (obj) =>
        {
            //invoke the callback when the resource is loaded
            if (obj.Status == AsyncOperationStatus.Succeeded)
                callback?.Invoke(obj);
            else
            {
                Debug.LogError("LoadRes Error:"+obj.OperationException.Message);
                if(resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        };
        resDic.Add(keyName,handle);
    }
    
    public void ReleaseRes<T>(string name)
    {
        string keyName = name + "_"+typeof(T).Name;
        if (resDic.ContainsKey(keyName))
        {
            //release the resource and remove it from the dictionary
            AsyncOperationHandle<T> handles= (AsyncOperationHandle<T>)resDic[keyName];
            Addressables.Release(handles);
            resDic.Remove(keyName);
        }
    }

    //load multiple resources
    public void LoadRes<T>(Addressables.MergeMode mode, Action<T> callbacks, params string[] keys)
    {
        //creat keyName
        List<string> list=new List<string>();
        string keyName = "";
        foreach (var key in list)
            keyName+= key + "_";
        keyName += typeof(T).Name;

        //if the resource has been loaded
        AsyncOperationHandle<IList<T>> handle;
        if (resDic.ContainsKey(keyName))
        {
            handle=(AsyncOperationHandle<IList<T>>)resDic[keyName];
            if (handle.IsDone)
            {
                foreach (T item in handle.Result)
                {
                    callbacks?.Invoke(item);
                }
            }
            else
            {
                handle.Completed += (obj) =>
                {
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                        foreach (T item in obj.Result)
                        {
                            callbacks?.Invoke(item);
                        }
                };
            }
            return;
        }
        //if the resource has not been loaded
        handle=Addressables.LoadAssetsAsync<T>(list, callbacks, mode);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("LoadRes Error:"+obj.OperationException.Message);
                if(resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        };
        resDic.Add(keyName,handle);
    }

    public void ReleaseRes<T>(params string[] keys)
    {
        List<string> list=new List<string>();
        string keyName = "";
        foreach (var key in list)
            keyName+= key + "_";
        keyName+= typeof(T).Name;
        if (resDic.ContainsKey(keyName))
        {
            AsyncOperationHandle<IList<T>> handle= (AsyncOperationHandle<IList<T>>)resDic[keyName];
            Addressables.Release(handle);
            resDic.Remove(keyName);
        }
    }

    public void Clear()
    {
        resDic.Clear();
        AssetBundle.UnloadAllAssetBundles(true);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
