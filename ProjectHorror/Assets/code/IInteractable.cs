using Cysharp.Threading.Tasks;
public interface IInteractable
{
    UniTask Interact();

    bool CanInteract();

}