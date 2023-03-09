namespace Framework.UI
{
    using scene.loading;
    using UnityEditor;
    using Tool = EditorTool;

    [CustomEditor(typeof(LoadingRoot), false)]
    /// <summary> UI LoadingRoot Editor </summary>
    public class UILoadingRootEditor : Base2DEditor
    {
        private float addScaleX;                   //缩放
        private SerializedProperty versionstype;   //开发版本  
        private SerializedProperty url_addr;       //是否需要显示，不受关闭其他面板的影响
        private SerializedProperty isLogCollect;   //游戏生成日志
        private LoadingRoot root;

        protected override void init()
        {
            root = (LoadingRoot)target;
            serObj = new SerializedObject(target);
            versionstype = serObj.FindProperty("versionstype");
            isLogCollect = serObj.FindProperty("isLogCollect");
            var one = root.versions.IndexOf('.');
            var two = root.versions.LastIndexOf('.');
            prefix = root.versions.Substring(0, one);
            last = root.versions.Substring(two + 1, root.versions.Length - two - 1);
            addScaleX = root.addScaleX;
            url_addr = serObj.FindProperty("url_addr");
        }

        private string prefix;
        private string last;

        protected override void inspector()
        {
            Tool.BE.Vertical(() =>
            {
                root.versions = Tool.AC.FieldText("版本号", root.versions);
              //  Tool.AC.FieldProperty("开发版本", versionstype);
                Tool.AC.FieldProperty("服务器地址", url_addr);
                Tool.AC.FieldProperty("生成日志", isLogCollect);
                addScaleX = Tool.AC.FieldFloat("缩放", addScaleX);

            }, GStyleTool.UIContent);
            Undo.RecordObject(target, "Undo UILoadingRootEditor");
        }

        protected override void change()
        {
            EditorUtility.SetDirty(target);
        }
    }
}
