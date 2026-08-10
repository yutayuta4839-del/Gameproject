using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Node", order = 1)]
public class DialogueData : ScriptableObject
{

    public string charname;

    //複数登録して、センテンスの内容に応じて、スプライトを変更
    public string[] sentences;
    public Sprite[] charicons;


}
