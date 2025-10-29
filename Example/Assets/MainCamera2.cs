using UnityEngine;
using WebEngageBridge;
using AOT;
using static WebEngageBridge.WebEngage;

public class MainCamera2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("WebEngage Testing");
        // WebEngage.Login("Shubham");
        WebEngage.LoginWithJWT("sn_jwt", "yJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzbl9qd3QiLCJpYXQiOjE3NjE3MzkxMjMsImV4cCI6MTc3MjEwNzEyM30.wmy4wlhWAOgJYwsiVtMA6BMwDjkjYe4djceWW-gK4zjmmHeToJOqurIlrneVwzyje_mFCkzd3tI5dfg9e6p1jutXxanCUhUGujjEOtxRR2sYZiInCxoN9kjhymkw2DcyXtnWo-f1Kkm_oXHwKHUyJDxYRT0kT-JCNK-eS8QmozYkg5t4wCSUntLnfZRbO6tdlEQ3689GPc9L4Cf7XvNU8RSxL-xm8StyNePDzy0HhPu4OdF8iBvJwFvsOSchuhAcqPN2d5_BtJZ4kW-m6FQdA0irPHOWIkm4DV9HMIc22UDjbXHpUc4JKC6g-YBQUUiE3-WkrW1qAMr5bHudkIYKtiKSLddezWRlwI4odyA399dVoeaOmszvAEd6FvCtJMhEaNmpDENOHYZ3gggl3_d8onMMcwTaCJTM21kPbAqtkZFsUu340zijQEYpaV_nB--Kj4lfEf_XW5zSV8gssdcVDPCiwk6M9yHzJXPzFvsXhMc8vOHlmipkPS8SY2RnBo3Bz_E5idvkeHRFl57dOrZ6i18ufL2SAVxsQ93O2aQ3Cv6Tti45Wbrh_Pkf1u-MXXBeROKwxhzdL1ldxWsia3q8f-DVOBT2ayEvTGK7CgAnCuuN9ZVuu7xfmKdZYpNs6ZFfyf4eMnjQaPxqjSzfgfhqbhzSjCBkbQxr81fyWoxfb4Q");
        WebEngage.TrackEvent("Test New Event");
        WebEngage.ScreenNavigated("Test Screen Shubham");
        WebEngage.setJWTTokenInvalidatedCallBack(handleJwtTokenInvalidatedCallback);
        WebEngage.TrackEvent("Screen Name tracked....");
        WebEngage.SetOptIn("whatsapp", true);
        WebEngage.SetOptIn("viber", false);

        WebEngage.TrackEvent("Channel Opt In done....");
 // Example JSON object
     string jsonString = @"{
  ""isActive"": 1,
  ""fc"": 0,
  ""notificationEncId"": ""2k75k8i"",
  ""showTitle"": 1,
  ""layoutId"": ""i78eg76"",
  ""id"": ""~10cb4b543"",
  ""canMinimize"": 1,
  ""licenseCode"": ""d3a4b5a9"",
  ""actions"": [
    {
      ""actionEId"": ""3178372a"",
      ""actionText"": ""Click here"",
      ""actionLink"": ""www.google.com"",
      ""actionCategory"": ""CTA"",
      ""actionTarget"": ""_top"",
      ""isPrime"": 1,
      ""type"": ""DEEP_LINK"",
      ""sdkId"": 3
    }
  ],
  ""direction"": ""ltr"",
  ""canClose"": 1,
  ""config"": {
    ""closeIconColor"": ""#FFFFFF"",
    ""hideLogo"": 0
  }
}";
        WebEngage.AutoTrackUserLocationWithAccuracy(3);
        // Pass JSON string to GetDeeplinkFor
      Debug.Log( WebEngage.GetDeeplinkFor(jsonString, "3178372a"));
        // WebEngage.SetProxyURL("http://shubhamnaidu.com");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
     [MonoPInvokeCallback(typeof(callback))]
        public static void handleJwtTokenInvalidatedCallback(string json)
        {
            Debug.Log("WebEngage: UNity Callback received on handleJwtTokenInvalidatedCallback");
        }
}
