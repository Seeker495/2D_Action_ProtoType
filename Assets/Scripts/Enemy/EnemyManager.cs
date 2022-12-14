using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <概要>
 *  敵の管理クラス。
 *  <仕組み>
 *  EnemyManagerを生成する際に敵のプレハブをあらかじめ読み込んでおき,
 *  複製しやすいようにしている(つもり)
 *******************************************************************/
public class EnemyManager : MonoBehaviour
{
    private int MAX_ENEMY_NUM;
    private List<GameObject> Enemies = new List<GameObject>();

    async void Awake()
    {
        MAX_ENEMY_NUM = 1/*Random.Range(10, 20)*/;
        var enemy = await Addressables.LoadAssetAsync<GameObject>("Enemy_3").Task;

        for (int i = 0; i < MAX_ENEMY_NUM; ++i)
        {
            Enemies.Add(Instantiate(enemy, transform));
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemyObject in Enemies)
        {
            if (enemyObject == null) continue;
            enemyObject.GetComponent<EnemyBase>().Execute();
        }
    }

    private void FixedUpdate()
    {
    }

    public void SetSpawnPosition(ref Map map)
    {
        var info = map.GetMapData();
        for (int i = 0; i < Enemies.Count; ++i)
        {
            float width = Random.Range(0.0f, info.width);
            float height = Random.Range(-(info.height), 0.0f);
            Enemies[i].GetComponent<EnemyBase>().transform.position = new Vector2(-height, -width);
        }

    }

    public void SetMoveRange(ref Map map)
    {
        var range = map.GetEdgeRect();
        foreach (var enemy in Enemies)
        {
            if (enemy == null) continue;
            enemy.GetComponent<EnemyBase>().SetPosition(ref range.left, ref range.right, ref range.bottom, ref range.top);
        }
    }
}
