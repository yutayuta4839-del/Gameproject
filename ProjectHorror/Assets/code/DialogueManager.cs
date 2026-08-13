using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI要素")]
    public GameObject dialoguePanel;
    public GameObject nametext;
    public GameObject sentence;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sentenceText;
    

    public static DialogueManager Instance { get; private set; } //シングルトンパターン
    public bool isDialogueRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }
    public async UniTask StartDialogue(DialogueData data)
    {
       
        isDialogueRunning = true;

        dialoguePanel.SetActive(true);
        nametext.SetActive(true);
        sentence.SetActive(true);

        nameText.text = data.charname;

        foreach (string sentence in data.sentences)
        {
            sentenceText.text = sentence;
            Debug.Log(sentence);
            await WaitClick();
        }

        await UniTask.Delay(100);

        dialoguePanel.SetActive(false);
        nametext.SetActive(false);
        sentence.SetActive(false);
        nameText.text = "";
        sentenceText.text = "";
        isDialogueRunning = false;
      
    }

    private async UniTask WaitClick()
    {
        await UniTask.Delay(300);
        await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);

       
    }
}

