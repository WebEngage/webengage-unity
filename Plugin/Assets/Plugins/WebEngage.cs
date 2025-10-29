using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;

namespace WebEngageBridge
{
    
    
    #if (UNITY_ANDROID)
    public delegate void callback(string pushData);
    public sealed class WEPushNotificationCallback: AndroidJavaProxy {
        private callback recCallbackObj = null;
        private callback clickCallbackObj = null;
        public static WEPushNotificationCallback instance = null;
        private static readonly object padlock = new object();
        
        public static WEPushNotificationCallback Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new WEPushNotificationCallback();
                    }
                    return instance;
                }
            }
        }
        
        public void setPushReceivedCallBackObj(callback obj) {
            recCallbackObj = obj;
        }
        
        public void setPushClickCallBackObj(callback obj) {
            clickCallbackObj = obj;
        }
        
        
        private WEPushNotificationCallback() : base("com.webengage.sdk.android.unity.WEUnityPushCallbacks") {
            Debug.Log("WEPushNotificationCallback");
            new AndroidJavaObject("com.webengage.sdk.android.unity.WEUnityCallbacksPushImpl", this);
        }
        
        public string onPushNotificationReceived(string jsonString) {
            Debug.Log("WEPushNotificationCallback onPushNotificationReceived: " + jsonString);
            if (recCallbackObj != null) {
                recCallbackObj(jsonString);
            }
            return jsonString;
        }
        
        public bool onPushNotificationClicked(string jsonString) {
            Debug.Log("WEPushNotificationCallback onPushNotificationClicked: " + jsonString);
            if (clickCallbackObj != null) {
                clickCallbackObj(jsonString);
                return true;
            }
            return false;
        }
    }
    #endif
    
    #if (UNITY_ANDROID)
    public sealed class WEInAppNotificationCallback: AndroidJavaProxy {
        private callback callbackObjPrepared = null;
        private callback callbackObjShown = null;
        private callback callbackObjClicked = null;
        private callback callbackObjDismissed = null;
        public static WEInAppNotificationCallback instance = null;
        private static readonly object padlock2 = new object();
        
        public static WEInAppNotificationCallback Instance
        {
            get
            {
                lock (padlock2)
                {
                    if (instance == null)
                    {
                        instance = new WEInAppNotificationCallback();
                    }
                    return instance;
                }
            }
        }
        
        
        public void setInAppPrepared(callback obj) {
            callbackObjPrepared = obj;
        }
        
        public void setInAppDismissed(callback obj) {
            callbackObjDismissed = obj;
        }
        public void setInAppShown(callback obj) {
            callbackObjShown = obj;
        }
        public void setInAppClicked(callback obj) {
            callbackObjClicked = obj;
        }
        
        private WEInAppNotificationCallback() : base("com.webengage.sdk.android.unity.WEUnityInAppCallbacks") {
            new AndroidJavaObject("com.webengage.sdk.android.unity.WEUnityCallbacksInAppImpl", this);
        }
        
        public string onInAppNotificationPrepared(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationPrepared: " + jsonString);
            if (callbackObjPrepared != null) {
                callbackObjPrepared(jsonString);
            }
            return jsonString;
        }
        
        public void onInAppNotificationShown(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationShown: " + jsonString);
            if (callbackObjShown != null) {
                callbackObjShown(jsonString);
            }
        }
        
        public bool onInAppNotificationClicked(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationClicked: " + jsonString);
            if (callbackObjClicked != null) {
                callbackObjClicked(jsonString);
                return true;
            }
            return false;
        }
        
        public void onInAppNotificationDismissed(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationDismissed: " + jsonString);
            if (callbackObjDismissed != null) {
                callbackObjDismissed(jsonString);
            }
        }
    }
    #endif
    
    
    
    
    public class WebEngage
    {
        #if UNITY_ANDROID
        
        const string webengageClassPath = "com.webengage.sdk.android.WebEngage";
        private static AndroidJavaClass webEngageClass = null;
        
        private static AndroidJavaClass GetWebEngageClass()
        {
            if (webEngageClass == null)
            {
                webEngageClass = new AndroidJavaClass(webengageClassPath);
            }
            return webEngageClass;
        }
        
        private static AndroidJavaObject GetWebEngage()
        {
            return GetWebEngageClass().CallStatic<AndroidJavaObject>("get");
        }
        
        private static AndroidJavaObject GetAnalytics()
        {
            return GetWebEngage().Call<AndroidJavaObject>("analytics");
        }
        
        private static AndroidJavaObject GetUser()
        {
            return GetWebEngage().Call<AndroidJavaObject>("user");
        }
        
        private static AndroidJavaObject GetJavaObject(object val)
        {
            if (val is string)
            {
                AndroidJavaClass strClass = new AndroidJavaClass("java.lang.String");
                AndroidJavaObject strObj = strClass.CallStatic<AndroidJavaObject>("valueOf", val);
                return strObj;
            }
            else if (val is bool)
            {
                var booleanVal = new AndroidJavaObject("java.lang.Boolean", (bool) val);
                return booleanVal;
            }
            else if (val is int)
            {
                var integerVal = new AndroidJavaObject("java.lang.Integer", (int) val);
                return integerVal;
            }
            else if (val is long)
            {
                var longVal = new AndroidJavaObject("java.lang.Long", (long) val);
                return longVal;
            }
            else if (val is double)
            {
                var doubleVal = new AndroidJavaObject("java.lang.Double", (double) val);
                return doubleVal;
            }
            else if (val is float)
            {
                var floatVal = new AndroidJavaObject("java.lang.Float", (float) val);
                return floatVal;
            }
            else if (val is System.DateTime)
            {
                string strDate = ((System.DateTime) val).ToString("yyyy-MM-dd HH:mm:ss.fff");
                try
                {
                    var format = new AndroidJavaObject("java.text.SimpleDateFormat", "yyyy-MM-dd HH:mm:ss.SSS");
                    var date = format.Call<AndroidJavaObject>("parse", strDate);
                    return date;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("WebEngageBridge: Exception while parsing date object: " + strDate + ", " + e);
                }
                return GetJavaObject(strDate);
            }
            else if (val is Dictionary<string, string>)
            {
                return GetHashMapString((Dictionary<string, string>) val);
            }
            else if (val is Dictionary<string, object>)
            {
                return GetHashMap((Dictionary<string, object>) val);
            }
            else if (val is List<string>)
            {
                return GetArrayListString((List<string>) val);
            }
            else if (val is List<object>)
            {
                return GetArrayList((List<object>) val);
            }
            else
            {
                if (val != null)
                {
                    string str = val.ToString();
                    AndroidJavaClass strClass = new AndroidJavaClass("java.lang.String");
                    AndroidJavaObject strObj = strClass.CallStatic<AndroidJavaObject>("valueOf", str);
                    return strObj;
                }
                else
                {
                    return null;
                }
            }
        }
        
        private static AndroidJavaObject GetHashMap(Dictionary<string, object> dict)
        {
            AndroidJavaObject hashMap = new AndroidJavaObject("java.util.HashMap");
            var put = AndroidJNIHelper.GetMethodID(hashMap.GetRawClass(), "put", "(Ljava/lang/String;Ljava/lang/Object;)Ljava/lang/Object;");
            
            foreach (KeyValuePair<string, object> entry in dict)
            {
                AndroidJavaObject javaObject = GetJavaObject(entry.Value);
                AndroidJNI.CallObjectMethod(hashMap.GetRawObject(), put, AndroidJNIHelper.CreateJNIArgArray(new object[] { entry.Key, javaObject }));
            }
            return hashMap;
        }
        
        private static AndroidJavaObject GetHashMapString(Dictionary<string, string> dict)
        {
            AndroidJavaObject hashMap = new AndroidJavaObject("java.util.HashMap");
            var put = AndroidJNIHelper.GetMethodID(hashMap.GetRawClass(), "put", "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;");
            
            foreach (KeyValuePair<string, string> entry in dict)
            {
                AndroidJNI.CallObjectMethod(hashMap.GetRawObject(), put, AndroidJNIHelper.CreateJNIArgArray(new object[] { entry.Key, entry.Value }));
            }
            return hashMap;
        }
        
        private static AndroidJavaObject GetArrayList(List<object> list)
        {
            AndroidJavaObject arrayList = new AndroidJavaObject("java.util.ArrayList");
            var add = AndroidJNIHelper.GetMethodID(arrayList.GetRawClass(), "add", "(Ljava/lang/Object;)Z;", false);
            
            foreach (object val in list)
            {
                AndroidJavaObject javaObject = GetJavaObject(val);
                AndroidJNI.CallBooleanMethod(arrayList.GetRawObject(), add, AndroidJNIHelper.CreateJNIArgArray(new object[] { javaObject }));
            }
            return arrayList;
        }
        
        private static AndroidJavaObject GetArrayListString(List<string> list)
        {
            AndroidJavaObject arrayList = new AndroidJavaObject("java.util.ArrayList");
            var add = AndroidJNIHelper.GetMethodID(arrayList.GetRawClass(), "add", "(Ljava/lang/String;)Z;", false);
            foreach (string val in list)
            {
                AndroidJNI.CallBooleanMethod(arrayList.GetRawObject(), add, AndroidJNIHelper.CreateJNIArgArray(new object[] { val }));
            }
            return arrayList;
        }
        
        private const string eventReportingStrategyClassPath = "com.webengage.sdk.android.actions.database.ReportingStrategy";
        private const string locationTrackingStrategyClassPath = "com.webengage.sdk.android.LocationTrackingStrategy";
        private const string genderClassPath = "com.webengage.sdk.android.utils.Gender";
        private const string channelClassPath = "com.webengage.sdk.android.Channel";
        
        private static AndroidJavaObject GetEnum(string classPath, string enumName)
        {
            AndroidJavaClass enumClass = new AndroidJavaClass(classPath);
            return enumClass.GetStatic<AndroidJavaObject>(enumName);
        }
        
        #elif UNITY_IOS
        
        [DllImport("__Internal")]
        private static extern void login(string s);
        
        [DllImport("__Internal")]
        private static extern void loginWithJWT(string cuid, string jwt);
        
        [DllImport("__Internal")]
        private static extern void setSecureToken(string cuid, string jwt);
        
        [DllImport("__Internal")]
        private static extern void logout();
        
        [DllImport("__Internal")]
        private static extern void trackEvent(string s);
        
        [DllImport("__Internal")]
        private static extern void trackEventWithAttributes(string s, string attributes);
        
        [DllImport("__Internal")]
        private static extern void setFirstName(string firstName);
        
        [DllImport("__Internal")]
        private static extern void setLastName(string lastName);
        
        [DllImport("__Internal")]
        private static extern void setEmail(string email);
        
        [DllImport("__Internal")]
        private static extern void setHashedEmail(string hashedEmail);
        
        [DllImport("__Internal")]
        private static extern void setPhoneNumber(string phoneNumber);
        
        [DllImport("__Internal")]
        private static extern void setHashedPhoneNumber(string hashedPhoneNumber);
        
        [DllImport("__Internal")]
        private static extern void setGender(string gender);
        
        [DllImport("__Internal")]
        private static extern void setBirthDate(string birthDate);
        
        [DllImport("__Internal")]
        private static extern void setCompany(string company);
        
        [DllImport("__Internal")]
        private static extern void setOptIn(string channel, bool optIn);
        
        [DllImport("__Internal")]
        private static extern void setLocation(double latitude, double longitude);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeString(string key, string value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeBool(string key, bool value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeInt(string key, int value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeLong(string key, long value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeFloat(string key, float value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeDouble(string key, double value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributeDate(string key, string value);
        
        [DllImport("__Internal")]
        private static extern void setUserAttributes(string attributes);
        
        [DllImport("__Internal")]
        private static extern void deleteUserAttribute(string key);
        
        [DllImport("__Internal")]
        private static extern void deleteUserAttributes(string keys);
        
        [DllImport("__Internal")]
        private static extern void screenNavigated(string screen);
        
        [DllImport("__Internal")]
        private static extern void screenNavigatedWithData(string screen, string data);
        
        [DllImport("__Internal")]
        private static extern void setScreenData(string data);
        
        [DllImport("__Internal")]
        private static extern void autoTrackUserLocationWithAccuracy(int accuracy);
        
        [DllImport("__Internal")]
        private static extern void setProxyURL(string proxyURL);
        
        [DllImport("__Internal")]
        private static extern void resetProxyURL();
        
        [DllImport("__Internal")]
        private static extern string getDeeplinkFor(string inAppNotificationData, string actionId);
        
        
        // Push Notification Callbacks
        public delegate void callback(string pushData);
        
        //[DllImport("__Internal")]
        //public static extern void isSDKInitialsed(callback callback);
        [DllImport("__Internal")]
        public static extern void pushClickCallBack(callback callback);
        [DllImport("__Internal")]
        public static extern void InAppPreparedCallBack(callback callback);
        [DllImport("__Internal")]
        public static extern void InAppShownCallBack(callback callback);
        [DllImport("__Internal")]
        public static extern void InAppCLickedCallBack(callback callback);
        [DllImport("__Internal")]
        public static extern void InAppDismissedCallBack(callback callback);
        [DllImport("__Internal")]
        public static extern void jwtTokenInvalidatedCallBack(callback callback);
        [DllImport("__Internal")]
        private static extern bool isSDKInitialised();
        
        #endif
        
        
        //public static void setisSDKInitialsedCallBack(callback obj)
        //{
        //    if (Application.platform == RuntimePlatform.IPhonePlayer)
        //    {
        //        isSDKInitialsed(obj);
        //    }
        //}
        
        public static void setPushClickCallBack(callback obj)
        {
            #if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                pushClickCallBack(obj);
            }
            #endif
            #if (UNITY_ANDROID)
            if (Application.platform == RuntimePlatform.Android){
                var call = WEPushNotificationCallback.Instance;
                call.setPushClickCallBackObj(obj);
            }
            #endif
        }
        
        #if (UNITY_ANDROID)
        public static void setPushReceivedCallBack(callback obj)
        {
            Debug.Log("setPushReceivedCallBack");
            if (Application.platform == RuntimePlatform.Android)
            {
                var call = WEPushNotificationCallback.Instance;
                call.setPushReceivedCallBackObj(obj);
            }
        }
        #endif
        
        public static void setInAppPreparedCallBack(callback obj)
        {
            #if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                InAppPreparedCallBack(obj);
            }
            #endif
            #if (UNITY_ANDROID)
            if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppPrepared(obj);
            }
            #endif
        }
        public static void setInAppShownCallBack(callback obj)
        {
            #if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                InAppShownCallBack(obj);
            }
            #endif
            #if (UNITY_ANDROID)
            if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppShown(obj);
            }
            #endif
        }
        public static void setInAppClickedCallBack(callback obj)
        {
            #if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                InAppCLickedCallBack(obj);
            }
            #endif
            #if (UNITY_ANDROID)
            if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppClicked(obj);
            }
            #endif
        }
        public static void setInAppDismissedCallBack(callback obj)
        {
            #if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                InAppDismissedCallBack(obj);
            }
            #endif
            #if (UNITY_ANDROID)
            if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppDismissed(obj);
            }
            #endif
        }
        
        public static void setJWTTokenInvalidatedCallBack(callback obj)
        {
            #if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                jwtTokenInvalidatedCallBack(obj);
            }
            #endif
        }
        
        public static bool getIsSDKInitialised() {
            #if UNITY_IOS
            return isSDKInitialised();
            #elif (UNITY_ANDROID)
            return GetWebEngageClass().CallStatic<bool>("isEngaged");
            #endif
            
        }
        
        public static void Engage()
        {
            #if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            
            GetWebEngageClass().CallStatic("engage", context);
            
            var x = new AndroidJavaObject("com.webengage.sdk.android.WebEngageActivityLifeCycleCallbacks", activity);
            #endif
        }
        
        public static void Engage(string licenseCode, bool isDebug)
        {
            #if UNITY_ANDROID
            AndroidJavaObject configBuilder = new AndroidJavaObject("com.webengage.sdk.android.WebEngageConfig$Builder");
            configBuilder = configBuilder.Call<AndroidJavaObject>("setWebEngageKey", licenseCode);
            configBuilder = configBuilder.Call<AndroidJavaObject>("setDebugMode", isDebug);
            AndroidJavaObject config = configBuilder.Call<AndroidJavaObject>("build");
            
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            
            GetWebEngageClass().CallStatic("engage", context, config);
            #endif
        }
        
        public static void Start()
        {
            #if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            GetAnalytics().Call("start", activity);
            #endif
        }
        
        public static void Stop()
        {
            #if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            GetAnalytics().Call("stop", activity);
            #endif
        }
        
        // Configurations for Android
        public static void SetEventReportingStrategy(string strategy)
        {
            #if UNITY_ANDROID
            if ("buffer".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
                GetWebEngage().Call("setEventReportingStrategy", GetEnum(eventReportingStrategyClassPath, "BUFFER"));
            }
            else if ("force_sync".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
                GetWebEngage().Call("setEventReportingStrategy", GetEnum(eventReportingStrategyClassPath, "FORCE_SYNC"));
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid event reporting strategy: " + strategy + ". Must be one of [buffer, force_sync]");
            }
            #endif
        }
        
        public static void SetLocationTrackingStrategy(string strategy)
        {
            #if UNITY_ANDROID
            if ("accuracy_best".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "ACCURACY_BEST"));
            }
            else if ("accuracy_city".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "ACCURACY_CITY"));
            }
            else if ("accuracy_country".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "ACCURACY_COUNTRY"));
            }
            else if ("disabled".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "DISABLED"));
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid location tracking strategy: " + strategy + ". Must be one of [accuracy_best, accuracy_city, accuracy_country, disabled]");
            }
            #endif
        }
        
        // Tracking events
        public static void TrackEvent(string eventName)
        {
            #if UNITY_ANDROID
            GetAnalytics().Call("track", eventName);
            #elif UNITY_IOS
            trackEvent(eventName);
            #endif
        }
        
        public static void TrackEvent(string eventName, Dictionary<string, object> attributes)
        {
            #if UNITY_ANDROID
            var attr = GetHashMap(attributes);
            GetAnalytics().Call("track", eventName, attr);
            #elif UNITY_IOS
            var json = new JSONObject(attributes);
            trackEventWithAttributes(eventName, json.ToString());
            #endif
        }
        
        // Tracking users
        public static void Login(string cuid)
        {
            #if UNITY_ANDROID
            GetUser().Call("login", cuid);
            #elif UNITY_IOS
            login(cuid);
            #endif
        }
        
        public static void LoginWithJWT(string cuid, string jwt)
        {

            //TODO:- need to implement this for Android too, as of now this is a work Around
            #if UNITY_ANDROID
            GetUser().Call("login", cuid);
            #elif UNITY_IOS
            loginWithJWT(cuid, jwt);
            #endif
        }
        
        public static void SetSecureToken(string cuid, string jwt)
        {
            #if UNITY_ANDROID
            GetUser().Call("setSecureToken", cuid);
            #elif UNITY_IOS
            setSecureToken(cuid, jwt);
            #endif
        }
        
        public static void Logout()
        {
            #if UNITY_ANDROID
            GetUser().Call("logout");
            #elif UNITY_IOS
            logout();
            #endif
        }
        
        public static void SetFirstName(string firstName)
        {
            #if UNITY_ANDROID
            GetUser().Call("setFirstName", firstName);
            #elif UNITY_IOS
            setFirstName(firstName);
            #endif
        }
        
        public static void SetLastName(string lastName)
        {
            #if UNITY_ANDROID
            GetUser().Call("setLastName", lastName);
            #elif UNITY_IOS
            setLastName(lastName);
            #endif
        }
        
        public static void SetEmail(string email)
        {
            #if UNITY_ANDROID
            GetUser().Call("setEmail", email);
            #elif UNITY_IOS
            setEmail(email);
            #endif
        }
        
        public static void SetHashedEmail(string hashedEmail)
        {
            #if UNITY_ANDROID
            GetUser().Call("setHashedEmail", hashedEmail);
            #elif UNITY_IOS
            setHashedEmail(hashedEmail);
            #endif
        }
        
        public static void SetPhoneNumber(string phone)
        {
            #if UNITY_ANDROID
            GetUser().Call("setPhoneNumber", phone);
            #elif UNITY_IOS
            setPhoneNumber(phone);
            #endif
        }
        
        public static void SetHashedPhoneNumber(string hashedPhone)
        {
            #if UNITY_ANDROID
            GetUser().Call("setHashedPhoneNumber", hashedPhone);
            #elif UNITY_IOS
            setHashedPhoneNumber(hashedPhone);
            #endif
        }
        
        public static void SetCompany(string company)
        {
            #if UNITY_ANDROID
            GetUser().Call("setCompany", company);
            #elif UNITY_IOS
            setCompany(company);
            #endif
        }
        
        public static void SetBirthDate(string birthDate)
        {
            #if UNITY_ANDROID
            GetUser().Call("setBirthDate", birthDate);
            #elif UNITY_IOS
            setBirthDate(birthDate);
            #endif
        }
        
        public static void SetLocation(double latitude, double longitude)
        {
            #if UNITY_ANDROID
            GetUser().Call("setLocation", latitude, longitude);
            #elif UNITY_IOS
            setLocation(latitude, longitude);
            #endif
        }
        
        public static void SetGender(string gender)
        {
            if (string.Compare(gender, "male", true) == 0)
            {
                #if UNITY_ANDROID
                GetUser().Call("setGender", GetEnum(genderClassPath, "MALE"));
                #elif UNITY_IOS
                setGender("male");
                #endif
            }
            else if (string.Compare(gender, "female", true) == 0)
            {
                #if UNITY_ANDROID
                GetUser().Call("setGender", GetEnum(genderClassPath, "FEMALE"));
                #elif UNITY_IOS
                setGender("female");
                #endif
            }
            else if (string.Compare(gender, "other", true) == 0)
            {
                #if UNITY_ANDROID
                GetUser().Call("setGender", GetEnum(genderClassPath, "OTHER"));
                #elif UNITY_IOS
                setGender("other");
                #endif
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid gender: " + gender + ". Must be one of [male, female, other]");
            }
        }
        
        public static void SetOptIn(string channel, bool optIn)
        {
            #if UNITY_ANDROID
            if ("push".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "PUSH"), optIn);
            }
            else if ("in_app".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "IN_APP"), optIn);
            }
            else if ("email".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "EMAIL"), optIn);
            }
            else if ("sms".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "SMS"), optIn);
            }
            ////TODO:- Enable below when implementation done on Android 
            // else if ("whatsapp".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            // {
            //     GetUser().Call("setOptIn", GetEnum(channelClassPath, "WHATSAPP"), optIn);
            // }
            // else if ("viber".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            // {
            //     GetUser().Call("setOptIn", GetEnum(channelClassPath, "VIBER"), optIn);
            // }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid channel name: " + channel + ". Must be one of [push, in_app, email, sms]");
            }
            #elif UNITY_IOS
            setOptIn(channel, optIn);
            #endif
        }
        
        public static void SetUserAttribute(string key, object value)
        {
            #if UNITY_ANDROID
            AndroidJavaObject javaObject = GetJavaObject(value);
            GetUser().Call("setAttribute", key, javaObject);
            #elif UNITY_IOS
            if (value == null)
            {
                Debug.LogError("User attribute is null for: " + key);
            }
            else if (value is string)
            {
                setUserAttributeString(key, (string)value);
            }
            else if (value is bool)
            {
                setUserAttributeBool(key, (bool)value);
            }
            else if (value is int)
            {
                setUserAttributeInt(key, (int)value);
            }
            else if (value is long)
            {
                setUserAttributeLong(key, (long)value);
            }
            else if (value is float)
            {
                setUserAttributeFloat(key, (float)value);
            }
            else if (value is double)
            {
                setUserAttributeDouble(key, (double)value);
            }
            else if (value is System.DateTime)
            {
                setUserAttributeDate(key, ((System.DateTime)value).ToString(WebEngageBridge.JSONObject.DATE_FORMAT));
            }
            else
            {
                Debug.LogError("Invalid datatype for user attribute: " + key + ", converting value to string");
                setUserAttributeString(key, value.ToString());
            }
            #endif
        }
        
        public static void SetUserAttributes(Dictionary<string, object> attributes)
        {
            #if UNITY_ANDROID
            AndroidJavaObject hashMap = GetHashMap(attributes);
            GetUser().Call("setAttributes", hashMap);
            #elif UNITY_IOS
            var json = new JSONObject(attributes);
            string jsonString = json.ToString();
            setUserAttributes(jsonString);
            #endif
        }
        
        public static void SetDevicePushOptIn(bool val)
        {
            #if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setDevicePushOptIn", val);
            #endif
        }
        
        public static void DeleteUserAttribute(string key)
        {
            #if UNITY_ANDROID
            GetUser().Call("deleteAttribute", key);
            #elif UNITY_IOS
            deleteUserAttribute(key);
            #endif
        }
        
        public static void DeleteUserAttributes(List<string> keys)
        {
            #if UNITY_ANDROID
            AndroidJavaObject arrayList = GetArrayListString(keys);
            GetUser().Call("deleteAttributes", arrayList);
            #elif UNITY_IOS
            var json = new JSONObject(keys);
            string jsonString = json.ToString();
            deleteUserAttributes(jsonString);
            #endif
        }
        
        // Tracking Screens
        public static void ScreenNavigated(string screen)
        {
            #if UNITY_ANDROID
            GetAnalytics().Call("screenNavigated", screen);
            #elif UNITY_IOS
            screenNavigated(screen);
            #endif
        }
        
        public static void ScreenNavigated(string screen, Dictionary<string, object> data)
        {
            #if UNITY_ANDROID
            AndroidJavaObject hashMap = GetHashMap(data);
            GetAnalytics().Call("screenNavigated", screen, hashMap);
            #elif UNITY_IOS
            var json = new JSONObject(data);
            string jsonString = json.ToString();
            screenNavigatedWithData(screen, jsonString);
            #endif
        }
        
        public static void SetScreenData(Dictionary<string, object> data)
        {
            #if UNITY_ANDROID
            AndroidJavaObject hashMap = GetHashMap(data);
            GetAnalytics().Call("setScreenData", hashMap);
            #elif UNITY_IOS
            var json = new JSONObject(data);
            string jsonString = json.ToString();
            setScreenData(jsonString);
            #endif
        }
        
        // Push Messaging APIs for Android
        public static void SetPushToken(string token)
        {
            #if UNITY_ANDROID
            GetWebEngage().Call("setRegistrationID", token);
            #endif
        }
        
        public static void SendPushData(Dictionary<string, string> data)
        {
            #if UNITY_ANDROID
            AndroidJavaObject hashMap = GetHashMapString(data);
            GetWebEngage().Call("receive", hashMap);
            #endif
        }
        
        public static void UpdateFcmToken()
        {
            #if UNITY_ANDROID
            try
            {
                AndroidJavaClass webEngageFcmClass = new AndroidJavaClass("com.webengage.android.fcm.WebEngageFirebaseMessagingService");
                webEngageFcmClass.CallStatic("updateToken");
            }
            catch (AndroidJavaException aje)
            {
                Debug.LogError("WebEngage Unity Helper library not found: " + aje);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Exception while updating FCM token: " + e);
            }
            #endif
        }
        
        public static void AutoTrackUserLocationWithAccuracy(int accuracy)
        {
            #if UNITY_IOS
            autoTrackUserLocationWithAccuracy(accuracy);
            #endif
        }
        
        public static void SetProxyURL(string proxyURL)
        {
            #if UNITY_IOS
            setProxyURL(proxyURL);
            #endif
        }
        
        public static void ResetProxyURL()
        {
            #if UNITY_IOS
            resetProxyURL();
            #endif
        }
        
        public static string GetDeeplinkFor(string inAppNotificationData, string actionId)
        {
            #if UNITY_IOS
            return getDeeplinkFor(inAppNotificationData, actionId);
            #else
            return null;
            #endif
        }
    }
}
