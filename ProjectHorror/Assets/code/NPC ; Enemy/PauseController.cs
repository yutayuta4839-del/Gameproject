using UnityEngine;

public class PauseController : MonoBehaviour
{
    private static int pauseRequestCount = 0;
    public static bool IsGamePosed => pauseRequestCount > 0;//自動的に、数値を読んで、true/falseに変化させる。count0>0の場合、false,count1>0の場合true,１課０しかないので可能。
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetOnLoad()
    {
        pauseRequestCount = 0;
    }
    public static void RequestPause() { pauseRequestCount++; }
    public static void ReleasePause() { pauseRequestCount = Mathf.Max(0, pauseRequestCount - 1); }

}