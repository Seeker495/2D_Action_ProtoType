using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> MapChips = new List<GameObject>();
    [SerializeField]
    private string StageName;
    async void Start()
    {
        var map = LoadStage.Load(StageName);
        for(int i= 0; i < map.stage.Count; i++)
        {
            var chip = await Addressables.LoadAssetAsync<GameObject>($"Object_{map.stage[i]}").Task;
            chip.transform.position = new Vector3(-5.0f + (i % map.height), 5.0f - (i / map.width), 0.0f);
            MapChips.Add(Instantiate(chip));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
