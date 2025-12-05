using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class EnemiesSpawner : MonoBehaviour
{
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public GameObject[] enemyPrefabs;
    public GameObject[] intrusiPrefabs;
    public GameObject playerPrefab;
    public GameObject turnsUIPrefab;

    private List<Vector3> spawnPositions = new List<Vector3>();

    public int enemiesToSpawn;
    public int intrusiToSpawn;

    void Start()
    {
        Instantiate(turnsUIPrefab);
        DontDestroyOnLoad(turnsUIPrefab);

        enemiesToSpawn = Random.Range(3, 6);
        intrusiToSpawn = Random.Range(1, 3);

        CacheFloorPositions();
        SpawnEnemies();
        SpawnPlayer();
    }

    void Update()
    {
        GameObject[] intrusis = GameObject.FindGameObjectsWithTag("Intrusis");
        Debug.Log(intrusis.Length);

        if (intrusis.Length == 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            playerStats.Instance.NextTurn();
            
        }
    }

    public static void LoadRandomScene()
    {
        string[] scenes = {
            "Map 1", "Map 2", "Map 3",
            "Map 4", "Map 5", "Map 6", "Map 7"
        };


        SceneManager.LoadScene(
            scenes[Random.Range(0, scenes.Length)]
        );
    }


    void CacheFloorPositions()
{
    spawnPositions.Clear();

    foreach (var pos in floorTilemap.cellBounds.allPositionsWithin)
    {
        if (!floorTilemap.HasTile(pos))
            continue;

        
        bool nearWall =
            wallTilemap.HasTile(pos + new Vector3Int(1, 0, 0)) || 
            wallTilemap.HasTile(pos + new Vector3Int(-1, 0, 0)) || 
            wallTilemap.HasTile(pos + new Vector3Int(0, 1, 0)) || 
            wallTilemap.HasTile(pos + new Vector3Int(0, -1, 0));  

        if (nearWall)
            continue; 

        spawnPositions.Add(floorTilemap.GetCellCenterWorld(pos));
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

    void SpawnPlayer()
    {

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            print(player + "player");
            if (!player.GetComponent<playerStats>()) { 
                Destroy(player);
            }
        }

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("Aucune position de sol trouvée pour spawn le player !");
            return;
        }

        int randomIndex = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[randomIndex];

        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }
}
