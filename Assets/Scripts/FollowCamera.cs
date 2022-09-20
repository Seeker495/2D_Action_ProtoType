using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera Camera;
    [SerializeField]
    private GameObject Following_Object;
    private Vector3 EyePosition;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        EyePosition = new Vector3(Following_Object.transform.position.x, Following_Object.transform.position.y, Camera.transform.position.z);
        Camera.transform.position = EyePosition;
    }
}
