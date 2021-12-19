using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rhythm_Game_NoteTrail : MonoBehaviour
{
    //* private vars
    private Rhythm_Game_NoteLong trailedNote;    // the note that generated the trail
    private float fallSpeed;                // speed at which the note falls along the lane
    private bool isHeld;                    // bool that indicates whether the original long note has been pressed


    public void initialize(Rhythm_Game_NoteLong note, Sprite trailSprite) {
        // initialize private vars
        trailedNote = note;
        fallSpeed = PlayerPrefs.GetFloat("rhythm_scrollSpeed") / 5;

        // initialize the note's sprite
        setTrailImage(trailSprite);

        // initialize falling
        StartCoroutine("Fall");
    } 


    IEnumerator Fall() {
        while (true) {
            if (PlayerPrefs.GetInt("rhythm_paused") % 2 == 0) {
                transform.Translate(0, -fallSpeed, 0);

                // destroy if trailed note was hit and position is right.
                if (isHeld && gameObject.transform.localPosition.y < 0f)
                    Destroy(gameObject);

                // destroy if trail falls too low.
                if (gameObject.transform.localPosition.y < -150f)
                    Destroy(gameObject);
            }
            yield return null;
        }
    }


    public void miss(Sprite missedSprite) {
        setTrailImage(missedSprite);
        setIsHeld(false);
    } 
    public void setIsHeld(bool holdingState) => isHeld = holdingState;


    private void setTrailImage(Sprite trailSprite) {
        GetComponent<Image>().sprite = trailSprite;
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.3f);
    }
}
