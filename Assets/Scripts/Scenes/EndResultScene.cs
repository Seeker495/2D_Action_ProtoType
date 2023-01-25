using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndResultScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Parameter.TOTAL_SCORE += Parameter.CURRENT_SCORE;
        if (Parameter.TOTAL_SCORE > ScoreFile.GetHighScore())
            ScoreFile.Save();

        Parameter.NEXT_SCENE_NAME = "Title";
        SceneManager.LoadSceneAsync("Loading");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
