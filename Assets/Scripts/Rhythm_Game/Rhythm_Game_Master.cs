using System.Collections;
using UnityEngine;

public class Rhythm_Game_Master : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Rhythm_System sys;
    [SerializeField] private Rhythm_Game_UI ui;
    [SerializeField] private Rhythm_Chart chart;
    [SerializeField] private Rhythm_TimingIndicator timingIndicator;
    [SerializeField] private Transform noteInputTransformLeft;
    [SerializeField] private Transform noteInputTransformDown;
    [SerializeField] private Transform noteInputTransformUp;
    [SerializeField] private Transform noteInputTransformRight;

    [Header("Prefab References")]
    [SerializeField] private GameObject leftJudgementWindowPrefabReference;
    [SerializeField] private GameObject downJudgementWindowPrefabReference;
    [SerializeField] private GameObject upJudgementWindowPrefabReference;
    [SerializeField] private GameObject rightJudgementWindowPrefabReference;

    private GameObject currentJudgementWindowLeft;
    private GameObject currentJudgementWindowDown;
    private GameObject currentJudgementWindowUp;
    private GameObject currentJudgementWindowRight;


    void Start() {
        ui.initialize();
        chart.initialize();
        timingIndicator.initialize(sys);

        StartCoroutine("CheckForButtonPress");
        StartCoroutine("CheckForButtonRelease");
    }


    IEnumerator CheckForButtonPress() {
        while(true) {
            if (Input.GetKeyDown(sys.inputLeft)) judgeInput(Rhythm_InputDirection.LEFT);
            if (Input.GetKeyDown(sys.inputDown)) judgeInput(Rhythm_InputDirection.DOWN);
            if (Input.GetKeyDown(sys.inputUp)) judgeInput(Rhythm_InputDirection.UP);
            if (Input.GetKeyDown(sys.inputRight)) judgeInput(Rhythm_InputDirection.RIGHT);

            yield return null;
        }
    }

    IEnumerator CheckForButtonRelease() {
        while(true) {
            if (Input.GetKeyUp(sys.inputLeft)) Destroy(currentJudgementWindowLeft); 
            if (Input.GetKeyUp(sys.inputDown)) Destroy(currentJudgementWindowDown); 
            if (Input.GetKeyUp(sys.inputUp)) Destroy(currentJudgementWindowUp); 
            if (Input.GetKeyUp(sys.inputRight)) Destroy(currentJudgementWindowRight); 

            yield return null;
        }
    }


    /*
        Sets current judgement window object to a prefab reference and instantiates a judgement window.

        @param input - The direction (LEFT, DOWN, UP or RIGHT) pressed by the player
    */
    private void judgeInput(Rhythm_InputDirection input) {
        switch (input) {
            case Rhythm_InputDirection.LEFT: currentJudgementWindowLeft = spawnJudgementWindow(leftJudgementWindowPrefabReference, noteInputTransformLeft); break;
            case Rhythm_InputDirection.DOWN: currentJudgementWindowDown = spawnJudgementWindow(downJudgementWindowPrefabReference, noteInputTransformDown); break;
            case Rhythm_InputDirection.UP: currentJudgementWindowUp = spawnJudgementWindow(upJudgementWindowPrefabReference, noteInputTransformUp); break;
            case Rhythm_InputDirection.RIGHT: currentJudgementWindowRight = spawnJudgementWindow(rightJudgementWindowPrefabReference, noteInputTransformRight); break;
        }
    }
    private GameObject spawnJudgementWindow(GameObject judgementWindow, Transform judgementParent) => Instantiate(judgementWindow, judgementParent);
}
