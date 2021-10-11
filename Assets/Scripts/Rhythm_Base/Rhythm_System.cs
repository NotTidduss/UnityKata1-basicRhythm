using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rhythm_System : MonoBehaviour
{
    [Header("Scene Names")]
    public string mainSceneName = "1x_Rhythm_Main";
    public string gameSceneName = "2x_Rhythm_Game";
    public string resultSceneName = "3x_Rhythm_Result";

    [Header("Game Backend Data")]
    public string defaultKeyConfigText = "Press new key";
    public int optionsMenuSlideInTimeInFrames = 150;

    [Header("Timing Indicator Preferences")]
    public Sprite spriteIndicatorPerfect;
    public Sprite spriteIndicatorGood;
    public Sprite spriteIndicatorFine;
    public Sprite spriteIndicatorMiss;
    public GameObject timingIndicatorPrefab;
    public float timingIndicatorFadeTimeInFrames = 400;

    [Header("Key Bindings")]
    public KeyCode inputLeft;
    public KeyCode inputDown;
    public KeyCode inputUp;
    public KeyCode inputRight;
    public KeyCode inputQuickRestart;
    public KeyCode inputPause;
    public List<KeyCode> currentKeybindings;


    void Awake() => updateKeybindings();


    /*
        Fetch keybindings from PlayerPrefs.
    */
    public void updateKeybindings() {
        inputLeft = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyLeft");
        inputDown = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyDown");
        inputUp = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyUp");
        inputRight = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyRight");
        inputQuickRestart = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyQuickRestart");
        inputPause = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyPause");

        resetKeybindingsList();
    }

    // SceneManagement
    public void loadGameScene() => SceneManager.LoadScene(gameSceneName);
    public void loadMainScene() => SceneManager.LoadScene(mainSceneName);
    public void loadResultScene() => SceneManager.LoadScene(resultSceneName);


    /*
        Fill a list with currently assigned keybindings, used in input configuration.
    */
    private void resetKeybindingsList() {
        currentKeybindings = new List<KeyCode>();
        currentKeybindings.Add(inputLeft);
        currentKeybindings.Add(inputDown);
        currentKeybindings.Add(inputUp);
        currentKeybindings.Add(inputRight);
        currentKeybindings.Add(inputQuickRestart);
        currentKeybindings.Add(inputPause);
    }
}

public enum Rhythm_InputKeysConfigSteps {
    INPUT_LEFT,
    INPUT_DOWN,
    INPUT_UP,
    INPUT_RIGHT,
    FINISH
}

public enum Rhythm_InputDirection {
    LEFT,
    DOWN,
    UP,
    RIGHT
}