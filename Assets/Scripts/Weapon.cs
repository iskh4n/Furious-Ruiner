
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //Recoil
    public Rigidbody playerRb;
    public float recoilForce;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    [SerializeField] private LayerMask groundMask;

    Animator animator;
    public float fireRate = 1;
    public float firetime, nextTime;

    public Transform muzzle;

    public float attackOffset = 1f; // Örneğin, karakterin boyutuna göre belirlenen bir değer


    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found in parent!");
        }
    }
    private void Update()
    {
        MyInput();

        firetime += Time.deltaTime;
        nextTime = 1 / fireRate;
        if (firetime >= nextTime)
        {
            Shoot();
            firetime = 0;
        }


        //Set ammo display, if it exists :D
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading 
       // if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        //if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Debug.Log("Shooting works");
            //Set bullets shot to 0
            bulletsShot = 0;
           // if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
           // {

           //  Shoot();
            
           
            //}
        }

    }

    public void Shoot()
    {
        readyToShoot = false;

         //Find the exact hit position using a raycast

         Ray ray = fpsCam.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z));
         // Ray ray = fpsCam.ScreenPointToRay(Input.touches[0].position); //mobil için

         RaycastHit hit;

         //check if ray hits something
         Vector3 targetPoint;
         if (Physics.Raycast(ray, out hit))
         {
             targetPoint = hit.point;
         }
         else
         {
             targetPoint = ray.GetPoint(75); //Just a point far away from the player

         }
         //Calculate direction from attackPoint to targetPoint
         Vector3 directionWithSpread = targetPoint - attackPoint.position;

         //Calculate spread
         float x = Random.Range(-spread, spread);
         float y = Random.Range(-spread, spread);
         //-----------------------------------------------------------


         //Instantiate bullet/projectile
         GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
         //Rotate bullet to shoot direction
         currentBullet.transform.forward = directionWithSpread.normalized;

         //Add forces to bullet
         currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
         currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

       
   

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null) muzzleFlash.GetComponent<ParticleSystem>().Play();//Instantiate(muzzleFlash, fpsCam.transform.position, Quaternion.identity);  

        bulletsLeft--;
        bulletsShot++;
        Destroy(currentBullet, 1f); 
        


        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add recoil to player (should only be called once)
           playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
            //playerRb.AddForce(-attackDirection.normalized * recoilForce, ForceMode.Impulse);

        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    
        }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;

    }
   


}
