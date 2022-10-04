using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class LoadStage
{
    public struct MapData
    {
        public List<int> stage;
        public int width;
        public int height;
    }



    public static MapData Load(string fileName)
    {
        MapData map = new MapData();
        string StagePath = "";
#if UNITY_EDITOR
        StagePath = $"Assets/Stage/";
#endif
        if (Application.platform == RuntimePlatform.WindowsPlayer)
            StagePath = $"Stage/";
        using (StreamReader reader = new StreamReader($"{StagePath}{fileName}.csv", Encoding.UTF8))
        {
            string line = string.Empty;
            map.stage = new List<int>();
            string[] stageString;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                stageString = line.Split(',');
                if (map.width == 0)
                {
                    map.width = stageString.Length;
                }
                map.stage.AddRange(Array.ConvertAll(stageString, int.Parse));
            }
            map.height = map.stage.Count / map.width;

        }
        return map;
    }
}
