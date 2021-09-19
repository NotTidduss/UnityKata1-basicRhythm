using UnityEngine;
using System.Collections;

public class Rhythm_ShortNote : MonoBehaviour
{
    private float fallSpeed;


    void Start() {
        fallSpeed = PlayerPrefs.GetFloat("rhythm_scrollSpeed") / 5;
        StartCoroutine("Fall");
    } 


    IEnumerator Fall() {
        while (true) {
            transform.Translate(0, -fallSpeed, 0);

            if (gameObject.transform.position.y < -150f) {
                PlayerPrefs.SetInt("rhythm_mapScore", PlayerPrefs.GetInt("rhythm_mapScore") + PlayerPrefs.GetInt("rhythm_judgementValue_outOfBounds"));
                PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "MISS");
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
