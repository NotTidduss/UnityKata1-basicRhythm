using System.Collections;
using UnityEngine;

public class Rhythm_Game_Chart : MonoBehaviour
{   
    [Header("Scene References")]
    [SerializeField] private Transform leftLaneTransform;
    [SerializeField] private Transform downLaneTransform;
    [SerializeField] private Transform upLaneTransform;
    [SerializeField] private Transform rightLaneTransform;


    [Header("Prefab References")]
    [SerializeField] private GameObject shortNotePrefab;
    [SerializeField] private GameObject longNotePrefab;
    [SerializeField] private GameObject noteTrailPrefab;


    //* private vars
    private int noteLeftIndex, noteDownIndex, noteUpIndex, noteRightIndex = 0;      // note indices are used to keep track of the next-to-hit notes.
    private float rngNoteLength;                                                    // a random value used for generating long notes.
    private float scrollSpeedWaitTime;                                              // time waited between falling notes.
    private Sprite noteLeftSprite, noteDownSprite, noteUpSprite, noteRightSprite;   // colors of individual notes in lanes.


    public void initialize(Rhythm_System sys) {
        // initialite private vars
        noteLeftSprite = sys.spriteNoteLeft;
        noteDownSprite = sys.spriteNoteDown;
        noteUpSprite = sys.spriteNoteUp;
        noteRightSprite = sys.spriteNoteRight;

        // start placing notes in lanes
        StartCoroutine("PlaceNotesInLeftLane");
        StartCoroutine("PlaceNotesInDownLane");
        //StartCoroutine("PlaceNotesInUpLane");
        //StartCoroutine("PlaceNotesInRightLane");

        // calculate wait time between notes based on set scroll speed
        scrollSpeedWaitTime = PlayerPrefs.GetFloat("rhythm_scrollSpeed") / 5f;
    }


    // Place a short note in the left lane regularly.
    public IEnumerator PlaceNotesInLeftLane() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                placeNoteInLane(Rhythm_InputDirection.LEFT);
                yield return new WaitForSeconds(scrollSpeedWaitTime);
            }
            else yield return null;
        }
    }

    // Place a short note in the down lane regularly.
    public IEnumerator PlaceNotesInDownLane() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                placeNoteInLane(Rhythm_InputDirection.DOWN);
                yield return new WaitForSeconds(scrollSpeedWaitTime);
            }
            else yield return null;
        }
    }

    
    // Place a short note in the up lane regularly.
    public IEnumerator PlaceNotesInUpLane() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                placeNoteInLane(Rhythm_InputDirection.UP);
                yield return new WaitForSeconds(scrollSpeedWaitTime);
            }
            else yield return null;
        }
    }

    
    // Place a short note in the right lane regularly.
    public IEnumerator PlaceNotesInRightLane() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                placeNoteInLane(Rhythm_InputDirection.RIGHT);
                yield return new WaitForSeconds(scrollSpeedWaitTime);
            }
            else yield return null;
        }
    }


    /* 
        Randomly place a note into the given lane.
        @param lane - lane in which the note shall be placed
    */
    private void placeNoteInLane(Rhythm_InputDirection lane) {
        int rng = Random.Range(0, 2);
        if (rng == 0) {
            switch(lane) {
                case Rhythm_InputDirection.LEFT: 
                    placeNote(shortNotePrefab, leftLaneTransform, noteLeftIndex++, noteLeftSprite);
                    break;
                case Rhythm_InputDirection.DOWN:
                    placeNote(shortNotePrefab, downLaneTransform, noteDownIndex++, noteDownSprite);
                    break;
                case Rhythm_InputDirection.UP:
                    placeNote(shortNotePrefab, upLaneTransform, noteUpIndex++, noteUpSprite);
                    break;
                case Rhythm_InputDirection.RIGHT:
                    placeNote(shortNotePrefab, rightLaneTransform, noteRightIndex++, noteRightSprite);
                    break;
            }
        }        
    }

    /*
        Place a given note prefab into the chart area.
        @param notePrefab - note prefab
        @param lane - the lane to place the note in
        @param currentNoteIndex - index of note that is placed
        @param noteSprite - texture of the note
    */
    private void placeNote(GameObject notePrefab, Transform lane, int currentNoteIndex, Sprite noteSprite) {
        GameObject newNote = Instantiate(notePrefab, lane);
        newNote.GetComponent<Rhythm_Game_Note>().initialize(currentNoteIndex, noteSprite, rngNoteLength);
    } 
}
