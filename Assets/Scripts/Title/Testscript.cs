using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testscript : MonoBehaviour
{

    public menu_Script menu_Script;

    float keepPos = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Color color = gameObject.GetComponent<Image>().color;        
        // �摜�𓧉߂����鏈��
        color.a = 1.0f;
        gameObject.GetComponent<Image>().color = color;

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        // ���W���擾
        Vector3 pos = myTransform.position;

        keepPos = -menu_Script.GetButtonNum() * 100;

    
        pos.y = keepPos + 600;


        myTransform.position = pos;  // ���W��ݒ�
    }
}
