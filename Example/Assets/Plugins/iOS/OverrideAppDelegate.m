#import "UnityAppController.h"
#import <WebEngage/WebEngage.h>


@interface OverrideAppDelegate : UnityAppController<WEGInAppNotificationProtocol,WEGAppDelegate>
@end


IMPL_APP_CONTROLLER_SUBCLASS(OverrideAppDelegate)

@implementation OverrideAppDelegate

-(BOOL)application:(UIApplication*) application didFinishLaunchingWithOptions:(NSDictionary*) options
{
    [[WebEngage sharedInstance] application:application
              didFinishLaunchingWithOptions:options
                       notificationDelegate:nil
                               autoRegister:YES];
    return [super application:application didFinishLaunchingWithOptions:options];
}


#pragma mark - WebEngage InApp Notification Callback

//- (NSDictionary *)notificationPrepared:(NSDictionary<NSString *,id> *)inAppNotificationData shouldStop:(BOOL *)stopRendering {
//    NSLog(@"/*/*/*/* Notif Prepared: %@", inAppNotificationData);
//    const char *jsonObject = [self getJSonStringFrom:inAppNotificationData];
//    [self callUnityObject:"Main Camera" Method:"InAppNotificationPrepared" Parameter:jsonObject];
//    return inAppNotificationData;
//}
//
//- (void)notificationShown:(NSDictionary<NSString *,id> *)inAppNotificationData {
//    NSLog(@"/*/*/*/* Notif Shown: %@", inAppNotificationData);
//    const char *jsonObject = [self getJSonStringFrom:inAppNotificationData];
//    [self callUnityObject:"Main Camera" Method:"InAppNotificationShown" Parameter:jsonObject];
//}
//
//- (void)notification:(NSDictionary<NSString *,id> *)inAppNotificationData clickedWithAction:(NSString *)actionId {
//    NSLog(@"/*/*/*/* Notif Clicked: Action: %@ Data: %@", actionId, inAppNotificationData);
//    const char *jsonObject = [self getJSonStringFrom:inAppNotificationData];
//    [self callUnityObject:"Main Camera" Method:"InAppNotificationClicked" Parameter:jsonObject];
//}
//
//- (void)notificationDismissed:(NSDictionary<NSString *,id> *)inAppNotificationData {
//    NSLog(@"/*/*/*/* Notif Closed: %@", inAppNotificationData);
//    const char *jsonObject = [self getJSonStringFrom:inAppNotificationData];
//    [self callUnityObject:"Main Camera" Method:"InAppNotificationDismssed" Parameter:jsonObject];
//}
//
//- (void)callUnityObject:(const char*)object Method:(const char*)method Parameter:(const char*)parameter
//{
//    UnitySendMessage(object, method, parameter);
//}
//
//- (const char *)getJSonStringFrom:(NSDictionary *)dict{
//    NSError * err;
//    NSData * jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:&err];
//    NSString * myString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
//    return  [myString UTF8String];
//}
//
//#pragma mark - WebEngage Push Notification Callback
//- (void)WEGHandleDeeplink:(NSString *)deeplink userData:(NSDictionary *)data{
//    const char *jsonObject = [self getJSonStringFrom:data];
//    NSLog(@"/*/*/*/* Push Notification Clicked: %@", data);
//    [self callUnityObject:"Main Camera" Method:"pushNotificationClicked" Parameter:jsonObject];
//}

@end
