using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rhythm_System : MonoBehaviour
{
    [Header("PlayerPrefsMaster")]
    [SerializeField] private Rhythm_PlayerPrefsMaster playerPrefsMaster;


    [Header("Scene Names")]
    public string mainSceneName = "1x_Rhythm_Main";
    public string gameSceneName = "2x_Rhythm_Game";
    public string resultSceneName = "3x_Rhythm_Result";


    [Header("Key Bindings")]
    public KeyCode inputLeft;
    public KeyCode inputDown;
    public KeyCode inputUp;
    public KeyCode inputRight;
    public KeyCode inputQuickRestart;
    public KeyCode inputPause;
    public List<KeyCode> currentKeybindings;


    [Header("Note Sprites")]
    public Sprite spriteNoteLeft;
    public Sprite spriteNoteDown;
    public Sprite spriteNoteUp;
    public Sprite spriteNoteRight;
    public Sprite spriteNoteMissed;


    [Header("Timing Indicator related Preferences")]
    public Sprite spriteIndicatorPerfect;
    public Sprite spriteIndicatorGood;
    public Sprite spriteIndicatorFine;
    public Sprite spriteIndicatorMiss;
    public GameObject timingIndicatorPrefab;
    public float timingIndicatorFadeTimeInFrames = 400;


    [Header("Game Backend Data")]
    public string defaultKeyConfigText = "Press new key";
    public int optionsMenuSlideInTimeInFrames = 150;
    public float transitionDurationInFrames = 500;


    [Header("Judgement Window related Preferences")]
    public int judgementScoreValuePerfect = 100;
    public int judgementScoreValueGood = 60;
    public int judgementScoreValueFine = 20;
    public int judgementScoreValueMiss = 0;
    public float judgementWindowEntryPointY = 125;
    public float judgementWindowUpperFineEntryPointY = 100;
    public float judgementWindowUpperGoodEntryPointY = 60;
    public float judgementWindowUpperPerfectEntryPointY = 20;
    public float judgementWindowLowerFineExitPointY = -100;
    public float judgementWindowLowerGoodExitPointY = -60;
    public float judgementWindowLowerPerfectExitPointY = -20;


    [Header("Health related Preferences")]
    public float healingValuePerfect = 3.3f;
    public float healingValueGood = 0;
    public float damageValueFine = 2.2f;
    public float damageValueMiss = 6.6f;


    public void initialize() {
        resetPlayerPrefs();
        updateKeybindings();
    } 


    // Use PlayerPrefsMaster to reset PlayerPrefs.
    public void resetPlayerPrefs() => playerPrefsMaster.resetPlayerPrefs();


    // Fetch keybindings from PlayerPrefs.
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
    public void loadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    public void loadGameScene() => loadScene(gameSceneName);
    public void loadMainScene() => loadScene(mainSceneName);
    public void loadResultScene() => loadScene(resultSceneName);


    // Fill a list with currently assigned keybindings, used in input configuration.
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

// used for configuring 4K keys
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

public enum Rhythm_Note_Type {
    SHORT,
    LONG
}

public enum Rhythm_Judgement {
    MISS,
    FINE,
    GOOD,
    PERFECT
}