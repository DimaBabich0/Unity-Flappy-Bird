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
        this.transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);
    }
}
