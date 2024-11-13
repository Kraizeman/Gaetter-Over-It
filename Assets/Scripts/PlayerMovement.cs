using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private int shotCount;
    private float charge;

    private bool grounded;
    private float shootTime;
    private Material material;

    [SerializeField] private MeshRenderer viewmodelRenderer;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private float recoil;

    [SerializeField]
    private LayerMask groundMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        material = viewmodelRenderer.materials[2];
    }
    private void Update()
    {
        HandleShooting();
        HandleColorChange();
    }
    private void FixedUpdate()
    {
        HandleGroundCheck();
    }

    private void HandleShooting()
    {
        shootTime = Mathf.MoveTowards(shootTime, 0, Time.deltaTime);

        if (shotCount == 0)
            return;

        if (Input.GetButton("Fire1"))
        {
            charge += Time.deltaTime;
        }
        if (shootTime > 0)
            return;

        if (Input.GetButtonUp("Fire1"))
        {
            rb.AddForce(CameraDirection() * (recoil * charge), ForceMode.Impulse);
            shotCount--;
            shootTime = 0.2f;
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

    private Vector3 CameraDirection()
    {
        return -Camera.main.transform.forward;
    }

}