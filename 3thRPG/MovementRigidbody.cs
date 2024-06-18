using UnityEngine;

public class MovementRigidbody : MonoBehaviour
{
    private Rigidbody rigid;
    private float moveSpeed = 5;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector3(x, 0, z) * moveSpeed;
    }
}
