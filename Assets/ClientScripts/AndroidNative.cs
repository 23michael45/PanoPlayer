using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidNative
{
    private static T CallJavaFunc<T>(string funcName,params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (var androidPlugin = new AndroidJavaObject("com.pi.panoandroidnativeplugin.PanoAndroidNativePlugin", currentActivity))
                    {
                        return androidPlugin.Call<T>(funcName, args);
                    }
                }
            }
        }
        return default(T);

    }
   
    public static float GetBatteryLevel()
    {
        return CallJavaFunc<float>("GetBatteryPct");

    }
    public static string GetDCIMPath()
    {
        return CallJavaFunc<string>("GetDCIMPath");

    }
    public static string[] GetFilesInPath(string path)
    {
        return CallJavaFunc<string[]>("ListFileInPath",path);

    }
}
