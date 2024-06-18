using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFunction : MonoBehaviour
{
	private void Update()
	{
		Debug.Log("Update!");
	}

	private void LateUpdate()
	{
		Debug.Log("LateUpdate!");
	}

	private void FixedUpdate()
	{
		Debug.Log("FixedUpdate!");
	}
}
