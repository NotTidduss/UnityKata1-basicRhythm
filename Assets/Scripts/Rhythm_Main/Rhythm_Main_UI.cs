using System.Collections;
using System.Collections.Generic;
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
    private List<KeyCode> illegalKeybindings;
    private int slideInTimeInFrames;
    private float optionsMenuSourcePositionX, optionsMenuTargetPositionX, slideInDegree;
    private bool isOptionsMenuShown = false;
    private bool isOptionsMenuCurrentlyMoving = false;
    private bool isCurrentlyConfiguring = false;


    void Start() {
        initializePrivateVariables();

        initializeScollSpeedSlider();
        
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
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(key) && !illegalKeybindings.Contains(key)) {
                    illegalKeybindings.Add(key);

                    switch (keybindingsConfigSteps) {
                        case Rhythm_KeybindingsConfigSteps.INPUT_LEFT:
                            PlayerPrefs.SetInt("rhythm_inputKeyLeft", (int)key);
                            optionsMenuKeyBindingsButtonText.text = "" + key + ", _ , _ , _";
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_DOWN;
                            break;
                        case Rhythm_KeybindingsConfigSteps.INPUT_DOWN:
                            PlayerPrefs.SetInt("rhythm_inputKeyDown", (int)key);
                            optionsMenuKeyBindingsButtonText.text = optionsMenuKeyBindingsButtonText.text.Split('_')[0] + key + ", _ , _"; 
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_UP;
                            break;
                        case Rhythm_KeybindingsConfigSteps.INPUT_UP:
                            PlayerPrefs.SetInt("rhythm_inputKeyUp", (int)key);
                            optionsMenuKeyBindingsButtonText.text = optionsMenuKeyBindingsButtonText.text.Split('_')[0] + key + ", _";
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_RIGHT;
                            break;
                        case Rhythm_KeybindingsConfigSteps.INPUT_RIGHT:
                            PlayerPrefs.SetInt("rhythm_inputKeyRight", (int)key);
                            optionsMenuKeyBindingsButtonText.text = optionsMenuKeyBindingsButtonText.text.Split('_')[0] + key;
                            keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.FINISH;
                            break;
                    }
                }
            }
            if (keybindingsConfigSteps == Rhythm_KeybindingsConfigSteps.FINISH) {
                keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_LEFT;
                sys.updateKeybindings();
                resetIllegalKeybindings();
                isCurrentlyConfiguring = false;
            }

            yield return null;
        }
    }
    

    private void initializePrivateVariables()
    {
        // keybindingsConfigSteps
        keybindingsConfigSteps = Rhythm_KeybindingsConfigSteps.INPUT_LEFT;

        // illegalKeybindings List
        resetIllegalKeybindings();

        // floats
        slideInTimeInFrames = sys.optionsMenuSlideInTimeInFrames;
        optionsMenuSourcePositionX = optionsMenu.transform.localPosition.x;
        optionsMenuTargetPositionX = optionsMenuSourcePositionX - optionsMenuBackground.GetComponent<RectTransform>().rect.width;
        slideInDegree = (optionsMenuSourcePositionX - optionsMenuTargetPositionX) / slideInTimeInFrames;
    }

    private void resetIllegalKeybindings() {
        illegalKeybindings = new List<KeyCode>();
        List<KeyCode> allKeybindings = new List<KeyCode>((KeyCode[])System.Enum.GetValues(typeof(KeyCode)));
        allKeybindings.ForEach(keybind => {
            if ((int)keybind >= 320) illegalKeybindings.Add(keybind);
        });
    }

    private void initializeScollSpeedSlider() => optionsScrollSpeedSlider.value = PlayerPrefs.GetFloat("rhythm_scrollSpeed");
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
