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
        List<T> list = new List<T>();
        List<string> allLines = System.IO.File.ReadAllLines(fileName).ToList();


        foreach (var line in allLines)
        {
            string[] splits = line.Split(",");
            if(CreateInstance<T>() is EnemyParameter enemy)
            {
                enemy.ID = int.Parse(splits[0]);
                enemy.Name = splits[1];
                enemy.Exp = int.Parse(splits[2]);
                enemy.Money = int.Parse(splits[3]);
                enemy.HP = int.Parse(splits[4]);
                enemy.Attack = float.Parse(splits[5]);
                enemy.Defense = float.Parse(splits[6]);
                enemy.Speed = float.Parse(splits[7]);
                enemy.TouchPower = float.Parse(splits[8]);
                enemy.MovePattern = int.Parse(splits[9]);
                enemy.AttackPattern = int.Parse(splits[10]);
                T obj = CreateInstance<T>();
                obj = enemy as T;
                list.Add(obj);
            }
        }

        return list;
    }
}
