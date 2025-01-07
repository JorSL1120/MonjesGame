using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using static Unity.VisualScripting.Round<TInput, TOutput>;

public class Player : MonoBehaviour
{
    //Acceso al CharacterController del player
    private CharacterController CC;

    //Variables movimiento
    public int Speed = 5;
    private int Sprint = 2;
    public int SpeedRotation = 5;

    //Variables salto
    public float JumpForce = 2f;
    private float gravity = -9.81f;
    private float ySpeed = 0;
    private bool isGrounded;

    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovPlayer();
    }

    private void MovPlayer()
    {
        isGrounded = CC.isGrounded;

        if (isGrounded && ySpeed < 0)
        {
            ySpeed = -2f;
        }

        Vector3 movimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movimiento.Normalize();

        if (movimiento != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * SpeedRotation);
        }

        float currentSpeed = Input.GetAxis("Run") > 0 ? Speed * Sprint : Speed;
        Vector3 moveDirection = movimiento * currentSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ySpeed = Mathf.Sqrt(JumpForce * -2f * gravity);
        }

        ySpeed += gravity * Time.deltaTime;
        moveDirection.y = ySpeed;

        CC.Move(moveDirection * Time.deltaTime);
    }
}
