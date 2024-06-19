using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField, Header("時間文字")]
    private TextMeshProUGUI timeText;
    [SerializeField, Header("倒數時間")]
    private float countdownTime = 5f;
    [SerializeField, Header("凍結時間圓框")]
    private Image imgTime;

    private float currentTime;
    private bool isCountingDown = false;

    private void Start()
    {
        // gameObject.SetActive(true);
        currentTime = countdownTime;
        UpdateTimeText();
        UpdateCircleFillAmount();
        StartCountdown();
    }

    private void UpdateTimeText()
    {
        timeText.text = currentTime.ToString("0");
    }

    private void UpdateCircleFillAmount()
    {
        if (imgTime != null)
        {
            imgTime.fillAmount = currentTime / countdownTime;
        }
    }

    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            gameObject.SetActive(true);
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        isCountingDown = true;
        gameObject.SetActive(true);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0) currentTime = 0;  
            UpdateTimeText();
            UpdateCircleFillAmount();  
            yield return null;
        }

        currentTime = 0;
        UpdateTimeText();
        UpdateCircleFillAmount();  
        isCountingDown = false;

        OnCountdownEnd();
    }

    private void OnCountdownEnd()
    {
        gameObject.SetActive(false);
    }
}
