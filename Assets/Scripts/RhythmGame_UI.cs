using UnityEngine;
using UnityEngine.UI;

public class RhythmGame_UI : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private Text scoreText;

    private int scoreValue = 0;

    public void updateScoreText(int valueUpdate) {
        scoreValue += valueUpdate;

        scoreText.text = scoreValue.ToString();
    }
}
