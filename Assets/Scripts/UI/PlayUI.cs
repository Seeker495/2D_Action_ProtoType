using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor.Build.Pipeline;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*******************************************************************
 *  <概要>
 *  プレイヤーの情報をUIで表示するためのクラス。
 *  <やれる事>
 *  プレイヤーのステータスを表示できる。
 *******************************************************************/
public class PlayUI : MonoBehaviour
{
    private GameObject m_hp;
    private GameObject m_attack;
    private GameObject m_defence;
    private GameObject m_water;
    private GameObject m_food;
    private Image m_hpColorSprite;
    [SerializeField]
    private ScoreUI m_scoreUI;
    [SerializeField]
    private GameObject m_gameOverText;
    [SerializeField]
    private GameObject m_hitUIObject;
    private HitUI m_hitUI;

    private float m_gameOverTime = 0.0f;
    private Player m_player;
    // Start is called before the first frame update
    void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.name = "PlayUI";
        m_hp = GameObject.Find("PlayUI/HP");
        m_attack = GameObject.Find("PlayUI/Attack");
        m_defence = GameObject.Find("PlayUI/Defence");
        m_water = GameObject.Find("PlayUI/Water");
        m_food = GameObject.Find("PlayUI/Food");
        m_hitUI = GameObject.FindWithTag("HitUI").GetComponent<HitUI>();
        m_hitUI.gameObject.SetActive(false);
        m_hpColorSprite = m_hp.GetComponentsInChildren<Image>()[3];
        m_gameOverText = Instantiate(m_gameOverText, new Vector3(960, 540, 0), Quaternion.identity, transform);
        m_gameOverText.SetActive(false);


    }

    private void Start()
    {
        m_hp.GetComponentInChildren<Slider>().maxValue = m_player.GetStatus().maxHP;
        m_water.GetComponentInChildren<Slider>().maxValue = 100;
        m_food.GetComponentInChildren<Slider>().maxValue = 100;

        m_hp.GetComponentInChildren<Slider>().value = m_player.GetStatus().actorStatus.hp;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.attack.ToString();
        m_defence.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.defence.ToString();
        m_water.GetComponentInChildren<Slider>().value = m_player.GetWaterGauge();
        m_food.GetComponentInChildren<Slider>().value = m_player.GetFoodGauge();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayStatus();
        ChangeStatusColor();
        if (!m_player.IsArrive())
        {
            m_gameOverText.SetActive(true);
            m_gameOverText.GetComponent<TextMeshProUGUI>().text = "GameOver";
            m_gameOverText.GetComponent<TextMeshProUGUI>().color = Color.red;
            m_gameOverText.GetComponent<TextMeshProUGUI>().fontSize = 100;

            m_gameOverText.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
            m_gameOverTime += Time.deltaTime;
            if (m_gameOverTime >= Parameter.GAME_OVER_TO_OTHER_SCENE)
            {
                Parameter.NEXT_SCENE_NAME = "EndResult";
                PlayerData.SetStatus(m_player.GetStatus());
                SceneManager.LoadSceneAsync("Loading");
            }
        }
        if (m_player.GetCombo() <= 0) return;
        m_hitUI.SetHitCombo(m_player.GetCombo());
    }

    private void FixedUpdate()
    {
        m_scoreUI.Execute();
    }

    private void ChangeStatusColor()
    {
        Color haveFoodColor = m_player.HaveFood() ? Color.white : Color.blue;
        Color haveWaterColor = m_player.HaveWater() ? Color.green : Color.magenta;

        m_hpColorSprite.color = haveWaterColor;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().color = haveFoodColor;
        m_defence.GetComponentInChildren<TextMeshProUGUI>().color = haveFoodColor;

        if (m_player.GetStatus().actorStatus.hp <= 0)
        {
            m_hp.GetComponentsInChildren<Image>()[1].color = Color.red;
        }
        else if (m_player.GetStatus().actorStatus.hp <= m_player.GetStatus().maxHP * Parameter.UI_DANGER_HP_RATIO)
        {
            m_hp.GetComponentsInChildren<Image>()[1].color =
                Vector4.MoveTowards(
                    new Color(1f, 0f, 0f, 1f),
                    new Color(1f, 0f, 0f, 0f),
                    math.fmod(Time.time, Parameter.UI_DANGER_HP_BLINK_INTERVAL));
        }


    }

    private void DisplayStatus()
    {
        m_hp.GetComponentInChildren<Slider>().value = m_player.GetStatus().actorStatus.hp;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.attack.ToString();
        m_defence.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.defence.ToString();
        m_water.GetComponentInChildren<Slider>().value = m_player.GetWaterGauge();
        m_food.GetComponentInChildren<Slider>().value = m_player.GetFoodGauge();
    }

    private void Damage()
    {
        m_hp.GetComponentsInChildren<Image>()[0].transform.DOShakePosition(1.0f, 10.0f);
    }

    public void SetScore()
    {
        m_scoreUI.SettingScore(m_player.GetAddScore());
    }
}
