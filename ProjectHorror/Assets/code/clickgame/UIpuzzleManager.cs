using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIpuzzleManager : MonoBehaviour
{
    private GameObject puzzleprefab;
    GameObject canvasObj;
    private RectTransform canvasrectTransform;
    

    public static UIpuzzleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
   
    public GameObject ShowPuzzle(GameObject backgroundprefab)
    {
        canvasObj = GameObject.FindWithTag("Canvas");
        canvasrectTransform = canvasObj.GetComponent<RectTransform>();
        GameObject newobj = Instantiate(backgroundprefab, canvasrectTransform);
        return newobj;
    }

}
