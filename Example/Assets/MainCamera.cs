using System.Collections;
using System.Collections.Generic;
using AOT;
using UnityEngine;
using WebEngageBridge;
using static WebEngageBridge.WebEngage;

public class MainCamera : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unity running from the main camera script");
//        WebEngage.Login("BHAVESH SARWAR");
        //WebEngage.setisSDKInitialsedCallBack(handleIsSDKInitialised);
        WebEngage.setPushClickCallBack(handlepushNotificationClicked);
        WebEngage.setInAppPreparedCallBack(handleInAppNotificationPrepared);
        WebEngage.setInAppShownCallBack(handleInAppNotificationShown);
        WebEngage.setInAppClickedCallBack(handleInAppNotificationClicked);
        WebEngage.setInAppDismissedCallBack(handleInAppNotificationDismssed);


        //WebEngage.setInAppPreparedCallBack(InAppNotificationPrepared);
        //WebEngage.setInAppShownCallBack(InAppNotificationShown);
    }

    private void Awake()
    {
#if (UNITY_ANDROID)
        WebEngage.Engage();
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    //[MonoPInvokeCallback(typeof(callback))]
    //public static void handleIsSDKInitialised(string json)
    //{
    //    Debug.Log("/*/*/*/* UNity Callback received on SDK Initialis");
    //}

    // In app Callbacks
    [MonoPInvokeCallback(typeof(callback))]
    public static void handleInAppNotificationPrepared(string json)
    {
        Debug.Log("/*/*/*/* UNity Callback received on InAppNotificationPrepared");
        Debug.Log("/*/*/*/* isSDKInitialised" + WebEngage.getIsSDKInitialised());
    }

    [MonoPInvokeCallback(typeof(callback))]
    public static void handleInAppNotificationShown(string json)
    {
        Debug.Log("/*/*/*/* UNity Callback received on InAppNotificationShown");
    }

    [MonoPInvokeCallback(typeof(callback))]
    public static void handleInAppNotificationClicked(string json)
    {
        Debug.Log("/*/*/*/* UNity Callback received on InAppNotificationClicked");
    }

    [MonoPInvokeCallback(typeof(callback))]
    public static void handleInAppNotificationDismssed(string json)
    {
        Debug.Log("/*/*/*/* UNity Callback received on InAppNotificationDismssed");
    }

    // Push Notification Callbacks
    [MonoPInvokeCallback(typeof(callback))]
    public static void handlepushNotificationClicked(string json)
    {
        Debug.Log("/*/*/*/* UNity Callback received on PUSH NOTIFICATION CLICKED");
    }

}
