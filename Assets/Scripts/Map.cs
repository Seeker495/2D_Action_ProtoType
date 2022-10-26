using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <概要>
 *  ステージを表示するクラス。
 *******************************************************************/
public class Map : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> m_mapChips = new List<GameObject>();
    [SerializeField]
    private string m_stageName;
    private bool m_isLoadFinished = false;
    public bool IsLoadFinished => m_isLoadFinished;

    public struct MapRect
    {
        public float left, top, right, bottom;
    }

    private MapRect m_mapRect;

    public struct MapInfo
    {
        public int width, height;
    }

    private MapInfo m_mapInfo;


    public async Task Load(string stageName)
    {
        m_isLoadFinished = false;
        m_mapChips.Clear();
        m_stageName = stageName;
        var mapData = LoadStage.Load(m_stageName);

        var mapObjects = new List<GameObject>(2)
        {
            await Addressables.LoadAssetAsync<GameObject>($"Object_0").Task,
            await Addressables.LoadAssetAsync<GameObject>($"Object_1").Task,
        };


        m_mapInfo.width = mapData.width;
        m_mapInfo.height = mapData.height;

        for (int i = 0; i < mapData.stage.Count; i++)
        {
            var chip = mapObjects[mapData.stage[i]];
            chip.transform.localScale = gameObject.transform.localScale;
            chip.transform.position = new Vector3(0.0f + (i / m_mapInfo.width) * chip.transform.localScale.x, 0.0f - (i % m_mapInfo.width) * chip.transform.localScale.y, 0.0f);
            m_mapChips.Add(Instantiate(chip, transform));

        }
        m_isLoadFinished = true;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public Vector2 GetCenterPosition()
    {
        Vector2 chipScale = m_mapChips.First().transform.localScale;
        return new Vector2(m_mapChips[m_mapInfo.width / 2].transform.position.x - 0.5f * chipScale.x, m_mapChips[m_mapInfo.width * (m_mapInfo.height / 2)].transform.position.y + 0.5f * chipScale.y);
    }

    public MapRect GetEdgeRect()
    {
        Vector2 chipScale = m_mapChips.First().transform.localScale;
        m_mapRect.left = m_mapChips.First().transform.position.x - 0.5f * chipScale.x;
        m_mapRect.top = m_mapChips.First().transform.position.y + 0.5f * chipScale.y;
        m_mapRect.right = m_mapChips.Last().transform.position.x + 0.5f * chipScale.x;
        m_mapRect.bottom = m_mapChips.Last().transform.position.y - 0.5f * chipScale.y;
        return m_mapRect;
    }

    public MapInfo GetMapData()
    {
        return m_mapInfo;
    }

}
