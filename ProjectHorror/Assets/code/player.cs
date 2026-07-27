using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction moveaction;

    private Vector2 movevalue;
    private Animator moveanimator;
    private Rigidbody2D playerrigidbody;

    [SerializeField] float speed = 5;

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
        playerrigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        movevalue = moveaction.ReadValue<Vector2>();
        playerwalk();

    }

    private void playerwalk()
    {
        playerrigidbody.linearVelocity = movevalue * speed;
      
    }
}
