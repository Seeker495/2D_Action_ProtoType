using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    GameObject m_titleText;
    // Start is called before the first frame update
    void Start()
    {
        m_titleText.transform.DORotate(new Vector3(0, 0, 180f), 1f, RotateMode.FastBeyond360)
         .SetDelay(1f)
         .SetRelative()
         .SetEase(Ease.InOutQuad)
         .SetLoops(-1, LoopType.Yoyo);

        m_titleText.transform.parent.GetComponent<CanvasGroup>().DOFade(0.0f, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void Press_Start()
    {
        Debug.Log("Press_Start");
        SceneManager.LoadSceneAsync("Play");
    }

    public void Press_Option()
    {
        Debug.Log("Press_Option");
    }

    public void Press_Quit()
    {
        Debug.Log("Press_Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
