using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIpuzzleManager : MonoBehaviour//puzzleを呼び出すクラス。他のクラスから、パズルを始めるかの反応を受け取る。
{
    private GameObject puzzleprefab; //puzzleのprefab配列作って、何個作るかも決める。
    GameObject canvasObj;
    private RectTransform canvasrectTransform;
   

    public static UIpuzzleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
   
    public GameObject ShowPuzzle(GameObject backgroundprefab)　//後々引数は番号にする。
    {
        canvasObj = GameObject.FindWithTag("Canvas");
        canvasrectTransform = canvasObj.GetComponent<RectTransform>();
        GameObject newobj = Instantiate(backgroundprefab, canvasrectTransform);
        return newobj;
    }

}
