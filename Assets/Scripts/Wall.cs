using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRange(ref Map map)
    {
        var info = map.GetMapData();
        var boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.usedByComposite = true;
        boxCollider.offset = new Vector2(info.height / 2 - 0.5f, -info.width / 2 + 0.5f);
        boxCollider.size = new Vector2(info.height, info.width);
    }
}
