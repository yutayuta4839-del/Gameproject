using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

public class interactobject : MonoBehaviour , ISearchInteractable
{
    private bool issearching;
    private GameObject canvasObj;
    private RectTransform canvasrectTransform;
    private GameObject spownedinteractObject;

    public virtual bool CanInteract()       //playerclassの入力系処理に対して、インタラクトできたかを返す。
    {
        return !issearching; //仮置き
    }

    public virtual void SearchInteract(GameObject Searchwindow)
    {
        if (issearching)
        {
            EndSearch(Searchwindow);
        }
        else
        {
            StartSearch(Searchwindow);
        }
    }

    public void StartSearch(GameObject Searchobjectwindow)　　//interactfunc引数Searchwindowと同義
    {
        issearching = true;
                            //pausecontroller.requestpause();//後で変える
        canvasObj = GameObject.FindWithTag("Canvas");
        canvasrectTransform = canvasObj.GetComponent<RectTransform>();
        spownedinteractObject = Instantiate(Searchobjectwindow, canvasrectTransform);
    }


    public void EndSearch(GameObject SearchObjectwindow)
    {
        issearching = false;
        //pausecontroller.releasepause();//後で変える
        Destroy(spownedinteractObject);
    }

}
