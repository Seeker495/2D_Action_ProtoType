using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <概要>
 *  プレイシーンのオブジェクトを管理するクラス。
 *******************************************************************/
public class PlayScene : MonoBehaviour
{

    [SerializeField]
    private Player m_player;
    [SerializeField]
    private Map m_map;
    [SerializeField]
    private EnemyManager m_enemyManager;
    [SerializeField]
    private Wall m_wall;
    [SerializeField]
    private GameObject m_cameraObject;
    [SerializeField]
    private GameObject m_enemyManagerObject;
    [SerializeField]
    private GameObject m_playUIObject;
    private PlayUI m_playUI;

    private void Awake()
    {
        m_map.Load("StageMODOKI");
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_map = GameObject.FindWithTag("Map").GetComponent<Map>();
        m_enemyManager = Instantiate(m_enemyManagerObject, null).GetComponent<EnemyManager>();
        m_wall = GameObject.FindWithTag("Wall").GetComponent<Wall>();
        m_cameraObject = GameObject.Find("CM vcam1");
        m_cameraObject.GetComponent<CinemachineVirtualCamera>().Follow = m_player.transform;
        m_playUI = Instantiate(m_playUIObject, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<PlayUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_wall.SetRange(ref m_map);
        // ランダム配置
        //m_player.SetSpawnPosition(ref m_map);
        //m_enemyManager.SetSpawnPosition(ref m_map);
        m_cameraObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = m_wall.GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!m_map.IsLoadFinished) return;
        m_player.SetMoveRange(ref m_map);
        m_enemyManager.SetMoveRange(ref m_map);
    }
}
