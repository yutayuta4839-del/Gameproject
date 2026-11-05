using UnityEngine;
using UnityEngine.InputSystem;


public class playerManager : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction moveaction;
    private Animator moveanimator;
    private Rigidbody2D rb2D;
    private Vector2 movement;

    public Transform Flashlight;
    bool isMoving = false;

    [SerializeField] float speed = 5;

    [SerializeField] float playersanityvalue;

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
        DontDestroyOnLoad(gameObject);

        moveaction = InputSystem.actions.FindAction("Move");
        moveanimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
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
    }

    private void playerwalk()
    {
        movement = moveaction.ReadValue<Vector2>();
        rb2D.linearVelocity = movement * speed;

        
        ChangeAnimation();
    }

    private void ChangeAnimation()
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
