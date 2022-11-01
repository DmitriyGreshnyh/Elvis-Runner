using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _obstaclesPrefab;
    [SerializeField] Transform[] _rowPosition;

    

    void Start()
    {
        StartCoroutine(ObstacleSpawneCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ObstacleSpawneCoroutine()
    {
        while (true)
        { 
            int obstacleRand = Random.Range(0, _obstaclesPrefab.Length);
            int rowRand = Random.Range(0, _rowPosition.Length);

            Instantiate(_obstaclesPrefab[obstacleRand], _rowPosition[rowRand].position, Quaternion.identity, transform);

            yield return new WaitForSeconds(5);
        }
    
    }
}
