using Codice.CM.SEIDInfo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectFounder<T> : EditorWindow where T : ScriptableObject
{
    public List<T> Create(in string fileName)
    {
        T obj = CreateInstance<T>();
        List<T> list = new List<T>();
        List<string> allLines = System.IO.File.ReadAllLines(fileName).ToList();


        foreach (var line in allLines)
        {
            string[] splits = line.Split(",");
            if(typeof(T) == typeof(EnemyParameter))
            {
                (obj as EnemyParameter).ID = int.Parse(splits[0]);
                (obj as EnemyParameter).Name = splits[1];
                (obj as EnemyParameter).Exp = int.Parse(splits[2]);
                (obj as EnemyParameter).Money = int.Parse(splits[3]);
                (obj as EnemyParameter).HP = int.Parse(splits[4]);
                (obj as EnemyParameter).Attack = float.Parse(splits[5]);
                (obj as EnemyParameter).Defence = float.Parse(splits[6]);
                (obj as EnemyParameter).Speed = float.Parse(splits[7]);
                (obj as EnemyParameter).TouchPower = float.Parse(splits[8]);
                (obj as EnemyParameter).MovePattern = int.Parse(splits[9]);
            }
            list.Add(obj);
        }

        return list;
    }
}
