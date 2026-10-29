using UnityEngine;

public interface IDialogueableInteractable
{
    bool CanDialogue();

    void DialogueInteract(DialogueData Dialoguedata);

   
}
