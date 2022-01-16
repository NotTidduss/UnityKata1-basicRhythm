using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Game_TimingIndicatorMaster : MonoBehaviour
{
    //* private vars
    private GameObject indicatorImagePrefab;                                                               // reference to indicator image prefab
    private Sprite spriteIndicatorPerfect, spriteIndicatorGood, spriteIndicatorFine, spriteIndicatorMiss;  // references to sprites that are derived from System
    private float fadeTimeInFrames, fadeDegree;                                                            // variables for fading


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
        @param judgement - used to fetch sprite for timing indicator
    */
    public void spawnIndicatorByJudgement(Rhythm_Judgement judgement) {
        GameObject indicatorImagePrefabClone = Instantiate(indicatorImagePrefab, transform);
        indicatorImagePrefabClone.GetComponent<Image>().sprite = (getNewSprite(judgement)); 
        indicatorImagePrefabClone.GetComponent<Rhythm_Game_TimingIndicator>().initialize(fadeTimeInFrames, fadeDegree);
    }

    public void spawnMissIndicator() {
        GameObject indicatorImagePrefabClone = Instantiate(indicatorImagePrefab, transform);
        indicatorImagePrefabClone.GetComponent<Image>().sprite = spriteIndicatorMiss; 
        indicatorImagePrefabClone.GetComponent<Rhythm_Game_TimingIndicator>().initialize(fadeTimeInFrames, fadeDegree);
    }


    // Return a sprite that fits the given judgement. Return MISS sprite by default.
    private Sprite getNewSprite(Rhythm_Judgement judgement) {
        switch (judgement) {
            case Rhythm_Judgement.PERFECT: return spriteIndicatorPerfect;
            case Rhythm_Judgement.GOOD: return spriteIndicatorGood;
            case Rhythm_Judgement.FINE: return spriteIndicatorFine;
            default: return spriteIndicatorMiss;
        }
    }
}
