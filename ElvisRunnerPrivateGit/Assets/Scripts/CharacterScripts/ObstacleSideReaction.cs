using UnityEngine;

public class ObstacleSideReaction : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private enum SideName {right, left};

    [SerializeField] private SideName _sideName;
    private int _intDirection;

    void Start()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();

        if (_sideName == SideName.right) _intDirection = -1;
        else _intDirection = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _characterMovement.LineCurrent += _intDirection;
        }
    }
}
