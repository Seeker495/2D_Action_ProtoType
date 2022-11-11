using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    /* プレイヤー関連 */
    [SerializeField]
    private Player m_player;
    /* マップ関連 */
    [SerializeField]
    private GameObject m_mapObject;
    private Map m_map;

    /* 敵関連 */
    [SerializeField]
    private GameObject m_enemyManagerObject;
    private EnemyManager m_enemyManager;

    /* マップの制限関連 */
    [SerializeField]
    private GameObject m_wallObject;
    private Wall m_wall;

    /* カメラ(プレイヤーの追従カメラ)の関連 */
    [SerializeField]
    private GameObject m_cameraObject;

    /* プレイヤーのUI関連 */
    [SerializeField]
    private GameObject m_playUIObject;
    private PlayUI m_playUI;

    private void Awake()
    {
        /* オブジェクトの複製及び代入を行う */
        m_map = Instantiate(m_mapObject, null).GetComponent<Map>();

        // マップの読み込み(先に読み込まないとプレイヤーを取得できないため)
        m_map.Load("StageMODOKI");

        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_enemyManager = Instantiate(m_enemyManagerObject, null).GetComponent<EnemyManager>();
        m_wall = Instantiate(m_wallObject, null).GetComponent<Wall>();
        m_cameraObject = Instantiate(m_cameraObject, null);
        m_cameraObject.GetComponent<CinemachineVirtualCamera>().Follow = m_player.transform;
        m_playUI = Instantiate(m_playUIObject, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<PlayUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // マップの移動制限のための範囲を決める
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
