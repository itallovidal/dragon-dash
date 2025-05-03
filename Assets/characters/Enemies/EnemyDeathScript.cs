using UnityEngine;

public class EnemyDeathScript : MonoBehaviour
{
    private void Update()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationDuration);
        }
    }
}