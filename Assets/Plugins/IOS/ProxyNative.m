#import "WXApi.h"
#import "ProxyNative.h"
#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <AudioToolbox/AudioToolbox.h>
#import "Reachability.h"
#import "dlfcn.h"

#define IOS_CELLULAR    @"pdp_ip0"
#define IOS_WIFI        @"en0"
#define IOS_VPN         @"utun0"
#define IP_ADDR_IPv4    @"ipv4"
#define IP_ADDR_IPv6    @"ipv6"
#import <ifaddrs.h>
#import <arpa/inet.h>
#import <net/if.h>

#import <DTShareKit/DTOpenKit.h>
#import <QiSuDunSdk/XQiSuDunManager.h>


ProxyNative *proxyNative=nil;

@implementation ProxyNative

@synthesize callback_login_panel;
@synthesize callback_login_method;
@synthesize shareid;


+(instancetype)instance
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        proxyNative = [[self alloc] init];
    });
    return proxyNative;
}

+(NSData*) compressImage:(UIImage*)originImage PixelCompress:(BOOL)pc MaxPixel:(CGFloat)maxPixel JPEGCompress:(BOOL)jc MaxSize_KB: (CGFloat)maxKB
{
    /*
     压缩策略： 支持最多921600个像素点
     像素压缩：（调整像素点个数）
     当图片长宽比小于3:1 or 1:3时，图片长和宽最多为maxPixel像素；
     当图片长宽比在3:1 和 1:3之间时，会保证图片像素压缩到921600像素以内；
     JPEG压缩：（调整每个像素点的存储体积）
     默认压缩比0.99;
     如果压缩后的数据仍大于IMAGE_MAX_BYTES，那么将调整压缩比将图片压缩至IMAGE_MAX_BYTES以下。
     策略调整：
     不进行像素压缩，或者增大maxPixel，像素损失越小。
     使用无损压缩，或者增大IMAGE_MAX_BYTES.
     注意：
     jepg压缩比为0.99时，图像体积就能压缩到原来的0.40倍了。
     */
    UIImage * scopedImage = nil;
    NSData * data = nil;
    //CGFloat maxbytes = maxKB * 1024;
    
    if (originImage == nil) {
        return nil;
    }
    
    if ( pc == YES ) {    //像素压缩
        // 像素数最多为maxPixel*maxPixel个
        CGFloat photoRatio = originImage.size.height / originImage.size.width;
        if ( photoRatio < 0.3333f )
        {                           //解决宽长图的问题
            CGFloat FinalWidth = sqrt ( maxPixel*maxPixel/photoRatio );
            scopedImage = [ProxyNative convertImage:originImage scope:MAX(FinalWidth, maxPixel)];
        }
        else if ( photoRatio <= 3.0f )
        {                           //解决高长图问题
            scopedImage = [ProxyNative convertImage:originImage scope: maxPixel];
        }
        else
        {                           //一般图片
            CGFloat FinalHeight = sqrt ( maxPixel*maxPixel*photoRatio );
            scopedImage = [ProxyNative convertImage:originImage scope:MAX(FinalHeight, maxPixel)];
        }
    }
    else {          //不进行像素压缩
        scopedImage = originImage;
    }
    
    [scopedImage retain];
    
    if ( jc == YES ) {     //JPEG压缩
        data = UIImageJPEGRepresentation(scopedImage, 0.8);
        //NSLog(@"data compress with ratio (0.9) : %d KB", data.length/1024);
    }
    else {
        data = UIImageJPEGRepresentation(scopedImage, 1.0);
        //NSLog(@"data compress : %d KB", data.length/1024);
    }
    
    [scopedImage release];
    
    return data;
}

+ (UIImage *)convertImage:(UIImage *)origImage scope:(CGFloat)scope
{
    UIImage *image = nil;
    CGSize size = origImage.size;
    if (size.width < scope && size.height < scope) {
        // do nothing
        image = origImage;
    } else {
        CGFloat length = size.width;
        if (size.width < size.height) {
            length = size.height;
        }
        CGFloat f = scope/length;
        NSInteger nw = size.width * f;
        NSInteger nh = size.height * f;
        if (nw > scope) {
            nw = scope;
        }
        if (nh > scope) {
            nh = scope;
        }
        
        CGSize newSize = CGSizeMake(nw, nh);
        //        CGSize newSize = CGSizeMake(size.width*f, size.height*f);
        
        //
        UIGraphicsBeginImageContext(newSize);
        //UIGraphicsBeginImageContextWithOptions(newSize, NO, 0.0f);
        // Tell the old image to draw in this new context, with the desired
        // new size
        [origImage drawInRect:CGRectMake(0, 0, newSize.width, newSize.height)];
        // Get the new image from the context
        image = UIGraphicsGetImageFromCurrentImageContext();
        UIGraphicsEndImageContext();
    }
    return image;
}

