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
    private List<GameObject> m_money = new List<GameObject>();
    private List<float> m_angle = new List<float>();

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    async void Start()
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


        foreach (eDropSize size in Enum.GetValues(typeof(eDropSize)))
        {
            for (int i = 0; i < expAmounts[size]; ++i)
            {
                var exp = await Addressables.LoadAssetAsync<GameObject>("Exp").Task;
                m_exp.Add(Instantiate(exp, transform));
                m_exp[i].transform.localScale = new Vector3(objectScale[size], objectScale[size], 0.0f);
                m_exp[i].GetComponent<DropObjectBase>().SetSize(size);
                m_exp[i].SetActive(false);
            }
        }

        foreach (eDropSize size in Enum.GetValues(typeof(eDropSize)))
        {
            for (int i = 0; i < moneyAmounts[size]; ++i)
            {
                var money = await Addressables.LoadAssetAsync<GameObject>("Money").Task;
                m_money.Add(Instantiate(money, transform));
                m_money[i].transform.localScale = new Vector3(objectScale[size], objectScale[size], 0.0f);
                m_money[i].GetComponent<DropObjectBase>().SetSize(size);
                m_money[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (transform.parent != null)
        {
            transform.position = transform.parent.position;
            m_money.ForEach(money => money.transform.position = transform.position);
            m_exp.ForEach(exp => exp.transform.position = transform.position);
        }
        if(transform.childCount == 0 && transform.parent == null)
            Destroy(gameObject);
    }

    public IEnumerator Diffusion()
    {
        Vector2 randomDirection;
        foreach (var exp in m_exp)
        {
            exp.SetActive(true);
            randomDirection = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            float toAngle = Mathf.Atan2(randomDirection.y, randomDirection.x);
            m_angle.Add(toAngle);

            if (m_angle.Count(angle => Mathf.Abs(toAngle - angle) < 2.0f) != 0)
                randomDirection = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));

            exp.GetComponent<Rigidbody2D>().velocity = randomDirection * UnityEngine.Random.Range(3.0f, 4.0f);
            yield return exp;

        }

        foreach (var money in m_money)
        {
            money.SetActive(true);
            randomDirection = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            float toAngle = Mathf.Atan2(randomDirection.y, randomDirection.x);
            m_angle.Add(toAngle);

            if (m_angle.Count(angle => Mathf.Abs(toAngle - angle) < 2.0f) != 0)
                randomDirection = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            money.GetComponent<Rigidbody2D>().velocity = randomDirection * UnityEngine.Random.Range(3.0f, 4.0f);
            yield return money;
        }


    }
}
