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
        if (Instance == null)
        {
            canvasObj = GameObject.FindWithTag("Canvas");
        }
Instance = this;
    }
   
    public GameObject ShowPuzzle(GameObject backgroundprefab)
    {
        
        GameObject newobj = Instantiate(backgroundprefab, canvasrectTransform);
        return newobj;
    }

}
