using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************
 *  <概要>
 *  タイトルのキャラクタークラス。
 *******************************************************************/
public class TitleCharacterScript : MonoBehaviour
{
    public Image sampleImage;
    // 画像切り替え
    public List<Sprite> sprite;

    // 画像切り替え時間
    int changeImageTime = 0;

    // 画像の種類
    int imageType = 0;

    // 歩くスピード
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
