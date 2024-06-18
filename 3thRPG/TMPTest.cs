using UnityEngine;
using TMPro;			// TextMeshProUGUI

public class TMPTest : MonoBehaviour
{
	private TextMeshProUGUI textVariable;

	private void Awake()
	{
		textVariable = GetComponent<TextMeshProUGUI>();

		// Text Input Box에 있는 텍스트 내용 수정
		textVariable.text		= "This font don't support korean.";
		// Vertex Color
		textVariable.color		= Color.red;
		// Font Size
		textVariable.fontSize	= 40;
		// Font Style
		textVariable.fontStyle	= FontStyles.Bold | FontStyles.Italic;
	}
}

