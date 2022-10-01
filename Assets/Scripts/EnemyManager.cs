using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyManager : MonoBehaviour
{
    private int MAX_ENEMY_NUM;
    private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField]
    private GameObject Map;

    async void Start()
    {
        MAX_ENEMY_NUM = 5/*Random.Range(10, 20)*/;
        var enemy = await Addressables.LoadAssetAsync<GameObject>("Enemy").Task;
        var data = Map.GetComponent<Map>();
        var info = data.GetMapData();

        for (int i = 0; i < MAX_ENEMY_NUM; ++i)
        {
            float width = Random.Range(0.0f, info.width);
            float height = Random.Range(-(info.height), 0.0f);
            Enemies.Add(Instantiate(enemy, transform));
            Enemies[i].GetComponent<Enemy>().transform.position = new Vector2(-height,-width);
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
}
