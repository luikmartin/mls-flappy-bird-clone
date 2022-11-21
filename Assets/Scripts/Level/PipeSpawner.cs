using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipePrefab;

    public void StartSpawning() => InvokeRepeating("SpawnPipe", 0f, 1.3f);

    public void StopSpawning() => CancelInvoke();

    private void SpawnPipe() => Instantiate(_pipePrefab, new Vector2(5f, Random.Range(-1.4f, 2.9f)), Quaternion.identity);
}
