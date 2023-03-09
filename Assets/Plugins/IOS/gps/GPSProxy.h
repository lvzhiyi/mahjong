#import <UIKit/UIKit.h>
#import <CoreLocation/CoreLocation.h>
#import <AMapFoundationKit/AMapFoundationKit.h>
#import <AMapLocationKit/AMapLocationKit.h>

@interface GPSProxy:UIViewController<CLLocationManagerDelegate,AMapLocationManagerDelegate>
{
	CLLocationManager *locationManager;//定位服务
	NSString* currentCity;//当前城市
	NSString* strlatitude;//经度
	NSString* strlongitude;//维度

	NSString *callback_gps_panel;
	NSString *callback_gps_method;

	AMapLocationManager *locations;
}

@property (nonatomic,retain) NSString *callback_gps_panel;
@property (nonatomic,retain) NSString *callback_gps_method;

@end