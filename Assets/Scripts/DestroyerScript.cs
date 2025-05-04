using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        Transform current = obj.transform;
        Transform parent = null;
        do
        {
            parent = current.parent;
            GameObject.Destroy(current.gameObject);
            current = parent;
        } while (parent != null);
    }

    public static void ClearField()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            DeepDestroy(obj);
        }
        foreach (var obj in GameObject.FindGameObjectsWithTag("Food"))
        {
            DeepDestroy(obj);
        }
    }

    private static void DeepDestroy(GameObject obj)
    {
        Transform current = obj.transform;
        Transform parent = null;
        do
        {
            parent = current.parent;
            GameObject.Destroy(current.gameObject);
            current = parent;
        } while (parent != null);
    }
}
