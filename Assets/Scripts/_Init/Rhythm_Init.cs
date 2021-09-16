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

        // INT rhythm_judgementValue_perfect - the point increase for a perfect hit
        if (PlayerPrefs.GetInt("rhythm_judgementValue_perfect") == 0) 
            PlayerPrefs.SetInt("rhythm_judgementValue_perfect", 100);

        // INT rhythm_judgementValue_good - the point increase for a good hit
        if (PlayerPrefs.GetInt("rhythm_judgementValue_good") == 0) 
            PlayerPrefs.SetInt("rhythm_judgementValue_good", 60);

        // INT rhythm_judgementValue_fine - the point increase for a fine hit
        if (PlayerPrefs.GetInt("rhythm_judgementValue_fine") == 0) 
            PlayerPrefs.SetInt("rhythm_judgementValue_fine", 20);

        // INT rhythm_judgementValue_miss - the point penalty for a early missed hit
        if (PlayerPrefs.GetInt("rhythm_judgementValue_miss") == 0) 
            PlayerPrefs.SetInt("rhythm_judgementValue_miss", -20);

        // INT rhythm_judgementValue_outOfBounds - the point penalty for a late miss hit
        if (PlayerPrefs.GetInt("rhythm_judgementValue_outOfBounds") == 0) 
            PlayerPrefs.SetInt("rhythm_judgementValue_outOfBounds", -50);

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

        // INT rhythm_mapScore - the score of the currently played map. Initial value should be 0.
        PlayerPrefs.SetInt("rhythm_mapScore", 0);
    }
}
