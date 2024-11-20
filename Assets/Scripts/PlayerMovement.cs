using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;

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

    [SerializeField]
    private LayerMask groundMask;

    private bool isCharging;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        material = viewmodelRenderer.materials[2];
        cam = Camera.main;
    }
    private void Update()
    {
        HandleShooting();
        HandleColorChange();

        if(transform.position.y < -10)
        transform.position = Vector3.up;
    }
    private void FixedUpdate()
    {
        HandleGroundCheck();
        HandleFOVDistortion();
    }

    private void HandleShooting()
    {
        shootTime = Mathf.MoveTowards(shootTime, 0, Time.deltaTime);

        if (shotCount == 0)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
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

        if (hit.distance < 0.1f)
            grounded = true;
        else
            grounded = false;


        if (grounded && shootTime == 0)
        {
            shotCount = 2;
        }
    }

    private void HandleFOVDistortion()
    {
        float dotProduct = Vector2.Dot(rb.velocity, CameraDirection()) / 20;

        cam.fieldOfView =
        Mathf.MoveTowards(cam.fieldOfView,
        Mathf.LerpUnclamped(baseFOV, speedFOV, Easing.Smooth7.Step(dotProduct)),
        Time.deltaTime * 15);
    }

    private Vector3 CameraDirection()
    {
        return Camera.main.transform.forward;
    }

}