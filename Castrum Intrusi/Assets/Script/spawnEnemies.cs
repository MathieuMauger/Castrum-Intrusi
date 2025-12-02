using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemiesToSpawn = 8;

    private Tilemap floorTilemap;

    void Start()
    {
        enemiesToSpawn = Random.Range(4, 10);
        floorTilemap = GetComponent<Tilemap>();
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        BoundsInt bounds = floorTilemap.cellBounds; // get tilemap bounds

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // generate random position within tilemap bounds
            float randomX = Random.Range(bounds.xMin, bounds.xMax);
            float randomY = Random.Range(bounds.yMin, bounds.yMax);

            Vector3 spawnPos = floorTilemap.layoutGrid.CellToWorld(new Vector3Int((int)randomX, (int)randomY, 0));

            // Pick a random enemy prefab among the available ones
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], spawnPos, Quaternion.identity);
        }
    }
}