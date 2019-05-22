//
//  TTT.mm
//  Unity-iPhone
//
//  Created by tangz on 2019/4/28.
//

#import <TTTracker/TTTracker.h>
#import <TTTracker/TTTracker+Game.h>

extern "C" {
    
    //初始化
    //appID和channel分别表示app唯⼀一标示(头条数据仓库组分配)和App发布的渠道名(建议内测版⽤用loc altest，正式版⽤用App Store，灰度版⽤用发布的渠道名，如pp)
    bool _TTInit(char *appid,char *channel,char *appname){
       NSLog(@"TTTINIT_Start");
        [TTTracker startWithAppID:[NSString stringWithCString:appid encoding:NSUTF8StringEncoding] channel:[NSString stringWithCString:channel encoding:NSUTF8StringEncoding] appName:[NSString stringWithCString:appname encoding:NSUTF8StringEncoding]];
        NSLog(@"TTTINIT_End");
        return YES;
    }
    
    void _UploadPurchase(char *content, char *type,int payment,BOOL result){
        NSLog(@"Purchase_Start");
        [TTTracker purchaseEventWithContentType:[NSString stringWithCString:content encoding:NSUTF8StringEncoding]
                                    contentName:[NSString stringWithCString:type encoding:NSUTF8StringEncoding]
                                      contentID:@"null"
                                  contentNumber:-1
                                 paymentChannel:@"null"
                                       currency:@"null"
                                currency_amount:payment
                                      isSuccess:result];
        NSLog(@"Purchase_End");
    }
}
