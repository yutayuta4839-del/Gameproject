using Cysharp.Threading.Tasks;
using UnityEngine;

public class sampleinteractobject : MonoBehaviour,IInteractable
{
    public bool isopend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool CanInteract()
    {
        return !isopend;
    }

    public async UniTask Interact()
    {
        if (!CanInteract()) return;

        Debug.Log("Ç®Å[Ç¢");

    }
}
