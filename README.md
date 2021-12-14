
# WebEngage Unity

WebEngage Unity plugin is for Android and iOS apps built with Unity. This unity-package will not work on any other platform.


## Installation

 1. Download the [WebEngageUnity.unitypackage](https://github.com/WebEngage/webengage-unity/raw/master/WebEngageUnity.unitypackage).

 2. Import the downloaded unitypackage into your Unity project through `Assets` > `Import Package` > `Custom Package...`.

 3. Replace the AAR file at `Assets/Plugins/Android/webengage-android-unity-X.X.X.aar` with the latest [webengage-android-unity.aar](https://github.com/WebEngage/webengage-unity-android/raw/master/Assets/Plugins/Android/webengage-android-unity-3.16.0.aar).

 4. Replace the framework file at `Assets/Plugins/iOS/WebEngage.framework` with the latest [WebEngage framework](https://github.com/WebEngage/webengage-unity-ios/raw/master/WebEngage.framework.zip). Unzip the downloaded zip file to get the framework.


## Update

 1. Replace the AAR file at `Assets/Plugins/Android/webengage-android-unity-X.X.X.aar` with the latest [webengage-android-unity.aar](https://github.com/WebEngage/webengage-unity-android/raw/master/Assets/Plugins/Android/webengage-android-unity-3.16.0.aar).

 2. Replace the framework file at `Assets/Plugins/iOS/WebEngage.framework` with the latest [WebEngage framework](https://github.com/WebEngage/webengage-unity-ios/raw/master/WebEngage.framework.zip). Unzip the downloaded zip file to get the framework.


## Initialization

 1. For Android, add the following meta-data tags in `Assets/Plugins/Android/AndroidManifest.xml` file of your Unity project.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    ...>

    <application
    	...
        android:allowBackup="false">

	    <meta-data android:name="com.webengage.sdk.android.key" android:value="YOUR-WEBENGAGE-LICENSE-CODE" />

	    <!-- true if development build else false -->
	    <meta-data android:name="com.webengage.sdk.android.debug" android:value="true" />

	    ...
	</application>
</manifest>
```

If `AndroidManifest.xml` file does not exist in `Assets/Plugins/Android/` directory of your Unity project, then you can create a new `AndroidManifest.xml` file and copy the below content in it.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:tools="http://schemas.android.com/tools">

    <application
        android:label="@string/app_name"
        android:icon="@drawable/app_icon"
        android:allowBackup="false">

        <meta-data android:name="com.webengage.sdk.android.key" android:value="YOUR-WEBENGAGE-LICENSE-CODE" />

        <meta-data android:name="com.webengage.sdk.android.debug" android:value="true" />

        <activity
            android:name="com.unity3d.player.UnityPlayerActivity"
            android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen">

            <intent-filter>

                <action
                    android:name="android.intent.action.MAIN" />

                <category
                    android:name="android.intent.category.LAUNCHER" />

                <category
                    android:name="android.intent.category.LEANBACK_LAUNCHER" />
            </intent-filter>

            <meta-data
                android:name="unityplayer.UnityActivity"
                android:value="true" />
        </activity>
    </application>
</manifest>
```

**Note:** Replace YOUR-WEBENGAGE-LICENSE-CODE with your own WebEngage license code.


 2. Initialize the WebEngage SDK at start of your application.

```csharp
using WebEngageBridge;
...

public class YourScript : MonoBehaviour
{
    private void Awake()
    {
        WebEngage.Engage();
        ...
    }
    ...
}
```


 3. For iOS, add the following values in `/Assets/Editor/WebEngagePostProcessBuild.cs` file.

 ```csharp
...

public class WebEngagePostProcessBuild
{
    [PostProcessBuild]
    public static void EditXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
        	// Add your WebEngage license code
        	string WEBENGAGE_LICENSE_CODE = "YOUR-WEBENGAGE-LICENSE-CODE";

        	// Set debug log level
            string logLevel = "VERBOSE";
        	...
        }
    }
}
 ```

**Note:** Replace YOUR-WEBENGAGE-LICENSE-CODE with your own WebEngage license code.

 4. Initialize the WebEngage SDK in your `AppDelegate.m` class.

```objective-c
#import <WebEngage/WebEngage.h>
...

-(BOOL)application:(UIApplication*) application didFinishLaunchingWithOptions:(NSDictionary*) options
{
    [[WebEngage sharedInstance] application:application didFinishLaunchingWithOptions:options];
    
    ...
}

```

If you are not already implementing `AppDelegate.m` in your Unity app, then create a new file at `/Assets/Plugins/iOS/OverrideAppDelegate.m` and copy the below contents in it.

```objective-c
#import "UnityAppController.h"
#import <WebEngage/WebEngage.h>


@interface OverrideAppDelegate : UnityAppController
@end


IMPL_APP_CONTROLLER_SUBCLASS(OverrideAppDelegate)

@implementation OverrideAppDelegate

-(BOOL)application:(UIApplication*) application didFinishLaunchingWithOptions:(NSDictionary*) options
{
    [[WebEngage sharedInstance] application:application didFinishLaunchingWithOptions:options];
    
    return [super application:application didFinishLaunchingWithOptions:options];
}

@end

```


## Attribution Tracking

In order to track 'App Installed' events and install-referrer URLs in Android, follow the steps below.

1. Make sure that you are using webengage-android-unity.aar version 3.16.0 or above.

2. Download the latest version of [Android Install Referrer library (aar)](https://mvnrepository.com/artifact/com.android.installreferrer/installreferrer).

3. Add the aar file to your Unity project at the location `/Assets/Plugins/Android/installreferrer-X.X.X.aar`.


## Tracking Users

 1. Login and Logout

```csharp
using WebEngageBridge;
...

public class YourScript : MonoBehaviour
{
    ...

    // User login
    WebEngage.Login("userId");

    // User logout
    WebEngage.Logout();
}
```

 2. Set system user attributes as shown below.

```csharp
using WebEngageBridge;
...

public class YourScript : MonoBehaviour
{
    // Set user first name
    WebEngage.SetFirstName("John");

    // Set user last name
    WebEngage.SetLastName("Doe");  

    // Set user email
    WebEngage.SetEmail("john.doe@email.com");

    // Set user hashed email
    WebEngage.SetHashedEmail("144e0424883546e07dcd727057fd3b62");

    // Set user phone number
    WebEngage.SetPhoneNumber("+551155256325");

    // Set user hashed phone number
    WebEngage.SetHashedPhoneNumber("e0ec043b3f9e198ec09041687e4d4e8d");

    // Set user gender, allowed values are ['male', 'female', 'other']
    WebEngage.SetGender("male");

    // Set user birth-date, supported format: 'yyyy-mm-dd'
    WebEngage.SetBirthDate("1994-04-29");

    // Set user company
    WebEngage.SetCompany("Google");

    // Set opt-in status, channels: ['push', 'in_app', 'email', 'sms']
    WebEngage.SetOptIn("push", true);

    // Set user location
    double latitude = 19.0822;
    double longitude = 72.8417;
    WebEngage.SetLocation(latitude, longitude);
}
```

3. Set custom user attributes as shown below.

```csharp
using WebEngageBridge;
    ...
    // Set custom user attributes
    WebEngage.SetUserAttribute("age", 25);
    WebEngage.SetUserAttribute("premium", true);

    // Set multiple custom user attributes
    Dictionary<string, object> customAttributes = new Dictionary<string, object>();
    customAttributes.Add("Twitter Email", "john.twitter@mail.com");
    customAttributes.Add("Subscribed", true);
    WebEngage.SetUserAttributes(customAttributes);
```
**Note:** WebEngage SDK only supports the following data-types: string, bool, int, long, float, double, DateTime, List and Dictionary.

4. Delete custom user attributes as shown below.

```csharp
using WebEngageBridge;
    ...
    
    WebEngage.DeleteUserAttribute("age");
```


## Tracking Events

Track custom events as shown below.

```csharp
using WebEngageBridge;
    ...

    // Track simple event
    WebEngage.TrackEvent("Product - Page Viewed");

    // Track event with attributes
    Dictionary<string, object> orderPlacedAttributes = new Dictionary<string, object>();
    orderPlacedAttributes.Add("Amount", 808.48);
    orderPlacedAttributes.Add("Product 1 SKU Code", "UHUH799");
    orderPlacedAttributes.Add("Product 1 Name", "Armani Jeans");
    orderPlacedAttributes.Add("Product 1 Price", 300.49);
    orderPlacedAttributes.Add("Product 1 Size", "L");
    orderPlacedAttributes.Add("Product 2 SKU Code", "FBHG746");
    orderPlacedAttributes.Add("Product 2 Name", "Hugo Boss Jacket");
    orderPlacedAttributes.Add("Product 2 Price", 507.99);
    orderPlacedAttributes.Add("Product 2 Size", "L");
    orderPlacedAttributes.Add("Delivery Date", System.DateTime.ParseExact("2017-10-21 09:27:37.100", "yyyy-MM-dd HH:mm:ss.fff", null));
    orderPlacedAttributes.Add("Delivery City", "San Francisco");
    orderPlacedAttributes.Add("Delivery ZIP", "94121");
    orderPlacedAttributes.Add("Coupon Applied", "BOGO17");
    WebEngage.TrackEvent("Order Placed", orderPlacedAttributes);

    // Track complex event
    Dictionary<string, object> product1 = new Dictionary<string, object>();
    product1.Add("SKU Code", "UHUH799");
    product1.Add("Product Name", "Armani Jeans");
    product1.Add("Price", 300.49);

    Dictionary<string, object> detailsProduct1 = new Dictionary<string, object>();
    detailsProduct1.Add("Size", "L");
    product1.Add("Details", detailsProduct1);

    Dictionary<string, object> product2 = new Dictionary<string, object>();
    product2.Add("SKU Code", "FBHG746");
    product2.Add("Product Name", "Hugo Boss Jacket");
    product2.Add("Price", 507.99);

    Dictionary<string, object> detailsProduct2 = new Dictionary<string, object>();
    detailsProduct2.Add("Size", "L");
    product2.Add("Details", detailsProduct2);

    Dictionary<string, object> deliveryAddress = new Dictionary<string, object>();
    deliveryAddress.Add("City", "San Francisco");
    deliveryAddress.Add("ZIP", "94121");

    Dictionary<string, object> orderPlacedAttributes = new Dictionary<string, object>();
    List<object> products = new List<object>();
    products.Add(product1);
    products.Add(product2);

    List<string> coupons = new List<string>();
    coupons.Add("BOGO17");

    orderPlacedAttributes.Add("Products", products);
    orderPlacedAttributes.Add("Delivery Address", deliveryAddress);
    orderPlacedAttributes.Add("Coupons Applied", coupons);

    WebEngage.TrackEvent("Order Placed", orderPlacedAttributes);
```

**Note:** WebEngage SDK only supports the following data-types: string, bool, int, long, float, double, DateTime, List and Dictionary.


## Push Notifications for Android

#### 1. Using FCM Unity Plugin

1. Import FCM Unity plugin as instructed [here](https://firebase.google.com/docs/cloud-messaging/unity/client) into your Unity project.

2. If you have replaced the `Assets/Plugins/Android/AndroidManifest.xml` then make sure to add back your WebEngage license-code and debug-mode meta-data tags in the `AndroidManifest.xml` file.

3. In your script where you have registered callbacks for `OnTokenReceived` and `OnMessageReceived`, add the following code snippets.

```csharp
using WebEngageBridge;
    ...
    Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        }
        else
        {
            ...
        }
    });
    
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        ...
        WebEngage.SetPushToken(token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Dictionary<string, string> data = new Dictionary<string, string>(e.Message.Data);
        if (data.ContainsKey("source") && "webengage".Equals(data["source"]))
        {
            WebEngage.SendPushData(data);
        }
        ...
    }
```

Push notifications will work as expected when app is in foreground.

**Note**: Drawback of this approach is that push notifications will not be shown when app is in background. However those push notifications are cached and will be shown on next app launch. If you wish to prevent this drawback, then follow the Overriding FCM Unity Plugin approach given below.

#### 2. Overriding FCM Unity Plugin

1. Import FCM Unity plugin as instructed [here](https://firebase.google.com/docs/cloud-messaging/unity/client) into your Unity project.

2. If you have replaced the `Assets/Plugins/Android/AndroidManifest.xml` then make sure to add back your WebEngage license-code and debug-mode meta-data tags in the `AndroidManifest.xml` file.

3. Download and add the [webengage-android-fcm.aar](https://github.com/WebEngage/webengage-unity-android/raw/master/webengage-android-fcm-0.0.1.aar) file in `Assets/Plugins/Android/` directory of your Unity project.

4. Add the following service tag in your `Assets/Plugins/Android/AndroidManifest.xml` file as shown below.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    ...>

    <application
        ...>

        <service android:name="com.webengage.android.fcm.WebEngageFirebaseMessagingService">
            <intent-filter>
                <action android:name="com.google.firebase.MESSAGING_EVENT" />
            </intent-filter>
        </service>

        ...
    </application>
</manifest>
```

5. Update the FCM registration token on app start as shown below.

```csharp
using WebEngageBridge;
    ...

    WebEngage.UpdateFcmToken();
```


## Push Notifications for iOS

 1. Build your iOS app through Unity Editor and open Unity-iPhone.xcodeproj in your Xcode IDE.

 2. Select your main app target (Unity-iPhone), under Capabilities enable Push Notifications.

 3. Also under Capabilities enable Background Modes and check Remote notifications.


### Rich Push Notifications

 1. Enable Push Notifications, App Groups and add the app group `group.<your-bundle-identifier>.WEGNotificationGroup` to your App ID from your Apple Developer Account. Download and install the updated provisioning profile of your app in your Xcode.

#### 1. Banner Push Notifications

 1. Add a new App Bundle ID `<your-bundle-identifier>.NotificationService`, enable Push Notifications, App Groups and add the group `group.<your-bundle-identifier>.WEGNotificationGroup` in this newly created App ID from your Apple Developer Account. Download and install the provisioning profile of this App ID in your Xcode.

 2. Download the [WebEngageNotificationService.unitypackage](https://github.com/WebEngage/webengage-unity-ios/raw/master/WebEngageNotificationService.unitypackage).

 3. Import the downloaded unitypackage into your Unity project through `Assets` > `Import Package` > `Custom Package...`.

 4. Build your iOS app through Unity Editor and open Unity-iPhone.xcodeproj in your Xcode IDE.

 5. Verify that NotificationService extension is added and linked to your main app target.

#### 2. Rating and Carousel Push Notifications

 1. Add the App Bundle ID `<your-bundle-identifier>.NotificationViewController`, enable Push Notifications, App Groups and add the group `group.<your-bundle-identifier>.WEGNotificationGroup` in this newly created App ID from your Apple Developer Account. Download and install the provisioning profile of this App ID in your Xcode.

 2. Download the [WebEngageNotificationContent.unitypackage](https://github.com/WebEngage/webengage-unity-ios/raw/master/WebEngageNotificationContent.unitypackage).

 3. Import the downloaded unitypackage into your Unity project through `Assets` > `Import Package` > `Custom Package...`.

 4. Build your iOS app through Unity Editor and open Unity-iPhone.xcodeproj in your Xcode IDE.

 5. Verify that NotificationViewController extension is added and linked to your main app target.


#### Troubleshooting for Rich Push Notifications

 **1. If you are facing integration or build issues with rich-push notification unity plugins, then try adding the extensions and pods manually.**

 1. Remove the WebEngageNotificationService.unitypackage and WebEngageNotificationContent.unitypackage plugins (if added).

 2. Build your iOS app through Unity Editor and open Unity-iPhone.xcodeproj in your Xcode IDE.

 3. Follow the instructions at [WebEngage documentation](https://docs.webengage.com/docs/ios-push-messaging#section-5-rich-push-notifications).


## In-app Notifications

No additional steps are required for in-app notifications.

### Tracking Screens

```csharp
using WebEngageBridge;
    ...
    
    // Set screen name
    WebEngage.ScreenNavigated("Purchase Screen");

    // Update current screen data
    Dictionary<string, object> currentData = new Dictionary<string, object>();
    currentData.Add("productId", "~hs7674");
    currentData.Add("price", 1200);
    WebEngage.SetScreenData(currentData);

    // Set screen name with data
    Dictionary<string, object> data = new Dictionary<string, object>();
    data.Add("productId", "~hs7674");
    data.Add("price", 1200);
    WebEngage.ScreenNavigated("Purchase Screen", data);
```

# Callbacks

### Push Notification Callbacks

Set up callbacks in your Unity project by following these steps:

#### 1. Push Notification Received (Only For Android Platform) : 
 - `SetPushReceivedCallBack` allows you to obtain a callback when you get
   a push notification.
   
 - Create  `[MonoPInvokeCallback(typeof(callback))]` type method with any name in which you wish to receive callback

 Eg.  
```csharp
[MonoPInvokeCallback(typeof(callback))]
    public static void pushReceivedCallback(string json)
    {
        Debug.Log("/*/*/*/* Unity Callback received on PUSH NOTIFICATION RECEIVED”);
    }
```

 - Add `SetPushReceivedCallBack` in your C#(.cs) code and send the previously constructed function as an argument
    Eg.
```csharp
    WebEngage.setPushReceivedCallBack(pushReceivedCallback); 
```

*As a result, whenever you receive a push notification, the code defined in the callback handler will run.*



#### 2. Push Notification Clicked :
 - `setPushClickCallBack` allows you to obtain a callback when you click a push notification.
   
 - Create  `[MonoPInvokeCallback(typeof(callback))]` type method with any name in which you wish to receive callback

 Eg.  
```csharp
[MonoPInvokeCallback(typeof(callback))]
public static void handlepushNotificationClicked(string json)
{
	Debug.Log("/*/*/*/* Unity Callback received on PUSH NOTIFICATION CLICKED");
}
```

 - Add `setPushClickCallBack` in your C#(.cs) code and send the previously constructed function as an argument
    Eg.
```csharp
    WebEngage.setPushClickCallBack(handlepushNotificationClicked); 
```

*As a result, whenever you click a push notification, the code defined in the callback handler will run.*


### InApp Notification Callbacks

Set up callbacks in your Unity project by following these steps:

#### 1. InApp Notification Prepared :
 - `setInAppPreparedCallBack` allows you to obtain a callback when InApp Notification is prepared.
   
 - Create  `[MonoPInvokeCallback(typeof(callback))]` type method with any name in which you wish to receive callback

 Eg.  
```csharp
[MonoPInvokeCallback(typeof(callback))]
    public static void handleInAppNotificationPrepared(string json)
    {
        Debug.Log("/*/*/*/* Unity Callback received on InApp Notification prepared”);
    }
```

 - Add `setInAppPreparedCallBack` in your C#(.cs) code and send the previously constructed function as an argument
    Eg.
```csharp
    WebEngage.setInAppPreparedCallBack(handleInAppNotificationPrepared); 
```

*As a result, whenever you receive a push notification, the code defined in the callback handler will run.*



#### 2. InApp Notification Shown :
 - `setInAppShownCallBack` allows you to obtain a callback when InApp Notification is shown on the screen.
   
 - Create  `[MonoPInvokeCallback(typeof(callback))]` type method with any name in which you wish to receive callback

 Eg.  
```csharp
[MonoPInvokeCallback(typeof(callback))]
public static void handleInAppNotificationShown(string json)
{
	Debug.Log("/*/*/*/* Unity Callback received on InApp Notification Shown“);
}
```

 - Add `setInAppShownCallBack` in your C#(.cs) code and send the previously constructed function as an argument    
 
Eg.
```csharp
    WebEngage.setInAppShownCallBack(handleInAppNotificationShown); 
```

*As a result, whenever InApp Notification is shown on to the screen, the code defined in the callback handler will run.*

#### 3. InApp Notification Clicked :
 - `setInAppClickedCallBack` allows you to obtain a callback when InApp Notification is Clicked.
   
 - Create  `[MonoPInvokeCallback(typeof(callback))]` type method with any name in which you wish to receive callback

 Eg.  
```csharp
[MonoPInvokeCallback(typeof(callback))]
public static void handleInAppNotificationClicked(string json)
{
	Debug.Log("/*/*/*/* Unity Callback received on InApp Notification Clicked“);
}
```

 - Add `setInAppClickedCallBack` in your C#(.cs) code and send the previously constructed function as an argument    
 
Eg.
```csharp
    WebEngage.setInAppClickedCallBack(handleInAppNotificationClicked); 
```

*As a result, whenever InApp Notification is Clicked, the code defined in the callback handler will run.*

#### 4.  InApp Notification Dismissed :
 - `setInAppDismissedCallBack` allows you to obtain a callback when InApp Notification is Dismissed.
   
 - Create  `[MonoPInvokeCallback(typeof(callback))]` type method with any name in which you wish to receive callback

 Eg.  
```csharp
[MonoPInvokeCallback(typeof(callback))]
public static void handleInAppNotificationDismissed(string json)
{
	Debug.Log("/*/*/*/* Unity Callback received on InApp Notification Dismissed“);
}
```

 - Add `setInAppDismissedCallBack` in your C#(.cs) code and send the previously constructed function as an argument    
 
Eg.
```csharp
    WebEngage.setInAppDismissedCallBack(handleInAppNotificationDismissed); 
```

*As a result, whenever InApp Notification is Dismissed, the code defined in the callback handler will run.*
