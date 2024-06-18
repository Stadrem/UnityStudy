using UnityEngine;
using UnityEngine.EventSystems;

public class MovementTransform : MonoBehaviour
{
	private float moveSpeed = 3;
	private Vector3 moveDirection;
	    private void Awake()
    {
        moveSpeed = 5.0f;
        moveDirection = Vector3.right;
    }

    private void Update()
    {
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, 0, z);

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
