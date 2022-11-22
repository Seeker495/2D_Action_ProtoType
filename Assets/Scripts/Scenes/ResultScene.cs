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
    [SerializeField]
    private GameObject m_playerController;


    // Start is called before the first frame update
    void Awake()
    {
        m_resultUI = Instantiate(m_resultUIObject, GameObject.FindWithTag("Canvas").transform).GetComponent<ResultUI>();
        m_playerController = Instantiate(m_playerController, null);
    }

    public void BackToTitle(InputAction.CallbackContext context)
    {
        Parameter.NEXT_SCENE_NAME = "Title";
        PlayerController playerController = GameObject.FindWithTag("GameController").GetComponent<PlayerController>();
        playerController.Disable();
        SceneManager.LoadSceneAsync("Loading");
    }
}
