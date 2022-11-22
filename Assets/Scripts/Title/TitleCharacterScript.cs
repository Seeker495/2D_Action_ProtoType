using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************
 *  <�T�v>
 *  �^�C�g���̃L�����N�^�[�N���X�B
 *******************************************************************/
public class TitleCharacterScript : MonoBehaviour
{
    public Image sampleImage;
    // �摜�؂�ւ�
    public List<Sprite> sprite;

    // �摜�؂�ւ�����
    int changeImageTime = 0;

    // �摜�̎��
    int imageType = 0;

    // �����X�s�[�h
    public int walkSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        changeImageTime++;

        if (changeImageTime % walkSpeed == 0) 
        {
            imageType++;
            changeImageTime = 0;
            if (imageType > sprite.Count - 1) 
            {
                imageType = 0;
            }
        }
        sampleImage.sprite = sprite[imageType];
    }
}
