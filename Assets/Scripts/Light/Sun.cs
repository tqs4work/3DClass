using UnityEngine;

public class Sun : MonoBehaviour
{
    public float dayDurationInSeconds = 120f; // Duration of a full day-night cycle
    public Light sunLight;
   
    void Update()
    {
        RotateSun();
        UpdateSunLight();
        UpdateSunColor();
    }
    void RotateSun()
    {
        var angleperSecond = 360f / dayDurationInSeconds;
        transform.Rotate(Vector3.right, angleperSecond * Time.deltaTime);


    }
    void UpdateSunLight()
    {
        sunLight.intensity = Mathf.Clamp01(Vector3.Dot(transform.forward, Vector3.down));
    }
    void UpdateSunColor()
    {
        var t = Mathf.Clamp01(Vector3.Dot(transform.forward, Vector3.down));
        sunLight.color = Color.Lerp(new Color(1f, 0.5f, 0f), Color.white, t);
    }
}
