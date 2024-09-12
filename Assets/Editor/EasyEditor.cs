using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EasyEditor : Editor
{
    [MenuItem("Custom/ConfigToResources")]
    public static void ConfigToResources()
    {
        var srcPath = Application.dataPath + "/../Config/";
        var dstPath = Application.dataPath + "/Resources/Config/";
        Directory.Delete(dstPath,true);
        Directory.CreateDirectory(dstPath);
        foreach (var filePath in Directory.GetFiles(srcPath))
        {
            var fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
            File.Copy(filePath, dstPath + fileName + ".bytes",true);
        }
        AssetDatabase.Refresh();
        //Debug.Log("配置文件赋值完毕");
    }
}