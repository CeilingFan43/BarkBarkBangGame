using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
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
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColour.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f ) - 90f, 170, 0));

            //NOTE FOR DECLAN!!!
            //Alter rotation so it works from under the map to a fixed sunrise position. Then make it so that the rotation doesn't happen until x% of the timer is completed 
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
