using UnityEngine;
using UnityEngine.UI;

public class Rhythm_Result_UI : MonoBehaviour
{
    [Header ("Scene Reference")]
    [SerializeField] private Text perfectCountText;
    [SerializeField] private Text goodCountText;
    [SerializeField] private Text fineCountText;
    [SerializeField] private Text missCountText;


    //* private vars
    private Rhythm_System sys;              // reference to the System class
    private Rhythm_Result_Master master;    // reference to the Result Master class


    public void initialize(Rhythm_Result_Master masterRef, Rhythm_System sysRef) {
        // initialize scene references
        master = masterRef;
        sys = sysRef;

        // initialize text objects
        perfectCountText.text = "" + PlayerPrefs.GetInt("rhythm_perfectHitCount");
        goodCountText.text = "" + PlayerPrefs.GetInt("rhythm_goodHitCount");
        fineCountText.text = "" + PlayerPrefs.GetInt("rhythm_fineHitCount");
        missCountText.text = "" + PlayerPrefs.GetInt("rhythm_missCount");
    }


#region Buttons
    public void onRetry() => master.switchScene(sys.gameSceneName);
    public void onBack() => master.switchScene(sys.mainSceneName);
#endregion
}
