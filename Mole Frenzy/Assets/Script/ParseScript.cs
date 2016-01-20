using UnityEngine;
using System.Collections;
using Parse;

public class ParseScript : MonoBehaviour
{

    void Awake()
    {
        ParsePush.ParsePushNotificationReceived += (sender, args) =>
        {
#if UNITY_ANDROID
            AndroidJavaClass parseUnityHelper = new AndroidJavaClass("com.parse.ParseUnityHelper");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // Call default behavior.
            parseUnityHelper.CallStatic("handleParsePushNotificationReceived", currentActivity, args.StringPayload);
#endif
        };
    }
}
