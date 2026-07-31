using UnityEngine;
using UnityEngine.InputSystem;


public class player : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction moveaction;
    private InputAction clickaction;

    private Vector2 movevalue;
    private Animator moveanimator;
    private Rigidbody2D playerrigidbody;
    private Vector2 movement;
    public Sprite NormalSprite;
    public Sprite backPlayerSprite;
    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;
    bool isPlayerInRange;

    [SerializeField] float speed = 5;
    private SpriteRenderer playerrenderer;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void Disable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveaction = InputSystem.actions.FindAction("Move");
        moveanimator = GetComponent<Animator>();

        moveanimator = GetComponent<Animator>();
        playerrigidbody = GetComponent<Rigidbody2D>();

         playerrenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        movevalue = moveaction.ReadValue<Vector2>();
        playerwalk();
        movement = moveaction.ReadValue<Vector2>();



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

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isPlayerInRange)
            {

                Debug.Log("osare");
                
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            isPlayerInRange = true;
            Debug.Log("NPCに近づきました。クリックすると話せます。");
            // 画面に「[クリックで話す]」などのUIを表示する処理をここに書いてもOKです
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            isPlayerInRange = false;
            Debug.Log("NPCから離れました。");
            // UIを非表示にする処理などをここに書きます
        }
    }
    private void playerwalk()
    {
        playerrigidbody.linearVelocity = movevalue * speed;

    }

   
}
