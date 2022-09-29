using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MagicManager : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Vector2 startPosition, Vector2 direction)
    {
        Shoot(startPosition, direction);
    }

    public async void Shoot(Vector2 startPosition, Vector2 direction)
    {
        var magic = await Addressables.LoadAssetAsync<GameObject>("Fire").Task;
        Debug.Log(magic);
        GameObject magicObject = Instantiate(magic, transform);
        magicObject.GetComponent<Fire>().Attack(startPosition, direction);
    }

}
