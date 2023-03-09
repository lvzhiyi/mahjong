using UnityEngine;

namespace cambrian.game
{
    public class AdapterManager
    {
        public static AdapterManager adapter = new AdapterManager();

        public static int width = 1920;

        //public static int adapterx = Screen.width-2016>0? Screen.width - 2016:0;

        public static int getAdapterx()
        {
            return Screen.width - (int)(Screen.height * 1.775f);
        }

        public static bool isAdapterx()
        {
            return false;
//
//#if UNITY_ANDROID
//            return false;
//#endif
//
//#if UNITY_IPHONE 
//            if (Screen.width * 1.0f / Screen.height <= 1.61f|| (Screen.width <= width&&Screen.width!= 1792))
//                return false;
//            return true;
//#endif

        }
    }
}
