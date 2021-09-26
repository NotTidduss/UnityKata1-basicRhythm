using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_TimingIndicator : MonoBehaviour
{
    // private vars
    Image currentImage;
    float fTime, fDegree;


    IEnumerator FadeOut() {
        for (int i = 0; i < fTime; i++) {
            changeColorAlpha(currentImage.color.a - fDegree);
            yield return null;
        }
        Destroy(this.gameObject);
    }


    public void initialize(float fadeTimeInFrames, float fadeDegree) {
        currentImage = GetComponent<Image>();
        fTime = fadeTimeInFrames;
        fDegree = fadeDegree;

        StartCoroutine("FadeOut");
    }


    private void changeColorAlpha(float a) => currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, a);
}
