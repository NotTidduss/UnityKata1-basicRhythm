using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Game_NoteLong : Rhythm_Game_Note
{
    [Header ("Long Note Trail Prefab")]
    [SerializeField] private GameObject trailPrefab;


    //* private vars
    List<Rhythm_Game_NoteTrail> trailElements;           // list of all elements that make up the long note trail


    public override void initialize(int index, Sprite sprite, float length) {
        base.initialize(index, sprite, length);
        base.noteType = Rhythm_Note_Type.LONG;

        trailElements = fillTrailElementsList(trailPrefab);
    }

    public override void hold() {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0);
        StopCoroutine("Fall");
        trailElements.ForEach(trail => trail.setIsHeld(true));
    }

    public override void release() {
        trailElements.ForEach(trail => trail.setIsHeld(false));
        Destroy(gameObject);
    }

    public override void changeSprite(Sprite missedSprite) {
        GetComponent<Image>().sprite = missedSprite;
        trailElements.ForEach(trail =>  {
            if (trail != null) 
                trail.miss(missedSprite);
        });
    }


    private List<Rhythm_Game_NoteTrail> fillTrailElementsList(GameObject trail) => fillTrailElementsListRec(trail, 0, new List<Rhythm_Game_NoteTrail>());

    private List<Rhythm_Game_NoteTrail> fillTrailElementsListRec(GameObject trail, float currentTrailLength, List<Rhythm_Game_NoteTrail> currentTrailList) {
        if (currentTrailLength >= noteLength*10) {
            return currentTrailList;
        } else {
            GameObject trailClone = Instantiate(trail, transform.parent);
            trailClone.transform.localPosition += new Vector3(0, (10 * currentTrailLength), 0);

            currentTrailList.Add(trailClone.GetComponent<Rhythm_Game_NoteTrail>());
            trailClone.GetComponent<Rhythm_Game_NoteTrail>().initialize(this, GetComponent<UnityEngine.UI.Image>().sprite);

            return fillTrailElementsListRec(trail, ++currentTrailLength, currentTrailList);
        }
    }
}
