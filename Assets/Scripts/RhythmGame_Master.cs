using System.Collections;
using UnityEngine;

public class RhythmGame_Master : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private RhythmGame_Settings settings;
    [SerializeField] private RhythmGame_Chart chart;
    [SerializeField] private Transform noteInputTransformLeft;
    [SerializeField] private Transform noteInputTransformDown;
    [SerializeField] private Transform noteInputTransformUp;
    [SerializeField] private Transform noteInputTransformRight;

    [Header("Prefab References")]
    [SerializeField] private GameObject judgementWindowPrefabReferenceLeft;
    [SerializeField] private GameObject judgementWindowPrefabReferenceDown;
    [SerializeField] private GameObject judgementWindowPrefabReferenceUp;
    [SerializeField] private GameObject judgementWindowPrefabReferenceRight;

    private GameObject currentJudgementWindowLeft;
    private GameObject currentJudgementWindowDown;
    private GameObject currentJudgementWindowUp;
    private GameObject currentJudgementWindowRight;

    void Start() {
        StartCoroutine("PlaceRandomNoteViaChart");
        StartCoroutine("CheckForButtonPress");
        StartCoroutine("CheckForButtonRelease");
    }

    IEnumerator PlaceRandomNoteViaChart() {
        while(true) {
            chart.placeRandomNote();

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CheckForButtonPress() {
        while(true) {
            if (Input.GetKeyDown(settings.inputLeft)) judgeInput(RhythmGame_InputDirection.LEFT);
            if (Input.GetKeyDown(settings.inputDown)) judgeInput(RhythmGame_InputDirection.DOWN);
            if (Input.GetKeyDown(settings.inputUp)) judgeInput(RhythmGame_InputDirection.UP);
            if (Input.GetKeyDown(settings.inputRight)) judgeInput(RhythmGame_InputDirection.RIGHT);

            yield return null;
        }
    }

    IEnumerator CheckForButtonRelease() {
        while(true) {
            if (Input.GetKeyUp(settings.inputLeft)) Destroy(currentJudgementWindowLeft); 
            if (Input.GetKeyUp(settings.inputDown)) Destroy(currentJudgementWindowDown); 
            if (Input.GetKeyUp(settings.inputUp)) Destroy(currentJudgementWindowUp); 
            if (Input.GetKeyUp(settings.inputRight)) Destroy(currentJudgementWindowRight); 

            yield return null;
        }
    }

    /*
        Sets current judgement window object to a prefab reference and instantiates a judgement window.

        @param input: The direction (LEFT, DOWN, UP or RIGHT) pressed by the player
    */
    private void judgeInput(RhythmGame_InputDirection input) {
        switch (input) {
            case RhythmGame_InputDirection.LEFT: currentJudgementWindowLeft = spawnJudgementWindow(judgementWindowPrefabReferenceLeft, noteInputTransformLeft); break;
            case RhythmGame_InputDirection.DOWN: currentJudgementWindowDown = spawnJudgementWindow(judgementWindowPrefabReferenceDown, noteInputTransformDown); break;
            case RhythmGame_InputDirection.UP: currentJudgementWindowUp = spawnJudgementWindow(judgementWindowPrefabReferenceUp, noteInputTransformUp); break;
            case RhythmGame_InputDirection.RIGHT: currentJudgementWindowRight = spawnJudgementWindow(judgementWindowPrefabReferenceRight, noteInputTransformRight); break;
        }
    }
    private GameObject spawnJudgementWindow(GameObject judgementWindow, Transform judgementParent) => Instantiate(judgementWindow, judgementParent);
}
