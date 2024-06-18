using UnityEngine;

[System.Serializable]
public struct ItemData
{
	public	Sprite	icon;		// ������ ������
	public	string	name;		// ������ �̸�
	public	int		count;		// ������ ����
	[TextArea(0, 30)]
	public	string	details;	// ������ ����
	public int hpRecovery; // ������ ��� ȿ�� (ü�� ȸ��)
	public int spRecovery;
}


// ����ó�� �ܼ��� ��� �������� ü���� ȸ���ϴ� ���� �ƴ�
// �Ϲ������δ� �����ۿ� ���� �پ��� ȿ���� ������ �ֱ� ������
// C#�� Ŭ������ �������̽� ���� �̿��� �� �����ϰ� �����ؾ� ������
// �ʱ� �����̱� ������ �ܼ��ϰ� ����

