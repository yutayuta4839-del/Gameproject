using UnityEngine;

public class Samplepuzzle : InteractPuzzule, Iwindowprovider
{
    [SerializeField] GameObject puzzlewindow;
    public GameObject Window => puzzlewindow;
}
