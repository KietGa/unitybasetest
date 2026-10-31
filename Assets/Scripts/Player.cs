using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string SPEED = "Speed";
    private const string ISGROUNDED = "IsGrounded";
    private const string JUMP = "Jump";
    private const string VERTICALVELOCITY = "verticalVelocity";
    private const string DIE = "Die";
    private const string INTERACT = "Interact";

    [SerializeField] private Transform cameraTrans;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;

    private Animator animator;
    private CharacterController cc;
    private float verticalVelocity;

    public bool IsDeath { get; private set; }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsDeath) return;

        MoveAndJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DeathZone>())
        {
            Die();
        }
    }

    public void PlayInteractAnimation()
    {
        animator.SetTrigger(INTERACT);
    }


    private void MoveAndJump()
    {
        //Move
        float moveX = Input.GetAxisRaw(HORIZONTAL);
        float moveY = Input.GetAxisRaw(VERTICAL);

        Vector3 forward = cameraTrans.forward;
        Vector3 right = cameraTrans.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveY + right * moveX;
        moveDirection.Normalize();

        //Jump
        if (cc.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            animator.SetTrigger(JUMP);
        }

        verticalVelocity += gravity * Time.deltaTime;

        //Velocity
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = verticalVelocity;
        cc.Move(velocity * Time.deltaTime);
        Rotate(moveDirection);

        animator.SetFloat(SPEED, moveDirection.magnitude);
        animator.SetBool(ISGROUNDED, cc.isGrounded);
        animator.SetFloat(VERTICALVELOCITY, verticalVelocity);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    private void Die()
    {
        IsDeath = true;
        animator.SetTrigger(DIE);
        UIManager.Instance.SetLose();
    }
}
