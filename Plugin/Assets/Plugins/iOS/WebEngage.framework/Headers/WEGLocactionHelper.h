//
//  WEGLocactionHelper.h
//  WebEngage
//
//  Created by Shubham Naidu on 05/06/24.
//  Copyright © 2024 Saumitra R. Bhave. All rights reserved.
//
#import <Foundation/Foundation.h>
#ifdef __has_include
#if __has_include(<WELocation/WEGLocationManager.h>)
#define INCLUDE_WELOCATION
#import <WELocation/WELocationManagerProtocol.h>
#endif
#endif

#ifdef INCLUDE_WELOCATION
@interface WEGLocactionHelper : NSObject <LocationManagerProtocol>
#else
@interface WEGLocactionHelper : NSObject
#endif

+ (instancetype)sharedInstance;
- (void)saveToPreferencesTheKey:(NSString *)aKey withValue:(id)aVal shouldOverride:(BOOL)theOverride;
- (void)trackSDKEventWithName:(NSString *)eventName andValue:(NSDictionary *)eventData;

@end
