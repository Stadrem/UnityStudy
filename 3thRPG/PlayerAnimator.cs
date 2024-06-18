using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject attackCollision;

    public void OnAttackCollision()
    {
        attackCollision.SetActive(true);
    }
}