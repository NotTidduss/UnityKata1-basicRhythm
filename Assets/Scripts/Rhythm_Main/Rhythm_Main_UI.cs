using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Main_UI : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private Rhythm_System sys;

    [Header ("UI References")]
    [SerializeField] private Slider optionsScrollSpeedSlider;
    [SerializeField] private Text optionsScrollSpeedValue;
    [SerializeField] private Text optionsMenuKeyBindingsButtonText;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject optionsMenuBackground;

    private Rhythm_KeybindingsConfigSteps keybindingsConfigSteps;
    private int slideInTimeInFrames;
    private float optionsMenuSourcePositionX, optionsMenuTargetPositionX, slideInDegree;
    private bool isOptionsMenuShown = false;
    private bool isOptionsMenuCurrentlyMoving = false;
    private bool isCurrentlyConfiguring = false;


    void Start() {
        initializePrivateVariables();

        updateScrollSpeedValueText();
        updateKeybindingsButtonText();
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

    IEnumerator ConfigureKeybindings() {
        while (isCurrentlyConfiguring) {
            switch (keybindingsConfigSteps) {
                case Rhythm_KeybindingsConfigSteps.INPUT_LEFT: 
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                        if (Input.GetKeyDown(key) && (int) key < 320) {
                            PlayerPrefs.SetInt("rhythm_inputKeyLeft", (int) key);
                            sys.inputLeft = key;
                            optionsMenuKeyBindingsButtonText.text = "" + sys.inputLeft + ", _ , _ , _";
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_DOWN;
                        }
                    }
                    break;
                case Rhythm_KeybindingsConfigSteps.INPUT_DOWN:
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                        if (Input.GetKeyDown(key) && (int) key < 320 && key != sys.inputLeft) {
                            PlayerPrefs.SetInt("rhythm_inputKeyDown", (int) key);
                            sys.inputDown = key;
                            optionsMenuKeyBindingsButtonText.text = "" + sys.inputLeft + ", " + sys.inputDown + ", _ , _";
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_UP;
                        }
                    }
                    break;
                case Rhythm_KeybindingsConfigSteps.INPUT_UP:
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                        if (Input.GetKeyDown(key) && (int) key < 320 && key != sys.inputLeft && key != sys.inputDown) {
                            PlayerPrefs.SetInt("rhythm_inputKeyUp", (int) key);
                            sys.inputUp = key;
                            optionsMenuKeyBindingsButtonText.text = "" + sys.inputLeft + ", " + sys.inputDown + ", " + sys.inputUp + ", _";
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_RIGHT;
                        }
                    }
                    break;
                case Rhythm_KeybindingsConfigSteps.INPUT_RIGHT:
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                        if (Input.GetKeyDown(key) && (int) key < 320 && key != sys.inputLeft && key != sys.inputDown && key != sys.inputUp) {
                            PlayerPrefs.SetInt("rhythm_inputKeyRight", (int) key);
                            sys.inputRight = key;
                            optionsMenuKeyBindingsButtonText.text = "" + sys.inputLeft + ", " + sys.inputDown + ", " + sys.inputUp + "," + sys.inputRight;
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.FINISH;
                        }
                    }
                    break;
                case Rhythm_KeybindingsConfigSteps.FINISH:
                    keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_LEFT;
                    isCurrentlyConfiguring = false;
                    break;
            }
            yield return null;
        }
    }


    private void initializePrivateVariables() {
        // keybindingsConfigSteps
        keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_LEFT;

        // floats
        slideInTimeInFrames = sys.optionsMenuSlideInTimeInFrames;
        optionsMenuSourcePositionX = optionsMenu.transform.localPosition.x;
        optionsMenuTargetPositionX = optionsMenuSourcePositionX - optionsMenuBackground.GetComponent<RectTransform>().rect.width;
        slideInDegree = (optionsMenuSourcePositionX - optionsMenuTargetPositionX) / slideInTimeInFrames;
    }

    private void updateScrollSpeedValueText() => optionsScrollSpeedValue.text = PlayerPrefs.GetFloat("rhythm_scrollSpeed").ToString();
    private void updateKeybindingsButtonText() => optionsMenuKeyBindingsButtonText.text = "" + sys.inputLeft + "," + sys.inputDown + "," + sys.inputUp + "," + sys.inputRight;


    public void changeScrollSpeed() {
        PlayerPrefs.SetFloat("rhythm_scrollSpeed", Mathf.Round(optionsScrollSpeedSlider.value * 10)/10);
        updateScrollSpeedValueText();
    }

    public void onPlayButtonPress() {
        if (!isCurrentlyConfiguring) {
            sys.loadGameScene();
        }
    } 

    public void onOptionsButtonPress() {
        if (!isOptionsMenuCurrentlyMoving && !isCurrentlyConfiguring) {
            isOptionsMenuCurrentlyMoving = true;
            StartCoroutine("ToggleOptionsMenuVisibility");
        }
    }

    public void onExitButtonPress() {
        if (!isCurrentlyConfiguring) {
            Application.Quit();
        }
    } 

    public void onKeybindingsButtonPress() {
        if (!isCurrentlyConfiguring) {
            isCurrentlyConfiguring = true;
            optionsMenuKeyBindingsButtonText.text = "Press 4 input buttons";

            StartCoroutine("ConfigureKeybindings");
        }
    }
}
