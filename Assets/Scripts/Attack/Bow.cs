using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Bow : MonoBehaviour,IAttack
{
    // Start is called before the first frame update

    private static Vector2 m_direction;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void IAttack.Attack()
    {
        Shoot(-30.0f, 30.0f, 3);
    }

    Sprite IAttack.GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    eAttackType IAttack.GetAttackType()
    {
        return eAttackType.BOW;
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

}