void _openWeiXin()
{
    [WXApi openWXApp];
}

void _exitsystem()
{
	exit(0);
}

int _getSislePort(char* id,int port)
{
	return XQiSuDunManagerOpen([NSString stringWithCString:id encoding:NSUTF8StringEncoding],port);
}

char* _getShareInfo()
{
    NSString* info= [ProxyNative instance].shareid;
    if(info==NULL)
    {
        return 0;
    }
    const char* bb= [info UTF8String];
    
    //[ProxyNative instance].shareid=@"0";
    char* cc=(char*)malloc(strlen(bb)+1);
    strcpy(cc, bb);
    
    [ProxyNative instance].shareid=@"0";
    return cc;
    //return 0;
}

char* _getClipborad()
{
	UIPasteboard *board = [UIPasteboard generalPasteboard]; 
	NSString* text=board.string;
	if(!text||[text length]==0||text==NULL||text==nil)
	{
		text=@"-1";
	}
	NSLog(@"%@",text); 
	const char* tt=[text UTF8String];
	char* cc=(char*)malloc(strlen(tt)+1);
	strcpy(cc,tt);
	return cc;
}

char* _setClipContent(char* content)
{
	UIPasteboard *board = [UIPasteboard generalPasteboard]; 
	NSString *ct=[NSString stringWithCString:content encoding:NSUTF8StringEncoding];
	board.string=ct;
    
	const char* t="1";
	char* cc=(char*)malloc(strlen(t)+1);
	strcpy(cc,t);
	return cc;
}


bool _isInstallWX()
{
   if([[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:@"wx376e0766fd140c63://"]])
        return true;
    else
        return false;
}

bool _isInstallXL()
{
    
    return false;
}

bool _isInstallCN()
{
	return false;
}

bool _isInstallDD()
{	if([DTOpenAPI isDingTalkInstalled]) return true;
	return false;
}

void _initWeiXin(char* appid,char* xlappid)
{
   
    
}

int _checkVersion()
{
    return 0;
}

void _login(char* scope,char* state,char* panel,char* method)
{
     ProxyNative *proxy= [ProxyNative instance];
     proxy.callback_login_panel=[NSString stringWithCString:panel encoding:NSUTF8StringEncoding];
     proxy.callback_login_method=[NSString stringWithCString:method encoding:NSUTF8StringEncoding];
    
    SendAuthReq* req=[[[SendAuthReq alloc] init] autorelease];
    NSString *s=[NSString stringWithCString:scope encoding:NSUTF8StringEncoding];
    NSString *st=[NSString stringWithCString:state encoding:NSUTF8StringEncoding];
    req.scope=s;
    req.state=st;
    [WXApi sendAuthReq:req viewController:[ProxyNative instance].viewController delegate:[ProxyNative instance] completion:nil];
}

void _loginXL(char* state,char* panel,char* method)
{
	 
}

void _shareIconToXianLiao(char* data,int length)
{
    
}

void _shareIconToChuiNiu(char* data,int length)
{
	
}


 void _shareRoomToChuiNiu(char* url,char* title,char* description,int roomid,char* token)
{
	
}

  void _shareLineToXianLiao(char* url,char* title,char* description,int roomid,char* token)
{
   
}
//分享链接
void _shareLineToWeiXin(char* url,char* title,char* description,int type)
{
    WXMediaMessage *message=[WXMediaMessage message];
    message.title=[NSString stringWithCString:title encoding:NSUTF8StringEncoding];
    message.description=[NSString stringWithCString:description encoding:NSUTF8StringEncoding];
    
    NSString *path=[[NSBundle mainBundle] pathForResource:@"share" ofType:@"png"];
    [message setThumbImage:[UIImage imageWithContentsOfFile:path]];
    
    //[message setThumbImage:[UIImage imageNamed:@"AppIcon40x40.png"]];
    
    WXWebpageObject *webpageObject=[WXWebpageObject object];
    webpageObject.webpageUrl=[NSString stringWithCString:url encoding:NSUTF8StringEncoding];
    message.mediaObject=webpageObject;
    
    SendMessageToWXReq *req=[[SendMessageToWXReq alloc] init];
    req.bText=NO;
    req.message=message;
    if(type==1)
    {
        req.scene=WXSceneSession;
    }
    else
    {
        req.scene=WXSceneTimeline;
    }
    
    [WXApi sendReq:req completion:^(BOOL callBackType) {
        //NSLog(@"======================");
    }];
}

