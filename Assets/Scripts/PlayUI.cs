using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    private GameObject m_hp;
    private GameObject m_attack;
    private GameObject m_defense;
    private GameObject m_water;
    private GameObject m_food;
    private Image m_hpColorSprite;


    private GameObject m_player;
    // Start is called before the first frame update
    void Awake()
    {
        m_hp = GameObject.Find("PlayUI/HP");
        m_attack = GameObject.Find("PlayUI/Attack");
        m_defense = GameObject.Find("PlayUI/Defense");
        m_water = GameObject.Find("PlayUI/Water");
        m_food = GameObject.Find("PlayUI/Food");
        m_hpColorSprite = m_hp.GetComponentsInChildren<Image>()[2];

        m_player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        m_hp.GetComponentInChildren<Slider>().maxValue = m_player.GetComponent<Player>().GetStatus().maxHP;
        m_water.GetComponentInChildren<Slider>().maxValue = 100;
        m_food.GetComponentInChildren<Slider>().maxValue = 100;

        m_hp.GetComponentInChildren<Slider>().value = m_player.GetComponent<Player>().GetStatus().actorStatus.hp;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetComponent<Player>().GetStatus().actorStatus.attack.ToString();
        m_defense.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetComponent<Player>().GetStatus().actorStatus.defense.ToString();
        m_water.GetComponentInChildren<Slider>().value = m_player.GetComponent<Player>().GetWaterGauge();
        m_food.GetComponentInChildren<Slider>().value = m_player.GetComponent<Player>().GetFoodGauge();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayStatus();
        ChangeStatusColor();

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
        m_hp.GetComponentInChildren<Slider>().value = m_player.GetComponent<Player>().GetStatus().actorStatus.hp;
        m_attack.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetComponent<Player>().GetStatus().actorStatus.attack.ToString();
        m_defense.GetComponentInChildren<TextMeshProUGUI>().text = m_player.GetComponent<Player>().GetStatus().actorStatus.defense.ToString();
        m_water.GetComponentInChildren<Slider>().value = m_player.GetComponent<Player>().GetWaterGauge();
        m_food.GetComponentInChildren<Slider>().value = m_player.GetComponent<Player>().GetFoodGauge();

    }
}
