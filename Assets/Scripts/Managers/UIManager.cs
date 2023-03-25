using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shuttleConfirmationPanelGameObject;
    public GameObject ShuttleConfirmationPanelGameObject => shuttleConfirmationPanelGameObject;

    [SerializeField] private GameObject gameOverPanel;
    public GameObject GameOverPanel => gameOverPanel;
}
