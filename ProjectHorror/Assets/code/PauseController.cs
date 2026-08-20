using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePosed { get; private set; } = false;

    public static void SetPause(bool pause)
    {
        IsGamePosed = pause;
    }

}
