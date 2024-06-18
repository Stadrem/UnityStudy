using UnityEngine;

public class MovementCharacterController : MonoBehaviour
{
    [SerializeField]
    private UIInventory uiInventory;
    [SerializeField]
    private PlayerStamina playerStamina;

    [SerializeField]
    private float walkSpeed = 2.0f;
    [SerializeField]
    private float runSpeed = 6.0f;
    [SerializeField]
    private float moveSpeed = 2.0f;             // �̵� �ӵ�
    [SerializeField]
    private float gravity = -9.81f;             // �߷� ���
    [SerializeField]
    private float jumpForce = 3.0f;             // �پ� ������ ��
    private Vector3 moveDirection = Vector3.zero;   // �̵� ����

    [SerializeField]
    private Transform mainCamera;
    private CharacterController characterController;
    private Animator animator;

    // �ٸ� Ŭ�������� �̵��ӵ� ���� ���� Ȯ���� �� �ֵ��� Get Property ����
    public float MoveSpeed => moveSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();

        GravityAndJump();

        Attack();

        // CharacterController�� ���ǵǾ� �ִ� Move() �޼ҵ带 �̿��� �̵�
        // �Ű������� ������ �� �̵� �Ÿ� ���� ���� (�̵� ���� * �̵� �ӵ� * Time.deltaTime)
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // ���� ī�޶� �ٶ󺸰� �ִ� ���� ������ ������ ����
        transform.rotation = Quaternion.Euler(0, mainCamera.eulerAngles.y, 0);
    }

    private void Movement()
    {
        // Ű �Է����� x, z�� �̵� ���� ����
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // ������Ʈ�� �̵� �ӵ� ���� (ShiftŰ�� ������ ������ walkSpeed, ������ runSpeed)
        moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxis("Sprint"));

        // ShiftŰ�� ������ ������ 0.5, ������ 1
        float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;

        // ���¹̳� ��� ���� ����� ���� �޸��� �Ұ���
        if (playerStamina.IsEmergencyMode == true)
        {
            moveSpeed = walkSpeed;
            offset = 0.5f;
        }

        animator.SetFloat("horizontal", x * offset);
        animator.SetFloat("vertical", z * offset);

        // ������Ʈ�� �̵� ���� ����
        Vector3 dir = mainCamera.rotation * new Vector3(x, 0, z);
        moveDirection = new Vector3(dir.x, moveDirection.y, dir.z);
    }

    private void GravityAndJump()
    {
        // Space Ű�� ������ �� �÷��̾ �ٴڿ� ������ ����
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded == true)
        {
            animator.SetTrigger("onJump");
            moveDirection.y = jumpForce;
        }

        // �÷��̾ ���� ��� ���� ������
        // y�� �̵����⿡ gravity * Time.deltaTime�� ������
        if (characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("onAttack");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Eatable"))
        {
            int index = hit.gameObject.GetComponent<EatableObject>().ItemIndex;
            uiInventory.GetItem(index);
            Destroy(hit.gameObject);

            Debug.Log(hit.gameObject.name);
        }
    }
}