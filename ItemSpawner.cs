using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> AllowedObjectsToSpawn = new List<GameObject>();
    public float TimeUntilRespawn = 60.0f;

    private float counterRespawn = 0.0f;
    private Renderer rend;

    public GameObject CurrentlySpawned { get; private set; }

    public void SpawnNewItems()
    {
        if (AllowedObjectsToSpawn.Count > 0)
        {
            if (CurrentlySpawned != null) Destroy(CurrentlySpawned);
            CurrentlySpawned = null;

            GameObject newPrefab = Instantiate(AllowedObjectsToSpawn[RandomNumGen.Instance.Next(0, AllowedObjectsToSpawn.Count)], transform);
            CurrentlySpawned = newPrefab;
            newPrefab.transform.position = transform.position;
        }
    }

    private void Awake()
    {
        counterRespawn = TimeUntilRespawn;
        rend = transform.GetChild(0).GetComponent<Renderer>();
    }

    private void Update()
    {
        counterRespawn += Time.deltaTime;
        if (!rend.isVisible && counterRespawn >= TimeUntilRespawn)
        {
            counterRespawn = 0.0f;
            SpawnNewItems();
        }
    }
}
