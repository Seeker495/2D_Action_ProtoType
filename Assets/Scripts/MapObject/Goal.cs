using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData.SetStatus(collision.GetComponent<Player>().GetStatus());
            ToNextDay();
        }
    }

    private void ToNextDay()
    {
        Parameter.CURRENT_ALIVE_DAY++;
        if (Parameter.CURRENT_ALIVE_DAY == Parameter.LAST_ALIVE_DAY)
            Parameter.NEXT_SCENE_NAME = "Result";
        else
            Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");

    }

}
