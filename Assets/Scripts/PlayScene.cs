using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_map = GameObject.FindWithTag("Map").GetComponent<Map>();
        m_enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        m_wall = GameObject.FindWithTag("Wall").GetComponent<Wall>();
        m_cameraObject = GameObject.Find("CM vcam1");
    }

    // Start is called before the first frame update
    async void Start()
    {
        await m_map.Load("SampleStage");
        m_wall.SetRange(ref m_map);
        m_player.SetSpawnPosition(ref m_map);
        m_enemyManager.SetSpawnPosition(ref m_map);
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
        //m_enemyManager.SetMoveRange(ref m_map);
    }
}
