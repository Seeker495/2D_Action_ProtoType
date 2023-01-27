using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardScript : MonoBehaviour
{

    // �ύX��̉摜�����X�v���C�g�B
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite sprite2;
    private SpriteRenderer spriteRenderer;
    // �V�[���̔ԍ�
    public int SceneNum = 0;

    // ����
    private float spriteChangeTime = 0.0f;

    // �����H�΂�������
    private float spriteNextChange = 20.0f;

    // ���̃X�s�[�h
    public float bardSpeed = 0.01f;

    // �t���O�@
    private bool spriteChangeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out spriteRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        spriteChangeTime++;
        if (spriteChangeTime >= spriteNextChange)
        {
            spriteChangeFlag = !spriteChangeFlag;
            spriteChangeTime = 0.0f;
        }

        // �ύX�Ώۂ̃I�u�W�F�N�g������ SpriteRenderer ���擾

        // �����ŉ摜��ς���
        if (spriteChangeFlag)
            spriteRenderer.sprite = sprite;

        else
            spriteRenderer.sprite = sprite2;


        Vector2 position = transform.position;
        if (SceneNum == 0)
        {
            position.x -= bardSpeed;
            if (position.x <= -9.5f)
                position.x = 10.0f;
        }
        if (SceneNum == 1)
        {
            position.x += bardSpeed;
            if (position.x >= 9.0f)
                position.x = -10.5f;
        }
        transform.position = position;
    }
}
