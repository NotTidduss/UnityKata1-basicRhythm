using UnityEngine;

// TODO: Note judgement will be reworked to be based on transform position instead of collision detection; this class might be obsolete then.
public class Rhythm_JudgementWindow : MonoBehaviour
{
    [Header ("Judgement Window Attributes")]
    [SerializeField] private Rhythm_JudgementType judgementType;

    // private vars
    private int judgementValue;


    void Start() {
        switch(judgementType) {
            case Rhythm_JudgementType.PERFECT: judgementValue = PlayerPrefs.GetInt("rhythm_judgementValue_perfect"); break;
            case Rhythm_JudgementType.GOOD: judgementValue = PlayerPrefs.GetInt("rhythm_judgementValue_good"); break;
            case Rhythm_JudgementType.FINE: judgementValue = PlayerPrefs.GetInt("rhythm_judgementValue_fine"); break;
            case Rhythm_JudgementType.MISS: judgementValue = PlayerPrefs.GetInt("rhythm_judgementValue_miss"); break;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Note") {
            PlayerPrefs.SetInt("rhythm_mapScore", PlayerPrefs.GetInt("rhythm_mapScore") + judgementValue);
            PlayerPrefs.SetString("rhythm_lastNoteHitTiming", judgementType.ToString());
            Destroy(other.gameObject);
        }
    }
}
