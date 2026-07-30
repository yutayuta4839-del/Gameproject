using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Node",order = 1)]
public class Dialogue : ScriptableObject
{
    public Sprite charicon;
    public Sprite sentencebar;

    public string charname;
    public string sentence;
   

}
