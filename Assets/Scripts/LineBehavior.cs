using System.Collections;
using UnityEngine;

public class LineBehavior : MonoBehaviour
{
    private ScoreController score;

    void Start() {
        score = GameObject.Find("Master").GetComponent<ScoreController>();
        StartCoroutine("CheckUpInput");
    }

    IEnumerator CheckUpInput() {
        while(true) {
            if (Input.GetKeyUp(KeyCode.LeftArrow) && gameObject.name.Contains("LineLeft")) Destroy(gameObject); 
            if (Input.GetKeyUp(KeyCode.DownArrow) && gameObject.name.Contains("LineDown")) Destroy(gameObject); 
            if (Input.GetKeyUp(KeyCode.UpArrow) && gameObject.name.Contains("LineUp")) Destroy(gameObject); 
            if (Input.GetKeyUp(KeyCode.RightArrow) && gameObject.name.Contains("LineRight")) Destroy(gameObject); 

            yield return null;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note") {
            Destroy(other.gameObject);

            if (gameObject.name.Contains("OK")) score.updateScore(10);
            else score.updateScore(100);;
        }
    }
}
