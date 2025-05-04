using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundScript : MonoBehaviour
{
    private Vector3 startPosition;
    private float endPosition;

    [SerializeField]
    public float speed = 1f;

    void Start()
    {
        startPosition = transform.position;

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        if (sprites.Length >= 2)
        {
            float firstX = sprites[0].transform.position.x;
            float secondX = sprites[sprites.Length - 1].transform.position.x;
            endPosition = startPosition.x - secondX - firstX;
        }
    }

    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);

        if (this.transform.position.x <= endPosition)
        {
            this.transform.position = startPosition;
        }
    }
}
