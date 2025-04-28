using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    private Image indicator;

    void Start()
    {
        indicator = transform.Find("Indicator").GetComponent<Image>();
    }

    void Update()
    {
        indicator.fillAmount = Mathf.Clamp01(BirdScript.health);
        indicator.color = new Color(
            1f - indicator.fillAmount,
            indicator.fillAmount,
            0.2f
        );
    }
}
