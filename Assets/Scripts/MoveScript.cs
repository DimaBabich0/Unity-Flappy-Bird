using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private float easyModeSpeed = 2.5f;
    [SerializeField]
    private float hardModeSpeed = 5f;
    [SerializeField]
    private bool isEasyMode = true;

    private float speed;

    void Update()
    {
        speed = isEasyMode ? easyModeSpeed : hardModeSpeed;
        this.transform.Translate(speed * Time.deltaTime * Vector3.left);
        
        if(this.transform.position.x < -10f)
        {
            RestartPosition();
        }
    }

    public void RestartPosition()
    {
        float newY = Random.Range(-1f, 0f);
        this.transform.position = new Vector3(10, newY, 0);
        Debug.Log($"New Y position: {newY.ToString("F2")}");
    }
}
