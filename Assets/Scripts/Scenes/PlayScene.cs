using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private GameObject m_farCameraObject;

    /* �v���C���[��UI�֘A */
    [SerializeField]
    private GameObject m_playUIObject;
    private PlayUI m_playUI;


    [SerializeField]
    private GameObject m_pauseDisplayObject;
    private PauseDisplay m_pauseDisplay;

#if UNITY_EDITOR
    [SerializeField] private GameObject m_debugDisplayObject;
    [SerializeField]
    private List<string> m_debugStageNames;
#endif

    [SerializeField]
    private List<string> m_stageNames;

    [SerializeField] private GameObject m_soundManagerObject;

    private void OnEnable()
    {
        m_soundManagerObject = Instantiate(m_soundManagerObject, null);
        SoundPlayer.SetUp(m_soundManagerObject);
        SoundPlayer.PlayBGM(eBGM.PLAY);

        PlayerController.Controller.Play.Enable();
        PlayerController.Controller.Play.ToPause.started += Pause;
#if UNITY_EDITOR
        PlayerController.Controller.Play.OpenDebugMode.started += OpenDebugMode;
#endif
    }

    private void OnDisable()
    {
        PlayerController.Controller.Play.ToPause.started -= Pause;
#if UNITY_EDITOR
        PlayerController.Controller.Play.OpenDebugMode.started -= OpenDebugMode;
#endif
        PlayerController.Controller.Play.Disable();

    }


    private void Awake()
    {
        if(Time.timeScale <= 0.0f) Time.timeScale = 1.0f;

        /* �I�u�W�F�N�g�̕����y�ё�����s�� */
        Instantiate(m_mapObject, null).TryGetComponent(out m_map);
#if UNITY_EDITOR
        if (Parameter.IS_DEBUG_MODE)
            // �}�b�v�̓ǂݍ���(��ɓǂݍ��܂Ȃ��ƃv���C���[���擾�ł��Ȃ�����)
            m_map.Load(m_debugStageNames[Parameter.DEBUG_MAP_INDEX]);
        else
            // �}�b�v�̓ǂݍ���(��ɓǂݍ��܂Ȃ��ƃv���C���[���擾�ł��Ȃ�����)
            m_map.Load(m_stageNames[(int)Parameter.CURRENT_ALIVE_DAY]);

#else
        // �}�b�v�̓ǂݍ���(��ɓǂݍ��܂Ȃ��ƃv���C���[���擾�ł��Ȃ�����)
        m_map.Load(m_stageNames[(int)Parameter.CURRENT_ALIVE_DAY]);
#endif

        GameObject.FindWithTag("Player").TryGetComponent(out m_player);
        m_cameraObject = Instantiate(m_cameraObject, null);
        m_farCameraObject = Instantiate(m_farCameraObject, null);
        Instantiate(m_wallObject, null).TryGetComponent(out m_wall);
        Instantiate(m_enemyManagerObject, null).TryGetComponent(out m_enemyManager);
        Instantiate(m_playUIObject, GameObject.FindGameObjectWithTag("Canvas").transform).TryGetComponent(out m_playUI);
        Instantiate(m_pauseDisplayObject, GameObject.FindGameObjectWithTag("Canvas").transform).TryGetComponent(out m_pauseDisplay);
        if (Parameter.CURRENT_ALIVE_DAY != 0)
        {
            m_player.SetParameter(PlayerData.GetStatus());
            m_player.SetSkill(PlayerData.GetSkillList());
        }
        m_cameraObject.GetComponent<CinemachineVirtualCamera>().Follow = m_player.transform;
        m_farCameraObject.GetComponent<CinemachineVirtualCamera>().Follow = m_player.transform;
        m_pauseDisplay.gameObject.SetActive(false);
#if UNITY_EDITOR
        m_debugDisplayObject = Instantiate(m_debugDisplayObject, GameObject.FindGameObjectWithTag("Canvas").transform);
        m_debugDisplayObject.SetActive(false);
#endif


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
        m_farCameraObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = m_wall.GetComponent<CompositeCollider2D>();
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


    public void Pause(InputAction.CallbackContext context)
    {
        Time.timeScale = 0.0f;
        m_pauseDisplay.gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    private void OpenDebugMode(InputAction.CallbackContext context)
    {
        m_debugDisplayObject.SetActive(true);
    }
#endif
}
