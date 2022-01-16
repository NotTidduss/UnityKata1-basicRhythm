using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Game_UI : MonoBehaviour
{
    [Header("Master Reference")]
    [SerializeField] private Rhythm_Game_Master master;


    [Header("Menu References")]
    [SerializeField] private GameObject pauseMenu;


    [Header("Text References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text comboText;
    [SerializeField] private Text perfectCountText;
    [SerializeField] private Text goodCountText;
    [SerializeField] private Text fineCountText;
    [SerializeField] private Text missCountText;


    //* private vars
    private Rhythm_System sys;                                          // reference to the System class
    private int mapScore = 0;                                           // current score
    private int combo = 0;                                              // current combo


    public void initialize(Rhythm_System sysRef) {
        sys = sysRef;

        pauseMenu.SetActive(false);
    } 


    /*
        Update indicator counts, combo and score based on given judgement.
        @param judgement - indicates how well the note was hit.
        @param judgementScore - update value for the current score.
    */
    public void handleJudgement(Rhythm_Judgement judgement, int judgementScore) {
        updateIndicatorCountsByJudgement(judgement);
        updateComboByJudgement(judgement);
        mapScore += judgementScore;
        updateScoreText();
    }

    public void togglePauseMenu() => pauseMenu.SetActive(!pauseMenu.activeInHierarchy);


    private void updateIndicatorCountsByJudgement(Rhythm_Judgement judgement) {
        switch (judgement) {
            case Rhythm_Judgement.PERFECT: updatePlayerPrefAndText("rhythm_perfectHitCount", perfectCountText); break; 
            case Rhythm_Judgement.GOOD: updatePlayerPrefAndText("rhythm_goodHitCount", goodCountText); break;
            case Rhythm_Judgement.FINE: updatePlayerPrefAndText("rhythm_fineHitCount", fineCountText); break;
            case Rhythm_Judgement.MISS: updatePlayerPrefAndText("rhythm_missCount", missCountText); break;
        }
    }

    private void updatePlayerPrefAndText(string playerPrefKey, Text t) {
        PlayerPrefs.SetInt(playerPrefKey, PlayerPrefs.GetInt(playerPrefKey) + 1);
        setText(t, "" + PlayerPrefs.GetInt(playerPrefKey));
    }

    private void updateComboByJudgement(Rhythm_Judgement judgement) {
        switch (judgement) {
            case Rhythm_Judgement.MISS:
                setText(comboText, ""); 
                combo = 0;
                break;
            default:
                setText(comboText, "" + ++combo);
                break;
        }
    }

    private void updateScoreText() => setText(scoreText, mapScore.ToString());
    private void setText(Text t, string s) => t.text = s;


#region Buttons
    public void onResumeButtonPress() => master.togglePaused();
    public void onRetryButtonPress() => master.switchScene(sys.gameSceneName);
    public void onExitButtonPress() => master.switchScene(sys.resultSceneName);
#endregion
}
