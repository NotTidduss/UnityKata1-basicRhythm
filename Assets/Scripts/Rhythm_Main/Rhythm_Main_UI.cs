using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Main_UI : MonoBehaviour
{
    [Header("Master")]
    [SerializeField] private Rhythm_Main_Master master;


    [Header ("UI References")]
    [SerializeField] private Slider optionsScrollSpeedSlider;
    [SerializeField] private Text optionsScrollSpeedValue;
    [SerializeField] private Text optionsMenuInputKeysButtonText;
    [SerializeField] private Text optionsMenuQuickRestartButtonText;
    [SerializeField] private Text optionsMenuPauseButtonText;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject optionsMenuBackground;


    //* private vars
    private Rhythm_System sys;
    private Rhythm_InputKeysConfigSteps inputKeysConfigSteps;
    private List<KeyCode> illegalKeybindings, assignedKeybindings;
    private int slideInTimeInFrames;
    private float optionsMenuSourcePositionX, optionsMenuTargetPositionX, slideInDegree;
    private bool isOptionsMenuShown = false;
    private bool isOptionsMenuCurrentlyMoving = false;
    private bool isCurrentlyConfiguring = false;


    public void initialize(Rhythm_System sysRef) {
        sys = sysRef;

        initializePrivateVariables();

        initializeScollSpeedSlider();
        initializeTexts();
    }


    IEnumerator ToggleOptionsMenuVisibility() {
        for (int i = 0; i < slideInTimeInFrames; i++) {
            if (isOptionsMenuShown) optionsMenu.transform.localPosition += new Vector3(slideInDegree,0,0);
            else optionsMenu.transform.localPosition -= new Vector3(slideInDegree,0,0);
            yield return null;
        }
        isOptionsMenuShown = isOptionsMenuShown ? false : true;
        isOptionsMenuCurrentlyMoving = false;
    }

    IEnumerator Configure4KInputKeys() {
        while (isCurrentlyConfiguring) {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(key) && !illegalKeybindings.Contains(key)) {
                    if (assignedKeybindings.Contains(key)) {
                        // TODO: show something on the UI here.
                        Debug.Log(key + " is already assigned, no go further");
                    } else {
                        switch (inputKeysConfigSteps) {
                            case Rhythm_InputKeysConfigSteps.INPUT_LEFT:
                                PlayerPrefs.SetInt("rhythm_inputKeyLeft", (int)key);
                                optionsMenuInputKeysButtonText.text = "" + key + ", _ , _ , _";
                                inputKeysConfigSteps = Rhythm_InputKeysConfigSteps.INPUT_DOWN;
                                break;
                            case Rhythm_InputKeysConfigSteps.INPUT_DOWN:
                                PlayerPrefs.SetInt("rhythm_inputKeyDown", (int)key);
                                optionsMenuInputKeysButtonText.text = optionsMenuInputKeysButtonText.text.Split('_')[0] + key + ", _ , _"; 
                                inputKeysConfigSteps = Rhythm_InputKeysConfigSteps.INPUT_UP;
                                break;
                            case Rhythm_InputKeysConfigSteps.INPUT_UP:
                                PlayerPrefs.SetInt("rhythm_inputKeyUp", (int)key);
                                optionsMenuInputKeysButtonText.text = optionsMenuInputKeysButtonText.text.Split('_')[0] + key + ", _";
                                inputKeysConfigSteps = Rhythm_InputKeysConfigSteps.INPUT_RIGHT;
                                break;
                            case Rhythm_InputKeysConfigSteps.INPUT_RIGHT:
                                PlayerPrefs.SetInt("rhythm_inputKeyRight", (int)key);
                                optionsMenuInputKeysButtonText.text = optionsMenuInputKeysButtonText.text.Split('_')[0] + key;
                                inputKeysConfigSteps = Rhythm_InputKeysConfigSteps.FINISH;
                                break;
                        }
                    }
                }
            }
            if (inputKeysConfigSteps == Rhythm_InputKeysConfigSteps.FINISH) {
                inputKeysConfigSteps = Rhythm_InputKeysConfigSteps.INPUT_LEFT;
                sys.updateKeybindings();
                updateAssignedKeybindings();
                isCurrentlyConfiguring = false;
            }

            yield return null;
        }
    }

    IEnumerator ConfigureKey(string buttonTextIndicator) {
        isCurrentlyConfiguring = true;
        setButtonText(buttonTextIndicator, sys.defaultKeyConfigText);
        unassignKeybinding(buttonTextIndicator);

        while (isCurrentlyConfiguring) {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(key) && !illegalKeybindings.Contains(key)) {
                    if (assignedKeybindings.Contains(key)) {
                        // TODO: show something on the UI here.
                        Debug.Log(key + " is already assigned, no go further");
                    } else {
                        PlayerPrefs.SetInt(buttonTextIndicator, (int)key);
                        sys.updateKeybindings();
                        updateAssignedKeybindings();
                        setButtonText(buttonTextIndicator, getKeyButtonText(buttonTextIndicator));
                        isCurrentlyConfiguring = false;
                    }
                }
            }
            yield return null;
        }
    }


    private void initializePrivateVariables() {
        // inputKeysConfigSteps
        inputKeysConfigSteps = Rhythm_InputKeysConfigSteps.INPUT_LEFT;

        // keybindings lists
        resetIllegalKeybindings();
        updateAssignedKeybindings();

        // floats
        slideInTimeInFrames = sys.optionsMenuSlideInTimeInFrames;
        optionsMenuSourcePositionX = optionsMenu.transform.localPosition.x;
        optionsMenuTargetPositionX = optionsMenuSourcePositionX - optionsMenuBackground.GetComponent<RectTransform>().rect.width;
        slideInDegree = (optionsMenuSourcePositionX - optionsMenuTargetPositionX) / slideInTimeInFrames;
    }

    private void initializeScollSpeedSlider() => optionsScrollSpeedSlider.value = PlayerPrefs.GetFloat("rhythm_scrollSpeed");

    private void initializeTexts() {
        updateScrollSpeedValueText();
        updateInputKeysButtonText();
        updateQuickRestartKeyButtonText();
        updatePauseKeyButtonText();
    }
    private void updateScrollSpeedValueText() => optionsScrollSpeedValue.text = PlayerPrefs.GetFloat("rhythm_scrollSpeed").ToString();
    private void updateInputKeysButtonText() => optionsMenuInputKeysButtonText.text = "" + sys.inputLeft + "," + sys.inputDown + "," + sys.inputUp + "," + sys.inputRight;
    private void updateQuickRestartKeyButtonText() => optionsMenuQuickRestartButtonText.text = "" + sys.inputQuickRestart;
    private void updatePauseKeyButtonText() => optionsMenuPauseButtonText.text = "" + sys.inputPause;

    private void setButtonText(string buttonTextIndicator, string newText) {
        switch (buttonTextIndicator) {
            case "rhythm_inputKeyQuickRestart": optionsMenuQuickRestartButtonText.text = newText; break;
            case "rhythm_inputKeyPause": optionsMenuPauseButtonText.text = newText; break;
        }
    }

    /*
        Get desired text that should be set after an input key has been configured.
    */
    private string getKeyButtonText(string buttonTextIndicator) {
        switch (buttonTextIndicator) {
            case "rhythm_inputKeyQuickRestart": return "" + sys.inputQuickRestart;
            case "rhythm_inputKeyPause": return "" + sys.inputPause;
            default: return "";
        }
    }


