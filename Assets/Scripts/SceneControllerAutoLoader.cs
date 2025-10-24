using UnityEngine;

public class SceneControllerAutoLoader : MonoBehaviour
{
    [SerializeField] GameObject sceneControllerPrefab;
    void Awake()
    {
        if(SceneController.instance == null)
        {
            Instantiate(sceneControllerPrefab);
        }
    }
}
