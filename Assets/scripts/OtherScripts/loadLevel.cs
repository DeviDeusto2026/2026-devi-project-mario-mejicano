using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    public int numeroScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(numeroScene);
        }
    }
    public void CargarLobby()
    {
        SceneManager.LoadScene(0);
    }
}
