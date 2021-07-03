using UnityEngine;

public class RhythmGame_JudgementWindow : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note") {
            Destroy(other.gameObject);

            // TODO: REWORK score system pls 
            if (gameObject.name.Contains("OK")) GameObject.Find("UI").GetComponent<RhythmGame_UI>().updateScoreText(10);
            else GameObject.Find("UI").GetComponent<RhythmGame_UI>().updateScoreText(100);
        }
    }


}
