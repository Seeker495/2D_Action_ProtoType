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
        // プレイヤーのゲームオブジェクトを見つけた上でプレイヤーのコンポーネントを入手する
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // テキストを設定する
        GetComponentInChildren<TextMeshProUGUI>().text = $"HP: {m_player.GetComponent<IActor>().GetBaseStatus().hp}";
    }
}
