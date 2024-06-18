using System;
using UnityEngine;

public class ButtonExercise : MonoBehaviour
{
    public void Method01()
    {
        Debug.Log($"Button Click.");
    }
        public void Method02(int index)
    {
        Debug.Log($"Button Click {index}");
    }
}
