using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighSpeedMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Move(GameObject gameObject, bool isRigidily, float dashingTime = 0.1f, float speed = 20.0f)
    {
        while (dashingTime >= 0.0f)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized * speed;
            dashingTime -= Time.deltaTime;
            yield return null;
        }

        if (isRigidily)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(1.0f);
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
    }
}
