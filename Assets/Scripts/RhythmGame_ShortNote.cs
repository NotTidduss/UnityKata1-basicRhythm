using System.Collections;
using UnityEngine;

public class RhythmGame_ShortNote : MonoBehaviour
{
    [Header("Attributes of this Note")]
    [SerializeField] public float fallSpeed = 2.5f;

    void Start() => StartCoroutine("Fall");

    IEnumerator Fall() {
        while (true) {
            transform.Translate(0, -fallSpeed, 0);

            if (gameObject.transform.position.y < -150f) {
                // TODO: use PlayerPrefs instead?
                GameObject.Find("UI").GetComponent<RhythmGame_UI>().updateScoreText(-50);

                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
