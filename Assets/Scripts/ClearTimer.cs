using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ClearTimer : MonoBehaviour
{
    [SerializeField, Header("時間文字")]
    private TextMeshProUGUI timeText;
    [SerializeField, Header("勝利時間")]
    private float victoryTime = 25f; 
    private float clearTime;
    private bool stop = true;

    public UnityEvent OnVictory;

    private CanvasManager canvasManager;

    private void Start()
    {
        stop = false;
        clearTime = 0;
        if (OnVictory == null)
        {
            OnVictory = new UnityEvent();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (stop) return;
        clearTime += Time.deltaTime;
        timeText.text = System.TimeSpan.FromSeconds(clearTime).ToString(@"mm\:ss\:ff");
        if (clearTime >= victoryTime)
        {
            OnTimeUp();
        }
    }

    public void StopTimer()
    {
        stop = true;
    }

    public void StartTimer()
    {
        stop = false;
        clearTime = 0;
    }

    private void OnTimeUp()
    {
        StopTimer();
        OnVictory.Invoke();
        GameObject canvasController = GameObject.Find("畫布控制器");
        canvasManager = canvasController.GetComponent<CanvasManager>();
        canvasManager.ShowVictoryCanvas();
        Time.timeScale = 0;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopTimer();
        clearTime = 0;
        timeText.text = System.TimeSpan.FromSeconds(clearTime).ToString(@"mm\:ss\:ff");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
