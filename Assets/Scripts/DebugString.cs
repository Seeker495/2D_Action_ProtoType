using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugString : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"HP: {player.HP}";
    }
}
