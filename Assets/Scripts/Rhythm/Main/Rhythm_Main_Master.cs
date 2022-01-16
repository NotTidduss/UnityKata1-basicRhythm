using UnityEngine;

public class Rhythm_Main_Master : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Rhythm_System sys;
    [SerializeField] private Rhythm_Main_UI ui;
    [SerializeField] private Rhythm_TransitionMaster transitionMaster;


    void Start() {
        sys.initialize();
        ui.initialize(sys);
        transitionMaster.initialize(sys);
    }


    public void switchScene(string sceneName) => transitionMaster.transitionToNextScene(sceneName);
}
