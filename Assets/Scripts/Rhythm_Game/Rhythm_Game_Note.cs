using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rhythm_Game_Note : MonoBehaviour
{
    //* private vars
    internal Rhythm_Note_Type noteType;     // type of note
    internal float noteLength;              // length of long note, will always be 0 for short notes
    private int index;                      // index that keeps track of the note's place in the chart.
    private float fallSpeed;                // speed at which the note falls along the lane


    public virtual void initialize(int newIndex, Sprite sprite, float length) {
        // initialize private vars
        noteLength = length;
        index = newIndex;
        fallSpeed = PlayerPrefs.GetFloat("rhythm_scrollSpeed") / 5;

        // initialize the note's sprite
        GetComponent<Image>().sprite = sprite;

        // initialize falling
        StartCoroutine("Fall");
    } 

    public virtual void hold() {}
    public virtual void release() {}
    public virtual void changeSprite(Sprite missedSprite) {}


    IEnumerator Fall() {
        while (true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                transform.Translate(0, -fallSpeed, 0);

                // count as miss if note falls too low, and destroy this object.
                if (gameObject.transform.localPosition.y < -300f) {
                    PlayerPrefs.SetInt("rhythm_fallenNoteCountForTiming", PlayerPrefs.GetInt("rhythm_fallenNoteCountForTiming") + 1);
                    PlayerPrefs.SetInt("rhythm_fallenNoteCountForHealth", PlayerPrefs.GetInt("rhythm_fallenNoteCountForHealth") + 1);
                    Destroy(gameObject);
                }
            }
            yield return null;
        }
    }

    public int getIndex() => index;
    public Rhythm_Note_Type getNoteType() => noteType;
    public void setMissed() => index = 0;
}
