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

    [Serializable]
    public struct MapObjectInfo
    {
        public string mapIndex;
        public GameObject mapObject;
    }

    public List<MapObjectInfo> m_mapObjects = new List<MapObjectInfo>();

    public void Load(string stageName)
    {
        m_isLoadFinished = false;
        m_mapChips.Clear();
        m_stageName = stageName;
        var mapData = LoadStage.Load(m_stageName);

        if (mapData.width == 0 && mapData.height == 0) return;

        m_mapInfo.width = mapData.width;
        m_mapInfo.height = mapData.height;
        Debug.Log($"幅:{m_mapInfo.width}, 高さ:{m_mapInfo.height}");
        for (int i = 0; i < mapData.stage.Count; i++)
        {

            //Debug.Assert(int.Parse(mapData.stage[i]) >= 0, "正しい形式で呼ばれていません。");
            var chip = m_mapObjects.Find(obj => obj.mapIndex == mapData.stage[i]).mapObject;
            Debug.Assert(chip, $"{i}番目で{m_mapObjects.Find(obj => obj.mapIndex == mapData.stage[i]).mapIndex}を参照しました\n{i % m_mapInfo.height} {i / m_mapInfo.width}");

            chip.transform.position = new Vector3(0.0f + (i % m_mapInfo.width), 0.0f - ((i / m_mapInfo.width) % m_mapInfo.height), 0.0f);
            switch (mapData.stage[i])
            {
                case "":
                case "0":
                    m_mapChips.Add(Instantiate(chip, transform));
                    break;
                case "12":
                case "13":
                    Instantiate(m_mapObjects.Find(obj => obj.mapIndex == "").mapObject, chip.transform.position, Quaternion.identity, transform);
                    m_mapChips.Add(Instantiate(chip, transform));
                    break;
                default:
                    Instantiate(m_mapObjects.Find(obj => obj.mapIndex == "").mapObject, chip.transform.position, Quaternion.identity, transform);
                    m_mapChips.Add(Instantiate(chip, null));
                    break;
            }

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
