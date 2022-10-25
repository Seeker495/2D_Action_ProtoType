using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private Player m_player;
    // Start is called before the first frame update
    void Awake()
    {
        m_player = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log(m_player.GetWeaponSprite());
        GetComponent<Image>().sprite = m_player?.GetWeaponSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Image>().sprite != m_player?.GetWeaponSprite() && m_player?.GetWeaponSprite() != null)
            GetComponent<Image>().sprite = m_player?.GetWeaponSprite();
    }
}
