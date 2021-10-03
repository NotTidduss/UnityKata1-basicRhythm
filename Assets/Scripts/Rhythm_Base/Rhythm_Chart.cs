using System.Collections;
using UnityEngine;

public class Rhythm_Chart : MonoBehaviour
{   
    [Header("Scene References")]
    [SerializeField] private Transform chartAreaTransform;

    [Header("Prefab References")]
    [SerializeField] private GameObject shortNoteLeft;
    [SerializeField] private GameObject shortNoteDown;
    [SerializeField] private GameObject shortNoteUp;
    [SerializeField] private GameObject shortNoteRight;


    public void initialize() => StartCoroutine("RunChart");


    IEnumerator RunChart() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                placeRandomNote();
                yield return new WaitForSeconds(1f);
            }
            else yield return null;
        }
    }


    /*
        Randomly select a note and place it using (@link placeNote).
    */
    public void placeRandomNote() {
        int rng = Random.Range(0,4);

        switch(rng){
            case 0: placeNote(shortNoteLeft); break;
            case 1: placeNote(shortNoteDown); break;
            case 2: placeNote(shortNoteUp); break;
            case 3: placeNote(shortNoteRight); break;
        }
    }


    /*
        Place a given note prefab into the chart area.
        @param note - note prefab
    */
    private void placeNote(GameObject note) => Instantiate(note, chartAreaTransform);
}
