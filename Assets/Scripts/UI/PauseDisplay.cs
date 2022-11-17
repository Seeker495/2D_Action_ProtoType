using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseDisplay : MonoBehaviour
{
    public List<Button> m_buttons = new List<Button>();
    public int m_index = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        m_buttons = GetComponentsInChildren<Button>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectUp(InputAction.CallbackContext context)
    {
        m_index = System.Math.Abs(--m_index + m_buttons.Count) % m_buttons.Count;
        m_buttons[m_index].Select();
    }

    public void SelectDown(InputAction.CallbackContext context)
    {
        m_index = System.Math.Abs(++m_index) % m_buttons.Count;
        m_buttons[m_index].Select();
    }

    public void Enter(InputAction.CallbackContext context)
    {
        List<System.Action> actions = new List<System.Action>
        {
            Resume,RetryFromStart,BackToTitle
        };

        actions[m_index]();
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        GameObject.FindWithTag("GameController").GetComponent<PlayerController>().SetPause(false);
    }

    private void RetryFromStart()
    {
        Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");
    }

    private void BackToTitle()
    {
        Parameter.NEXT_SCENE_NAME = "Title";
        SceneManager.LoadSceneAsync("Loading");
    }

}
