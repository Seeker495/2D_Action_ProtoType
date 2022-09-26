using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> MapChips = new List<GameObject>();
    [SerializeField]
    private string StageName;
    public int Width;
    public int Height;

    LoadStage.MapData mapData;
    public struct MapRect
    {
        public float left, top, right, bottom;
    }

    MapRect mapRect;

    void Start()
    {
        Load("SampleStage");
    }

    public async void Load(string stageName)
    {
        mapData = LoadStage.Load(stageName);

        var mapObjects = new List<GameObject>(2)
        {
            await Addressables.LoadAssetAsync<GameObject>($"Object_0").Task,
            await Addressables.LoadAssetAsync<GameObject>($"Object_1").Task,
        };

        Width = mapData.width;
        Height = mapData.height;

        for (int i = 0; i < mapData.stage.Count; i++)
        {
            var chip = mapObjects[mapData.stage[i]];
            chip.transform.localScale = gameObject.transform.localScale;
            chip.transform.position = new Vector3(0.0f + (i / Width) * chip.transform.localScale.x, 0.0f - (i % Width) * chip.transform.localScale.y, 0.0f);
            MapChips.Add(Instantiate(chip, transform));
            Debug.Log(chip.transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }



    public Vector2 GetCenterPosition()
    {
        Vector2 chipScale = MapChips.First().transform.localScale;
        return new Vector2(MapChips[Width / 2].transform.position.x - 0.5f * chipScale.x, MapChips[Width * (Height / 2)].transform.position.y + 0.5f * chipScale.y);
    }

    public MapRect GetEdgeRect()
    {
        if (MapChips.Count == 0) { Start(); }
        else
        {
            Vector2 chipScale = MapChips.First().transform.localScale;
            mapRect.left = MapChips.First().transform.position.x - 0.5f * chipScale.x;
            mapRect.top = MapChips.First().transform.position.y + 0.5f * chipScale.y;
            mapRect.right = MapChips.Last().transform.position.x + 0.5f * chipScale.x;
            mapRect.bottom = MapChips.Last().transform.position.y - 0.5f * chipScale.y;
        }
        return mapRect;
    }

    public LoadStage.MapData GetMapData()
    {
        if (MapChips.Count == 0) { Start(); }
        return mapData;
    }

}
