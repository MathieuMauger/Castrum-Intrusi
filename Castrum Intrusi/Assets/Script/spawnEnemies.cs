using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemiesSpawner : MonoBehaviour
{
    public Tilemap floorTilemap;
    public GameObject[] enemyPrefabs;
    public GameObject[] intrusiPrefabs;

    private List<Vector3> spawnPositions = new List<Vector3>();

    public int enemiesToSpawn;
    public int intrusiToSpawn;

    void Start()
    {
        enemiesToSpawn = Random.Range(3, 6);
        intrusiToSpawn = Random.Range(1, 3);

        CacheFloorPositions();
        SpawnEnemies();
    }

    void CacheFloorPositions()
    {
        spawnPositions.Clear();

        foreach (var pos in floorTilemap.cellBounds.allPositionsWithin)
        {
            if (floorTilemap.HasTile(pos))
            {
                spawnPositions.Add(floorTilemap.GetCellCenterWorld(pos));
            }
        }

        Debug.Log("Nombre de positions valides trouvées : " + spawnPositions.Count);
    }

    void SpawnEnemies()
    {
        int total = enemiesToSpawn + intrusiToSpawn;

        for (int i = 0; i < total; i++)
        {
            if (spawnPositions.Count == 0)
            {
                Debug.LogWarning("Aucune position de sol trouvée pour spawn !");
                return;
            }

            int randomIndex = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPos = spawnPositions[randomIndex];

            if (i < enemiesToSpawn)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(intrusiPrefabs[Random.Range(0, intrusiPrefabs.Length)], spawnPos, Quaternion.identity);
            }
        }
    }
}
