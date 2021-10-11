using System.Collections;
using UnityEngine;

public class Rhythm_Game_Master : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Rhythm_System sys;
    [SerializeField] private Rhythm_Game_UI ui;
    [SerializeField] private Rhythm_JudgementWindow judgementWindowLeft;
    [SerializeField] private Rhythm_JudgementWindow judgementWindowDown;
    [SerializeField] private Rhythm_JudgementWindow judgementWindowUp;
    [SerializeField] private Rhythm_JudgementWindow judgementWindowRight;
    [SerializeField] private Rhythm_Chart chart;
    [SerializeField] private Rhythm_TimingIndicatorMaster timingIndicator;


    void Start() {
        // initialize PlayerPrefs
        PlayerPrefs.SetInt("rhythm_paused", 0);

        // initialize Scene References
        ui.initialize();
        judgementWindowLeft.initialize();
        judgementWindowDown.initialize();
        judgementWindowUp.initialize();
        judgementWindowRight.initialize();
        chart.initialize();
        timingIndicator.initialize(sys);

        // start coroutines
        StartCoroutine("CheckForButtonPress");
        StartCoroutine("CheckForButtonRelease");
        StartCoroutine("HandleTimingIndicator");
    }


    IEnumerator CheckForButtonPress() {
        while(true) {
            if (Input.GetKeyDown(sys.inputLeft)) judgementWindowLeft.judge();
            if (Input.GetKeyDown(sys.inputDown)) judgementWindowDown.judge();
            if (Input.GetKeyDown(sys.inputUp)) judgementWindowUp.judge();
            if (Input.GetKeyDown(sys.inputRight)) judgementWindowRight.judge();
            if (Input.GetKeyDown(sys.inputQuickRestart)) restartMap();
            if (Input.GetKeyDown(sys.inputPause)) togglePaused();

            yield return null;
        }
    }

    IEnumerator CheckForButtonRelease() {
        while(true) {
            if (Input.GetKeyUp(sys.inputLeft)) judgementWindowLeft.clear(); 
            if (Input.GetKeyUp(sys.inputDown)) judgementWindowDown.clear(); 
            if (Input.GetKeyUp(sys.inputUp)) judgementWindowUp.clear(); 
            if (Input.GetKeyUp(sys.inputRight)) judgementWindowRight.clear(); 
            yield return null;
        }
    }

    IEnumerator HandleTimingIndicator() {
        while(true) {
            if (PlayerPrefs.GetString("rhythm_lastNoteHitTiming") != "") {
                timingIndicator.spawnIndicator();
                ui.updateIndicatorCounts();
                PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "");
            }
            yield return null;
        }
    }


    public void restartMap() => sys.loadGameScene();
    public void exitToMainMenu() => sys.loadMainScene();

    public void togglePaused() {
        ui.togglePauseMenu();
        PlayerPrefs.SetInt("rhythm_paused", PlayerPrefs.GetInt("rhythm_paused") + 1);
    }
}
