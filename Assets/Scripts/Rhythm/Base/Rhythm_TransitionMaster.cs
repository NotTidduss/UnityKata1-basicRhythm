using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_TransitionMaster : MonoBehaviour
{
    //* private vars
    private Rhythm_System sys;                      // system reference
    private Image img;                              // actual image of the image object = black screen
    private float transitionDuration;               // reference to the System class
    private float transitionFadeDegree;             // the degree the images is fading out / in at


    public void initialize(Rhythm_System sysRef) {
        // save system reference
        sys = sysRef;

        // initialize private veriables
        transitionDuration = sys.transitionDurationInFrames;
        transitionFadeDegree = 1 / transitionDuration;

        // initialize image component on prefab
        img = GetComponent<Image>();
        img.enabled = true;

        // start initial transition
        StartCoroutine(TransitionIntoScene());
    }  


    // Fade outm to 100% transparency 
    IEnumerator TransitionIntoScene() {
        for (int i = 0; i < transitionDuration; i++) {
            changeColorAlpha(img.color.a - transitionFadeDegree);
            yield return null;
        }

        img.enabled = false;
    }

    // Fade in, to 100% opacity
    IEnumerator TransitionToNextScene(string nextSceneName) {
        img.enabled = true;

        for (int i = 0; i < transitionDuration; i++) {
            changeColorAlpha(img.color.a + transitionFadeDegree);
            yield return null;
        }

        sys.loadScene(nextSceneName);
    }


    public void transitionToNextScene(string sceneName) => StartCoroutine(TransitionToNextScene(sceneName));


    private void changeColorAlpha(float a) => img.color = new Color(img.color.r, img.color.g, img.color.b, a);
}
