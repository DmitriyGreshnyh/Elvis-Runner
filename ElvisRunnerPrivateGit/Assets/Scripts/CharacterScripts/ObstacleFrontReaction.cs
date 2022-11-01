using UnityEngine;

public class ObstacleFrontReaction : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _animator.SetTrigger("Death");
        }
    }
}
