using UnityEngine;

//이것과 부딪힌 오브젝트를 삭제

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //부딪힌 오브젝트가 Bullet이라면
        if (other.gameObject.name.Contains("Bullet"))
        {
            //비활성화
            other.gameObject.SetActive(false);
        }
        else
        {
            //부딪힌 오브젝트를 삭제
            Destroy(other.gameObject);
        }
    }
}
