using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_JudgementWindow : MonoBehaviour
{
    //* private vars
    // reference to Image
    private Image image;
    // reference to lane transform
    private Transform laneTransform;
    // judgement values sote the points awarded for hitting certain timings.
    private int judgementValuePerfect, judgementValueGood, judgementValueFine, judgementValueMiss;
    // index of currently tracked note
    private int currentNoteIndex = 0;


    public void initialize() {
        image = GetComponent<Image>();
        laneTransform = transform.parent.parent;

        judgementValuePerfect = PlayerPrefs.GetInt("rhythm_judgementValue_perfect");
        judgementValueGood = PlayerPrefs.GetInt("rhythm_judgementValue_good");
        judgementValueFine = PlayerPrefs.GetInt("rhythm_judgementValue_fine");
        judgementValueMiss = PlayerPrefs.GetInt("rhythm_judgementValue_miss");
    }

    public void judge() {
        // show judgement window Image
        image.enabled = true;

        // is there a note?
        GameObject currentNote = findNoteByIndex(currentNoteIndex);
        if (currentNote != null) {
            float currentNotePosition = currentNote.transform.localPosition.y;
            if (currentNotePosition < 125) {
                // is it an early MISS?
                if (currentNotePosition > 100) {
                    updateScore(judgementValueMiss);
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "MISS");
                } 
                // is it in the upper FINE area?
                else if (currentNotePosition > 60) {
                    updateScore(judgementValueFine);
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "FINE");
                }
                // is it in the upper GOOD area?
                else if (currentNotePosition > 20) {
                    updateScore(judgementValueGood);
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "GOOD");
                }
                // is it in the PERFECT area?
                else if (currentNotePosition > -20) {
                    updateScore(judgementValuePerfect);
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "PERFECT");
                }
                // is it in the lower GOOD area?
                else if (currentNotePosition > -60) {
                    updateScore(judgementValueGood);
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "GOOD");
                }
                // else it must be in the lower FINE area?
                else {
                    updateScore(judgementValueFine);
                    PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "FINE");
                }
                
                // if a note was judged, destroy it, and increase index of currently tracked note
                Destroy(currentNote);
                currentNoteIndex++;
            }
        }
    }

    public void clear() {
        // hide judgement window image
        image.enabled = false;
    }


    private GameObject findNoteByIndex(int id) {
        List<Rhythm_ShortNote> notes = new List<Rhythm_ShortNote>(laneTransform.GetComponentsInChildren<Rhythm_ShortNote>());
        foreach(Rhythm_ShortNote note in notes) {
            if (note.getIndex() == currentNoteIndex) {
                return note.gameObject;
            }
        }
        return null;
    }

    private void updateScore(int points) => PlayerPrefs.SetInt("rhythm_mapScore", PlayerPrefs.GetInt("rhythm_mapScore") + points);
}
