using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

/*******************************************************************
 *  <概要>
 *  敵のドロップするオブジェクト(主に経験値やお金)を管理するクラス。
 *******************************************************************/
public class DropManager : MonoBehaviour
{
    private List<GameObject> m_exp = new List<GameObject>();
    //private List<GameObject> m_money = new List<GameObject>();
    private List<float> m_angle = new List<float>();

    [SerializeField]
    private GameObject m_expObject;
    //[SerializeField]
    //private GameObject m_moneyObject;

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    void Start()
    {

        Dictionary<eDropSize, int> expAmounts = new Dictionary<eDropSize, int>(3)
        {
            {eDropSize.SMALL,   transform.parent.GetComponent<EnemyBase>().GetExp() % 10 },
            {eDropSize.MEDIUM,  (transform.parent.GetComponent<EnemyBase>().GetExp() / 10) % 10 },
            {eDropSize.LARGE,   transform.parent.GetComponent<EnemyBase>().GetExp() / 100 },
        };

        Dictionary<eDropSize, int> moneyAmounts = new Dictionary<eDropSize, int>(3)
        {
            {eDropSize.SMALL,   transform.parent.GetComponent<EnemyBase>().GetMoney() % 10 },
            {eDropSize.MEDIUM,  (transform.parent.GetComponent<EnemyBase>().GetMoney() / 10) % 10 },
            {eDropSize.LARGE,   transform.parent.GetComponent<EnemyBase>().GetMoney() / 100 },
        };

        Dictionary<eDropSize, float> objectScale = new Dictionary<eDropSize, float>(3)
        {
            {eDropSize.SMALL,   Parameter.DROP_EFFECT_SIZE_MULTIPLY * 1.0f},
            {eDropSize.MEDIUM,  Parameter.DROP_EFFECT_SIZE_MULTIPLY * 2.0f},
            {eDropSize.LARGE,   Parameter.DROP_EFFECT_SIZE_MULTIPLY * 3.0f},
        };

        m_expObject.SetActive(false);
        //m_moneyObject.SetActive(false);

        foreach (eDropSize size in Enum.GetValues(typeof(eDropSize)))
        {
            for (int i = 0; i < expAmounts[size]; ++i)
            {
                var exp = Instantiate(m_expObject, transform);
                exp.transform.localScale = new Vector3(objectScale[size], objectScale[size], 0.0f);
                exp.GetComponent<DropObjectBase>().SetSize(size);
                m_exp.Add(exp);
            }
        }

        //foreach (eDropSize size in Enum.GetValues(typeof(eDropSize)))
        //{
        //    for (int i = 0; i < moneyAmounts[size]; ++i)
        //    {
        //        var money = Instantiate(m_moneyObject, transform);
        //        money.transform.localScale = new Vector3(objectScale[size], objectScale[size], 0.0f);
        //        money.GetComponent<DropObjectBase>().SetSize(size);
        //        m_money.Add(money);
        //    }
        //}
    }

    void Update()
    {
        if (transform.parent != null)
        {
            transform.position = transform.parent.position;
            //m_money.ForEach(money => money.transform.position = transform.position);
            m_exp.ForEach(exp => exp.transform.position = transform.position);
        }
        if(transform.childCount == 0 && transform.parent == null)
            Destroy(gameObject);
    }

    public IEnumerator Diffusion()
    {
        List<GameObject> dropObjects = new List<GameObject>();
        dropObjects.AddRange(m_exp);
        //dropObjects.AddRange(m_money);
        dropObjects.ForEach(dropObject => dropObject.SetActive(true));

        Vector2 randomDirection;
        foreach (var dropObject in dropObjects)
        {
            randomDirection = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            float toAngle = Mathf.Atan2(randomDirection.y, randomDirection.x);
            m_angle.Add(toAngle);
            if (m_angle.Count(angle => Mathf.Abs(toAngle - angle) < 2.0f) != 0)
                randomDirection = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));

            dropObject.GetComponent<Rigidbody2D>().velocity = randomDirection * UnityEngine.Random.Range(3.0f, 4.0f);
            yield return null;
        }

    }
}
