using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class InteractionDetector : MonoBehaviour  
{
    private ISearchInteractable InteractableinRange = null;
    private GameObject InteractionIcon;
    Iwindowprovider windowclient;
    private GameObject window;
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISearchInteractable interactable) && interactable == InteractableinRange)
        {
            InteractableinRange = null;
            InteractionIcon.SetActive(false);
            window = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractableinRange?.SearchInteract(window);　
        }
    }


}
