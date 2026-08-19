using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.Animations;

public class DialogueManager : MonoBehaviour
{
    [Header("UI要素")]
    public GameObject dialoguePanel;
    public GameObject nametext;
    public GameObject sentenceobj;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sentenceText;
    public Transform ChoiceContainer;
    public GameObject ChoiceButtonprefab;

    public static DialogueManager Instance { get; private set; } //シングルトンパターン


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }



    public void ShowDialolgueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void giveinfotoNPC(string Charaname)//あとでspriteの変数も追加
    {
        nameText.text = Charaname;
        //あとでスプライトの変更の設定も加ええる
    }

    public void SetDialoguetext(string text)
    {
        sentenceText.text = text;
    }


    public void Clearchoices()
    {
        foreach (Transform child in ChoiceContainer) Destroy(child.gameObject);
    }

    public GameObject CreateChocieButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(ChoiceButtonprefab, ChoiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
        return choiceButton;
    }
}

