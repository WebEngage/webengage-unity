//
//  WEGInAppNotificationProtocol.h
//  WebEngage
//
//  Copyright (c) 2015 Webklipper Technologies Pvt Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol WEGInAppNotificationProtocol <NSObject>

- (NSDictionary *)notificationPrepared:(NSDictionary *)inAppNotificationData shouldStop:(BOOL *)stopRendering;

- (void)notificationShown:(NSDictionary *)inAppNotificationData;

- (void)notification:(NSDictionary *)inAppNotificationData clickedWithAction:(NSString *)actionId;

- (void)notificationDismissed:(NSDictionary *)inAppNotificationData;

@end
