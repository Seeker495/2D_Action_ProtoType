using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        var virtualCamera = GameObject.Find("CM vcam1");
        var map = Map.GetComponent<Map>();
        var info = map.GetMapData();
        var boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.usedByComposite = true;
        boxCollider.offset = new Vector2(info.height / 2 - 0.5f, -info.width / 2 + 0.5f);
        boxCollider.size = new Vector2(info.height, info.width);
        virtualCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
