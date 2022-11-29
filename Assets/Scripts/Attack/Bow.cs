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

    // Start is called before the first frame update

    private static Vector2 m_direction;
    void Start()
    {
        m_rigidBody2D = GetComponentInParent<Rigidbody2D>();
    }


    public override void Attack()
    {
        Shoot(-30.0f, 30.0f, 3);
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
        var arrow = await Addressables.LoadAssetAsync<GameObject>("Arrow").Task;

        var startPosition = GetComponentInParent<Rigidbody2D>().position;
        m_direction = GetComponentInParent<IActor>().GetDirection();

        GameObject[] arrowObjects = new GameObject[arrowNum];
        for (int i = 0; i < arrowObjects.Length; ++i)
        {
            arrowObjects[i] = Instantiate(arrow, transform);
        }


        for (int i = 0; i < arrowObjects.Length; ++i)
        {
            arrowObjects[i].GetComponent<Arrow>().Shoot(startPosition, startDegree + i * angleInterval, m_direction);
        }
    }

    void FixedUpdate()
    {
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
