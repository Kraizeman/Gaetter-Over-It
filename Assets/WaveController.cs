using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyPrefab;

    [SerializeField]
    private Transform enemySpawn;

    private void Start()
    {

        for (int i = 0; i < 100000; i++)
        {
            Instantiate(enemyPrefab, enemySpawn.position, Quaternion.identity);
        }
    }
}
