using UnityEngine;

public class UIPositionAutoSetter : MonoBehaviour
{
	[SerializeField]
	private	Vector3			distance = Vector3.zero;
	private	Transform		target;
	private	RectTransform	rectTransform;

	public void Setup(Transform target)
	{
		// UI가 쫓아다닐 target 설정
		this.target		= target;
		// RectTransform 컴포넌트 정보 얻어오기
		rectTransform	= GetComponent<RectTransform>();
	}

	private void LateUpdate()
	{
		// 쫓아다닐 대상이 없어지면 UI도 삭제 (예외처리)
		if ( target == null )
		{
			Destroy(gameObject);
			return;
		}

		// 오브젝트의 위치가 갱신된 이후에 UI도 함께
		// 위치를 설정하도록 하기 위해 LateUpdate()에서 호출

		// 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
		// 화면내에서 좌표 + distance만큼 떨어진 위치를 UI의 위치로 설정
		rectTransform.position = screenPosition + distance;
	}
}

