using UnityEngine;

/*******************************************************************
 *  <概要>
 *  ドロップするオブジェクトの基底クラス。
 *******************************************************************/
public abstract class DropObjectBase : MonoBehaviour
{
    private Rigidbody2D m_rigidBody2D;
    private eDropSize m_dropSize;
    private Player m_player;
    private float m_arriveTime;

    public virtual void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_arriveTime = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        if (transform.parent.parent == null)
            m_arriveTime += Time.deltaTime;
        if (m_arriveTime > 1.5f)
            m_rigidBody2D.velocity = (m_player.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * Parameter.DROP_EFFECT_SPEED;

        if (Vector2.Distance(m_player.GetComponent<Rigidbody2D>().position, m_rigidBody2D.position) <= 2.0f)
            m_rigidBody2D.velocity = (m_player.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * Parameter.DROP_EFFECT_SPEED * 0.7f;

    }

    public int Get()
    {
        return (int)m_dropSize;
    }

    public Vector2 GetPosition()
    {
        return m_rigidBody2D.position;
    }

    public Vector2 GetVelocity()
    {
        return m_rigidBody2D.velocity;
    }

    public void SetSize(in eDropSize size)
    {
        m_dropSize = size;
    }
}
