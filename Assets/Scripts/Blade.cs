using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour,IWeapon
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
    const float BLADE_SPEED = 6.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Vector2 startPosition, Vector2 direction)
    {
        StartCoroutine(Attacking(startPosition, 120.0f,direction));
    }

    public IEnumerator Attacking(Vector2 startPosition, float degree , Vector2 direction)
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        Rigidbody2D.position = startPosition;
        float angleZ = 0.0f;
        float angle;
        while(angleZ * BLADE_SPEED <= degree)
        {
            transform.localPosition = direction * 0.3f;
            transform.rotation = Quaternion.Euler(0, 0, -angleZ * BLADE_SPEED);
            Rigidbody2D.velocity = transform.rotation * direction;
            angle = (Mathf.Atan2(Rigidbody2D.velocity.y, Rigidbody2D.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
            transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);

            angleZ += 0.3f;
            yield return null;
        }

        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

    }

    public Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public string GetTagName()
    {
        return gameObject.tag;
    }

}
