using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_TimingIndicator : MonoBehaviour
{
    private Image currentImage;
    private float fTime, fDegree;


    public void initialize(float fadeTimeInFrames, float fadeDegree) {
        // initialize private vars
        currentImage = GetComponent<Image>();
        fTime = fadeTimeInFrames;
        fDegree = fadeDegree;

        StartCoroutine("FadeOut");
    }


    IEnumerator FadeOut() {
        for (int i = 0; i < fTime; i++) {
            changeColorAlpha(currentImage.color.a - fDegree);
            yield return null;
        }
        Destroy(this.gameObject);
    }


    private void changeColorAlpha(float a) => currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, a);
}
