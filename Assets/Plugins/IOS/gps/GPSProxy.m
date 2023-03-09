#import "GPSProxy.h"

GPSProxy* gpsNative=nil;

@implementation GPSProxy

@synthesize callback_gps_panel;
@synthesize callback_gps_method;


+(instancetype)instance
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        gpsNative = [[self alloc] init];
        [AMapServices sharedServices].apiKey=@"7c9f1a196fc2c9bbb694058f3a70c6f2";
        [gpsNative startlocation];
    });
    return gpsNative;
}

int _isOpenGPS()
{
	 if (([CLLocationManager locationServicesEnabled]) && ([CLLocationManager authorizationStatus] == 4||[CLLocationManager authorizationStatus]==3))
	 {
        
         return 1;
	 }
	 else
	 {
        	return 0;
	 }
}

void _openGPS()
{
    //定位服务设置界面
   // NSURL *url = [NSURL URLWithString:@"prefs:root=LOCATION_SERVICES"];
   // if ([[UIApplication sharedApplication] canOpenURL:url])
   // {
   //     [[UIApplication sharedApplication] openURL:url];
   // }
     [[UIApplication sharedApplication]openURL:[NSURL URLWithString:UIApplicationOpenSettingsURLString]];
}

void _getGPSLocation(char* panel,char* method)
{
	 GPSProxy *proxy= [GPSProxy instance];
	 proxy.callback_gps_panel=[NSString stringWithCString:panel encoding:NSUTF8StringEncoding];
	 proxy.callback_gps_method=[NSString stringWithCString:method encoding:NSUTF8StringEncoding];
    
	[proxy startLoc];
}

-(void) startLoc
{
    [locations requestLocationWithReGeocode:YES completionBlock:^(CLLocation *location, AMapLocationReGeocode *regeocode, NSError *error) {
        
        if (error)
        {
         //   NSLog(@"locError:{%ld - %@};", (long)error.code, error.localizedDescription);
            
            if (error.code == AMapLocationErrorLocateFailed)
            {
                return;
            }
        }
        
//      NSLog(@"location:%@", location);
        
        if (regeocode)
        {
            NSString* latitude=[NSString stringWithFormat:@"%f",location.coordinate.latitude];
            NSString* longitude=[NSString stringWithFormat:@"%f", location.coordinate.longitude];
            NSString* loc=[latitude stringByAppendingFormat:@"%@%@",@",",longitude];
            
            
            GPSProxy *proxy= [GPSProxy instance];
            UnitySendMessage([proxy.callback_gps_panel UTF8String], [proxy.callback_gps_method UTF8String], [loc cStringUsingEncoding:NSUTF8StringEncoding]);
           // NSLog(@"reGeocode:%@", regeocode);
        }
    }];}

-(void)startlocation
{
    locations=[[AMapLocationManager alloc] init];
   
    locations.delegate = self;
    
    // 带逆地理信息的一次定位（返回坐标和地址信息）
    [locations setDesiredAccuracy:kCLLocationAccuracyBest];
    //   定位超时时间，最低2s，此处设置为10s
    locations.locationTimeout =5;
    //   逆地理请求超时时间，最低2s，此处设置为10s
    locations.reGeocodeTimeout = 5;
   // if([CLLocationManager locationServicesEnabled])
   // {
        
    //    locationManager=[[CLLocationManager alloc] init];
    //    locationManager.delegate=self;
    //    [locationManager requestAlwaysAuthorization];
     //   currentCity=[[NSString alloc] init];
     //   [locationManager requestWhenInUseAuthorization];
        
      //  locationManager.desiredAccuracy=kCLLocationAccuracyBest;
     //   locationManager.distanceFilter=5.0;
     //   [locationManager startUpdatingLocation];
        
  //  }
}


#pragma mark CoreLocation delegate(定位失败!)
-(void)locationManager:(CLLocationManager *)manager didFailWithError:(NSError *)error
{
    
}


#pragma mark CoreLocation delegate(定位成功)
-(void) locationManager:(CLLocationManager *)manager didUpdateLocations:(NSArray<CLLocation *> *)locations
{
	[locationManager stopUpdatingLocation];

	CLLocation *currentLocation=[locations lastObject];

	NSString* latitude=[NSString stringWithFormat:@"%f",currentLocation.coordinate.latitude];
	NSString* longitude=[NSString stringWithFormat:@"%f",currentLocation.coordinate.longitude];
	NSString* loc=[latitude stringByAppendingFormat:@"%@%@",@",",longitude];
    
    
	GPSProxy *proxy= [GPSProxy instance];
	UnitySendMessage([proxy.callback_gps_panel UTF8String], [proxy.callback_gps_method UTF8String], [loc cStringUsingEncoding:NSUTF8StringEncoding]);
}
@end