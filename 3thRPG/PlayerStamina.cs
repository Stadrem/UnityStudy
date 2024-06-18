using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField]
    private MovementCharacterController movement;

    [SerializeField]
    private float max = 100;                    // 최대 스태미나
    private float current;                  // 현재 스태미나
    private float normalRecovery = 1.0f;        // 초당 스태미나 회복량
    private float emergencyRecovery = 20.0f;    // 긴급 복구 스태미나 회복량
    private float decreaseWhenRun = 5.0f;       // 달릴 때 초당 감소 스태미나

    // 외부에서 스태미나 정보를 열람할 수 있도록 Get Property 정의
    public float Max => max;
    public float Current
    {
        set => current = Mathf.Clamp(value, 0, max);
        get => current;
    }

    // 긴급 복구 여부를 나타내는 Property
    public bool IsEmergencyMode { set; get; } = false;

    private void Awake()
    {
        current = max;
    }

    private void Update()
    {
        Recovery();

        // 현재 스태미나가 남아있고, 이동속도가 2보다 클 때(달릴 때)
        if ( /*current > 0 &&*/ movement.MoveSpeed > 2)
        {
            current -= Time.deltaTime * decreaseWhenRun;
        }

        // 현재 스태미나가 0이 되면 긴급 복구 모드로 변경
        if (current <= 0)
        {
            IsEmergencyMode = true;
        }

        // 스태미나의 최소 값은 0, 최대 값은 max을 넘어가지 않도록 설정
        current = Mathf.Clamp(current, 0, max);
    }

    private void Recovery()
    {
        // 긴급 복구 모드로 스태미나가 초당 20씩 회복
        if (IsEmergencyMode == true)
        {
            current += Time.deltaTime * emergencyRecovery;

            // 스태미나가 최대가 되면 긴급 복구 모드 종료
            if (current >= max)
            {
                IsEmergencyMode = false;
            }
        }
        // 긴급 복구 모드가 아닐 때는 스태미나가 초당 1씩 회복
        else
        {
            current += Time.deltaTime * normalRecovery;
        }
    }
}


