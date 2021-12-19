using System;
using System.Collections;
using UnityEngine;

public class Rhythm_Game_Master : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Rhythm_System sys;
    [SerializeField] private Rhythm_Game_UI ui;
    [SerializeField] private Rhythm_Game_HealthMaster healthMaster;
    [SerializeField] private Rhythm_TransitionMaster transitionMaster;
    [SerializeField] private Rhythm_Game_JudgementWindow judgementWindowLeft;
    [SerializeField] private Rhythm_Game_JudgementWindow judgementWindowDown;
    [SerializeField] private Rhythm_Game_JudgementWindow judgementWindowUp;
    [SerializeField] private Rhythm_Game_JudgementWindow judgementWindowRight;
    [SerializeField] private Rhythm_Game_Chart chart;
    [SerializeField] private Rhythm_Game_TimingIndicatorMaster timingIndicator;


    void Start() {
        // initialize PlayerPrefs
        PlayerPrefs.SetInt("rhythm_paused", 0);
        PlayerPrefs.SetInt("rhythm_perfectHitCount", 0);
        PlayerPrefs.SetInt("rhythm_goodHitCount", 0);
        PlayerPrefs.SetInt("rhythm_fineHitCount", 0);
        PlayerPrefs.SetInt("rhythm_missCount", 0);

        // initialize Scene References
        ui.initialize(sys);
        healthMaster.initialize(sys);
        transitionMaster.initialize(sys);
        judgementWindowLeft.initialize(this, sys);
        judgementWindowDown.initialize(this, sys);
        judgementWindowUp.initialize(this, sys);
        judgementWindowRight.initialize(this, sys);
        chart.initialize(sys);
        timingIndicator.initialize(sys);

        // start coroutines
        StartCoroutine("CheckForButtonPress");
        StartCoroutine("CheckForButtonRelease");
        StartCoroutine("HandleNoteHit");
        StartCoroutine("CheckForGameOver");
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

    IEnumerator HandleNoteHit() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_fallenNoteCountForTiming") > 0) {
                timingIndicator.spawnMissIndicator();
                ui.handleJudgement(Rhythm_Judgement.MISS, 0);
                PlayerPrefs.SetInt("rhythm_fallenNoteCountForTiming", PlayerPrefs.GetInt("rhythm_fallenNoteCountForTiming") - 1);
            }
            yield return null;
        }
    }

    // If health reaches 0, transition into result scene.
    IEnumerator CheckForGameOver() {
        while(true) {
            if (PlayerPrefs.GetInt("rhythm_gameOver") == 1) switchScene(sys.resultSceneName);

            yield return null;
        }
    }


    public void restartMap() => sys.loadGameScene();

    public void togglePaused() {
        ui.togglePauseMenu();
        PlayerPrefs.SetInt("rhythm_paused", PlayerPrefs.GetInt("rhythm_paused") + 1);
    }
    
    public void switchScene(string sceneName) => transitionMaster.transitionToNextScene(sceneName);

#region judgement communication
    public void communicateJudgementToTimingIndicatorMaster(Rhythm_Judgement judgement) => timingIndicator.spawnIndicatorByJudgement(judgement);
    public void communicateJudgementToHealthMaster(Rhythm_Judgement judgement) => healthMaster.handleJudgement(judgement);
    public void communicateJudgementToUI(Rhythm_Judgement judgement, int judgementScore) => ui.handleJudgement(judgement, judgementScore);
#endregion
}
