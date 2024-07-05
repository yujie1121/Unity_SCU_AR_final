using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private float displayTime = 3f;

    private PlayerSpawn playerSpawn;

    private void Start()
    {
        defeatCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        gameCanvas.SetActive(false);
    }

    public void ShowDefeatCanvas()
    {
        StartCoroutine(DisplayCanvas(defeatCanvas));
    }

    public void ShowVictoryCanvas()
    {
        StartCoroutine(DisplayCanvas(victoryCanvas));
    }

    private IEnumerator DisplayCanvas(GameObject canvasToShow)
    {
        canvasToShow.SetActive(true);
        yield return new WaitForSecondsRealtime(displayTime);
        canvasToShow.SetActive(false);
        gameCanvas.SetActive(true);
    }
}
