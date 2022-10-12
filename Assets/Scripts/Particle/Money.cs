using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : DropObjectBase
{
    public override void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
