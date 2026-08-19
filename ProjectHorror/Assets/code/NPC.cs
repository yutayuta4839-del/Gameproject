using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using System.Threading;

public class NPC : MonoBehaviour, IInteractable
{
    public BoxCollider2D Dialoguebox;

    public bool isPlayerInRange;
    private GameObject dialogueobject;
    private DialogueManager dialogueUI;
    public DialogueData DialogueData;
    private int dialogueindex;
   

    public bool isDialogueRunning = false;
    private bool istyping = false;




    private void Start()
    {
        dialogueobject = GameObject.Find("DialogueManager");
        dialogueUI = DialogueManager.Instance;
    }

    public bool CanInteract()
    {
        return !isDialogueRunning;
    }

    public async UniTask Interact()
    {
        if (DialogueData == null)//そしてpauseしていて、!dialoguerunnningな場合...
        {
            return;
        }

        if (isDialogueRunning)
        {
            await NextLine();
        }
        else
        {
            await StartDialogue();
        }

        if (istyping)
        {
           
        }


    }

    private async UniTask StartDialogue()
    {
        isDialogueRunning = true;

        dialogueUI.giveinfotoNPC(DialogueData.npcname);//あとでspriteの変更も行う
        dialogueUI.ShowDialolgueUI(true);
        //そこにPauseControllerのpause関数とかを入れたりする。
        await DisplaycurrentLine();




    }

    async UniTask NextLine()
    {

        dialogueUI.Clearchoices();

        if (DialogueData.EndDialogueLines.Length > dialogueindex && DialogueData.EndDialogueLines[dialogueindex])
        {
            Enddialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in DialogueData.choices)
        {
            if (dialogueChoice.dialogueindex == dialogueindex)
            {
                Displaychoices(dialogueChoice);
                return;
            }
        }


        if (dialogueindex + 1 < DialogueData.DialogueLines.Length)
        {
            ++dialogueindex;

            await DisplaycurrentLine();
        }
        else
        {
            Enddialogue();
        }
    }



    private async UniTask TypeLine()
    {
        istyping = true;

        dialogueUI.SetDialoguetext("");

        foreach (char letter in DialogueData.DialogueLines[dialogueindex])
        {
            dialogueUI.SetDialoguetext(dialogueUI.sentenceText.text += letter);
            Debug.Log(dialogueUI.sentenceText.text);
            await UniTask.Delay(60);

        }
        istyping = false;

    }

    private void Enddialogue()
    {
        

        isDialogueRunning = false;
        dialogueUI.SetDialoguetext("");
        dialogueUI.ShowDialolgueUI(false);
        dialogueUI.Clearchoices();
        //Pause解除関数


    }

    void Displaychoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextindex = choice.nextdialogueindexs[i];
            dialogueUI.CreateChocieButton(choice.choices[i], () => Chooseoption(nextindex));
        }
    }

    async void Chooseoption(int nextidex)
    {
        dialogueindex = nextidex;
        dialogueUI.Clearchoices();
        await DisplaycurrentLine();
        if (DialogueData.EndDialogueLines.Length > dialogueindex && DialogueData.EndDialogueLines[dialogueindex])
        {
            Enddialogue();
        }
       
    }

    async UniTask DisplaycurrentLine()
    {
        await TypeLine();
    }
   
}
