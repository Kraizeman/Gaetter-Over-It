using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    private Vector3 velocity;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        Vector3 wishDir = forward * input.y +right *input.x;
        wishDir.y = 0;
        wishDir.Normalize();

        velocity.x = Mathf.MoveTowards(velocity.x, wishDir.x * moveSpeed, acceleration * Time.deltaTime);
        velocity.z = Mathf.MoveTowards(velocity.z, wishDir.z * moveSpeed, acceleration * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }
}