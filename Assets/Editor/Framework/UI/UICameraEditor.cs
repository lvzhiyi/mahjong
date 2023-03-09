/* * * * * * * * * * * * * * * * * * * * * * * *
* mqipai
* FileName:         Framework.UI
* Author:           XiNan
* Version:          14.0.1
* UnityVersion:     2018.2.21f1
* Date:             2021-01-27
* Time:             17:42:12
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.UI
{
    using UnityEditor;
    using Tool = EditorTool;

    [CustomEditor(typeof(UnrealCamera), false)]
    internal class UICameraEditor : Base2DEditor
    {
        private UnrealCamera uicamera;

        protected override void init()
        {
            uicamera = (UnrealCamera)target;
        }

        protected override void inspector()
        {
            Tool.BE.Vertical(() =>
            {
                uicamera.height= Tool.AC.FieldFloat("游戏逻辑 高度", uicamera.height);
                uicamera.width = Tool.AC.FieldFloat("游戏逻辑 宽度", uicamera.width);

                if (EditorApplication.isPlaying)
                {
                    Tool.EA.Space();
                    Tool.AC.Label("Game Playing");
                    Tool.AC.FieldText("适配 X轴", uicamera.getScaleX().ToString(), GStyleTool.helpBox);
                    Tool.AC.FieldText("空白 高度", uicamera.spaceHeight.ToString(), GStyleTool.helpBox);
                    Tool.AC.FieldText("空白 宽度", uicamera.spaceWidth.ToString(), GStyleTool.helpBox);
                }
            }, GStyleTool.UIContent);
            Undo.RecordObject(target, "Undo UICameraEditor");
        }

        protected override void change()
        {
            EditorUtility.SetDirty(target);
        }
    }
}