using UnityEngine;
using FMODUnity;
using FMODUnityResonance;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private int shotCount;
    private float charge;
    private Camera cam;

    private bool grounded;
    private float shootTime;
    private Material material;

    [SerializeField] private MeshRenderer viewmodelRenderer;

    [SerializeField] private Animator viewmodelAnimator;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private float recoil;

    [SerializeField] private float baseFOV;

    [SerializeField] private float speedFOV;

    [SerializeField] private float airDrag;
    [SerializeField] private float groundedDrag;

    [SerializeField]
    private float maxWindSpeed = 25;

    [Header("Event References")]
    [SerializeField] private StudioEventEmitter shotgunSound;
    [SerializeField] private StudioEventEmitter landSound;
    [SerializeField] private StudioEventEmitter reloadSound;

    private StudioEventEmitter ambientEmitter;

    [SerializeField]

    private LayerMask groundMask;
    private StudioEventEmitter eventEmitter;

    private bool isCharging;

    [SerializeField]
    private StudioEventEmitter windEmitter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        eventEmitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        material = viewmodelRenderer.materials[2];
        cam = Camera.main;

        ambientEmitter = GameObject.Find("Ambience").GetComponent<StudioEventEmitter>();

        windEmitter.Play();
    }

    private void Update()
    {
        HandleShooting();
        HandleColorChange();

        if (transform.position.y < -10)
        {
            transform.position = Vector3.up;
            rb.velocity = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        HandleGroundCheck();
        HandleFOVDistortion();

        HandleWindSpeed();

    }

    private void HandleShooting()
    {
        shootTime = Mathf.MoveTowards(shootTime, 0, Time.deltaTime);

        if (shotCount == 0)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            shotgunSound.Play();
            isCharging = true;
        }
        if (Input.GetButtonUp("Fire1") || Input.GetButtonDown("Fire2"))
        {
            isCharging = false;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            charge = 0;
        }

        if (isCharging)
        {
            charge = Mathf.MoveTowards(charge, 1, Time.deltaTime * 1.5f);
        }

        if (shootTime > 0)
            return;

        if (Input.GetButtonUp("Fire1") && charge > 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
            rb.AddForce(-CameraDirection() * (recoil * charge), ForceMode.Impulse);
            viewmodelAnimator.Play("Shoot");
            shotCount--;
            shootTime = 0.2f;
            // Debug.Log(charge);
            // Debug.Log($"Charge and recoil: {charge * recoil}");
            charge = 0;

            shotgunSound.Stop();
        }
    }

    private void HandleColorChange()
    {
        material.SetColor("_BaseColor", Color.LerpUnclamped(Color.red, Color.green, charge));
        material.SetColor("_EmissionColor", Color.LerpUnclamped(Color.red, Color.green, charge));
    }

    private void HandleGroundCheck()
    {
        RaycastHit hit;
        Physics.Raycast(groundCheck.position, Vector3.down, out hit, 1000f, groundMask);
        bool previousGround = grounded;

        if (hit.distance < 0.1f)
            grounded = true;
        else
            grounded = false;

        if (grounded)
        {
            rb.drag = groundedDrag;

            //  RELOAD
            if (shootTime == 0)
            {
                if (shotCount != 2)
                {
                    if(isCharging)
                        return;
                    reloadSound.Play();
                }
                shotCount = 2;
            }
        }

        else
        {
            rb.drag = airDrag;
        }

        //  GROUNDED BUT ONLY RUNS FIRST FRAME
        if (!previousGround && grounded)
        {
            landSound.Play();
        }
    }

    private void HandleFOVDistortion()
    {
        float dotProduct = Vector2.Dot(rb.velocity, CameraDirection()) / 20;

        cam.fieldOfView =
        Mathf.MoveTowards(cam.fieldOfView,
        Mathf.LerpUnclamped(baseFOV, speedFOV, Easing.Smooth7.Step(Mathf.Max(dotProduct, 0))),
        Time.deltaTime * 15);
    }

    private Vector3 CameraDirection()
    {
        return Camera.main.transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinTrigger"))
        {
            Debug.Log("Hestsaus");
            ambientEmitter.Stop();
        }
    }

    private void HandleWindSpeed()
    {
        windEmitter.SetParameter("Speed", rb.velocity.magnitude / maxWindSpeed);
    }
}