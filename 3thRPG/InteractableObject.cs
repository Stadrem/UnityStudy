using System.Collections;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private	float			respawnTime = 5;
	private	MeshRenderer	meshRenderer;
	private	MeshCollider	meshCollider;

    public	float			RespawnTime => respawnTime;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		meshCollider = GetComponent<MeshCollider>();
	}

	public void Deactivate()
	{
        meshRenderer.enabled = false;	// Respawn�� ������ ȭ�鿡 ������ �ʵ��� ����
		meshCollider.enabled = false;	// Respawn�� ������ �ٸ� ������Ʈ�� �浹�� �ȵǵ��� ����

		StartCoroutine(nameof(RespawnProcess));
	}

	private IEnumerator RespawnProcess()
	{
        yield return new WaitForSeconds(respawnTime);

		meshRenderer.enabled = true;
		meshCollider.enabled = true;
    }
}
