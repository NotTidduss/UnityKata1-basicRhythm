using UnityEngine;
using UnityEngine.SceneManagement;

public class Rhythm_Init : MonoBehaviour
{
    void Awake() {
        initializePlayerPrefs();
        SceneManager.LoadScene("1x_Rhythm_Main");
    }

    private void initializePlayerPrefs() {
        // FLOAT rhythm_scrollSpeed - the speed at which notes are scrolling down the chart
        if (PlayerPrefs.GetFloat("rhythm_scrollSpeed") == 0)
            PlayerPrefs.SetFloat("rhythm_scrollSpeed", 15f);

        // INT rhythm_inputKeyLeft - left key button, default = LeftArrow
        if (PlayerPrefs.GetInt("rhythm_inputKeyLeft") == 0)
            PlayerPrefs.SetInt("rhythm_inputKeyLeft", (int) KeyCode.LeftArrow);

        // INT rhythm_inputKeyDown - down key button, default = DownArrow
        if (PlayerPrefs.GetInt("rhythm_inputKeyDown") == 0)
            PlayerPrefs.SetInt("rhythm_inputKeyDown", (int) KeyCode.DownArrow);

        // INT rhythm_inputKeyUp - up key button, default = UpArrow
        if (PlayerPrefs.GetInt("rhythm_inputKeyUp") == 0)
            PlayerPrefs.SetInt("rhythm_inputKeyUp", (int) KeyCode.UpArrow);

        // INT rhythm_inputKeyRight - right key button, default = RightArrow
        if (PlayerPrefs.GetInt("rhythm_inputKeyRight") == 0)
            PlayerPrefs.SetInt("rhythm_inputKeyRight", (int) KeyCode.RightArrow);

        // INT rhythm_inputKeyQuickRestart - button to quickly restart a map, default = R
        if (PlayerPrefs.GetInt("rhythm_inputKeyQuickRestart") == 0)
            PlayerPrefs.SetInt("rhythm_inputKeyQuickRestart", (int) KeyCode.R);

        // INT rhythm_inputKeyPause - button to pause the map, default = ESCAPE
        if (PlayerPrefs.GetInt("rhythm_inputKeyPause") == 0)
            PlayerPrefs.SetInt("rhythm_inputKeyPause", (int) KeyCode.Escape);

        // INT rhythm_mapScore - the score of the currently played map. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_mapScore", 0);

        // INT rhythm_paused - an indicator that shows if the game is currently paused. If odd, it's paused; if even, it's not. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_paused", 0);

        // INT rhythm_gameOver - an indicator that shows if health dropped below 0. If so, this will be 1. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_gameOver", 0);

        // INT rhythm_perfectHitCount - the current amount of perfect-hit notes. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_perfectHitCount", 0);

        // INT rhythm_goodHitCount - the current amount of good-hit notes. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_goodHitCount", 0);

        // INT rhythm_fineHitCount - the current amount of fine-hit notes. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_fineHitCount", 0);

        // INT rhythm_missCount - the current amount of missed notes. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_missCount", 0);

        // INT rhythm_fallenNoteCountForTiming - the amount of notes that fell too low, ready to be used by the timing master. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_fallenNoteCountForTiming", 0);

        // INT rhythm_fallenNoteCountForHealth - the amount of notes that fell too low, ready to be used by the health master. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_fallenNoteCountForHealth", 0);

        // STRING rhythm_lastNoteHitTimingIndicator - the timing in which the last not was hit. Either PERFECT, GOOD, FINE, MISS or "". Used for displaying the right timing.
        PlayerPrefs.SetString("rhythm_lastNoteHitTimingIndicator", "");
    }
}
