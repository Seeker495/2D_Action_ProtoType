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
    [SerializeField]
    private Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(m_player);
        //m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (!m_player.GetWeaponSprite()) return;
        GetComponent<Image>().sprite = m_player.GetWeaponSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_player.GetWeaponSprite() != null)
            GetComponent<Image>().sprite = m_player.GetWeaponSprite();
    }
}
