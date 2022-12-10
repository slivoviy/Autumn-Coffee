using UnityEngine.SceneManagement;
using UnityEngine;


public class GameplaySceneLoader : MonoBehaviour
{
    private void Start()
    {
        LoadGameplay();
    }

    public void LoadGameplay()
    {
        SceneManager.LoadScene("Gameplay_Topology", LoadSceneMode.Additive);
    }
}
