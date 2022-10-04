using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.GetWeaponSprite().Equals(null)) return;
        if(GetComponent<Image>().sprite != m_player.GetWeaponSprite())
            GetComponent<Image>().sprite = m_player.GetWeaponSprite();
    }
}
