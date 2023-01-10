using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class AddScore
{
    public static bool IS_LEVEL_UP = false;
    public static int LEVEL_COUNT = 0;
    public static IEnumerator Start(int score, string str, TextMeshProUGUI text, int rank = 1)
    {
        int currentScore = 0;
        while (currentScore <= score)
        {
            text.text = $"{currentScore.ToString()}{str}";
            yield return text;
            yield return null;
            currentScore += rank;
        }

        yield return "End";
    }

    public static IEnumerator Start(MonoBehaviour monobehavior, int currentLevel, float score, TextMeshProUGUI text, Slider slider, List<Level> levelList, float rank = 0.5f)
    {
        while (IS_LEVEL_UP)
        {
            yield return null;
            continue;
        }
        float currentScore = 0;
        slider.gameObject.SetActive(true);
        text.text = $"Lv {currentLevel}";
        slider.maxValue = levelList.Find(level => level.nowLevel == currentLevel).needExp;
        while (currentScore <= score)
        {
            slider.value = currentScore;
            yield return slider;
            yield return null;
            score -= rank;
            currentScore += rank;
            if(score <= 0) break;
            if (currentScore >= slider.maxValue)
            {
                currentLevel++;
                LEVEL_COUNT++;
                IS_LEVEL_UP = true;
                yield return Start(monobehavior, currentLevel, score, text, slider, levelList, rank);
                break;
            }
        }
        yield return "End";
    }

}