//分享图片
void _shareIconToWeiXin(char* data,int length,int type)
{
    CGDataProviderRef provider=CGDataProviderCreateWithData(NULL, data, length, NULL);
    CGImageRef img=CGImageCreateWithJPEGDataProvider(provider, NULL, false, kCGRenderingIntentDefault);
    UIImage* uiImage=[UIImage imageWithCGImage:img];
    
    NSData *imagedata= [ProxyNative compressImage:uiImage PixelCompress:YES MaxPixel:128 JPEGCompress:YES MaxSize_KB:32];
    
    WXMediaMessage *message=[WXMediaMessage message];
    [message setThumbImage:[UIImage imageWithData:imagedata]];
    
    
    WXImageObject *imageobject=[WXImageObject object];
    
    imageobject.imageData=UIImageJPEGRepresentation(uiImage,1);
    
    message.mediaObject=imageobject;
    
    
    SendMessageToWXReq *req=[[SendMessageToWXReq alloc] init];
    req.bText=NO;
    req.message=message;
    if(type==1)
    {
        req.scene=WXSceneSession;
    }
    else{
        req.scene=WXSceneTimeline;
    }
    [WXApi sendReq:req completion:nil];
}

void _payWx(char* partnerId,char* prepayId,char* packageVal,char* nonceStr,char* timeStamp,char* sign)
{
    
    NSString *pa=[NSString stringWithCString:partnerId encoding:NSUTF8StringEncoding];
    NSString *pr=[NSString stringWithCString:prepayId encoding:NSUTF8StringEncoding];
    NSString *pack=[NSString stringWithCString:packageVal encoding:NSUTF8StringEncoding];
    NSString *n=[NSString stringWithCString:nonceStr encoding:NSUTF8StringEncoding];
    NSString *t=[NSString stringWithCString:timeStamp encoding:NSUTF8StringEncoding];
    NSString *s=[NSString stringWithCString:sign encoding:NSUTF8StringEncoding];
    
    PayReq *req = [[[PayReq alloc] init] autorelease];
    req.partnerId = pa;
    req.prepayId= pr;
    req.package = pack;
    req.nonceStr= n;
    
    req.timeStamp=(UInt32)[t intValue];
    req.sign= s;
    [WXApi sendReq:req completion:nil];
}



-(void) onReq:(BaseReq *)req
{
    
}

