using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}