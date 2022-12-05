using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        if (Camera.main)
        {
            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = false;
            if (GetComponent<BoxCollider2D>())
                GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnBecameVisible()
    {
        if (Camera.main)
        {
            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = true;
            if (GetComponent<BoxCollider2D>())
                GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnWillRenderObject()
    {
        if (Camera.main)
        {
            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = true;
            if (GetComponent<BoxCollider2D>())
                GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
