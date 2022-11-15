using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

/*******************************************************************
 *  <概要>
 *  タイトルシーンのオブジェクトを管理するクラス。
 *******************************************************************/
public class TitleScene : MonoBehaviour
{
    [SerializeField]
    GameObject m_titleText;

    // Start is called before the first frame update
    // 初期化
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
    // 更新
    void Update()
    {
    }

    // ゲームスタートのボタン
    public void Press_Start()
    {

        Debug.Log("Press_Start");
        Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");
    }
    // コンティニューボタン
    public void Press_Continue()
    {
        Debug.Log("Press_Continue");
    }
    // エンドレスボタン
     public void Press_EndLess()
    {
        Debug.Log("Press_EndLess");
    }
    // コレクションボタン
     public void Press_Corection()
    {

        Debug.Log("Press_Corection");
    }

    // オプションのボタン
     public void Press_Option()
    {
        Debug.Log("Press_Option");
    }

    // ゲームを終わる
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
