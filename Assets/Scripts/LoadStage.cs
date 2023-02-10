using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/*******************************************************************
 *  <概要>
 *  ステージを読み込むクラス
 *  <仕組み>
 *  CSVから数字に応じたものを読み込んでステージを作成することが出来る。
 *******************************************************************/
public static class LoadStage
{
    public struct MapData
    {
        public List<string> stage;
        public int width;
        public int height;
    }



    public static MapData Load(string fileName)
    {
        MapData map = new MapData();
        string StagePath = "";
#if UNITY_EDITOR
        StagePath = $"{Application.streamingAssetsPath}/Stage/";
#elif UNITY_STANDALONE
        StagePath = $"{Application.streamingAssetsPath}/Stage/";
#endif

        if(!File.Exists($"{StagePath}{fileName}.csv"))
        {
            Parameter.LAST_ALIVE_DAY--;
            Parameter.NEXT_SCENE_NAME = "EndResult";
            SceneManager.LoadScene("Loading");
            return map;
        }
        using (StreamReader reader = new StreamReader($"{StagePath}{fileName}.csv", Encoding.UTF8))
        {
            string line = string.Empty;
            map.stage = new List<string>();
            string[] stageString;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                stageString = line.Split(',');
                if (map.width == 0)
                {
                    map.width = stageString.Length;
                }
                map.stage.AddRange(stageString);
            }
            map.height = map.stage.Count / map.width;

        }
        return map;
    }
}
