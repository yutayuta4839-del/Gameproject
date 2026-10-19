using UnityEngine;

public class InteractPuzzule : MonoBehaviour, ISearchInteractable
{
    private bool isinteractingpuzzle;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual bool CanInteract()   //playerclassの入力系処理に対して、インタラクトできたかを返す
    {
        return isinteractingpuzzle; //仮置き
    }

 

    public virtual void SearchInteract()
    {

    }
}
