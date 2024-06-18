using TMPro;
using UnityEngine;

public class ClearTimer : MonoBehaviour
{
    [SerializeField, Header("®É¶¡¤å¦r")]
    private TextMeshProUGUI timeText;
    private float clearTime;
    private bool stop = true;

    private void Start()
    {
        stop = false;
    }

    private void Update()
    {
        if (stop) return;
        clearTime += Time.deltaTime;
        timeText.text = System.TimeSpan.FromSeconds(clearTime).ToString(@"mm\:ss\:ff");
    }

    public void StopTimer()
    {
        stop = true;
    }

    public void StartTimer()
    {
        stop = false;
    }
}
