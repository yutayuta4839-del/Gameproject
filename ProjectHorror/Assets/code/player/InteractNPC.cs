using UnityEngine;
using UnityEngine.UI;

public class InteractNPC : MonoBehaviour, ISearchInteractable, IDialogueableInteractable
{
    private bool isinteractingnpc;
    private bool isinteractingdualogue;


    //------------------------------------------------
    //Search interface
    //------------------------------------------------
    public virtual bool CanInteract()    //playerclassの入力系処理に対して、インタラクトできたかを返す
    {
        return isinteractingnpc;　//仮置き
    }

    public virtual void SearchInteract(GameObject NPCSearchwindow)
    {

    }

    //------------------------------------------------
    //Dialogue interface
    //------------------------------------------------
    public virtual bool CanDialogue()　　　　　//playerclassの入力系処理に対して、インタラクトできたかを返す
    {
        return isinteractingdualogue;//仮置き
    }

    public virtual void DialogueInteract()
    {

    }


}
