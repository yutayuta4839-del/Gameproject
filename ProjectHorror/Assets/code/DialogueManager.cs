using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI要素")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sentenceText;

    public static DialogueManager Instance { get; private set; } //シングルトンパターン
    private bool isDialogueRunning = false;

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
        nameText.text = data.charname;

        foreach (string sentence in data.sentences)
        {
            sentenceText.text = sentence;
            Debug.Log(sentence);
            await WaitForClickAsync();
        }

        dialoguePanel.SetActive(false);
        isDialogueRunning = false;
    }

    private async UniTask WaitForClickAsync()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.8f));
        if (Mouse.current.leftButton.isPressed)
        {
            await UniTask.Yield();
        }
    }
}

