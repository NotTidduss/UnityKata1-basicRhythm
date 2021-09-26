using UnityEngine;
using UnityEngine.SceneManagement;

public class Rhythm_System : MonoBehaviour
{
    [Header("Game BackEnd Data")]
    public string gameSceneName = "2x_Rhythm_Game";
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


    void Awake() => updateKeybindings();


    public void updateKeybindings() {
        inputLeft = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyLeft");
        inputDown = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyDown");
        inputUp = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyUp");
        inputRight = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyRight");
        inputQuickRestart = (KeyCode)PlayerPrefs.GetInt("rhythm_inputKeyQuickRestart");
    }

    public void loadGameScene() => SceneManager.LoadScene(gameSceneName);
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

public enum Rhythm_JudgementType {
    PERFECT,
    GOOD,
    FINE,
    MISS
}