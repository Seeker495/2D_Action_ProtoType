using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    PlayerInput PlayerInput;

    private Vector2 Position;
    private Vector2 Velocity;
    private const float PLAYER_SPEED = 3.0f;
    private Dictionary<Vector2, Tile> Sprites;
    // Start is called before the first frame update
    async void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();

        Position = Rigidbody2D.position;
        Velocity = Rigidbody2D.velocity;

        Sprites = new Dictionary<Vector2, Tile>
    {
            {Vector2.up,await Addressables.LoadAssetAsync<Tile>("Characters_36").Task },
            {Vector2.right,await Addressables.LoadAssetAsync<Tile>("Characters_24").Task },
            {Vector2.left,await Addressables.LoadAssetAsync<Tile>("Characters_12").Task },
            {Vector2.down,await Addressables.LoadAssetAsync<Tile>("Characters_0").Task },
    };

        Initialize_Controller();
    }

    private void Initialize_Controller()
    {
        PlayerInput.actions["Move"].performed += Move;
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
    }

    async private void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        Velocity = move * PLAYER_SPEED;
        Rigidbody2D.velocity = Velocity;
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[move].sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


}
