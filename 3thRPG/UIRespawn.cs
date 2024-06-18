using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRespawn : MonoBehaviour
{
	[SerializeField]
	private	Image			imageFill;
	[SerializeField]
	private	TextMeshProUGUI	textRespawnTime;
	[SerializeField]
	private	TextMeshProUGUI	textObjectName;

	private	float			currentRespawnTime = 0;

	public void OnRespawn(string name, float respawnTime)
	{
		// 오브젝트 이름 출력
		textObjectName.text = name;

		StartCoroutine(nameof(Process), respawnTime);
	}

	private IEnumerator Process(float respawnTime)
	{
		currentRespawnTime = respawnTime;

		while ( currentRespawnTime > 0 )
		{
			currentRespawnTime -= Time.deltaTime;
			imageFill.fillAmount = currentRespawnTime / respawnTime;
			textRespawnTime.text = currentRespawnTime.ToString("F1");

			yield return null;
		}

		Destroy(gameObject);
	}
}

