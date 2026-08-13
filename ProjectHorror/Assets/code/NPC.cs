using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    public BoxCollider2D Dialoguebox;
    
    public bool isPlayerInRange;
    private GameObject dialogueobject;
    private DialogueManager dialogueManager;
    public DialogueData firstDialogueData;
    public DialogueData secondDialogueData;

    private DialogueData DialogueData;


    private int dialoguecount = 0;

    private void Start()
    {
        dialogueobject = GameObject.Find("DialogueManager");
        dialogueManager = dialogueobject.GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
          
            if (isPlayerInRange)
            {
                Transform actionmark = collision.transform.Find("actionmark");
                actionmark.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
        Transform actionmark = collision.transform.Find("actionmark");
        actionmark.gameObject.SetActive(false);
    }

    private async void Update()
    {
        if (isPlayerInRange && Mouse.current.leftButton.wasPressedThisFrame && !dialogueManager.isDialogueRunning)
        {
            switch(dialoguecount)
            {
                case 0:
                    DialogueData = firstDialogueData;
                    dialoguecount++;
                    break;
                case 1:
                    DialogueData = secondDialogueData;
                    break;
            }
            await DialogueManager.Instance.StartDialogue(DialogueData);        
        }

    }
}
