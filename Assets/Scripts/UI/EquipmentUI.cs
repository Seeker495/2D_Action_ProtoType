using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************
 *  <�T�v>
 *  �v���C���[�̑�����UI�ŕ\������N���X�B
 *  (��XPlayUI�N���X�ɓ����������ƍl���Ă���)
 *  <�o���鎖>
 *  ������\���ł���B
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
