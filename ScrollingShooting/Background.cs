using UnityEngine;

// UV ��ũ�Ѹ�

public class Background : MonoBehaviour
{
    //���׸���
    public Material bgMaterial;

    //��ũ�� �ӵ�
    public float speed = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������ �Ʒ��� ������
        Vector2 dir = Vector2.up;

        //��ũ�Ѹ�
        bgMaterial.mainTextureOffset += dir * speed * Time.deltaTime;
    }
}
