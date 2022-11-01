using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] GameObject _tilePrefab;
    public Transform LastPlatformPosition {get; set; }

    [SerializeField] int _numberOfPlatformOnStart = 5;

    public static LevelSpawner Instance;

    public Action CreatePlatformDelegate;

    [SerializeField] private int _platformSpeed = 2;
    public int PlatformSpeed{ 
        get { return _platformSpeed; }
        set { _platformSpeed = value; }
    }
   

    private void Awake()
    {
        Instance = this;
        CreatePlatformDelegate += CreatePlatform;

        PlatformSpeed = _platformSpeed;
        LastPlatformPosition = transform;
    }
    void Start()
    {
        for (int i = 0; i < _numberOfPlatformOnStart; i++)
        {
            CreatePlatformDelegate();
        }
    } 


    [ContextMenu("CreatePlatform()")]
    private void CreatePlatform()
    {
        Vector3 pos = LastPlatformPosition.position;
        Instantiate(_tilePrefab, pos, Quaternion.identity, transform);
    }

}
