using StarterAssets;
using UnityEngine;
using Random = UnityEngine.Random;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")] public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")] [SerializeField]
    private Animator gunAnimator;

    [Header("Gun Sounds")] [SerializeField]
    private AudioSource gunAudioSource;

    [SerializeField] private AudioClip shotAudioClip;


    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")] [Tooltip("Specify time to destory the casing object")] [SerializeField]
    private float destroyTimer = 2f;

    [Tooltip("Bullet Speed")] [SerializeField]
    private float shotPower = 500f;

    [Tooltip("Casing Ejection Speed")] [SerializeField]
    private float ejectPower = 150f;

    [Tooltip("Rate of fire in rounds per minute")] [SerializeField]
    private int rateOfFire = 21;

    [Header("Inputs")] [SerializeField] private StarterAssetsInputs _inputs;

    private float _shootTimeDelta;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_shootTimeDelta <= 0)
        {
            if (_inputs.shoot)
            {
                //Calls animation on the gun that has the relevant animation events that will fire
                gunAnimator.SetTrigger("Fire");
                _shootTimeDelta = 1 / ((float)rateOfFire / 60);
            }
        }
        else
        {
            _shootTimeDelta -= Time.deltaTime;
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        {
            return;
        }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>()
            .AddForce(barrelLocation.forward * shotPower);

        if (gunAudioSource != null && shotAudioClip != null)
        {
            gunAudioSource.PlayOneShot(shotAudioClip);
        }
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        {
            return;
        }

        //Create the casing
        GameObject tempCasing =
            Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        var casingRigidbody = tempCasing.GetComponent<Rigidbody>();
        casingRigidbody.AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower),
            (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        casingRigidbody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)),
            ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
}