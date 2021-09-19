using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Game_UI : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private Text scoreText;


    IEnumerator UpdateScore() {
        while(true) {
            updateScoreText();
            yield return null;
        }
    }


    public void initialize() => StartCoroutine("UpdateScore");
    public void updateScoreText() => scoreText.text = PlayerPrefs.GetInt("rhythm_mapScore").ToString();
}
