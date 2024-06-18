using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField]
    private bool x, y, z;	// target의 좌표와 동일하게 설정하고 싶은 축을 true로 설정
    [SerializeField]
    private Transform target;       // 목표 대상

    private void Update()
    {
        if (!target) return;

        transform.position = new Vector3((x ? target.position.x : transform.position.x),
                                         (y ? target.position.y : transform.position.y),
                                         (z ? target.position.z : transform.position.z));
    }
}

