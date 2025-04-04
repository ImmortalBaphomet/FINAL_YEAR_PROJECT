using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class AddressableCaller : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceGameObject> levelParts;
    [SerializeField] private List<GameObject> levelTrackers;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform player;
    [SerializeField] private float loadDistance = 10f;
    [SerializeField] private float unloadDistance = 15f;

    private Dictionary<int, AsyncOperationHandle<GameObject>> loadedHandles = new Dictionary<int, AsyncOperationHandle<GameObject>>();
    private Dictionary<int, bool> isLoaded = new Dictionary<int, bool>();

    void Start()
    {
        if (levelParts.Count != spawnPoints.Count || levelParts.Count != levelTrackers.Count)
        {
            Debug.LogError("Mismatch: Make sure all lists are the same size.");
            return;
        }

        for (int i = 0; i < levelParts.Count; i++)
        {
            isLoaded[i] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < levelParts.Count; i++)
        {
            float distance = Vector3.Distance(player.position, levelTrackers[i].transform.position);

            if (!isLoaded[i] && distance < loadDistance)
            {
                LoadLevelPart(i);
            }
            else if (isLoaded[i] && distance > unloadDistance)
            {
                UnloadLevelPart(i);
            }
        }
    }

    void LoadLevelPart(int index)
    {
        isLoaded[index] = true;
        AsyncOperationHandle<GameObject> handle = levelParts[index].InstantiateAsync(spawnPoints[index].position, spawnPoints[index].rotation);
        loadedHandles[index] = handle;

        handle.Completed += (h) =>
        {
            if (h.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Loaded level part {index}");
            }
            else
            {
                Debug.Log($"Failed to load level part {index}");
                isLoaded[index] = false;
            }
        };
    }

    void UnloadLevelPart(int index)
    {
        if (loadedHandles.ContainsKey(index))
        {
            Addressables.ReleaseInstance(loadedHandles[index]);
            loadedHandles.Remove(index);
            isLoaded[index] = false;
            Debug.Log($"Unloaded level part {index}");
        }
    }
}
