//using Unity.VisualScripting;
//using UnityEngine;
//using Cysharp.Threading.Tasks;
//using UnityEngine.InputSystem;
//using System.Threading;
//using Unity.VisualScripting.Antlr3.Runtime;

//public class NPC : MonoBehaviour
//{
//    public BoxCollider2D Dialoguebox;

//    public bool isPlayerInRange;
//    private GameObject dialogueobject;
//    private DialogueManager dialogueUI;
//    //public DialogueData DialogueData;
//    private int dialogueindex;
//    //private CancellationTokenSource _cts;

//    public bool isDialogueRunning = false;
//    private bool istyping = false;




//    private void Start()
//    {
//        dialogueobject = GameObject.Find("DialogueManager");
//        //dialogueUI = DialogueManager.Instance;
//    }
   
         
            
        
    
//    void StartDialogue(DialogueData dialoguedata)
//    {
//        //isDialogueRunning = true;
//        // NPCの会話開始時
//        PauseController.RequestPause();

//        // NPCの会話終了時
        
//        dialogueindex = 0;

//        dialogueUI.giveinfotoNPC(dialoguedata);//あとでspriteの変更も行う
//        dialogueUI.ShowDialolgueUI(true);
//        //そこにPauseControllerのpause関数とかを入れたりする。
//        DisplaycurrentLine();
//    }

//    void NextLine(DialogueData dialoguedata)
//    {
        
//        dialogueUI.Clearchoices();

        
//        if (dialoguedata.EndDialogueLines.Length > dialogueindex && dialoguedata.EndDialogueLines[dialogueindex])
//        {
//            Enddialogue();
//            return;
//        }

//        foreach (DialogueChoice dialogueChoice in dialoguedata.choices)
//        {
//            if (dialogueChoice.dialogueindex == dialogueindex)
//            {
//                Displaychoices(dialogueChoice);
//                return;
//            }
//        }

//        foreach (DialogueJump jump in dialoguedata.jumps)
//        {
//            if (jump.fromIndex == dialogueindex)
//            {
//                dialogueindex = jump.toIndex;
//                 DisplaycurrentLine();
//                return;
//            }
//        }


//        if (dialogueindex + 1 < dialoguedata.DialogueLines.Length)
//        {
//            ++dialogueindex;

//             DisplaycurrentLine();
//        }
//        else
//        {
//            Enddialogue();
//        }
//    }



//    void TypeLine(DialogueData dialoguedata)
//    {
//        istyping = true;
//        string fullText = dialoguedata.DialogueLines[dialogueindex];
//        dialogueUI.SetDialoguetext("");

//        try
//        {
            
//            foreach (char letter in fullText)
//            {
//                dialogueUI.sentenceText.text += letter;
//                dialogueUI.SetDialoguetext(dialogueUI.sentenceText.text);

//                Debug.Log(dialogueUI.sentenceText.text);
//           　   //非同期時間を経過させる

//            }
//            istyping = false;
//        }
//        catch (System.OperationCanceledException)
//        {
//            dialogueUI.SetDialoguetext(fullText);
//        }
//        finally
//        {
//            istyping = false;
//        }
//    }

//    void Enddialogue()
//    {
//        isDialogueRunning = false;
//        PauseController.ReleasePause();
//        dialogueUI.SetDialoguetext("");
//        dialogueUI.ShowDialolgueUI(false);
//        dialogueUI.Clearchoices();
//        //Pause解除関数


//    }

//    void Displaychoices(DialogueChoice choice)
//    {

//        for (int i = 0; i < choice.choices.Length; i++)
//        {
//            int nextindex = choice.nextdialogueindexs[i];
//            dialogueUI.CreateChocieButton(choice.choices[i], () => Chooseoption(nextindex,dialoguedata));
//        }
//    }

//     void Chooseoption(int nextidex,DialogueData dialoguedata)
//    {
//        dialogueindex = nextidex;
//        dialogueUI.Clearchoices();
//         DisplaycurrentLine();
//        if (dialoguedata.EndDialogueLines.Length > dialogueindex && dialoguedata.EndDialogueLines[dialogueindex])
//        {
//            Enddialogue();
//        }

//    }

//    void DisplaycurrentLine()
//    {
//         TypeLine();
//    }

   
//}
