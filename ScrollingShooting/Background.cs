using UnityEngine;

// UV 스크롤링

public class Background : MonoBehaviour
{
    //메테리얼
    public Material bgMaterial;

    //스크롤 속도
    public float speed = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //방향을 아래로 설정링
        Vector2 dir = Vector2.up;

        //스크롤링
        bgMaterial.mainTextureOffset += dir * speed * Time.deltaTime;
    }
}