#region Keybinding Functions
    private void updateAssignedKeybindings() => assignedKeybindings = sys.currentKeybindings;
    private void resetIllegalKeybindings() {
        illegalKeybindings = new List<KeyCode>();
        List<KeyCode> allKeybindings = new List<KeyCode>((KeyCode[])System.Enum.GetValues(typeof(KeyCode)));
        allKeybindings.ForEach(keybind => {
            if ((int)keybind >= 320) illegalKeybindings.Add(keybind);
        });
    }
    private void unassignKeybinding(string keybinding) {
        switch (keybinding) {
            case "rhythm_inputKeyLeft": assignedKeybindings.Remove(sys.inputLeft); break;
            case "rhythm_inputKeyDown": assignedKeybindings.Remove(sys.inputDown); break;
            case "rhythm_inputKeyUp": assignedKeybindings.Remove(sys.inputUp); break;
            case "rhythm_inputKeyRight": assignedKeybindings.Remove(sys.inputRight); break;
            case "rhythm_inputKeyQuickRestart": assignedKeybindings.Remove(sys.inputQuickRestart); break;
            case "rhythm_inputKeyPause": assignedKeybindings.Remove(sys.inputPause); break;
        }
    }
    private void unassignInputKeybindings() {
        assignedKeybindings.Remove(sys.inputLeft);
        assignedKeybindings.Remove(sys.inputDown);
        assignedKeybindings.Remove(sys.inputUp);
        assignedKeybindings.Remove(sys.inputRight);
    }
#endregion

#region Slider Functions
    public void changeScrollSpeed() {
        PlayerPrefs.SetFloat("rhythm_scrollSpeed", Mathf.Round(optionsScrollSpeedSlider.value * 10)/10);
        updateScrollSpeedValueText();
    }
#endregion

#region Button Functions
    public void onPlayButtonPress() {
        if (!isCurrentlyConfiguring) {
            master.switchScene(sys.gameSceneName);
        }
    } 

    public void onOptionsButtonPress() {
        if (!isOptionsMenuCurrentlyMoving && !isCurrentlyConfiguring) {
            isOptionsMenuCurrentlyMoving = true;
            StartCoroutine(ToggleOptionsMenuVisibility());
        }
    }

    public void onExitButtonPress() {
        if (!isCurrentlyConfiguring) {
            Application.Quit();
        }
    } 

    public void onInputKeysButtonPress() {
        if (!isCurrentlyConfiguring) {
            isCurrentlyConfiguring = true;
            optionsMenuInputKeysButtonText.text = "Press 4 input keys";
            unassignInputKeybindings();

            StartCoroutine(Configure4KInputKeys());
        }
    }

    public void onQuickRestartKeyButtonPress() {
        if (!isCurrentlyConfiguring) {
            StartCoroutine(ConfigureKey("rhythm_inputKeyQuickRestart"));
        }
    }

    public void onPauseKeyButtonPress() {
        if (!isCurrentlyConfiguring) {
            StartCoroutine(ConfigureKey("rhythm_inputKeyPause"));
        }
    }
#endregion
}
