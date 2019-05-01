using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Chart[] charts;

    public void StopAll() {
        foreach (Chart chart in charts) {
            chart.StopTimer();
        }
    }

    public void PauseAll() {
        foreach (Chart chart in charts) {
            chart.PauseTimer();
        }
    }
}
