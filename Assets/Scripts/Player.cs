using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();

        Position = Rigidbody2D.position;
        Velocity = Rigidbody2D.velocity;

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
        Dictionary<Vector2, int> directionSprite = new Dictionary<Vector2, int>
        {
            {Vector2.up,12*3 },
            {Vector2.right,12*2},
            {Vector2.left,12*1},
            {Vector2.down,12*0},

        };
        Velocity = move * PLAYER_SPEED;
        Rigidbody2D.velocity = Velocity;
        var tile = await Addressables.LoadAssetAsync<Tile>($"Characters_{directionSprite[move]}").Task;
        gameObject.GetComponent<SpriteRenderer>().sprite = tile.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


}
