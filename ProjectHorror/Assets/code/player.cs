using UnityEngine;
using UnityEngine.InputSystem;


public class player : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction moveaction;

    private Vector2 movevalue;
    private Animator moveanimator;
    private Rigidbody2D playerrigidbody;
    private Vector2 movement;
    public Sprite NormalSprite;
    public Sprite backPlayerSprite;
    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;

    [SerializeField] float speed = 5;
    private SpriteRenderer playerrenderer;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void Disenable()
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

        Debug.Log(movement);

        if (movement.x > 0.01f)
        {
            playerrenderer.sprite = rightPlayerSprite;
        }
        else if(movement.x < -0.01f)
        {
            playerrenderer.sprite = leftPlayerSprite;
        }

        if (movement.y > 0.01f)
        {
            playerrenderer.sprite = backPlayerSprite;
        }
        else if(movement.y < -0.01f)
        {
            playerrenderer.sprite = NormalSprite;
        }

       
    }

    private void playerwalk()
    {
        playerrigidbody.linearVelocity = movevalue * speed;

    }


}
