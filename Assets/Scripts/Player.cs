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
    private bool IsDash;
    // Start is called before the first frame update
    async void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();


        Sprites = new Dictionary<Vector2, Tile>(4)
        {
            {Vector2.up,await Addressables.LoadAssetAsync<Tile>("Characters_36").Task },
            {Vector2.right,await Addressables.LoadAssetAsync<Tile>("Characters_24").Task },
            {Vector2.left,await Addressables.LoadAssetAsync<Tile>("Characters_12").Task },
            {Vector2.down,await Addressables.LoadAssetAsync<Tile>("Characters_0").Task },
        };

        Map = GameObject.Find("Map").GetComponent<Map>();
        Rigidbody2D.position = StartPosition;
        var edge = Map.GetEdgeRect();

        bool positionWithout_x = edge.left > StartPosition.x || edge.right < StartPosition.x;
        bool positionWithout_y = edge.top > StartPosition.y || edge.bottom < StartPosition.y;
        if (positionWithout_x || positionWithout_y)
            //Rigidbody2D.position = Map.GetCenterPosition();

            Initialize_Controller();
    }

    private void Initialize_Controller()
    {
        PlayerInput.actions["Move"].performed += Move;
        PlayerInput.actions["Move"].canceled += MoveEnd;
        PlayerInput.actions["Attack"].performed += Attack;
        PlayerInput.actions["Dash"].started += Dash;
        PlayerInput.actions["Dash"].canceled += Dash;


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
    }

    private void FixedUpdate()
    {
        var edge = Map.GetEdgeRect();
        Rigidbody2D.position = new Vector2(Mathf.Clamp(Rigidbody2D.position.x, edge.left, edge.right), Mathf.Clamp(Rigidbody2D.position.y, edge.bottom, edge.top));
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        Velocity = move * PLAYER_SPEED;
        Rigidbody2D.velocity = Velocity;
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
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


}
