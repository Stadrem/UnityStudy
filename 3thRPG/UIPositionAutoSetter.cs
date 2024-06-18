using UnityEngine;

public class UIPositionAutoSetter : MonoBehaviour
{
	[SerializeField]
	private	Vector3			distance = Vector3.zero;
	private	Transform		target;
	private	RectTransform	rectTransform;

	public void Setup(Transform target)
	{
		// UI�� �Ѿƴٴ� target ����
		this.target		= target;
		// RectTransform ������Ʈ ���� ������
		rectTransform	= GetComponent<RectTransform>();
	}

	private void LateUpdate()
	{
		// �Ѿƴٴ� ����� �������� UI�� ���� (����ó��)
		if ( target == null )
		{
			Destroy(gameObject);
			return;
		}

		// ������Ʈ�� ��ġ�� ���ŵ� ���Ŀ� UI�� �Բ�
		// ��ġ�� �����ϵ��� �ϱ� ���� LateUpdate()���� ȣ��

		// ������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
		// ȭ�鳻���� ��ǥ + distance��ŭ ������ ��ġ�� UI�� ��ġ�� ����
		rectTransform.position = screenPosition + distance;
	}
}

