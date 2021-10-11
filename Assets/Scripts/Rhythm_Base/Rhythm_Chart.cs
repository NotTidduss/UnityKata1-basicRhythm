using System.Collections;
using UnityEngine;

public class Rhythm_Chart : MonoBehaviour
{   
    [Header("Scene References")]
    [SerializeField] private Transform leftLaneTransform;
    [SerializeField] private Transform downLaneTransform;
    [SerializeField] private Transform upLaneTransform;
    [SerializeField] private Transform rightLaneTransform;


    [Header("Prefab References")]
    [SerializeField] private GameObject shortNoteLeft;
    [SerializeField] private GameObject shortNoteDown;
    [SerializeField] private GameObject shortNoteUp;
    [SerializeField] private GameObject shortNoteRight;


    //* private vars
    // note indices are used to keep track of the next-to-hit notes.
    private int noteLeftIndex, noteDownIndex, noteUpIndex, noteRightIndex = 0;


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
            case 0: placeNote(shortNoteLeft, leftLaneTransform, noteLeftIndex++); break;
            case 1: placeNote(shortNoteDown, downLaneTransform, noteDownIndex++); break;
            case 2: placeNote(shortNoteUp, upLaneTransform, noteUpIndex++); break;
            case 3: placeNote(shortNoteRight, rightLaneTransform, noteRightIndex++); break;
        }
    }


    /*
        Place a given note prefab into the chart area.
        @param note - note prefab
    */
    private void placeNote(GameObject note, Transform lane, int currentNoteIndex) {
        GameObject newNote = Instantiate(note, lane);
        newNote.GetComponent<Rhythm_ShortNote>().initialize(currentNoteIndex);
    } 
}
