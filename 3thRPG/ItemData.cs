using UnityEngine;

[System.Serializable]
public struct ItemData
{
	public	Sprite	icon;		// 아이템 아이콘
	public	string	name;		// 아이템 이름
	public	int		count;		// 아이템 개수
	[TextArea(0, 30)]
	public	string	details;	// 아이템 설명
	public int hpRecovery; // 아이템 사용 효과 (체력 회복)
	public int spRecovery;
}


// 지금처럼 단순히 모든 아이템이 체력을 회복하는 것이 아닌
// 일반적으로는 아이템에 따라 다양한 효과를 가지고 있기 때문에
// C#의 클래스와 인터페이스 등을 이용해 더 복잡하게 구성해야 하지만
// 초급 강좌이기 때문에 단순하게 구현

