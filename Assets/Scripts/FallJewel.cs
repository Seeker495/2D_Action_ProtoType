using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallJewel : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_fallJewels;
    [SerializeField] private TextMeshProUGUI m_score;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FallingJewel(/*Parameter.TOTAL_SCORE / 1000000*/40));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FallingJewel(long jewelNum)
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
        yield return StartCoroutine(Score());
    }

    private IEnumerator Score()
    {
        m_score.text = "SCORE :";
        yield return new WaitForSeconds(1.0f);
        m_score.text = $"SCORE :{Parameter.TOTAL_SCORE}";
        yield return new WaitForSeconds(2.0f);
        Parameter.NEXT_SCENE_NAME = "Title";
        SceneManager.LoadSceneAsync("Loading");
        yield return null;
    }

}
