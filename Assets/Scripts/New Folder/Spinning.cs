using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float spinSpeed = 90f; // degrees per second
    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
