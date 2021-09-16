using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Game_UI : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private Text scoreText;


    public void updateScoreText() => scoreText.text = PlayerPrefs.GetInt("rhythm_mapScore").ToString();
}
