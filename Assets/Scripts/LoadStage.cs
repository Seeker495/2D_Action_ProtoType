using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] stageString = line.Split(',');
                if (map.width == 0)
                {
                    map.width = stageString.Length;
                }

                foreach (var str in stageString)
                {
                    map.stage.Add(int.Parse(str));
                }
            }
            map.height = map.stage.Count / map.width;

        }
        Debug.Log(map.width);
        Debug.Log(map.height);
        return map;
    }
}
