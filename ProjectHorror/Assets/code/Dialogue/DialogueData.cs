using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Node", order = 1)]
public class DialogueData : ScriptableObject
{
    public string npcname;
    public Sprite charicons;
    //複数登録して、センテンスの内容に応じて、スプライトを変更

    public string[] DialogueLines;
    public bool[] EndDialogueLines;//Mark where end the dialogue
    public float typingspeed = 0.05f;

    public DialogueChoice[] choices;
    public DialogueJump[] jumps;



}
[System.Serializable]
public class DialogueChoice
{
    public int dialogueindex; //it shows DialogueLines number array;
    public string[] choices; //player response

    public int[] nextdialogueindexs; // where choice lead



}
[System.Serializable]
public class DialogueJump
{
   
    public int fromIndex;
    public int toIndex;
}