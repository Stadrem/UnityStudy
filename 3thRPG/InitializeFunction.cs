using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeFunction : MonoBehaviour
{

	private void Start()
	{
		Debug.Log("Start!");
	}

	private void Awake()
	{
		Debug.Log("Awake!");
	}

	private void OnEnable()
	{
		Debug.Log("OnEnable!");
	}
}
