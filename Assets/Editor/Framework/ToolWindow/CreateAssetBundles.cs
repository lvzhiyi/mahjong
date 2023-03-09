using cambrian.common;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public enum ABType
{
    Null = -1,
    cfg = 0,
    res = 1,
    gameui = 2,
    updata = 3,
    qycfg = 4,
    hncfg = 5,
    aycfg = 6,
}

public enum BuildABTarget
{
    Android,
    iOS,
}

public class CreateAssetBundles : EditorWindow
{
    public CreateAssetBundles()
    {
        titleContent = new GUIContent("Package Manager");
        minSize = new Vector2(300, 300);

        packageList = new ArrayList<ABType>();
    }

    private static EditorWindow window;



    //[MenuItem("Tools/Package Manager Window %#f")]
    public static void Open()
    {
        window = window ?? GetWindow<CreateAssetBundles>(false, "Package Manager", true);//创建窗口
        window.wantsMouseMove = true;
        window.Show(true);//展示         
    }

    private ArrayList<ABType> packageList;
    private BuildABTarget buildTarget;
    private Vector2 vector;

    private void OnGUI()
    {
        vector = EditorGUILayout.BeginScrollView(vector);

        BuildTarget();//平台

        PackageList();//打包类型

        ButtonProcess();
        EditorGUILayout.EndScrollView();
    }

    private void BuildTarget()
    {
        EditorGUILayout.BeginHorizontal();
        buildTarget = (BuildABTarget)EditorGUILayout.EnumPopup("Build Target", buildTarget);
        EditorGUILayout.EndHorizontal();
    }

    private void PackageList()
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical();
        GUILayout.Label("AB TYPE：");
        for (int i = 0; i < packageList.count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            packageList.set((ABType)EditorGUILayout.EnumPopup(string.Concat("NO.", i), packageList.get(i)), i);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private void ButtonProcess()
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add ABType"))
        {
            packageList.add(ABType.Null);
        }
        if (GUILayout.Button("Sub ABType"))
        {
            if (packageList.count > 0) packageList.removeLast();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Package"))
        {
            var @srcpath = "";
            var @out = "";
            switch (buildTarget)
            {
                case BuildABTarget.iOS:
                    @srcpath = string.Concat(Application.persistentDataPath, "/ab/Package Android/");
                    @out = string.Concat(Application.persistentDataPath, "/ab/Package Out/Android/");
                    break;
                case BuildABTarget.Android:
                    @srcpath = string.Concat(Application.persistentDataPath, "/ab/Package IOS/");
                    @out = string.Concat(Application.persistentDataPath, "/ab/Package Out/IOS/");
                    break;
            }
            PackageFunc(@srcpath, @out);
        }
        EditorGUILayout.EndHorizontal();
    }

    #region Func

    /// <summary> 打包方法 </summary>
    /// <param name="srcpath">打包路径</param>
    /// <param name="out">输出路径</param>
    private static void PackageFunc(string @srcpath, string @out)
    {
        if (!Directory.Exists(@srcpath)) { Directory.CreateDirectory(@srcpath); }
        if (!Directory.Exists(@out)) { Directory.CreateDirectory(@out); }

        var root = new DirectoryInfo(@srcpath);

        if (root.GetDirectories().Length <= 0) { Debug.Log("Package Folder Count == 0 | Hint:" + @srcpath); return; }

        string fullname = "", outPutPath = ""; CompressZip compressZip; DirectoryInfo temp;
        foreach (var folder in root.GetDirectories())
        {
            temp = root.CreateSubdirectory("temp");
            Directory.Move(folder.FullName, string.Format(@"{0}\{1}", temp.FullName, folder.Name));
            Directory.Move(temp.FullName, folder.FullName);

            fullname = string.Concat(@srcpath, folder.Name, '/');
            outPutPath = string.Concat(@out, folder.Name, ".upk");
            compressZip = new CompressZip(fullname, outPutPath, folder.Name);
            compressZip.compressThread.Start();

            while (compressZip.compressThread.ThreadState == ThreadState.Running)
            {
                EditorUtility.DisplayProgressBar(string.Concat("Package ", PlayerSettings.applicationIdentifier, ' ', folder.Name), string.Concat(Mathf.CeilToInt(compressZip.getPercent() * 100), '%'), compressZip.getPercent());
            }

            Directory.Move(string.Format(@"{0}\{1}", folder.FullName, folder.Name), temp.FullName);
            Directory.Delete(folder.FullName);
            Directory.Move(temp.FullName, folder.FullName);
            folder.Refresh();

            EditorUtility.ClearProgressBar();
        }
        root.Refresh();
    }

    #endregion

    private void OnDestroy()
    {
        window = null;
    }
}