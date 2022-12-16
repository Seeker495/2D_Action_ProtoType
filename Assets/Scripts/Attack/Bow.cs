using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <äTóv>
 *  ã|ÉNÉâÉXÅB
 *******************************************************************/
public class Bow : AttackBase
{
    [SerializeField]
    Rigidbody2D m_rigidBody2D;
    private int m_time = 0;
    private bool m_canShoot = false;
    [SerializeField]
    GameObject m_arrowObject;
    // Start is called before the first frame update

    private static Vector2 m_direction;
    void Start()
    {
        m_rigidBody2D = GetComponentInParent<Rigidbody2D>();
    }


    public override void Attack()
    {
        if(m_canShoot)
            Shoot(-30.0f, 30.0f, 3);
        m_canShoot = false;
    }

    public override eAttackType GetAttackType()
    {
        return eAttackType.BOW;
    }

    public override void SetTarget(in GameObject target)
    {
    }
    public async void Shoot(float startDegree, float angleInterval, int arrowNum)
    {

        var startPosition = GetComponentInParent<Rigidbody2D>().position;
        m_direction = GetComponentInParent<IActor>().GetDirection();

        GameObject[] arrowObjects = new GameObject[arrowNum];
        for (int i = 0; i < arrowObjects.Length; ++i)
        {
            arrowObjects[i] = Instantiate(m_arrowObject, Vector3.zero, Quaternion.identity, transform);
        }


        for (int i = 0; i < arrowObjects.Length; ++i)
        {
            arrowObjects[i].GetComponent<Arrow>().Shoot(startPosition, startDegree + i * angleInterval, m_direction);
        }
    }

    void FixedUpdate()
    {
        if(!m_canShoot)
            m_time = ++m_time % Parameter.ATTACK_BOW_INTERVAL;
        if (m_time == 0)
            m_canShoot = true;
        Debug.Log(m_time);
        m_rigidBody2D.position = GetComponentInParent<Rigidbody2D>().position;
    }

    public override Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}
