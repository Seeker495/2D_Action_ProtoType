using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : AttackBase
{
    [SerializeField]
    private GameObject m_grenadeObject;
    public override void Attack()
    {
        var grenade = Instantiate(m_grenadeObject, null);
        grenade.GetComponent<Grenade>().Attack();
    }

    public override void Execute()
    {
    }

    public override eAttackType GetAttackType()
    {
        return eAttackType.GRENADE;
    }

    public override Sprite GetSprite()
    {
        return m_grenadeObject.GetComponent<SpriteRenderer>().sprite;
    }

    public override void SetTarget(in GameObject target = null)
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
