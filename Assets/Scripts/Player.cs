using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;

    [SerializeField]
    private Vector2 StartPosition;
    private Vector2 Velocity;
    private const float PLAYER_SPEED = 3.0f;
    private Dictionary<Vector2, Tile> Sprites;
    private Map Map;
    private Map.MapRect mapRect;
    private Vector2 Direction = Vector2.up;
    public int HP = 5;
    private bool IsDamaged = false;
    private List<IWeapon> WeaponsList;

    private int WeaponIndex = 0;
    // Start is called before the first frame update
    async void Start()
    {
        Map = GameObject.Find("Map").GetComponent<Map>();

        Rigidbody2D = GetComponent<Rigidbody2D>();


        Sprites = new Dictionary<Vector2, Tile>(4)
        {
            {Vector2.up,await Addressables.LoadAssetAsync<Tile>("Characters_36").Task },
            {Vector2.right,await Addressables.LoadAssetAsync<Tile>("Characters_24").Task },
            {Vector2.left,await Addressables.LoadAssetAsync<Tile>("Characters_12").Task },
            {Vector2.down,await Addressables.LoadAssetAsync<Tile>("Characters_0").Task },
        };

        WeaponsList = new List<IWeapon>(2)
        {
            GetComponentInChildren<Blade>(),
            GetComponentInChildren<Bow>(),
        };
        mapRect = Map.GetEdgeRect();
        var data = Map.GetMapData();

        StartPosition = new Vector2(Random.Range(0, data.width), Random.Range(-data.height, 0));
        Rigidbody2D.position = StartPosition;
    }



    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.position = new Vector2(Mathf.Clamp(Rigidbody2D.position.x, mapRect.left, mapRect.right), Mathf.Clamp(Rigidbody2D.position.y, mapRect.bottom, mapRect.top));
    }

    private void FixedUpdate()
    {
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        Velocity = move * PLAYER_SPEED;
        Rigidbody2D.velocity = Velocity;
        Direction = Rigidbody2D.velocity.normalized;
        //gameObject.GetComponent<SpriteRenderer>().sprite = Velocity != Vector2.zero ? Sprites[move].sprite : gameObject.GetComponent<SpriteRenderer>().sprite;
        Debug.Log(Rigidbody2D.velocity);
    }

    public void MoveEnd(InputAction.CallbackContext context)
    {
        Velocity = Vector2.zero;
        Rigidbody2D.velocity = Velocity;
    }


    public void Dash(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Velocity *= 2;
                break;
            case InputActionPhase.Canceled:
                Velocity /= 2;
                break;
        }
        Rigidbody2D.velocity = Velocity;
        Debug.Log(Rigidbody2D.velocity);

    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (Rigidbody2D.velocity != Vector2.zero && WeaponsList[WeaponIndex].GetTagName() == "Blade") return;
        WeaponsList[WeaponIndex].Attack(Rigidbody2D.position, Direction);
    }


    public Sprite GetWeaponSprite()
    {
        return WeaponsList[WeaponIndex].GetSprite();
    }
    public void SelectWeaponToLeft(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(--WeaponIndex) % WeaponsList.Count;
        Debug.Log($"SelectLeft:{WeaponIndex}");
    }

    public void SelectWeaponToRight(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(++WeaponIndex) % WeaponsList.Count;
        Debug.Log($"SelectRight:{WeaponIndex}");
    }

    public void Resurrection(InputAction.CallbackContext context)
    {
        gameObject.SetActive(true);
        if (IsArrive()) return;
        gameObject.tag = "Player";
        HP = 10;
        IsDamaged = false;
        GameObject.Find("PlayerController").GetComponent<PlayerController>().playerController.Enable();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDamaged) return;
        if (collision.gameObject.CompareTag("Enemy"))
            Damage(1);
        if (!IsArrive())
            Dead();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDamaged && !collision.CompareTag("Wall") && !collision.CompareTag("NormalObstacle") && !collision.CompareTag("WaterObstacle")) return;
        if (collision.gameObject.CompareTag("Magic"))
            Damage(2);
        if (!IsArrive())
            Dead();

    }


    public bool IsArrive()
    {
        return 0 < HP;
    }

    private void Dead()
    {
        gameObject.tag = "Untagged";
        Debug.Log("Dead!!!!");
        GameObject.Find("PlayerController").GetComponent<PlayerController>().playerController.Disable();
        gameObject.SetActive(false);

        //StartCoroutine(OnDead(0.1f, 0.3f));
    }

    void Damage(in int damage = 0)
    {
        HP -= damage;
        StartCoroutine(OnDamage(2.0f, 0.3f));
    }

    IEnumerator OnDamage(float duration, float interval)
    {
        IsDamaged = true;
        bool changed = false;
        int inter = 0;
        while (duration > 0.0f)
        {
            inter++;
            duration -= Time.deltaTime;
            if (inter % 30 == 0)
                changed = !changed;
            if(changed)
            GetComponent<SpriteRenderer>().color = Color.red;
            else
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        IsDamaged = false;
    }

}
