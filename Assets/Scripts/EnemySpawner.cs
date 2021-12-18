using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enemy Spawner | Caleb A. Collar | 10.7.21
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] private bool looping = false;
    private WaveConfig waveConfig;
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumEnemies(); i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemy(), waveConfig.GetWaypoints()[0].position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

    IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
