using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropObjectBase
{

    public override void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
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
