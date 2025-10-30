//
//  WEGInlineCGHelper.h
//  WebEngage
//
//  Created by Shubham Naidu on 09/07/24.
//  Copyright © 2024 Saumitra R. Bhave. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface WEGInlineCGHelper : NSObject

+ (void)handleControlGroupNotification:(NSDictionary *)notif variation:(NSDictionary*)variations eventName:(NSString *)eventName;

@end

