using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public float SpawnDelay = 1f;
    public float SpawnTimer = 0f;
    public bool isSpawned;
    public GameObject currentNpc = null;

    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 4f;
    public List<GameObject> NpcPool = new();

    public float moveSpeed = .5f;

    public Transform start;
    public Transform end;

    public void Start()
    {
        SpawnDelay = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    public void Update()
    {
        if (!isSpawned)
        {
            SpawnTimer += Time.deltaTime;

            if (SpawnDelay <= SpawnTimer)
            {
                // Spawn;
                SpawnNpc();
                SpawnDelay = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);
                SpawnTimer = 0f;
            }
        }

        if (currentNpc != null)
        {
            MoveNpc();
        }
    }

    public void MoveNpc()
    {
        currentNpc.transform.position = Vector2.MoveTowards(currentNpc.transform.position, end.position, moveSpeed * Time.deltaTime);

        if (math.distance(currentNpc.transform.position, end.transform.position) < 0.2f)
        {
            Destroy(currentNpc);
            currentNpc = null;
            isSpawned = false;
        }
    }

    public void SpawnNpc()
    {
        int randomIndex = UnityEngine.Random.Range(0, NpcPool.Count);

        currentNpc = Instantiate(NpcPool[randomIndex], start.position, quaternion.identity);

        isSpawned = true;
    }
}
