using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    public float duration = 1f;
    public int score = 0;

    public Text textTarget;

    void Awake()
    {
        //textTarget = GameObject.Find("DungeonCanvas/Text").GetComponent<Text>();
    }
    IEnumerator CountTo(int target)
    {
        int start = score;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            score = (int)Mathf.Lerp(start, target, progress);
            yield return null;
        }
        score = target;
    }

    void Update()
    {
        if(textTarget != null)
            textTarget.text = score.ToString();

        if (Input.GetKey(KeyCode.Space))
        {
            StopCoroutine("CountTo");
            StartCoroutine("CountTo", score + 5000);
        }
        if (Input.GetKey(KeyCode.V))
        {
            StopCoroutine("CountTo");
            StartCoroutine("CountTo", score / 2);
        }
    }
}
