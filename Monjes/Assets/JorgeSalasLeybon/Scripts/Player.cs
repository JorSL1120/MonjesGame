using Unity.VisualScripting;
using UnityEngine;
//using static Unity.VisualScripting.Round<TInput, TOutput>;

public class Player : MonoBehaviour
{
    private CharacterController CC;
    public int Speed;
    private int Sprint = 2;
    public int SpeedRotation;

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
        Vector3 movimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movimiento.Normalize();

        if (movimiento != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * SpeedRotation);
        }


        if (Input.GetAxis("Run") > 0)
        {
            CC.Move(movimiento * (Speed * Sprint) * Time.deltaTime);
        }
        else
        {
            CC.Move(movimiento * Speed * Time.deltaTime);
        }
    }
}
