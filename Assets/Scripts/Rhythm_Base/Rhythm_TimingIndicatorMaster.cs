using UnityEngine;
using UnityEngine.UI;

public class Rhythm_TimingIndicatorMaster : MonoBehaviour
{
    private GameObject indicatorImagePrefab;
    private Image currentImage;
    private Sprite spriteIndicatorPerfect, spriteIndicatorGood, spriteIndicatorFine, spriteIndicatorMiss;
    private float fadeTimeInFrames, fadeDegree;


    public void initialize(Rhythm_System sys) {
        // initialize private vars
        indicatorImagePrefab = sys.timingIndicatorPrefab;

        spriteIndicatorPerfect = sys.spriteIndicatorPerfect;
        spriteIndicatorGood = sys.spriteIndicatorGood;
        spriteIndicatorFine = sys.spriteIndicatorFine;
        spriteIndicatorMiss = sys.spriteIndicatorMiss;

        fadeTimeInFrames = sys.timingIndicatorFadeTimeInFrames;
        fadeDegree = 1 / fadeTimeInFrames;
    }

    /*
        Spawn indicator object, and set desired attributes.
    */
    public void spawnIndicator() {
        GameObject indicatorImagePrefabClone = Instantiate(indicatorImagePrefab, transform);
        indicatorImagePrefabClone.GetComponent<Image>().sprite = (getNewSprite(PlayerPrefs.GetString("rhythm_lastNoteHitTiming"))); 
        indicatorImagePrefabClone.GetComponent<Rhythm_TimingIndicator>().initialize(fadeTimeInFrames, fadeDegree);
    }


    private Sprite getNewSprite(string judgementType) {
        switch (judgementType) {
            case "PERFECT": return spriteIndicatorPerfect;
            case "GOOD": return spriteIndicatorGood;
            case "FINE": return spriteIndicatorFine;
            default: return spriteIndicatorMiss;
        }
    }
}
