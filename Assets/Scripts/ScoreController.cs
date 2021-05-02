using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreValText;

    private int scoreValue = 0;

    public void updateScore(int valueUpdate) {
        scoreValue += valueUpdate;

        scoreValText.text = scoreValue.ToString();
    }
}
