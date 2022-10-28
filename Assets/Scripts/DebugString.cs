using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  一時的にデバッグで情報を表示するためのクラス。
 *  <やれる事>
 *  TextMeshProUGUIを使ってプレイヤーの情報を表示できる。
 *  (オブジェクトを追加する事で増やせるが後々削除する予定のため,使用は非推奨)
 *******************************************************************/
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
        GetComponentInChildren<TextMeshProUGUI>().text =
            $"HP: {m_player.GetComponent<IActor>().GetBaseStatus().hp}\n" +
            $"Exp: {m_player.GetExp()}\n" +
            $"Money: {m_player.GetMoney()}\n" +
            $"Attack: {m_player.GetComponent<IActor>().GetBaseStatus().attack}" +
            $"Defence: {m_player.GetComponent<IActor>().GetBaseStatus().defence}";
    }
}
