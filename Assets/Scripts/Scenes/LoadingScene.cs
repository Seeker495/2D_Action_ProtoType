using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_loadingText;
    private const string LOADING_TEXT = "Now Loading";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {


    }


    private IEnumerator LoadScene()
    {

        var async = SceneManager.LoadSceneAsync(Parameter.NEXT_SCENE_NAME);
        while (!async.isDone)
        {
            m_loadingText.text = LOADING_TEXT;

            for (int i = 0; i < 3; ++i)
            {
                int randomTreasure = Random.Range(0, 4);
                m_loadingText.text += $"<sprite={randomTreasure}>";
                yield return new WaitForSeconds(0.02f);
            }
            m_loadingText.text = "";
            yield return null;
        }
    }
}
