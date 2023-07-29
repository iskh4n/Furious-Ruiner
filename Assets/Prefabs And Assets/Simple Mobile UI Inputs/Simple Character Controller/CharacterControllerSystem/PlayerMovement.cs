using UI_InputSystem.Base;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform, groundChecker;

    [SerializeField]
    private CharacterController controllerPlayer;

    [SerializeField]
    [Range(2f, 10f)]
    private float playerHorizontalSpeed = 8;

    [SerializeField]
    private bool useGravity = true;

    [SerializeField]
    [Range(-50f, -9.8f)]
    private float gravityValue = -10;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float groundDistance = 0.5f;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private bool allowedJumping = true;

    [SerializeField]
    private float jumpHeight = 2;

    private float JumpForce => Mathf.Sqrt(jumpHeight * -2f * gravityValue);
    private Vector3 gravityVelocity;
    
    //------------------
    private Vector3 movDir;
    public float desiredRotationSpeed = 0.01f;
    public float maxTurnAngle = 45.0f; // maksimum dönüş açısı


    public bool Grounded => Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

    private void OnEnable()
    {
       // UIInputSystem.ME.AddOnTouchEvent(ButtonAction.Jump, ProcessJumping);
    }

    private void OnDisable()
    {
     //   UIInputSystem.ME.RemoveOnTouchEvent(ButtonAction.Jump, ProcessJumping);
    }
    
    private void FixedUpdate()
    {

        MovePlayer();

        CalculateGravity();
///-------------------------------------------------------------//
        movDir = transform.forward * playerHorizontalSpeed;
        controllerPlayer.Move(movDir*Time.deltaTime);

    }
    private Vector3 PlayerMovementDirection()
    {
         var baseDirection = (playerTransform.right * UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) + playerTransform.forward * UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement))*playerHorizontalSpeed*Time.deltaTime;
         //var baseDirection = playerTransform.right * input.x + playerTransform.forward * input.z;
         //  baseDirection *= playerHorizontalSpeed * Time.deltaTime;
         // playerTransform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(baseDirection), desiredRotationSpeed);
         playerTransform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(baseDirection), desiredRotationSpeed);

         return baseDirection;
       
    }

    private void MovePlayer()
    {
        if (!playerTransform) return;
        controllerPlayer.Move(PlayerMovementDirection());

    }

    private void CalculateGravity() 
    {
        if (!useGravity) return;
        if (!groundChecker) return;

        ResetGravityIfGrounded();
        ApplyGravity();
    }

    private void ProcessJumping()
    {
        if (!allowedJumping) return;

        if (Grounded)      
            gravityVelocity.y = JumpForce;      
    }

    private void ApplyGravity()
    {
        gravityVelocity.y += gravityValue * Time.deltaTime;
        controllerPlayer.Move(gravityVelocity * Time.deltaTime);
    }

    private void ResetGravityIfGrounded()
    {
        if (Grounded && gravityVelocity.y < 0)
            gravityVelocity.y = -1.5f;
    }
    
    


  





}