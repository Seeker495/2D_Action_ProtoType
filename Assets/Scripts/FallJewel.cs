using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallJewel : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_fallJewels;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FallingJewel(100));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FallingJewel(int jewelNum)
    {
        while(jewelNum > 0)
        {
            float randomFallPositionX = Random.Range(-7.0f, -3.0f);
            int randomJewelIndex = Random.Range(0, m_fallJewels.Count);
            float angle = Random.Range(0.0f, 360.0f);
            var obj = Instantiate(m_fallJewels[randomJewelIndex], new Vector3(randomFallPositionX, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, angle), null);
            obj.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            obj.GetComponent<CircleCollider2D>().isTrigger = false;
            obj.GetComponent<CircleCollider2D>().radius = 3.0f;
            float waitSecond = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(waitSecond);
            jewelNum--;
        }
    }
}
