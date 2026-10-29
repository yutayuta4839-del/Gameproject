using TMPro;
using UnityEngine;

public class NPCRobot : InteractNPC , IDialogueDataprovider , Iwindowprovider
{
   
    [SerializeField] GameObject npcsearchwindow;
    public GameObject Window => npcsearchwindow;

    [SerializeField] DialogueData dialogue;
    public DialogueData Interfacedialoguedata => dialogue;


    
 

   

}
