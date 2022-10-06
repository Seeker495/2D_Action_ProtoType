using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugString : MonoBehaviour
{
    Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̃Q�[���I�u�W�F�N�g����������Ńv���C���[�̃R���|�[�l���g����肷��
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // �e�L�X�g��ݒ肷��
        GetComponentInChildren<TextMeshProUGUI>().text = $"HP: {m_player.GetComponent<IActor>().GetBaseStatus().hp}";
    }
}
