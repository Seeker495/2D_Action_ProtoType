using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ScoreFile
{
    private static long HIGH_SCORE;
    public static void Save()
    {
        HIGH_SCORE = Parameter.TOTAL_SCORE;
        StreamWriter writer = new StreamWriter($"{Application.streamingAssetsPath}/Score.dat", false, System.Text.Encoding.UTF8);
        writer.WriteLine($"HighScore:{HIGH_SCORE}");
        writer.Close();
    }

    public static void Load()
    {
        StreamReader reader = new StreamReader($"{Application.streamingAssetsPath}/Score.dat");
        var line = reader.ReadLine();
        HIGH_SCORE = long.Parse(line.Split(":").Last());
        Debug.Log(line.Split(":").Last());
        reader.Close();
    }

    public static long GetHighScore()
    {
        return HIGH_SCORE;
    }

}
