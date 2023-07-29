using UI_InputSystem.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    private HealthManagerScript healthManager;

    [SerializeField]
    private Transform playerTransform, groundChecker;

    [SerializeField]
    private CharacterController controllerPlayer;

    [SerializeField]
    [Range(2f, 50f)]
    public float playerHorizontalSpeed = 20f;

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
    private Vector3 moveDirection;
    public float desiredRotationSpeed = 100.0f;
    public float maxTurnAngle = 45.0f; // maksimum dönüş açısı
    
    public bool Grounded => Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);


    public float rotationSpeed = 10.0f; // Rotasyon hızı
    public float smoothingFactor = 0.5f; // Pürüzsüzleştirme faktörü (0-1 aralığında değer kullanın)
    /* private Vector3 smoothedAcceleration;
     private Quaternion initialRotation; // Başlangıç rotasyonu
     private Vector3 initialAccelerometerData; // Başlangıç ivmeölçer verileri

     private void OnEnable()
     {
         // UIInputSystem.ME.AddOnTouchEvent(ButtonAction.Jump, ProcessJumping);
     }

     private void OnDisable()
     {
         //   UIInputSystem.ME.RemoveOnTouchEvent(ButtonAction.Jump, ProcessJumping);
     }
   */
    private void Start()
    {
        //Input.gyro.enabled = true;
        //float x = Input.gyro.attitude.x;
        healthManager = GameObject.FindObjectOfType<HealthManagerScript>();

        /* initialRotation = playerTransform.rotation; // Başlangıç rotasyonunu kaydet
         initialAccelerometerData = Input.acceleration; // Başlangıç ivmeölçer verilerini kaydet*/
    }

    private void FixedUpdate()
      {
        

           MovePlayer();
           CalculateGravity();

        ///-------------------------------------------------------------//

         var baseDirection = (UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) * playerTransform.right + UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement) * playerTransform.forward) * playerHorizontalSpeed * Time.deltaTime;
         playerTransform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(baseDirection), desiredRotationSpeed);
         moveDirection = (playerTransform.forward) * playerHorizontalSpeed;
         controllerPlayer.Move(moveDirection * Time.deltaTime);
         //return baseDirection;
        
   

        /*Vector3 currentAccelerometerData = Input.acceleration; // Mevcut ivmeölçer verilerini al
        Vector3 accelerationDifference = currentAccelerometerData - initialAccelerometerData; // Başlangıç ivmeölçer verileri ile mevcut ivmeölçer verileri arasındaki farkı hesapla
        Quaternion rotation = Quaternion.Euler(0, accelerationDifference.x * rotationSpeed, 0); // Yatay ivmeölçer verisine göre rotasyonu hesapla

       playerTransform.rotation = initialRotation * rotation; // Başlangıç rotasyonunu ve hesaplanan rotasyonu birleştirerek karakterin rotasyonunu güncelle
        */



    }



   



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            Debug.Log("trigger calisiyor");

        }

        if (other.CompareTag("Finish"))
        {
            healthManager.healthScore = 0;
        }


    }

    private void MovePlayer()
    {
        // if (!playerTransform) return;
        // Debug.Log("çalışıyor");

        // controllerPlayer.Move(PlayerMovementDirection());


        if (playerTransform.position.y < 0f)
        {
            healthManager.healthScore = 0;
        }
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