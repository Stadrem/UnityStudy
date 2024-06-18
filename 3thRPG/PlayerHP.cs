using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float max = 100;
    private float current;
    private float normalDecrease = 1;

    public float Max => max;
    public float Current
    {
        set => current = Mathf.Clamp(value,0, max);
        get => current;
    }

    private void Awake()
    {
        current = max;
    }

    private void Update()
    {
        if ( current > 0)
        {
            current -= Time.deltaTime * normalDecrease;
        }
        else
        {
            Debug.Log("플레이어 사망 처리");
        }
    }
}
