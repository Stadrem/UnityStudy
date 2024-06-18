using System.Collections;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(nameof(AutoDisable));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitable"))
        {
            other.GetComponent<HitableObject>().TakeDamage(1);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}

