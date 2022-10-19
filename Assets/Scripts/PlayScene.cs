using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
    private GameObject m_gameOverText;

    private async void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_map = GameObject.FindWithTag("Map").GetComponent<Map>();
        m_enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        m_wall = GameObject.FindWithTag("Wall").GetComponent<Wall>();
        m_cameraObject = GameObject.Find("CM vcam1");
        m_gameOverText = Instantiate(await Addressables.LoadAssetAsync<GameObject>("Text").Task,GameObject.Find("Canvas").transform);
        m_gameOverText?.SetActive(false);
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
        if(!m_player.IsArrive())
        {
            m_gameOverText?.SetActive(true);
            m_gameOverText.GetComponent<TextMeshProUGUI>().text = "GameOver";
            m_gameOverText.GetComponent<TextMeshProUGUI>().fontSize = 100.0f;
        }
    }

    private void FixedUpdate()
    {
        if (!m_map.IsLoadFinished) return;
        m_player.SetMoveRange(ref m_map);
        m_enemyManager.SetMoveRange(ref m_map);
    }
}
