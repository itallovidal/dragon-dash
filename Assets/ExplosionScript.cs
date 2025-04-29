using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private void Start()
    {
        // Obtém o tempo de duração da animação e destrói o objeto após esse tempo
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Calcula a duração da animação com base no tempo total do clipe
            float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationDuration);
        }
        else
        {
            // Caso não tenha um Animator, destrói após um tempo padrão
            Destroy(gameObject, 1f);
        }
    }
}