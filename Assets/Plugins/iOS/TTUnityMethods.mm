//
//  UnityMethods.m
//  TGSDK
//
//  Created by Sun, Han (Jeremiah) on 10/25/15.
//  Copyright © 2015 SoulGame. All rights reserved.
//

#import "TTUnityMethods.h"
#import <TTTracker/TTTracker+Game.h>
#import <UIKit/UIKit.h>

#define CHAR2NSSTRING(c) (c?[NSString stringWithUTF8String:c]:@"")

extern "C" {
    void UnitySendMessage(const char* obj, const char* method, const char* msg);
};

@implementation TTUnityMethods

@end


extern "C" {
    char* _TT_MakeStringCopy (const char* string) {
        if (string == NULL)
            return NULL;
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }
    
    void _TT_setConfigParams(const char* useruniqueid, bool needencrypt) {
        [[TTTracker sharedInstance] setConfigParamsBlock:^(void) {
            NSMutableDictionary *params = [NSMutableDictionary dictionary];
            [params setValue:CHAR2NSSTRING(useruniqueid) forKey:@"useruniqueid"];
            [params setValue:@(needencrypt) forKey:@"needencrypt"];
            return [params copy];
        }];
    }
    
    void _TT_setSession(bool enable) {
        [[TTTracker sharedInstance] setSessionEnable:enable];
    }
    
    void _TT_start(const char* appID, const char* channel, const char* appName) {
        [TTTracker startWithAppID:CHAR2NSSTRING(appID) channel:CHAR2NSSTRING(channel) appName:CHAR2NSSTRING(appName)];
    }
    
    void _TT_registerEvent(const char* method, bool isSuccess) {
        [TTTracker registerEventByMethod:CHAR2NSSTRING(method) isSuccess:YES];
    }
    
    void _TT_purchaseEvent(const char* contentType, const char* contentName, const char* contentID, int contentNumber, const char* paymentChannel, const char* currency, unsigned long long currency_amount, bool isSuccess) {
        [TTTracker purchaseEventWithContentType:CHAR2NSSTRING(contentType) contentName:CHAR2NSSTRING(contentName) contentID:CHAR2NSSTRING(contentID) contentNumber:contentNumber paymentChannel:CHAR2NSSTRING(paymentChannel) currency:CHAR2NSSTRING(currency) currency_amount:currency_amount isSuccess:YES];
    }
    
    void _TT_updateLevelEvent(int level) {
        [TTTracker updateLevelEventWithLevel:level];
    }
    
    void _UploadEvent(const char* v3, const char* jsonParams) {
        NSError *error;
        NSDictionary *jsonDict = [NSJSONSerialization JSONObjectWithData:[CHAR2NSSTRING(jsonParams)  dataUsingEncoding:NSUTF8StringEncoding] options:0 error:&error];
        [TTTracker eventV3:CHAR2NSSTRING(v3) params:jsonDict];
	NSLog(@"UploadEvent");
    }
}
