public class Rhythm_Game_NoteShort : Rhythm_Game_Note
{
    public override void initialize(int index, UnityEngine.Sprite sprite, float length) {
        base.initialize(index, sprite, 0);
        base.noteType = Rhythm_Note_Type.SHORT;
    }
}
