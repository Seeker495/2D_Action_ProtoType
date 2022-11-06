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
    // Start is called before the first frame update

    private static Vector2 m_direction;
    void Start()
    {

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

        var startPosition = transform.parent.GetComponent<Rigidbody2D>().position;
        m_direction = transform.parent.GetComponent<IActor>().GetDirection();

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

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }
}
