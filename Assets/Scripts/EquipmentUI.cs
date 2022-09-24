using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private Player Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Image>().sprite != Player.GetWeaponSprite())
            GetComponent<Image>().sprite = Player.GetWeaponSprite();
    }
}
