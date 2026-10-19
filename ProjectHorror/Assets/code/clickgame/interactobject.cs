using Cysharp.Threading.Tasks;
using UnityEngine;

public class interactobject : MonoBehaviour, ISearchInteractable
{

    private bool isinteractingobj;

    public virtual bool CanInteract()       //playerclassの入力系処理に対して、インタラクトできたかを返す。
    {
        return !isinteractingobj; //仮置き
    }

    public virtual void SearchInteract()
    {

        if (isinteractingobj)
        {
            Endsearch();
        }
        else
        {
            Startsearch();
        }
    }

    public void Startsearch()
    {
        isinteractingobj = true;
        //pausecontroller.requestpause();//後で変える
        Debug.Log("探索ウィンドウを出現させる");
    }

    public void Endsearch()
    {
        isinteractingobj = false;
        //pausecontroller.releasepause();//後で変える
        Debug.Log("出した探索ウィンドウを破壊する");
    }


}
