using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableCaller : MonoBehaviour
{
    [SerializeField] AssetReferenceGameObject _ptOne;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))   
        {
            _ptOne.InstantiateAsync();
        }
    }

    void OnaddressableLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result);
        }
        else
        {
            Debug.Log("Map Loading Fail");
        }
    }
}
