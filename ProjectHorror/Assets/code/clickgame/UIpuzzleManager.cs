using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIpuzzleManager : MonoBehaviour//puzzle���Ăяo���N���X�B���̃N���X����A�p�Y�����n�߂邩�̔������󂯎��B
{
    private GameObject puzzleprefab; //puzzle��prefab�z�����āA����邩�����߂�B
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
