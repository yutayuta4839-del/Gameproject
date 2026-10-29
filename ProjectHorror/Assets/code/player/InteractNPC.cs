using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class InteractNPC : MonoBehaviour, ISearchInteractable, IDialogueableInteractable
{
    private bool isDialogueRunning = false;
    private bool istyping = false;
    private int dialogueindex = 0;

    private bool isnpcsearching = false;
    GameObject canvasObj;
    private RectTransform canvasrectTransform;
    private GameObject spownedinteractObject;

    private bool isinteractingnpc;
    private bool isinteractingdualogue;
    public DialogueManager dialogueUI;
    public float typeInterval = 0.5f;
    private Coroutine typingCoroutine;

    //------------------------------------------------
    //Search interface
    //------------------------------------------------
    public virtual bool CanInteract()    //playerclassの入力系処理に対して、インタラクトできたかを返す
    {
        return !isinteractingnpc;　//仮置き
    }

    public virtual void SearchInteract(GameObject NPCSearchwindow)
    {
        if (isnpcsearching)
        {
            EndNPCSearch(NPCSearchwindow);
        }
        else
        {
           
            StartNPCSearch(NPCSearchwindow);
        }
    }

    //------------------------------------------------
    //Dialogue interface
    //------------------------------------------------
    public virtual bool CanDialogue()　　　　　//playerclassの入力系処理に対して、インタラクトできたかを返す
    {
        return !isinteractingdualogue;//仮置き
    }

    public virtual void DialogueInteract(DialogueData dialoguedata)
    {
       
        if (dialoguedata == null)//そしてpauseしていて、!dialoguerunnningな場合...
        {
            return;
        }

        if (isDialogueRunning)
        {
           
            NextLine(dialoguedata);
        }
        else
        {
           
            StartDialogue(dialoguedata);
        }
    }

    public void StartNPCSearch(GameObject Searchobjectwindow)　　//interactfunc引数Searchwindowと同義
    {
        isnpcsearching = true;
        //pausecontroller.requestpause();//後で変える
        canvasObj = GameObject.FindWithTag("Canvas");
        canvasrectTransform = canvasObj.GetComponent<RectTransform>();
        spownedinteractObject = Instantiate(Searchobjectwindow, canvasrectTransform);
    }


    public void EndNPCSearch(GameObject SearchObjectwindow)
    {
        isnpcsearching = false;
        //pausecontroller.releasepause();//後で変える
        Destroy(spownedinteractObject);
    }

    void StartDialogue(DialogueData dialoguedata)
    {
        isDialogueRunning = true;
        // NPCの会話開始時
        //PauseController.RequestPause();

        // NPCの会話終了時

        dialogueindex = 0;

        dialogueUI.giveinfotoNPC(dialoguedata);//あとでspriteの変更も行う
        dialogueUI.ShowDialolgueUI(true);
        //そこにPauseControllerのpause関数とかを入れたりする。

        StartCoroutine(TypeLine(dialoguedata));
    }

    void NextLine(DialogueData dialoguedata)
    {

        if (istyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialoguetext(dialoguedata.DialogueLines[dialogueindex]);
            istyping = false;
            return;
        }
        dialogueUI.Clearchoices();


        if (dialoguedata.EndDialogueLines.Length > dialogueindex && dialoguedata.EndDialogueLines[dialogueindex])
        {
            Enddialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in dialoguedata.choices)
        {
            if (dialogueChoice.dialogueindex == dialogueindex)
            {
                Displaychoices(dialogueChoice, dialoguedata);
                return;
            }
        }

        foreach (DialogueJump jump in dialoguedata.jumps)
        {
            if (jump.fromIndex == dialogueindex)
            {
                dialogueindex = jump.toIndex;
                DisplayCurrentLine(dialoguedata);
                return;
            }
        }


        if (dialogueindex + 1 < dialoguedata.DialogueLines.Length)
        {
            ++dialogueindex;

            DisplayCurrentLine(dialoguedata);
        }
        else
        {
            Enddialogue();
        }
    }



    private IEnumerator TypeLine(DialogueData dialoguedata)
    {
        istyping = true;
        string fullText = dialoguedata.DialogueLines[dialogueindex];
        dialogueUI.SetDialoguetext("");



        foreach (char letter in fullText)
        {
            dialogueUI.sentenceText.text += letter;
            dialogueUI.SetDialoguetext(dialogueUI.sentenceText.text);

            Debug.Log(dialogueUI.sentenceText.text);
            yield return new WaitForSeconds(dialoguedata.typingspeed);

        }

        //SkipAlldialogue(fullText);

        istyping = false;


    }

    void Enddialogue()
    {

        StopAllCoroutines();
        isDialogueRunning = false;
        PauseController.ReleasePause();
        dialogueUI.SetDialoguetext("");
        dialogueUI.ShowDialolgueUI(false);
        dialogueUI.Clearchoices();
        //Pause解除関数


    }

    void Displaychoices(DialogueChoice choice, DialogueData dialoguedata)
    {

        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextindex = choice.nextdialogueindexs[i];
            dialogueUI.CreateChocieButton(choice.choices[i], () => Chooseoption(nextindex, dialoguedata));
        }
    }

    void Chooseoption(int nextidex, DialogueData dialoguedata)
    {
        dialogueindex = nextidex;
        dialogueUI.Clearchoices();
        DisplayCurrentLine(dialoguedata);

        if (dialoguedata.EndDialogueLines.Length > dialogueindex && dialoguedata.EndDialogueLines[dialogueindex])
        {
            Enddialogue();
        }

    }

    void DisplayCurrentLine(DialogueData dialoguedata)
{
    StopAllCoroutines();
    StartCoroutine(TypeLine(dialoguedata));
}

    void SkipAlldialogue(string text)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueUI.SetDialoguetext(text);
        istyping = false;
    }

    //void DisplaycurrentLine()
    //{
    //    TypeLine();
    //}



}
