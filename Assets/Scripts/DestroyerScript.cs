using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        Transform current = obj.transform;
        Transform parent;
        do
        {
            parent = current.parent;
            GameObject.Destroy(current.gameObject);
            current = parent;
        } while (parent != null);
    }
}
