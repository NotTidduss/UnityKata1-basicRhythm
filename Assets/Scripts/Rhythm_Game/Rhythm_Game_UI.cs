using System.Collections;
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
    [SerializeField] private Text perfectCountText;
    [SerializeField] private Text goodCountText;
    [SerializeField] private Text fineCountText;
    [SerializeField] private Text missCountText;

    // private vars
    private int perfectCount, goodCount, fineCount, missCount = 0;


    public void initialize() {
        pauseMenu.SetActive(false);

        StartCoroutine("UpdateScoreText");
    } 


    IEnumerator UpdateScoreText() {
        while(true) {
            setText(scoreText, PlayerPrefs.GetInt("rhythm_mapScore").ToString());
            yield return null;
        }
    }


    public void updateIndicatorCounts() {
        switch (PlayerPrefs.GetString("rhythm_lastNoteHitTiming")) {
            case "PERFECT": setText(perfectCountText, "" + ++perfectCount); break;
            case "GOOD": setText(goodCountText, "" + ++goodCount); break;
            case "FINE": setText(fineCountText, "" + ++fineCount); break;
            case "MISS": setText(missCountText, "" + ++missCount); break;
        }
    }

    public void togglePauseMenu() => pauseMenu.SetActive(!pauseMenu.activeInHierarchy);


    private void setText(Text t, string s) => t.text = s;


    #region Button Functions
    public void onResumeButtonPress() => master.togglePaused();
    public void onRetryButtonPress() => master.restartMap();
    public void onExitButtonPress() => master.exitToMainMenu();
    #endregion
}
