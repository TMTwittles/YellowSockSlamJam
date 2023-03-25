using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shuttleConfirmationPanelGameObject;
    public GameObject ShuttleConfirmationPanelGameObject => shuttleConfirmationPanelGameObject;

    [SerializeField] private GameObject gameOverPanel;
    public GameObject GameOverPanel => gameOverPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
