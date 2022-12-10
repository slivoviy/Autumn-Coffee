using UnityEngine;
using UnityEngine.SceneManagement;


public class Initialize : MonoBehaviour
{
    [SerializeField] private int _nextSceneIndex;

    private void Start()
    {
        SceneManager.LoadScene(_nextSceneIndex, LoadSceneMode.Single);
    }
}
