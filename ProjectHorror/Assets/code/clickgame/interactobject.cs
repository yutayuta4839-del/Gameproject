using Cysharp.Threading.Tasks;
using UnityEngine;

public class interactobject : MonoBehaviour, IInteractable
{
    public GameObject prefab;
    UIpuzzleManager puzzlemanege;
    private GameObject spawnedPuzzleObj;
    [SerializeField] public bool ispuzzling = false;

    void Awake()
    {
        puzzlemanege = UIpuzzleManager.Instance;
    }

    void Update()
    {
       
       
    }


    public bool CanInteract()
    {
        return !ispuzzling;
    }

    public UniTask Interact()
    {
       

        if (ispuzzling)
        {
            Endpuzzle();
        }
        else
        {
            if (!CanInteract()) return UniTask.CompletedTask;
            Startpuzzle();
        }

        return UniTask.CompletedTask;
    }

    public void Startpuzzle()
    {
        ispuzzling = true;
        PauseController.RequestPause();
        spawnedPuzzleObj = puzzlemanege.ShowPuzzle(prefab);//��Ŕԍ�
    }

    public void Endpuzzle()
    {
        Debug.Log("�Ă΂ꂽ");
        ispuzzling = false;
        PauseController.ReleasePause();
        Destroy(spawnedPuzzleObj);
    }


}
