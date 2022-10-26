using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  主に経験値やお金を回収するクラス。
 *******************************************************************/

public class RecoveryObject : MonoBehaviour
{
    private Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exp"))
        {
            m_player.AddExp(collision.GetComponent<DropObjectBase>().Get());
            Destroy(collision.gameObject);

        }

        if (collision.CompareTag("Money"))
        {
            m_player.AddMoney(collision.GetComponent<DropObjectBase>().Get());
            Destroy(collision.gameObject);
        }
    }

}
