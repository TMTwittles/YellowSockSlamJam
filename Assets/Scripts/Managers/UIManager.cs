using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shuttleConfirmationPanelGameObject;
    public GameObject ShuttleConfirmationPanelGameObject => shuttleConfirmationPanelGameObject;

    [SerializeField] private GameObject gameOverPanel;
    public GameObject GameOverPanel => gameOverPanel;

    [SerializeField] private GameObject timeTillEndOfTheWorld;
    public GameObject TimeTillEndOfTheWorld => timeTillEndOfTheWorld;

    [SerializeField] private GameObject structureButtons;
    public GameObject StructureButtons => structureButtons;

    [SerializeField] private GameObject pauseButton;
    public GameObject PauseButton => pauseButton;

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

    public void TurnOffEverythingExceptGameOverPanel()
    {
        shuttleConfirmationPanelGameObject.SetActive(false);
        timeTillEndOfTheWorld.SetActive(false);
        structureButtons.SetActive(false);
        pauseButton.SetActive(false);
    }
}
