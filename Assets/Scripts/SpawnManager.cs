using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject[] _powerups;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
