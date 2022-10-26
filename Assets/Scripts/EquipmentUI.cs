using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************
 *  <概要>
 *  プレイヤーの装備をUIで表示するクラス。
 *  (後々PlayUIクラスに統合したいと考えている)
 *  <出来る事>
 *  装備を表示できる。
 *******************************************************************/
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
