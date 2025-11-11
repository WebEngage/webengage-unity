using UnityEngine;
using WebEngageBridge;
using AOT;
using static WebEngageBridge.WebEngage;

public class MainCamera2 : MonoBehaviour
{


  private void Awake()
    {
        WebEngage.Engage();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    Debug.Log("WebEngage Testing");
        // WebEngage.Login("Shubham");
        WebEngage.LoginWithJWT("authcache_pp_emu", "yJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdWlkIjoiYXV0aGNhY2hlX3BwX2VtdSJ9.d-81rWb5wB1vWEElO1gc4VH6O7duTEm1Yd_CyuHPEXWhPV3-MBRYiz7YQl7IaD4HgpdxIQunmmVhPyrCJi0wS-21Sc0bOF6vIpcza_apjCgWCEBkNLt31Dzgvm7HzbvHpea9UIuMEJv0Gy_YIVKaEIhJ-rQ-CZZiExjsOHyL-QqIbkpzO2pPEkLsDnyyOEklhDP_4e_00VmXFDh0fxRj_VhwVSjPo4oTSD5JiSE9Kz-t9isS8KdHqaVRDaRqr0B5LunHHTX1KJ35-ocnbKLUmVIexAks0goFDBjX0rUZ8HgwErEtTK3PsRGhnkI3p7QO6TxG8XxBC-2vt59ceFJ0LrVbtHW88v8_AIk9HEPbxNG6Xjob-m0K7paZLevQKRluMwSLYDhZwbrsE5BlMqkebxbgw0aGhx5_T6qt104tcP4B1lgCXgaLB6sKwLSV03Yoj33CqBgN0fNSr_RkYha3MlmK2eZIV2uRfv6uVaL0_8jLPV0EKMfbbQJoI4VDhZj9ePZBfFEOoA6tFD9tgz2_2mzBhRMDcak67KzjMULbAFrPN5Dj37VG9gDMXoIfYqQRsM92fD6SwHMpk9OfgNF3sVrZz0HJicBKwM6suDFQSlkoEIlIc4gbOIcs4mmWjRfbTWAk1oHmAWRNmzX5sejHfoVcr1YhgrpJawnDSYylBik");
        WebEngage.TrackEvent("Test New Event");
        WebEngage.ScreenNavigated("Test Screen Shubham");
        WebEngage.setJWTTokenInvalidatedCallBack(handleJwtTokenInvalidatedCallback);
        WebEngage.TrackEvent("Screen Name tracked....");
        WebEngage.SetOptIn("whatsapp", true);
        WebEngage.SetOptIn("viber", true);
        WebEngage.SetOptIn("sms", true);
        WebEngage.SetOptIn("push", true);
    WebEngage.TrackEvent("Channel Opt In done....");
    WebEngage.StartGAIDTracking();
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
      // Debug.Log( WebEngage.GetDeeplinkFor(jsonString, "3178372a"));
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
