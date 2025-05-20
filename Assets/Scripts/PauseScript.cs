using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private GameObject content;
    private float startScale;

    void Start()
    {
        Transform c = transform.Find("Content");
        content = c.gameObject;
        content.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (content.activeInHierarchy)
            {
                content.SetActive(false);
                Time.timeScale = startScale;
            }
            else
            {
                startScale = Time.timeScale;
                content.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    public void OnActionButtonClick()
    {
        content.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnIntervalValueChangedDistance(System.Single value)
    {
        SpawnerScirpt.distanceDifficulty = value;
    }
    public void OnIntervalValueChangedGap(System.Single value)
    {
        SpawnerScirpt.gapDifficulty = value;
    }
}
