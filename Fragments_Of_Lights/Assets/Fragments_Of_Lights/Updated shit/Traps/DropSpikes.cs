using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpikes : MonoBehaviour
{
    [SerializeField] private List<Transform> spikeDropPts; // List of drop points
    [SerializeField] private List<GameObject> spikePrefabs; // Different spike types
    //[SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float fallDistance = 3f;
    [SerializeField] private float dropInterval = 3f; // Interval before new set of spikes
    [SerializeField] private float minDropDelay = 0.2f; // Min delay between spikes
    [SerializeField] private float maxDropDelay = 1f; // Max delay between spikes

    private Queue<GameObject> spikePool = new Queue<GameObject>();
    private Coroutine spawnCoroutine;

    void Start()
    {
        // Initialize object pool
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, spikePrefabs.Count);
            GameObject spike = Instantiate(spikePrefabs[randomIndex]);
            spike.SetActive(false); // Start inactive
            spikePool.Enqueue(spike);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnSpikesCoroutine());
            Debug.Log("Player Entered - Spawning spikes");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            Debug.Log("Player Left - Stopping spike drops");
        }
    }

    IEnumerator SpawnSpikesCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnSpikesWithPattern()); // Drop spikes randomly
            yield return new WaitForSeconds(dropInterval);
        }
    }

    IEnumerator SpawnSpikesWithPattern()
    {
        List<Transform> availableDropPoints = new List<Transform>(spikeDropPts);

        while (availableDropPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, availableDropPoints.Count);
            Transform spawnPoint = availableDropPoints[randomIndex];
            availableDropPoints.RemoveAt(randomIndex); // Remove from list to avoid repeats

            SpawnSpike(spawnPoint);
            yield return new WaitForSeconds(Random.Range(minDropDelay, maxDropDelay)); // Random delay between spikes
        }
    }

    void SpawnSpike(Transform spawnPoint)
    {
        if (spikePool.Count > 0)
        {
            GameObject spike = spikePool.Dequeue();
            spike.transform.position = spawnPoint.position;
            spike.SetActive(true);

            StartCoroutine(MoveSpikeDown(spike, spawnPoint.position.y - fallDistance));
        }
    }

    IEnumerator MoveSpikeDown(GameObject spike, float targetY)
    {
        while (spike.activeSelf && spike.transform.position.y > targetY)
        {
            spike.transform.position = Vector3.MoveTowards(spike.transform.position,
                new Vector3(spike.transform.position.x, targetY, spike.transform.position.z),
                fallSpeed * Time.deltaTime);
            yield return null;
        }

        //PlayParticleEffect(spike.transform.position);

        spike.SetActive(false); // Reset spike
        spikePool.Enqueue(spike);
    }

    //void PlayParticleEffect(Vector3 position)
    //{
    //    GameObject particleEffect = Instantiate(particleEffectPrefab, position, Quaternion.identity);
    //    Destroy(particleEffect, 1f); // Auto destroy after 1 second
    //}
}
