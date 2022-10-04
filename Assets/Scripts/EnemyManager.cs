using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyManager : MonoBehaviour
{
    private int MAX_ENEMY_NUM;
    private List<GameObject> Enemies = new List<GameObject>();

    async void Start()
    {
        MAX_ENEMY_NUM = 5/*Random.Range(10, 20)*/;
        var enemy = await Addressables.LoadAssetAsync<GameObject>("Enemy").Task;

        for (int i = 0; i < MAX_ENEMY_NUM; ++i)
        {
            Enemies.Add(Instantiate(enemy, transform));
        }

        for(int i = 0;i<Enemies.Count;++i)
        {
            Enemies[i].GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            Enemies[i].GetComponent<Enemy>().transform.position = new Vector2(-height, -width);
        }

    }

    public void SetMoveRange(ref Map map)
    {
        var range = map.GetEdgeRect();
        foreach (var enemy in Enemies)
        {
            enemy.GetComponent<Enemy>().SetPosition(ref range.left, ref range.right, ref range.top, ref range.bottom);
        }
    }
}
