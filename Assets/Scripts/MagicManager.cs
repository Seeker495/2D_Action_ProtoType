using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <概要>
 *  魔法の管理クラス。(現在,敵の子に持たせている)
 *******************************************************************/
public class MagicManager : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Shoot();
    }

    public async void Shoot()
    {
        var magic = await Addressables.LoadAssetAsync<GameObject>("Fire").Task;
        Debug.Log(magic);
        GameObject magicObject = Instantiate(magic, transform);
        AttackBase homing = magicObject.GetComponent<Homing>();
        homing.SetTarget(GameObject.FindWithTag("Player"));
        homing.Attack();
    }

}
