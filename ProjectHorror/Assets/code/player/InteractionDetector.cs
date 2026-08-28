using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable InteractableinRange = null;
    public GameObject InteractionIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractionIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {                                     //ここで、interactableにcollisionのInteractableComponentを渡している。コンポーネントが見つかったかどうか,見つかったコンポーネントの本体を手に入れるための二つの役割をこなす。
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            InteractableinRange = interactable;
            InteractionIcon.SetActive(true);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {  //InteractableinRangeがnull出なかったら...
            InteractableinRange?.Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out IInteractable interactable) && interactable == InteractableinRange)
        {
            InteractableinRange = null;
            InteractionIcon.SetActive(false);
        }
    }
}
