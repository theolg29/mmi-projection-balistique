using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private float minOffTime = 0.1f;
    private float maxOffTime = 1.0f;
    private float minOnTime = 0.1f;
    private float maxOnTime = 2.0f;

    // Effet de grésillement
    private float flickerDuration = 0.3f;
    private float flickerSpeed = 20f;

    // Détection par tag
    private bool onlyNeonEffect = false;
    private string neonTag = "neon-only";

    private List<GameObject> childObjects = new();
    private List<Light> lights = new();

    void Start()
    {
        onlyNeonEffect = gameObject.CompareTag(neonTag);

        foreach (Transform child in transform)
        {
            childObjects.Add(child.gameObject);

            Light light = child.GetComponent<Light>();
            if (light != null)
            {
                lights.Add(light);
            }
        }

        if (childObjects.Count == 0 && lights.Count == 0)
        {
            return;
        }

        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            if (!onlyNeonEffect)
            {
                SetChildrenActive(false);
                yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
                SetChildrenActive(true);
            }

            yield return StartCoroutine(NeonFlickerEffect());
            yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));
        }
    }

    void SetChildrenActive(bool state)
    {
        foreach (var obj in childObjects)
        {
            obj.SetActive(state);
        }
    }

    IEnumerator NeonFlickerEffect()
    {
        float timer = 0f;
        List<float> baseIntensities = new();

        foreach (var light in lights)
        {
            baseIntensities.Add(light.intensity);
        }

        while (timer < flickerDuration)
        {
            for (int i = 0; i < lights.Count; i++)
            {
                if (lights[i] != null)
                {
                    float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, i * 10f);
                    lights[i].intensity = baseIntensities[i] * noise;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < lights.Count; i++)
        {
            if (lights[i] != null)
                lights[i].intensity = baseIntensities[i];
        }
    }
}
