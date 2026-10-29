using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;

public class InteractionDetector : MonoBehaviour
{
    private ISearchInteractable InteractableinRange = null;
    private IDialogueableInteractable DialogueInteractRange = null;
    private GameObject InteractionIcon;
    Iwindowprovider windowclient;
    IDialogueDataprovider dialoguedataclient;
    private GameObject window;

    private DialogueData dialoguedetectedata;
    private GameObject interactedobj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform actionmarkChild = transform.GetChild(0);
        InteractionIcon = actionmarkChild.gameObject;
        InteractionIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {          //ここで、interactableにcollisionのInteractableComponentを渡している。コンポーネントが見つかったかどうか,見つかったコンポーネントの本体を手に入れるための二つの役割をこなす。
        if (collision.TryGetComponent(out ISearchInteractable interactable) && interactable.CanInteract())
        {


            InteractableinRange = interactable;


            InteractionIcon.SetActive(true);
            collision.TryGetComponent(out Iwindowprovider windowprovided);
            windowclient = windowprovided;
            if (window == null)
            {

                window = windowclient.Window;
            }

        }

        if (collision.TryGetComponent(out IDialogueableInteractable Dialogueable) && Dialogueable.CanDialogue())
        {

            DialogueInteractRange = Dialogueable;
            InteractionIcon.SetActive(true);
            collision.TryGetComponent(out IDialogueDataprovider dialoguedataprovided);
            dialoguedataclient = dialoguedataprovided;
            if (dialoguedetectedata == null)
            {
                dialoguedetectedata = dialoguedataclient.Interfacedialoguedata;
               
            }
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISearchInteractable interactable) && interactable == InteractableinRange)
        {
            InteractableinRange = null;
            InteractionIcon.SetActive(false);
            window = null;
        }

        if (collision.TryGetComponent(out IDialogueableInteractable Dialogueable) && Dialogueable == DialogueInteractRange)
        {
            DialogueInteractRange = null;
            InteractionIcon.SetActive(false);
        }
    }



    public void OnInteract(InputAction.CallbackContext searchcontext)
    {

        if (searchcontext.performed)
        {

            InteractableinRange?.SearchInteract(window);

        }
    }

    public void OnDialogue(InputAction.CallbackContext dialoguecontext)
    {
        if (dialoguecontext.performed)
        {
            DialogueInteractRange?.DialogueInteract(dialoguedetectedata);
        }
    }
}
