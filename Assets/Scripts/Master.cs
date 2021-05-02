using System.Collections;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject arrowLeft;
    public GameObject arrowDown;
    public GameObject arrowUp;
    public GameObject arrowRight;
    public GameObject lineLeft;
    public GameObject lineDown;
    public GameObject lineUp;
    public GameObject lineRight;
    public GameObject playField;
    public GameObject prefabLeft;
    public GameObject prefabDown;
    public GameObject prefabUp;
    public GameObject prefabRight;

    private Color arrowLeftColor;
    private Color arrowDownColor;
    private Color arrowUpColor;
    private Color arrowRightColor;

    void Start() {
        StartCoroutine("Main");
        StartCoroutine("CheckDownInput");
    }

    IEnumerator CheckDownInput() {
        while(true) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) Instantiate(lineLeft, arrowLeft.transform);
            if (Input.GetKeyDown(KeyCode.DownArrow)) Instantiate(lineDown, arrowDown.transform);
            if (Input.GetKeyDown(KeyCode.UpArrow)) Instantiate(lineUp, arrowUp.transform);
            if (Input.GetKeyDown(KeyCode.RightArrow)) Instantiate(lineRight, arrowRight.transform);

            yield return null;
        }
    }

    IEnumerator Main() {
        while(true) {
            spawnRandomNote();

            yield return new WaitForSeconds(1f);
        }
    }

    private void spawnRandomNote() {
        float noteIndex = Random.Range(0,4);

        switch(noteIndex){
            case 0:
                spawn(prefabLeft);
                break;
            case 1:
                spawn(prefabDown);
                break;
            case 2:
                spawn(prefabUp);
                break;
            case 3:
                spawn(prefabRight);
                break;
        }
    }

    private void spawn(GameObject arrowPrefab) => GameObject.Instantiate(arrowPrefab, playField.transform);
}
