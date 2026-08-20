using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;

public class NPC : MonoBehaviour, IInteractable
{
    public BoxCollider2D Dialoguebox;

    public bool isPlayerInRange;
    private GameObject dialogueobject;
    private DialogueManager dialogueUI;
    public DialogueData DialogueData;
    private int dialogueindex;
    private CancellationTokenSource _cts;

    public bool isDialogueRunning = false;
    private bool istyping = false;




    private void Start()
    {
        dialogueobject = GameObject.Find("DialogueManager");
        dialogueUI = DialogueManager.Instance;
    }

    void Update()
    {
        PauseController.SetPause(isDialogueRunning);
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
            
        
    }
    async UniTask StartDialogue()
    {
        isDialogueRunning = true;
        
        dialogueindex = 0;

        dialogueUI.giveinfotoNPC(DialogueData.npcname);//あとでspriteの変更も行う
        dialogueUI.ShowDialolgueUI(true);
        //そこにPauseControllerのpause関数とかを入れたりする。
        await DisplaycurrentLine();




    }

    async UniTask NextLine()
    {
        
        dialogueUI.Clearchoices();

        if (istyping)
        {
            _cts?.Cancel();
            return;
        }
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



    async UniTask TypeLine(CancellationToken token)
    {
        istyping = true;
        string fullText = DialogueData.DialogueLines[dialogueindex];
        dialogueUI.SetDialoguetext("");

        try
        {
            
            foreach (char letter in fullText)
            {
                dialogueUI.sentenceText.text += letter;
                dialogueUI.SetDialoguetext(dialogueUI.sentenceText.text);

                Debug.Log(dialogueUI.sentenceText.text);
                await UniTask.Delay(60, cancellationToken: token);

            }
            istyping = false;
        }
        catch (System.OperationCanceledException)
        {
            dialogueUI.SetDialoguetext(fullText);
        }
        finally
        {
            istyping = false;
        }
    }

    void Enddialogue()
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
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
        }
        _cts = new CancellationTokenSource();

        await TypeLine(_cts.Token);
    }

    private void OnDestroy()
    {
        // オブジェクト破棄時のメモリリーク対策
        _cts?.Dispose();
    }
}
