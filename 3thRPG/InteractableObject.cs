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
        meshRenderer.enabled = false;	// Respawn될 때까지 화면에 보이지 않도록 설정
		meshCollider.enabled = false;	// Respawn될 때까지 다른 오브젝트와 충돌이 안되도록 설정

		StartCoroutine(nameof(RespawnProcess));
	}

	private IEnumerator RespawnProcess()
	{
        yield return new WaitForSeconds(respawnTime);

		meshRenderer.enabled = true;
		meshCollider.enabled = true;
    }
}
