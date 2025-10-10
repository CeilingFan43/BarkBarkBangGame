using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Light DirectionalLightMoon;
    [SerializeField] private LightingConditions Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    public gameManager gameManager;
    //add refernce to game manager for time left in game to calculate change of lighting 

    private void Update()
    {
        if(Preset==null)
        {
            return;
        }
        if(Application.isPlaying)
        {
            float timePercent = 1f - (gameManager.currentTime / gameManager.maxTime);
            UpdateLighting(timePercent);
        }
        else
        {
            UpdateLighting(TimeOfDay / gameManager.maxTime);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColour.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            if (timePercent >= 0.8f)
            {
                float sunriseProgress = Mathf.InverseLerp(0.8f, 1f, timePercent);

                DirectionalLight.transform.localRotation =
                Quaternion.Euler(new Vector3((sunriseProgress * 90f) - 90f, 170, 0));
            }
            else
            {
                DirectionalLight.transform.localRotation =
                Quaternion.Euler(new Vector3(-90f, 170, 0));
            }
        }
    } 

private void OnValidate()
{
    if (DirectionalLight != null)
        return;

    if (RenderSettings.sun != null)
    {
        DirectionalLight = RenderSettings.sun;
    }
    else
    {
        Light[] lights = GameObject.FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                DirectionalLight = light;
                return;
            }
        }
    }
}
    
}
