using UnityEngine;

/*******************************************************************
 *  <概要>
 *  弓クラス。
 *******************************************************************/
public class Arrow : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigidBody2D;

    // 矢のスピード
    const float ARROW_SPEED = 3.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * 関数名: Shoot
     * 概要: 矢の発射
     * 引数: 開始位置,角度,方向
     */
    public void Shoot(Vector2 startPosition, float degree, Vector2 direction)
    {
        m_rigidBody2D.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 0, -degree);
        m_rigidBody2D.velocity = transform.rotation * direction * ARROW_SPEED;
        float angle = (Mathf.Atan2(m_rigidBody2D.velocity.y, m_rigidBody2D.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵及び通常の障害物に接触したら破壊する
        if (collision.CompareTag("Enemy") || collision.CompareTag("NormalObstacle"))
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 壁(マップの範囲外)に出たら破壊する
        if (collision.CompareTag("Wall"))
            Destroy(gameObject);
    }

}
