using System.Collections;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    void Start() {
        StartCoroutine("Fall");
    }

    IEnumerator Fall() {
        while (true) {
            gameObject.transform.position -= new Vector3(0, 3f, 0);

            if (gameObject.transform.position.y < -150f) {
                GameObject.Find("Master").GetComponent<ScoreController>().updateScore(-50);

                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
