using UnityEngine;

public class NPCRobot : InteractNPC
{
    [SerializeField] GameObject searchwindow;
    public GameObject Window => searchwindow;
}
