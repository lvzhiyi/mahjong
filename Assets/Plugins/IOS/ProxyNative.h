#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import "WXApi.h"
#import "sys/utsname.h"
#import <DTShareKit/DTOpenKit.h>

@interface ProxyNative : UIResponder<UIApplicationDelegate,WXApiDelegate,DTOpenAPIDelegate>
{
    NSString *callback_login_panel;
    NSString *callback_login_method;
}


//@property (strong,nonatomic) UIWindow *window;

@property (nonatomic,retain) NSString *callback_login_panel;
@property (nonatomic,retain) NSString *callback_login_method;
@property (nonatomic,retain) NSString *shareid;
@property (strong, nonatomic) UIViewController *viewController;

+(ProxyNative *)instance;

+(NSData*) compressImage:(UIImage*)originImage PixelCompress:(BOOL)pc MaxPixel:(CGFloat)maxPixel JPEGCompress:(BOOL)jc MaxSize_KB:(CGFloat)maxKB;

+ (UIImage *)convertImage:(UIImage *)origImage scope:(CGFloat)scope;
@end
