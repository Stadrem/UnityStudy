using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField]
    private MovementCharacterController movement;

    [SerializeField]
    private float max = 100;                    // �ִ� ���¹̳�
    private float current;                  // ���� ���¹̳�
    private float normalRecovery = 1.0f;        // �ʴ� ���¹̳� ȸ����
    private float emergencyRecovery = 20.0f;    // ��� ���� ���¹̳� ȸ����
    private float decreaseWhenRun = 5.0f;       // �޸� �� �ʴ� ���� ���¹̳�

    // �ܺο��� ���¹̳� ������ ������ �� �ֵ��� Get Property ����
    public float Max => max;
    public float Current
    {
        set => current = Mathf.Clamp(value, 0, max);
        get => current;
    }

    // ��� ���� ���θ� ��Ÿ���� Property
    public bool IsEmergencyMode { set; get; } = false;

    private void Awake()
    {
        current = max;
    }

    private void Update()
    {
        Recovery();

        // ���� ���¹̳��� �����ְ�, �̵��ӵ��� 2���� Ŭ ��(�޸� ��)
        if ( /*current > 0 &&*/ movement.MoveSpeed > 2)
        {
            current -= Time.deltaTime * decreaseWhenRun;
        }

        // ���� ���¹̳��� 0�� �Ǹ� ��� ���� ���� ����
        if (current <= 0)
        {
            IsEmergencyMode = true;
        }

        // ���¹̳��� �ּ� ���� 0, �ִ� ���� max�� �Ѿ�� �ʵ��� ����
        current = Mathf.Clamp(current, 0, max);
    }

    private void Recovery()
    {
        // ��� ���� ���� ���¹̳��� �ʴ� 20�� ȸ��
        if (IsEmergencyMode == true)
        {
            current += Time.deltaTime * emergencyRecovery;

            // ���¹̳��� �ִ밡 �Ǹ� ��� ���� ��� ����
            if (current >= max)
            {
                IsEmergencyMode = false;
            }
        }
        // ��� ���� ��尡 �ƴ� ���� ���¹̳��� �ʴ� 1�� ȸ��
        else
        {
            current += Time.deltaTime * normalRecovery;
        }
    }
}