-(void) onResp:(BaseResp *)resp;
{
    
    if([resp isKindOfClass:[SendAuthResp class]])
    {
        SendAuthResp *auth=(SendAuthResp *)resp;
        
        NSString *ecode=[NSString stringWithFormat:@"%d",auth.errCode];
        
        NSString *conent=nil;
        
        if(auth.errCode==WXSuccess)
        {
            NSString *code=auth.code;
            NSString *state=auth.state;
            conent=[NSString stringWithFormat:@"%@%s%@%s%@",ecode,",",code,",",state];
        }
        else
        {
            conent=ecode;
        }
        
        ProxyNative *proxy= [ProxyNative instance];
        
        UnitySendMessage([proxy.callback_login_panel UTF8String], [proxy.callback_login_method UTF8String], [conent cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    else if([resp isKindOfClass:[PayResp class]]){
        ProxyNative *proxy= [ProxyNative instance];
        UnitySendMessage([proxy.callback_login_panel UTF8String], [proxy.callback_login_method UTF8String], [[NSString stringWithFormat:@"%d",resp.errCode] UTF8String]);
    }
    else if([resp isKindOfClass:[SendMessageToWXResp class]])
    {
	SendMessageToWXResp *messageResp = (SendMessageToWXResp *)resp;
	char* panel="Root";
	char* method="share_call_back";
	 UnitySendMessage(panel,method , [[NSString stringWithFormat:@"%d",messageResp.errCode] UTF8String]);
    }
}

//震动
void _vibrate()
{
	AudioServicesPlaySystemSound(kSystemSoundID_Vibrate);
}

//剩余电量
void _getCursBattery(char* panel,char* method)
{
    [UIDevice currentDevice].batteryMonitoringEnabled=YES;
    int b= (int)([UIDevice currentDevice].batteryLevel*100);
    UnitySendMessage(panel, method, [[NSString stringWithFormat:@"%d",b] UTF8String]);
}

int isNetWork()
{
    Reachability *r = [Reachability reachabilityWithHostName:@"www.apple.com"];
    switch ([r currentReachabilityStatus]) {
        case NotReachable:
            // 没有网络连接
            return -1;
        case ReachableViaWWAN:
            // 使用3G网络
            return 0;
        case ReachableViaWiFi:
            // 使用WiFi网络
           return 1;
    }
}

void _getNetWorksStrength(char* panel,char* method)
{
    NSString* result=@"";
    int type=isNetWork();
    
  
    NSString *t=nil;
    NSString *strens=nil;
       if(type==-1)
       {
           UnitySendMessage(panel, method, "-1,-1");
       }
       else if(type==0)
       {
           id type=nil;
           UIApplication *app = [UIApplication sharedApplication];
           
           NSArray *children=nil;
           if([[app valueForKeyPath:@"_statusBar"] isKindOfClass:NSClassFromString(@"UIStatusBar_Modern")])
           {
                children = [[[[app valueForKeyPath:@"_statusBar"] valueForKeyPath:@"_statusBar"] valueForKeyPath:@"foregroundView"] subviews];
           }
           else
           {
                children = [[[app valueForKeyPath:@"_statusBar"] valueForKeyPath:@"foregroundView"] subviews];
           }
           
           for (id child in children) {
               if ([child isKindOfClass:NSClassFromString(@"UIStatusBarSignalStrengthItemView")]) {
                   type = [child valueForKeyPath:@"signalStrengthRaw"];
                   break;
               }
           }
           NSInteger signalStregth=[type integerValue];
           char* stregth="0,-1";
           
           if(signalStregth>-85)
           {
               stregth="0,4";
           }
           else if(signalStregth>-95)
           {
               stregth="0,3";
           }
           else if(signalStregth>-105)
           {
               stregth="0,2";
           }
           else if(signalStregth>-115)
           {
               stregth="0,1";
           }
           else if(signalStregth>-150)
           {
               stregth="0,0";
           }
           
           UnitySendMessage(panel, method, stregth);
       }
       else
       {
           
	          UIApplication *app = [UIApplication sharedApplication];
	         // NSArray *subviews = [[[app valueForKey:@"statusBar"] valueForKey:@"foregroundView"] subviews];
           
           
           NSArray *subviews=nil;
           if([[app valueForKeyPath:@"_statusBar"] isKindOfClass:NSClassFromString(@"UIStatusBar_Modern")])
           {
               subviews = [[[[app valueForKeyPath:@"_statusBar"] valueForKeyPath:@"_statusBar"] valueForKeyPath:@"foregroundView"] subviews];
           }
           else
           {
               subviews = [[[app valueForKeyPath:@"_statusBar"] valueForKeyPath:@"foregroundView"] subviews];
           }
           
	          NSString *dataNetworkItemView = nil;
    
	          for (id subview in subviews)
              {
	              if([subview isKindOfClass:[NSClassFromString(@"UIStatusBarDataNetworkItemView") class]])
                  {
		               dataNetworkItemView = subview;
		                break;
	              }
               }
           
           int signalStrength = [[dataNetworkItemView valueForKey:@"_wifiStrengthBars"] intValue];
           
           
           
           t=@"1,";
           strens=[NSString stringWithFormat:@"%d",signalStrength];
           result=[NSString stringWithFormat:@"%@%@",t,strens];
           UnitySendMessage(panel, method, [result UTF8String]);
	}
}


typedef NS_ENUM(NSInteger,DeviceType) {
    
    Unknown = 0,
    Simulator,
    IPhone_1G,          //基本不用
    IPhone_3G,          //基本不用
    IPhone_3GS,         //基本不用
    IPhone_4,           //基本不用
    IPhone_4s,          //基本不用
    IPhone_5,
    IPhone_5C,
    IPhone_5S,
    IPhone_SE,
    IPhone_6,
    IPhone_6P,
    IPhone_6s,
    IPhone_6s_P,
    IPhone_7,
    IPhone_7P,
    IPhone_8,
    IPhone_8P,
    IPhone_X,
};

char* _getPhoneMode()
{
	  // NSString *b=@"1";
           struct utsname systemInfo;
           uname(&systemInfo);
           NSString* deviceString = [NSString stringWithCString:systemInfo.machine encoding:NSUTF8StringEncoding];
           //iPhone
          // if ([deviceString isEqualToString:@"iPhone1,1"])    b=@"iPhone 1G";
           //if ([deviceString isEqualToString:@"iPhone1,2"])    b= @"iPhone 3G";
           //if ([deviceString isEqualToString:@"iPhone2,1"])    b= @"iPhone 3GS";
           //if ([deviceString isEqualToString:@"iPhone3,1"])    b= @"iPhone 4";
           //if ([deviceString isEqualToString:@"iPhone3,2"])    b= @"Verizon iPhone 4";
           //if ([deviceString isEqualToString:@"iPhone4,1"])    b= @"iPhone 4S";
           //if ([deviceString isEqualToString:@"iPhone5,1"])    b=@"iPhone 5";
           //if ([deviceString isEqualToString:@"iPhone5,2"])    b= @"iPhone 5";
           //if ([deviceString isEqualToString:@"iPhone5,3"])    b= @"iPhone 5C";
           //if ([deviceString isEqualToString:@"iPhone5,4"])    b=@"iPhone 5C";
           //if ([deviceString isEqualToString:@"iPhone6,1"])    b=@"iPhone 5S";
           //if ([deviceString isEqualToString:@"iPhone6,2"])   b= @"iPhone 5S";
           //if ([deviceString isEqualToString:@"iPhone7,1"])    b= @"iPhone 6 Plus";
           //if ([deviceString isEqualToString:@"iPhone7,2"])    b=@"iPhone 6";
           //if ([deviceString isEqualToString:@"iPhone8,1"])    b= @"iPhone 6s";
           //if ([deviceString isEqualToString:@"iPhone8,2"])   b= @"iPhone 6s Plus";
	   //if ([deviceString isEqualToString:@"iPhone10,4"])    b=@"IPhone8";
	   //if ([deviceString isEqualToString:@"iPhone10,2"])    b=@"IPhone8P";
           //if ([deviceString isEqualToString:@"iPhone10,5"])    b=@"IPhone8P";
           //if ([deviceString isEqualToString:@"iPhone10,3"])     b=@"IPhoneX";
           //if ([deviceString isEqualToString:@"iPhone10,6"])    b=@"IPhoneX";
		
	const char* tt=[deviceString UTF8String];
	char* cc=(char*)malloc(strlen(tt)+1);
	strcpy(cc,tt);

	   return cc;
}

char* _getSystemVersion()
{
	NSString* phoneVersion = [[UIDevice currentDevice] systemVersion];
	const char* tt=[phoneVersion UTF8String];
	char* cc=(char*)malloc(strlen(tt)+1);
	strcpy(cc,tt);
	return cc;
}


bool _isIpv6()
{
    NSArray *searchArray =
    @[ IOS_VPN @"/" IP_ADDR_IPv6,
       IOS_VPN @"/" IP_ADDR_IPv4,
       IOS_WIFI @"/" IP_ADDR_IPv6,
       IOS_WIFI @"/" IP_ADDR_IPv4,
       IOS_CELLULAR @"/" IP_ADDR_IPv6,
       IOS_CELLULAR @"/" IP_ADDR_IPv4 ] ;
    
    NSDictionary *addresses = [ProxyNative getIPAddresses];
  //  NSLog(@"addresses: %@", addresses);
    
    __block BOOL isIpv6 = NO;
    [searchArray enumerateObjectsUsingBlock:^(NSString *key, NSUInteger idx, BOOL *stop)
     {
         
       //  NSLog(@"---%@---%@---",key, addresses[key] );
         
         if ([key rangeOfString:@"ipv6"].length > 0  && ![[NSString stringWithFormat:@"%@",addresses[key]] hasPrefix:@"(null)"] ) {
             
             if ( ![addresses[key] hasPrefix:@"fe80"]) {
                 isIpv6 = YES;
             }
         }
         
     } ];
    
    return isIpv6;
}


+ (NSDictionary *)getIPAddresses
{
    NSMutableDictionary *addresses = [NSMutableDictionary dictionaryWithCapacity:8];
    // retrieve the current interfaces - returns 0 on success
    struct ifaddrs *interfaces;
    if(!getifaddrs(&interfaces)) {
        // Loop through linked list of interfaces
        struct ifaddrs *interface;
        for(interface=interfaces; interface; interface=interface->ifa_next) {
            if(!(interface->ifa_flags & IFF_UP) /* || (interface->ifa_flags & IFF_LOOPBACK) */ ) {
                continue; // deeply nested code harder to read
            }
            const struct sockaddr_in *addr = (const struct sockaddr_in*)interface->ifa_addr;
            char addrBuf[ MAX(INET_ADDRSTRLEN, INET6_ADDRSTRLEN) ];
            if(addr && (addr->sin_family==AF_INET || addr->sin_family==AF_INET6)) {
                NSString *name = [NSString stringWithUTF8String:interface->ifa_name];
                NSString *type;
                if(addr->sin_family == AF_INET) {
                    if(inet_ntop(AF_INET, &addr->sin_addr, addrBuf, INET_ADDRSTRLEN)) {
                        type = IP_ADDR_IPv4;
                        
                      //  NSLog(@"ipv4 %@",name);
                    }
                } else {
                    const struct sockaddr_in6 *addr6 = (const struct sockaddr_in6*)interface->ifa_addr;
                    if(inet_ntop(AF_INET6, &addr6->sin6_addr, addrBuf, INET6_ADDRSTRLEN)) {
                        type = IP_ADDR_IPv6;
                       // NSLog(@"ipv6 %@",name);
                        
                    }
                }
                if(type) {
                    NSString *key = [NSString stringWithFormat:@"%@/%@", name, type];
                    addresses[key] = [NSString stringWithUTF8String:addrBuf];
                }
            }
        }
        // Free memory
        freeifaddrs(interfaces);
    }
    return [addresses count] ? addresses : nil;
}


//接收邀请打开三方app回调 打开后 backInfo = CNShareInviteObject的reciveInviteBackInfo值
-(void)inviteBackWith:(id)backInfo{
    [ProxyNative instance].shareid =backInfo;
    
}




//钉钉
void _shareLineToDingDing(char *url, char *title, char *description)
{
    DTSendMessageToDingTalkReq *sendMessageReq = [[DTSendMessageToDingTalkReq alloc] init];
    
    DTMediaMessage *mediaMessage = [[DTMediaMessage alloc] init];
    DTMediaWebObject *webObject = [[DTMediaWebObject alloc] init];
    webObject.pageURL = [NSString stringWithCString:url encoding:NSUTF8StringEncoding];
    
    mediaMessage.title = [NSString stringWithCString:title encoding:NSUTF8StringEncoding];
    
    NSString *path=[[NSBundle mainBundle] pathForResource:@"share" ofType:@"png"];
    UIImage *uiImage=[UIImage imageWithContentsOfFile:path];
    
    NSData *imagedata= [ProxyNative compressImage:uiImage PixelCompress:YES MaxPixel:3000 JPEGCompress:YES MaxSize_KB:500];
    
    mediaMessage.thumbData = imagedata;
    
    // Or Set a image data which less than 32K.
    // mediaMessage.thumbData = UIImagePNGRepresentation([UIImage imageNamed:@"open_icon"]);
    
    mediaMessage.messageDescription = [NSString stringWithCString:description encoding:NSUTF8StringEncoding];
    mediaMessage.mediaObject = webObject;
    sendMessageReq.message = mediaMessage;
    
    [DTOpenAPI sendReq:sendMessageReq];
}

void _shareIconToDingDing(char* data,int length)
{
        CGDataProviderRef provider=CGDataProviderCreateWithData(NULL, data, length, NULL);
        CGImageRef img=CGImageCreateWithJPEGDataProvider(provider, NULL, false, kCGRenderingIntentDefault);
        UIImage* uiImage=[UIImage imageWithCGImage:img];
        
        NSData *imagedata= [ProxyNative compressImage:uiImage PixelCompress:YES MaxPixel:3000 JPEGCompress:YES MaxSize_KB:500];
        
        DTSendMessageToDingTalkReq *sendMessageReq = [[DTSendMessageToDingTalkReq alloc] init];
        
        DTMediaMessage *mediaMessage = [[DTMediaMessage alloc] init];
        
        DTMediaImageObject *imageObject = [[DTMediaImageObject alloc] init];
        
        imageObject.imageData =imagedata;
        
        mediaMessage.mediaObject = imageObject;
        sendMessageReq.message = mediaMessage;
        
        [DTOpenAPI sendReq:sendMessageReq];
}



@end
