using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float time;

    void Start()
    {
        time = 0f;
        clock = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        time += Time.deltaTime;

        int t = Mathf.FloorToInt(time);
        
        int h = t / 3600;
        int m = (t % 3600) / 60;
        int s = t % 60;
    
        if (h > 0)
        {
            clock.text = $"{h:00}:{m:00}:{s:00}";
        }
        else
        {
            clock.text = $"{m:00}:{s:00}";
        }
    }
}
