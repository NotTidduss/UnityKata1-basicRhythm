using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_TimingIndicatorMaster : MonoBehaviour
{
    private GameObject indicatorImagePrefab;
    private Image currentImage;
    private Sprite spriteIndicatorPerfect, spriteIndicatorGood, spriteIndicatorFine, spriteIndicatorMiss;
    private float fadeTimeInFrames, fadeDegree;


    IEnumerator CheckForNoteHit() {
        while(true) {
            if (PlayerPrefs.GetString("rhythm_lastNoteHitTiming") != "") {
                GameObject indicatorImagePrefabClone = Instantiate(indicatorImagePrefab, transform);
                indicatorImagePrefabClone.GetComponent<Image>().sprite = (getNewSprite(PlayerPrefs.GetString("rhythm_lastNoteHitTiming"))); 
                indicatorImagePrefabClone.GetComponent<Rhythm_TimingIndicator>().initialize(fadeTimeInFrames, fadeDegree);
                PlayerPrefs.SetString("rhythm_lastNoteHitTiming", "");
            }
            yield return null;
        }
    }


    public void initialize(Rhythm_System sys) {
        indicatorImagePrefab = sys.timingIndicatorPrefab;

        spriteIndicatorPerfect = sys.spriteIndicatorPerfect;
        spriteIndicatorGood = sys.spriteIndicatorGood;
        spriteIndicatorFine = sys.spriteIndicatorFine;
        spriteIndicatorMiss = sys.spriteIndicatorMiss;

        fadeTimeInFrames = sys.timingIndicatorFadeTimeInFrames;
        fadeDegree = 1 / fadeTimeInFrames;

        StartCoroutine("CheckForNoteHit");
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
