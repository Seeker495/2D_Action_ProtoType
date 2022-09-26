using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Bow : MonoBehaviour,IWeapon
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Vector2 startPosition,Vector2 direction)
    {
        Shoot(startPosition,-30.0f, 30.0f, 3,direction);
    }

    public async void Shoot(Vector2 startPosition,float startDegree, float angleInterval, int arrowNum,Vector2 direction)
    {
        var arrow = await Addressables.LoadAssetAsync<GameObject>("Arrow").Task;
        Debug.Log(arrow);
        GameObject[] arrowObjects = new GameObject[arrowNum];
        for (int i = 0; i < arrowNum; i++)
        {
            arrowObjects[i] = Instantiate(arrow, transform);
        }


        for (int i = 0; i < arrowNum; i++)
        {
            arrowObjects[i].GetComponent<Arrow>().Shoot(startPosition, startDegree + i * angleInterval,direction);
        }
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
