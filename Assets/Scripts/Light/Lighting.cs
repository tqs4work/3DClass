using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Light lightninglight;
    public AudioSource lightningsound;
    public AudioClip thunderSound;
    private float LightningTimer = 0f;
    private float LightningInterval = 5f;

     void Start()
    {
        lightninglight.enabled = false;
        LightningTimer = LightningInterval;
    }
    private void Update()
    {
        LightningTimer -= Time.deltaTime;
        if (LightningTimer <= 0f)
        {
            StartCoroutine(FlashLightning());
            LightningTimer = Random.Range(5f, 15f);
        }
    }
    IEnumerator FlashLightning()
    {
        lightninglight.enabled = true;
        //lightninglight.intensity = Random.Range(2f, 5f);
        lightningsound.PlayOneShot(thunderSound);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
        lightninglight.enabled = false;
    }
}
