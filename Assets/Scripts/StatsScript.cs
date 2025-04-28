using UnityEngine;

public class StatsScript : MonoBehaviour
{
    private static TMPro.TextMeshProUGUI message;
    void Start()
    {
        message = this.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public static void Show(string message)
    {
        StatsScript.message.text = message;
    }
}
