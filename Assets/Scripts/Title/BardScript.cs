using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardScript : MonoBehaviour
{

    // 変更後の画像を持つスプライト。
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite sprite2;
    private SpriteRenderer spriteRenderer;
    // シーンの番号
    public int SceneNum = 0;

    // 時間
    private float spriteChangeTime = 0.0f;

    // 鳥が羽ばたく時間
    private float spriteNextChange = 20.0f;

    // 鳥のスピード
    public float bardSpeed = 0.01f;

    // フラグ　
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

        // 変更対象のオブジェクトが持つ SpriteRenderer を取得

        // 向きで画像を変える
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
