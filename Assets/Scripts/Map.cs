using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> MapChips = new List<GameObject>();
    [SerializeField]
    private string StageName;
    private int Width;
    private int Height;
    public struct MapRect
    {
        public float left, top, right, bottom;
    }
    async void Start()
    {
        var map = LoadStage.Load(StageName);
        Width = map.width;
        Height = map.height;
        for (int i = 0; i < map.stage.Count; i++)
        {
            var chip = await Addressables.LoadAssetAsync<GameObject>($"Object_{map.stage[i]}").Task;
            chip.transform.localScale = gameObject.transform.localScale;
            chip.transform.position = new Vector3(-5.0f + (i % Height) * chip.transform.localScale.x, 5.0f - (i / Width) * chip.transform.localScale.y, 0.0f);
            MapChips.Add(Instantiate(chip));
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
        MapRect mapRect;
        Vector2 chipScale = MapChips.First().transform.localScale;

        mapRect.left = MapChips.First().transform.position.x - 0.5f * chipScale.x;
        mapRect.top = MapChips.First().transform.position.y + 0.5f * chipScale.y;
        mapRect.right = MapChips.Last().transform.position.x + 0.5f * chipScale.x;
        mapRect.bottom = MapChips.Last().transform.position.y - 0.5f * chipScale.y;
        return mapRect;
    }

}
