using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_Script : MonoBehaviour
{
    // �{�^���̃T�C�Y(�I��O)
    public Vector2 SizeSelect;

    // �{�^���̐F(�I��O)
    public Vector3 ColorSelect;

    // �{�^���̃T�C�Y(�I����)
    public Vector2 SizeNotSelect;

    // �{�^���̐F(�I����)
    public Vector3 ColorNotSelect;


    // Start is called before the first frame update
    void Start()
    {
        // �摜�𓧉߂����鏈��
        Color color = gameObject.GetComponent<Image>().color;
        color.r = 1.0f;
        color.g = 1.0f;
        color.b = 1.0f;
        color.a = 0.5f;
        gameObject.GetComponent<Image>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
