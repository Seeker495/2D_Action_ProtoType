using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CsvToScriptableObjectWindow : EditorWindow
{
    [MenuItem("MyWindow/Csv-to-ScriptableObject")]
    static void Open()
    {
        GetWindow<CsvToScriptableObjectWindow>("CSV->ScriptableObject�ϊ��c�[��");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("�ϊ��c�[��");
    }
}
