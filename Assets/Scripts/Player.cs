using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    PlayerInput PlayerInput;

    [SerializeField]
    private Vector2 StartPosition;
    private Vector2 Velocity;
    private const float PLAYER_SPEED = 3.0f;
    private Dictionary<Vector2, Tile> Sprites;
    private Map Map;
    private Map.MapRect mapRect;
    private Vector2 Direction;

    private List<IWeapon> WeaponsList;

    private int WeaponIndex = 0;
    // Start is called before the first frame update
    async void Start()
    {
        Map = GameObject.Find("Map").GetComponent<Map>();

        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();


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



        Initialize_Controller();
    }

    private void Initialize_Controller()
    {
        PlayerInput.actions["Move"].performed += Move;
        PlayerInput.actions["Move"].canceled += MoveEnd;
        PlayerInput.actions["Attack"].started += Attack;
        PlayerInput.actions["Dash"].started += Dash;
        PlayerInput.actions["Dash"].canceled += Dash;
        PlayerInput.actions["ChangeWeaponToLeft"].started += SelectWeaponToLeft;
        PlayerInput.actions["ChangeWeaponToRight"].started += SelectWeaponToRight;

    }

    private void Finalize_Controller()
    {
        //PlayerInput.actions["Move"].performed -= Move;
    }

    private void OnDisable()
    {
        Finalize_Controller();
        PlayerInput.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.position = new Vector2(Mathf.Clamp(Rigidbody2D.position.x, mapRect.left, mapRect.right), Mathf.Clamp(Rigidbody2D.position.y, mapRect.bottom, mapRect.top));
    }

    private void FixedUpdate()
    {
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        Velocity = move * PLAYER_SPEED;
        Rigidbody2D.velocity = Velocity;
        Direction = Rigidbody2D.velocity.normalized;
        //gameObject.GetComponent<SpriteRenderer>().sprite = Velocity != Vector2.zero ? Sprites[move].sprite : gameObject.GetComponent<SpriteRenderer>().sprite;
        Debug.Log(Rigidbody2D.velocity);
    }

    private void MoveEnd(InputAction.CallbackContext context)
    {
        Velocity = Vector2.zero;
        Rigidbody2D.velocity = Velocity;
    }


    private void Dash(InputAction.CallbackContext context)
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
    private void Attack(InputAction.CallbackContext context)
    {
        if (Rigidbody2D.velocity != Vector2.zero && WeaponsList[WeaponIndex].GetTagName() == "Blade") return;
        WeaponsList[WeaponIndex].Attack(Rigidbody2D.position,Direction);
    }


    public Sprite GetWeaponSprite()
    {
        if (WeaponsList[WeaponIndex].GetSprite().Equals(null)) return null;
        return WeaponsList[WeaponIndex].GetSprite();
    }
    private void SelectWeaponToLeft(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(--WeaponIndex) % WeaponsList.Count;
        Debug.Log($"SelectLeft:{WeaponIndex}");
    }

    private void SelectWeaponToRight(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(++WeaponIndex) % WeaponsList.Count;
        Debug.Log($"SelectRight:{WeaponIndex}");
    }



}
