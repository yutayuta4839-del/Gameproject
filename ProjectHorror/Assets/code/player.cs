using UnityEngine;
using UnityEngine.InputSystem;


public class player : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction moveaction;
    private InputAction clickaction;

    private Vector2 movevalue;
    private Animator moveanimator;
    private Rigidbody2D rb2D;
    private Vector2 movement;
    public Sprite NormalSprite;
    public Sprite backPlayerSprite;
    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;
    private GameObject dialogueobject;
    private DialogueManager dialogueManager;

    [SerializeField] float speed = 5;
    private SpriteRenderer playerrenderer;


    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        dialogueManager = DialogueManager.Instance;

        moveaction = InputSystem.actions.FindAction("Move");
        moveanimator = GetComponent<Animator>();

        dialogueobject = GameObject.Find("DialogueManager");
        dialogueManager = dialogueobject.GetComponent<DialogueManager>();

        moveanimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        playerrenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (PauseController.IsGamePosed)
        {
            Debug.Log("停止した");
            rb2D.linearVelocity = Vector2.zero;
            //アニメーションwalkingをfalseにする。
            return;
        }
      
        playerwalk();
        //そしてこの下に、速度が0.001以上あるとアニメーションを再開するようにする。magnitudeを使う。



    }

    private void playerwalk()
    {
        movement = moveaction.ReadValue<Vector2>();
        rb2D.linearVelocity = movement * speed;
        ChangeAnimation();
    }

    private void ChangeAnimation()// I change that sprite to animation
    {

        if (movement.x > 0.01f)
        {
            playerrenderer.sprite = rightPlayerSprite;
        }
        else if (movement.x < -0.01f)
        {
            playerrenderer.sprite = leftPlayerSprite;
        }

        if (movement.y > 0.01f)
        {
            playerrenderer.sprite = backPlayerSprite;
        }
        else if (movement.y < -0.01f)
        {
            playerrenderer.sprite = NormalSprite;
        }
    }

}
