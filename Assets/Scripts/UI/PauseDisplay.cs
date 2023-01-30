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
    public int m_index;
    public GameObject m_option;

    private void OnEnable()
    {
        PlayerController.Controller.Play.Disable();
        PlayerController.Controller.Pause.SelectLeft.started += SelectLeft;
        PlayerController.Controller.Pause.SelectRight.started += SelectRight;
        PlayerController.Controller.Pause.Enter.started += Enter;
        PlayerController.Controller.Pause.Enable();
    }

    private void OnDisable()
    {
        PlayerController.Controller.Pause.SelectLeft.started -= SelectLeft;
        PlayerController.Controller.Pause.SelectRight.started -= SelectRight;
        PlayerController.Controller.Pause.Enter.started -= Enter;
        PlayerController.Controller.Pause.Disable();
        PlayerController.Controller.Play.Enable();
    }



    // Start is called before the first frame update
    void Awake()
    {
        m_buttons = GetComponentsInChildren<Button>().ToList();
        Select();
        m_option = Instantiate(m_option, transform);
        m_option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectLeft(InputAction.CallbackContext context)
    {
        Deselect();
        m_index = System.Math.Abs(--m_index + m_buttons.Count) % m_buttons.Count;
        Select();
    }

    public void SelectRight(InputAction.CallbackContext context)
    {
        Deselect();
        m_index = System.Math.Abs(++m_index) % m_buttons.Count;
        Select();
    }

    public void Enter(InputAction.CallbackContext context)
    {
        List<System.Action> actions = new List<System.Action>
        {
            RetryFromStart,Resume,BackToTitle
        };

        actions[m_index]();
    }

    private void PopUpAnimation()
    {
        Animator animator = m_buttons[m_index].GetComponent<Animator>();
        int popup = Animator.StringToHash("IsPopUp");
        int back = Animator.StringToHash("IsBack");

        if (!animator.GetBool(popup) || !animator.GetBool(back)) return;

        StartCoroutine(PlayAnimation(animator, "IsPopUp"));
    }


    private void BackAnimation()
    {
        Animator animator = m_buttons[m_index].GetComponent<Animator>();
        int popup = Animator.StringToHash("IsPopUp");
        int back = Animator.StringToHash("IsBack");

        if (!animator.GetBool(popup) || !animator.GetBool(back)) return;

        StartCoroutine(PlayAnimation(animator, "IsBack"));
    }

    private void Deselect()
    {
        BackAnimation();
        ColorBlock colorBlock = m_buttons[m_index].colors;
        colorBlock.normalColor = Color.white;
        m_buttons[m_index].colors = colorBlock;
    }

    private IEnumerator PlayAnimation(Animator animator, string hash)
    {
        animator.SetBool(hash, false);

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(hash))
        {
            yield return null;
        }
        animator.SetBool(hash, true);
    }

    private void Select()
    {
        PopUpAnimation();
        ColorBlock colorBlock = m_buttons[m_index].colors;
        colorBlock.normalColor = Color.yellow;
        m_buttons[m_index].colors = colorBlock;
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    private void RetryFromStart()
    {
        m_option.SetActive(true);
    }

    private void BackToTitle()
    {
        Parameter.NEXT_SCENE_NAME = "Title";
        SceneManager.LoadSceneAsync("Loading");
    }

}
