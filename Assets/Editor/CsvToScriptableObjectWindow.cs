using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CsvToScriptableObjectWindow : EditorWindow
{
    [MenuItem("MyWindow/Csv-to-ScriptableObject")]
    static void Open()
    {
        GetWindow<CsvToScriptableObjectWindow>("CSV->ScriptableObject変換ツール");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("変換ツール");
    }
}
