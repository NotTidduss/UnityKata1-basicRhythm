using UnityEngine;
using System.Collections;

public class Rhythm_ShortNote : MonoBehaviour
{
    private float fallSpeed;


    // TODO: replace with initialize()
    void Start() {
        // initialize private vars
        fallSpeed = PlayerPrefs.GetFloat("rhythm_scrollSpeed") / 5;

        StartCoroutine("Fall");
    } 


    IEnumerator Fall() {
        while (true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                transform.Translate(0, -fallSpeed, 0);

                // count as miss if note falls too low, and destroy this object.
                if (gameObject.transform.position.y < -150f) {
                    PlayerPrefs.SetInt("rhythm_mapScore", PlayerPrefs.GetInt("rhythm_mapScore") + PlayerPrefs.GetInt("rhythm_judgementValue_outOfBounds"));
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "MISS");
                    Destroy(gameObject);
                }
            }
            yield return null;
        }
    }
}
