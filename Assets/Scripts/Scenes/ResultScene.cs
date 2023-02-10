using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    private GameObject m_resultUIObject;
    private ResultUI m_resultUI;

    /* ユーザーのコントローラー関連 */
    private void OnEnable()
    {
        PlayerController.Controller.Result.Enable();
        PlayerController.Controller.Result.GoToPlay.started += BackToTitle;
    }

    private void OnDisable()
    {
        PlayerController.Controller.Result.GoToPlay.started -= BackToTitle;
        PlayerController.Controller.Result.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_resultUI = Instantiate(m_resultUIObject, GameObject.FindWithTag("Canvas").transform).GetComponent<ResultUI>();
        Parameter.TOTAL_SCORE += Parameter.CURRENT_SCORE;
    }

    public void BackToTitle(InputAction.CallbackContext context)
    {
        Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");
    }
}
