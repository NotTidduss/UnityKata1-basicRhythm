using UnityEngine;

public class Rhythm_Main_Master : MonoBehaviour
{
    [Header("Scene References")]
    public Rhythm_Main_UI ui;


    void Start() {
        ui.initialize();
    }
}
