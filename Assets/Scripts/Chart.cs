using System;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour
{
    [SerializeField]
    Main MainController;

    [SerializeField]
    private Image filledPart;
    [SerializeField]
    private Text time;

    private const float MAX_TIME = 43200; // 12 hours

    private long totalTimeFixed;
    private long timerStart;

    bool isActive = false;

    void Start()
    {
        StopTimer();
    }

    void Update()
    {
        if (!isActive) return;

        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        float delta = totalTimeFixed + (now - timerStart);

        SetFilledPart(delta);
        SetTimer(delta);
    }

    public void TimerButton() {
        if (isActive) PauseTimer();
        else StartTimer();
    }

    public void StartTimer() {
        MainController.PauseAll();
        timerStart = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        isActive = true;
    }

    public void PauseTimer() {
        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (timerStart != 0) totalTimeFixed += now - timerStart;
        isActive = false;
    }

    public void StopTimer() {
        totalTimeFixed = 0;
        timerStart = 0;
        isActive = false;

        SetFilledPart(0);
        SetTimer(0);
    }

    private void SetFilledPart(float delta)
    {
        float percent = delta / MAX_TIME;
        filledPart.fillAmount = percent;
    }

    private void SetTimer(float delta)
    {
        string timeString = string.Format(
            "{0:00}:{1:00}:{2:00}",
            delta / 3600, (delta / 60) % 60, delta % 60
        );
        time.text = timeString;
    }
}
