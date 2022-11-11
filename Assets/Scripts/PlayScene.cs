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
 *  <�T�v>
 *  �v���C�V�[���̃I�u�W�F�N�g���Ǘ�����N���X�B
 *******************************************************************/
public class PlayScene : MonoBehaviour
{
    /* �v���C���[�֘A */
    [SerializeField]
    private Player m_player;
    /* �}�b�v�֘A */
    [SerializeField]
    private GameObject m_mapObject;
    private Map m_map;

    /* �G�֘A */
    [SerializeField]
    private GameObject m_enemyManagerObject;
    private EnemyManager m_enemyManager;

    /* �}�b�v�̐����֘A */
    [SerializeField]
    private GameObject m_wallObject;
    private Wall m_wall;

    /* �J����(�v���C���[�̒Ǐ]�J����)�̊֘A */
    [SerializeField]
    private GameObject m_cameraObject;

    /* �v���C���[��UI�֘A */
    [SerializeField]
    private GameObject m_playUIObject;
    private PlayUI m_playUI;

    private void Awake()
    {
        /* �I�u�W�F�N�g�̕����y�ё�����s�� */
        m_map = Instantiate(m_mapObject, null).GetComponent<Map>();

        // �}�b�v�̓ǂݍ���(��ɓǂݍ��܂Ȃ��ƃv���C���[���擾�ł��Ȃ�����)
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
        // �}�b�v�̈ړ������̂��߂͈̔͂����߂�
        m_wall.SetRange(ref m_map);

        // �����_���z�u
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
