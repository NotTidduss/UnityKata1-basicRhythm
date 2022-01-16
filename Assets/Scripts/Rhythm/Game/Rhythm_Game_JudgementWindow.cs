using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Game_JudgementWindow : MonoBehaviour
{
    //* private vars
    private Rhythm_Game_Master master;                                                              // reference to Master script.
    private Image image;                                                                            // reference to GameObject's Image
    private Sprite noteMissedSprite;                                                                // sprite taken from system, applied to missed long notes
    private Transform laneTransform;                                                                // reference to GameObject's parent transform = lane
    private int judgementValuePerfect, judgementValueGood, judgementValueFine, judgementValueMiss;  // judgement values sote the points awarded for hitting certain timings.
    private int currentNoteIndex = 0;                                                               // index of currently tracked note.
    private float currentNotePositionY;                                                             // y position of currently tracked note.
    private float judgementAreaEntryPointY;                                                         // y position of the judgement border, everything below shall be judged.
    private float judgementAreaUpperFineEntryPointY;                                                // y position of the upper fine border, everything below shall is at least FINE.
    private float judgementAreaUpperGoodEntryPointY;                                                // y position of the upper good border, everything below shall is at least GOOD.
    private float judgementAreaUpperPerfectEntryPointY;                                             // y position of the upper perfect border, below is just PERFECT.
    private float judgementAreaLowerFineExitPointY;                                                 // y position of the lower fine border, everything above is at least FINE.
    private float judgementAreaLowerGoodExitPointY;                                                 // y position of the lower good border, everything above is at least GOOD.
    private float judgementAreaLowerPerfectExitPointY;                                              // y position of the lower perfect border, everything above is just PERFECT.
    Rhythm_Game_Note currentNote = null;                                                            // placeholder for current note.


    public void initialize(Rhythm_Game_Master m, Rhythm_System sys) {
        // initialize master reference
        master = m;

        // initialize GameObject related vars
        image = GetComponent<Image>();
        noteMissedSprite = sys.spriteNoteMissed;
        laneTransform = transform.parent.parent;

        // initialize PlayerPrefs related vars
        judgementValuePerfect = sys.judgementScoreValuePerfect;
        judgementValueGood = sys.judgementScoreValueGood;
        judgementValueFine = sys.judgementScoreValueFine;
        judgementValueMiss = sys.judgementScoreValueMiss;

        // initialize judgement window related preferences
        judgementAreaEntryPointY = sys.judgementWindowEntryPointY;
        judgementAreaUpperFineEntryPointY = sys.judgementWindowUpperFineEntryPointY;
        judgementAreaUpperGoodEntryPointY = sys.judgementWindowUpperGoodEntryPointY;
        judgementAreaUpperPerfectEntryPointY = sys.judgementWindowUpperPerfectEntryPointY;
        judgementAreaLowerFineExitPointY = sys.judgementWindowLowerFineExitPointY;
        judgementAreaLowerGoodExitPointY = sys.judgementWindowLowerGoodExitPointY;
        judgementAreaLowerPerfectExitPointY = sys.judgementWindowLowerPerfectExitPointY;
    }


    public void judge() {
        // show judgement window Image
        image.enabled = true;

        // is there a note?
        currentNote = findCurrentNote();
        if (currentNote != null) {
            // if there is, get its position
            currentNotePositionY = currentNote.transform.localPosition.y;

            // if the note's position is within judgement boundaries, do some judging
            if (currentNotePositionY < judgementAreaEntryPointY) {
                Rhythm_Judgement judgement = getJudgement(currentNotePositionY);
                int judgementScore = getJudgementScore(judgement);

                // communicate judgement to roles who need it
                master.communicateJudgementToTimingIndicatorMaster(judgement);
                master.communicateJudgementToHealthMaster(judgement);
                master.communicateJudgementToUI(judgement, judgementScore);

                // handle notes
                switch (currentNote.getNoteType()) {
                    case Rhythm_Note_Type.SHORT:
                        // if a note was judged, destroy it, and increase index of currently tracked note
                        Destroy(currentNote.gameObject);
                        currentNoteIndex++;
                        break;
                    case Rhythm_Note_Type.LONG:
                        // if note was missed, darken it, and increase index to disable further interactions
                        if (currentNotePositionY > judgementAreaUpperFineEntryPointY) {
                            currentNote.changeSprite(noteMissedSprite);
                            currentNote.setMissed();
                            currentNoteIndex++;
                        }
                        // if it was hit, initialize holding sequence
                        else {
                            currentNote.hold();
                        }
                        break;
                }
            }
        }
    }

    public void clear() {
        // hide judgement window Image
        image.enabled = false;

        // let go of a note if it was a long note
        if (currentNote != null && currentNote.getIndex() == currentNoteIndex && currentNotePositionY < judgementAreaEntryPointY) {
            currentNote.changeSprite(noteMissedSprite);
            currentNote.release();
            currentNoteIndex++;
        }
    }


    // Return the first note in the lane.
    private Rhythm_Game_Note findCurrentNote() {
        List<Rhythm_Game_Note> notesInLane = new List<Rhythm_Game_Note>(laneTransform.GetComponentsInChildren<Rhythm_Game_Note>());
        if (notesInLane.Count == 0)
            return null;
        return notesInLane[0];
    }

    // Return judgement based on given position.
    private Rhythm_Judgement getJudgement(float currentNotePosition) {
        // is it an early MISS?
        if (currentNotePosition > judgementAreaUpperFineEntryPointY) {
            return Rhythm_Judgement.MISS;
        } 
        // is it in the upper FINE area?
        else if (currentNotePosition > judgementAreaUpperGoodEntryPointY) {
            return Rhythm_Judgement.FINE;
        }
        // is it in the upper GOOD area?
        else if (currentNotePosition > judgementAreaUpperPerfectEntryPointY) {
            return Rhythm_Judgement.GOOD;
        }
        // is it in the PERFECT area?
        else if (currentNotePosition > judgementAreaLowerPerfectExitPointY) {
            return Rhythm_Judgement.PERFECT;
        }
        // is it in the lower GOOD area?
        else if (currentNotePosition > judgementAreaLowerGoodExitPointY) {
            return Rhythm_Judgement.GOOD;
        }
        // is it in the lower FINE area?
        else if (currentNotePosition > judgementAreaLowerFineExitPointY) {
            return Rhythm_Judgement.FINE;
        }
        // else it's a complete miss.
        else {
            return Rhythm_Judgement.MISS;
        }
    }

    // Return a score that fits the given judgement. Return miss value by default.
    private int getJudgementScore(Rhythm_Judgement judgement) {
        switch (judgement) {
            case Rhythm_Judgement.PERFECT: return judgementValuePerfect;
            case Rhythm_Judgement.GOOD: return judgementValueGood;
            case Rhythm_Judgement.FINE: return judgementValueFine;
            default: return judgementValueMiss;
        }
    }
}
