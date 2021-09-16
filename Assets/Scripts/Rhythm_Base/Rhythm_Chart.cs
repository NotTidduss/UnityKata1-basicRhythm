using UnityEngine;

public class Rhythm_Chart : MonoBehaviour
{   
    [Header("Scene References")]
    [SerializeField] private Transform playAreaTransform;

    [Header("Prefab References")]
    [SerializeField] private GameObject shortNoteLeft;
    [SerializeField] private GameObject shortNoteDown;
    [SerializeField] private GameObject shortNoteUp;
    [SerializeField] private GameObject shortNoteRight;

    public void placeRandomNote() {
        int rng = Random.Range(0,4);

        switch(rng){
            case 0: placeNote(shortNoteLeft); break;
            case 1: placeNote(shortNoteDown); break;
            case 2: placeNote(shortNoteUp); break;
            case 3: placeNote(shortNoteRight); break;
        }
    }

    private void placeNote(GameObject note) => Instantiate(note, playAreaTransform);
}
