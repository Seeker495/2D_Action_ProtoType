using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
    private GameObject m_defense;
    private GameObject m_water;
    private GameObject m_food;
    private Image m_hpColorSprite;
    [SerializeField]
    private GameObject m_gameOverText;


    private Player m_player;
    // Start is called before the first frame update
    async void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        m_hp = GameObject.Find("PlayUI/HP");
        m_attack = GameObject.Find("PlayUI/Attack");
        m_defense = GameObject.Find("PlayUI/Defense");
        m_water = GameObject.Find("PlayUI/Water");
        m_food = GameObject.Find("PlayUI/Food");
        m_hpColorSprite = m_hp.GetComponentsInChildren<Image>()[2];
        m_gameOverText = Instantiate(await Addressables.LoadAssetAsync<GameObject>("Text").Task, new Vector3(960, 540, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        m_gameOverText.SetActive(false);

    }

    private void Start()
    {
        m_hp.GetComponentInChildren<Slider>().maxValue = m_player.GetStatus().maxHP;
        m_water.GetComponentInChildren<Slider>().maxValue = 100;
        m_food.GetComponentInChildren<Slider>().maxValue = 100;

        m_hp.GetComponentInChildren<Slider>().value = m_player.GetStatus().actorStatus.hp;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.attack.ToString();
        m_defense.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.defense.ToString();
        m_water.GetComponentInChildren<Slider>().value = m_player.GetWaterGauge();
        m_food.GetComponentInChildren<Slider>().value = m_player.GetFoodGauge();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayStatus();
        ChangeStatusColor();

        if(!m_player.GetComponent<Player>().IsArrive())
        {
            m_gameOverText.SetActive(true);
            m_gameOverText.GetComponent<TextMeshProUGUI>().text = "GameOver";
            m_gameOverText.GetComponent<TextMeshProUGUI>().fontSize = 100.0f;

        }



    }

    private void ChangeStatusColor()
    {
        Color haveFoodColor = m_player.GetComponent<Player>().HaveFood() ? Color.white : Color.blue;
        Color haveWaterColor = m_player.GetComponent<Player>().HaveWater() ? Color.green : Color.magenta;

        m_hpColorSprite.color = haveWaterColor;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().color = haveFoodColor;
        m_defense.GetComponentInChildren<TextMeshProUGUI>().color = haveFoodColor;
    }

    private void DisplayStatus()
    {
        m_hp.GetComponentInChildren<Slider>().value = m_player.GetStatus().actorStatus.hp;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.attack.ToString();
        if (m_player.GetStatus().actorStatus.attack < 1f)
            Debug.Log("Time"+Time.time);
        m_defense.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetStatus().actorStatus.defense.ToString();
        m_water.GetComponentInChildren<Slider>().value = m_player.GetWaterGauge();
        m_food.GetComponentInChildren<Slider>().value = m_player.GetFoodGauge();
    }
}
