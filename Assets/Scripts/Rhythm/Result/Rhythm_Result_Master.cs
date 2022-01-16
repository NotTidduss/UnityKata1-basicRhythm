using UnityEngine;

public class Rhythm_Result_Master : MonoBehaviour
{
    [Header ("Scene References")]
    [SerializeField] private Rhythm_System sys;
    [SerializeField] private Rhythm_Result_UI ui;
    [SerializeField] private Rhythm_TransitionMaster transitionMaster;


    void Start() {
        ui.initialize(this, sys);
        transitionMaster.initialize(sys);
    }

    public void switchScene(string sceneName) => transitionMaster.transitionToNextScene(sceneName);
}
