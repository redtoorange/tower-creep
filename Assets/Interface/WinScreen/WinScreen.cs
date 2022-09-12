using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}