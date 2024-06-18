using UnityEngine;

public class BombAction : MonoBehaviour
{
    //폭발 효과 프리팹
    public GameObject explosionFactory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //폭발효과 공장에서 폭발효과를 생성
        GameObject explo = Instantiate(explosionFactory);

        //생성된 폭발효과를 나의 위치에 배치
        explo.transform.position = transform.position;

        //나를 파괴
        Destroy(gameObject);
    }
}
