using System.Collections.Generic;	// List<>
using UnityEngine;
using UnityEngine.UI;				// GrapicRaycaster
using UnityEngine.EventSystems;		// PointerEventData

public class GraphicRaycasterTest : MonoBehaviour
{
	private GraphicRaycaster	graphicRaycaster;
	private	PointerEventData	ped;

	private void Awake()
	{
		graphicRaycaster = GetComponent<GraphicRaycaster>();
		ped				 = new PointerEventData(null);
	}

	private void Update()
	{
		ped.position = Input.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();

		graphicRaycaster.Raycast(ped, results);

		if ( results.Count <= 0 )
		{
			return;
		}
		
		Debug.Log("<color=blue>Hit : </color>"+results[0].gameObject.name);
		
		if ( Input.GetMouseButton(0) )
		{
			results[0].gameObject.transform.position = ped.position;
		}
	}
}

