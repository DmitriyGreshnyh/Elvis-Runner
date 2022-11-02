using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CharacterObstacleReaction : MonoBehaviour
{
    private enum SideName { right, left, front };
    [SerializeField] private SideName _sideName;

    static public event Action<string> OnObstacleHit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle") OnObstacleHit?.Invoke(_sideName.ToString());
    }
}
