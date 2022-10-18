using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EffekseerTool.Data;
using UnityEngine.WSA;
using Unity.VisualScripting;
using System.Linq;

public class CsvToScriptableObjectWindow : EditorWindow
{
    private Object m_csvField = null;
    private int m_selectFolderIndex = 0;
    private Vector2 m_buttonSize = new Vector2(100, 30);
    private List<string> m_subFolders = new List<string>();

    [MenuItem("MyWindow/Csv-to-ScriptableObject")]
    static void Open()
    {
        GetWindowWithRect<CsvToScriptableObjectWindow>(new Rect(new Vector2(640, 480), new Vector2(720, 540)), false, "変換ツール");
    }

    void OnGUI()
    {
        m_subFolders = AssetDatabase.GetSubFolders("Assets/ScriptableObjects").ToList();
        m_csvField = EditorGUILayout.ObjectField("変換に使用するファイル", m_csvField, typeof(TextAsset), false);

        m_selectFolderIndex = EditorGUILayout.Popup("保存先: ", m_selectFolderIndex, m_subFolders.ToArray());


        if (System.IO.Path.GetExtension(AssetDatabase.GetAssetPath(m_csvField)).Equals(".csv") && GUILayout.Button("変換"))
        {
            if (m_subFolders[m_selectFolderIndex].Contains("Enemy"))
            {
                ScriptableObjectFounder<EnemyParameter> enemy = new ScriptableObjectFounder<EnemyParameter>();
                var list = enemy.Create(AssetDatabase.GetAssetPath(m_csvField));
                foreach (var parameter in list)
                {
                    AssetDatabase.CreateAsset(parameter, $"{m_subFolders[m_selectFolderIndex]}/{parameter.Name}.asset");
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }
    }
}