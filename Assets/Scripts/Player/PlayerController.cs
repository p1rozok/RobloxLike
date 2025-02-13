using UnityEngine;
using PlayerFSM;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;  
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Falling Settings")]
    [SerializeField] private float fallSpeed = 2f;

    private PlayerState currentState;

    private void Start() => ChangeState(new IdleState(this));

    private void Update()
    {
        currentState?.Update();
        animator.SetBool("Slow Run", IsMoving());
        if (!IsGrounded() && rb.linearVelocity.y < 0)
        {
            Vector3 v = rb.linearVelocity;
            v += Vector3.down * fallSpeed * Time.deltaTime;
            rb.linearVelocity = v;
        }
    }

    public bool IsGrounded() =>
        Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool IsMoving() =>
        Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

    public bool IsJumpPressed() =>
        Input.GetKeyDown(KeyCode.Space);

    public void Move()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        Vector3 camF = cameraTransform.forward; 
        camF.y = 0; 
        camF.Normalize();
        Vector3 camR = cameraTransform.right; 
        camR.y = 0; 
        camR.Normalize();

        Vector3 dir = (camF * v + camR * h).normalized;
        Vector3 velocity = dir * moveSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    public void Jump()
    {
        if (IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public Animator Animator => animator;
    public Rigidbody Rigidbody => rb;
}
