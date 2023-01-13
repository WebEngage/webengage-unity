//MARK: -  WebEngageUnity public interface

#import <WebEngage/WebEngage.h>

@interface WebEngageUnity: NSObject<WEGAppDelegate, WEGInAppNotificationProtocol>
+ (instancetype)sharedInstance;
@end

// MARK: - SDK C++ methods exposed for C# of Unity

#ifdef __cplusplus
extern "C" {
#endif

    void login(const char* cuid);

    void logout(void);

    void trackEvent(const char* event);

    void trackEventWithAttributes(const char* event, const char* attributes);

    void setFirstName(const char* firstName);

    void setLastName(const char* lastName);

    void setEmail(const char* email);

    void setHashedEmail(const char* hashedEmail);

    void setPhoneNumber(const char* phoneNumber);

    void setHashedPhoneNumber(const char* hashedPhoneNumber);

    void setGender(const char* gender);

    void setBirthDate(const char* birthDate);

    void setCompany(const char* company);

    void setOptIn(const char* channel, bool optIn);

    void setLocation(double latitude, double longitude);

    void setUserAttributeString(const char* key, const char* value);

    void setUserAttributeBool(const char* key, bool value);

    void setUserAttributeInt(const char* key, int value);

    void setUserAttributeLong(const char* key, long value);

    void setUserAttributeFloat(const char* key, float value);

    void setUserAttributeDouble(const char* key, double value);

    void setUserAttributeDate(const char* key, const char* value);

    void setUserAttributes(const char* userAttributes);

    void deleteUserAttribute(const char* key);

    void deleteUserAttributes(const char* keys);

    void screenNavigated(const char* screen);

    void screenNavigatedWithData(const char* screen, const char* data);

    void setScreenData(const char* data);
    
    typedef void (*notificationCallBack)(const char* pushData);

    bool isSDKInitialised(void);

    void checkDelegateSanity(void);

    // Push Notifications callback

    void pushClickCallBack(notificationCallBack callback);

    // In App Notifications callback

    void InAppPreparedCallBack(notificationCallBack callback);
    void InAppShownCallBack(notificationCallBack callback);
    void InAppCLickedCallBack(notificationCallBack callback);
    void InAppDismissedCallBack(notificationCallBack callback);

#ifdef __cplusplus
}
#endif
