using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    public BoxCollider2D Dialoguebox;
    public DialogueData dialogueData;
    public bool isPlayerInRange;

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
        if (isPlayerInRange && Mouse.current.leftButton.wasPressedThisFrame)
        await DialogueManager.Instance.StartDialogue(dialogueData);
    }
}
