/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Editor.EditorMnager 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.4.0f1 
* Date:             2020-06-27
* Time:             19:29:22
* E-Mail:           1398581458@qq.com
* Description:      更新安装打包密码 无需自动输入  
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.UI
{
    using UnityEditor;

    [InitializeOnLoad]
    public class GlobalConfig
    {
        static GlobalConfig()
        {
            //PlayerSettings.companyName = "mqipai";
            //PlayerSettings.productName = "华宇游戏";
            //PlayerSettings.bundleVersion = "14.0.1";            
            //PlayerSettings.Android.keystoreName = "huayu";
            //PlayerSettings.Android.keystorePass = "huayu123sddfd3dsf";
            //PlayerSettings.Android.keyaliasName = "huayucp";
            //PlayerSettings.Android.keyaliasPass = "huayu123sddfd3dsf";
        }
    }
}