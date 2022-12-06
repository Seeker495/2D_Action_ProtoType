using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseDisplay : MonoBehaviour
{
    public List<Button> m_buttons;
    public int m_index = 0;
    // Start is called before the first frame update
    void Awake()
    {
        m_buttons = GetComponentsInChildren<Button>().ToList();
        Select();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectUp(InputAction.CallbackContext context)
    {
        Deselect();
        m_index = System.Math.Abs(--m_index + m_buttons.Count) % m_buttons.Count;
        Select();
    }

    public void SelectDown(InputAction.CallbackContext context)
    {
        Deselect();
        m_index = System.Math.Abs(++m_index) % m_buttons.Count;
        Select();
    }

    public void Enter(InputAction.CallbackContext context)
    {
        List<System.Action> actions = new List<System.Action>
        {
            Resume,RetryFromStart,BackToTitle
        };

        actions[m_index]();
    }

    private void Deselect()
    {
        ColorBlock colorBlock = m_buttons[m_index].colors;
        colorBlock.normalColor = Color.white;
        m_buttons[m_index].colors = colorBlock;
    }

    private void Select()
    {
        ColorBlock colorBlock = m_buttons[m_index].colors;
        colorBlock.normalColor = Color.yellow;
        m_buttons[m_index].colors = colorBlock;
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
        PlayerController playerController = GameObject.FindWithTag("GameController").GetComponent<PlayerController>();
        playerController.Disable();
        SceneManager.LoadSceneAsync("Loading");
    }

    private void BackToTitle()
    {
        Parameter.NEXT_SCENE_NAME = "Title";
        PlayerController playerController = GameObject.FindWithTag("GameController").GetComponent<PlayerController>();
        playerController.Disable();
        SceneManager.LoadSceneAsync("Loading");
    }

}
