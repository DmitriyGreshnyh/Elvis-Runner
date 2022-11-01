using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private Transform _tileEnd;
    private LevelSpawner _spawner;



    private void Awake()
    {
        _spawner = LevelSpawner.Instance;
        _spawner.LastPlatformPosition = _tileEnd;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= Vector3.forward* _spawner.PlatformSpeed * Time.deltaTime;
    }


    [ContextMenu("Set Start and End Position")]
    public void SetStartAndEndPosition()
    {
        _tileEnd = transform.Find("TileEnd").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformKiller"))
        {
            _spawner.CreatePlatformDelegate?.Invoke();
            Destroy(gameObject);
        }
    }
}
