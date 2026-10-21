using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class InteractPuzzule : MonoBehaviour, ISearchInteractable
{
    private bool isinteractingpuzzle;
    private GameObject canvasObj;
    private RectTransform canvasrectTransform;
    GameObject spownedpuzzleObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual bool CanInteract()   //playerclassの入力系処理に対して、インタラクトできたかを返す
    {
        return !isinteractingpuzzle; //仮置き
    }



    public virtual void SearchInteract(GameObject puzzlewindow)
    {
        if (isinteractingpuzzle)
        {
            EndPuzzle(puzzlewindow);
        }
        else
        {
            StartPuzzle(puzzlewindow);
        }
    }

    public void StartPuzzle(GameObject Searchobjectwindow)　　//interactfunc引数Searchwindowと同義
    {
        isinteractingpuzzle = true;
        //pausecontroller.requestpause();//後で変える
        canvasObj = GameObject.FindWithTag("Canvas");
        canvasrectTransform = canvasObj.GetComponent<RectTransform>();
        spownedpuzzleObject = Instantiate(Searchobjectwindow, canvasrectTransform);
    }


    public void EndPuzzle(GameObject SearchObjectwindow)
    {
        isinteractingpuzzle = false;
        //pausecontroller.releasepause();//後で変える
        Destroy(spownedpuzzleObject);
    }
}
