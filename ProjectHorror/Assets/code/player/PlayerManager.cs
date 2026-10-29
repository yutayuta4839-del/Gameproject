using UnityEngine;
using UnityEngine.InputSystem;


public class playerManager : MonoBehaviour
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
    public Transform Flashlight;
    public static playerManager Instance;
    bool isMoving = false;

  

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
        if (Instance != null && Instance != this)
        {
            // すでに存在する場合は自分を破棄(重複防止)
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //dialogueManager = DialogueManager.Instance;

        moveaction = InputSystem.actions.FindAction("Move");
        moveanimator = GetComponent<Animator>();

        dialogueobject = GameObject.Find("DialogueManager");
      

        moveanimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        playerrenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
      
       
        if (PauseController.IsGamePosed)
        {
            
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

        isMoving = movement.sqrMagnitude > 0.0001f;
        moveanimator.SetBool("isMoving", isMoving);

        if (!isMoving) return;

        if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
        {
            float x = Mathf.Sign(movement.x);
            moveanimator.SetFloat("MoveX", x);
            moveanimator.SetFloat("MoveY", 0f);
            RotateLight(x > 0 ? 90 : 270);

           

            //if (x > 0)
            //{
            //    angle = 90;
            //}
            //else
            //{
            //    angle = 270;
            //}
        }
        else
        {
            float y = Mathf.Sign(movement.y);
            moveanimator.SetFloat("MoveX", 0f);
            moveanimator.SetFloat("MoveY", y);
            RotateLight(y > 0 ? 180 : 360);

        }






    }

    private void RotateLight(int angle)
    {
        Flashlight.localEulerAngles = new Vector3(0, 0, angle);
    }
}
