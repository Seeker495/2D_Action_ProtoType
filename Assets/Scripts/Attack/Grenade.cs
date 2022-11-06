using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <äTóv>
 *  éËû÷íeÉNÉâÉXÅB
 *******************************************************************/
public class Grenade : AttackBase
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
        return eAttackType.GRENADE;
    }

    public override void SetTarget(in GameObject target)
    {
    }

    public async void Shoot(float startDegree, float angleInterval, int arrowNum)
    {
        var startPosition = transform.parent.GetComponent<Rigidbody2D>().position;
        m_direction = transform.parent.GetComponent<IActor>().GetDirection();

        GameObject grenade = await Addressables.LoadAssetAsync<GameObject>("Grenade").Task; ;

        //grenade.GetComponent<Grenade>().Shoot(startPosition, startDegree, m_direction);
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
