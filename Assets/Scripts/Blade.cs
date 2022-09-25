using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour,IWeapon
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
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
        StartCoroutine(Attacking(startPosition, 60.0f,direction));
    }

    public IEnumerator Attacking(Vector2 startPosition, float degree , Vector2 direction)
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        Rigidbody2D.position = startPosition;
        float angleZ = 0.0f;
        while(angleZ <= degree)
        {
            transform.rotation = Quaternion.Euler(0, 0, -angleZ);
            Rigidbody2D.velocity = transform.rotation * direction;
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
