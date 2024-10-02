using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private float timeofday;

    [SerializeField] private Gradient sunColor;
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equaterColor;
    [SerializeField] private float sunRotationSpeed;

    [SerializeField] private AnimationCurve fogDensity;

    private void Update()
    {
        timeofday += Time.deltaTime * sunRotationSpeed;
        if(timeofday >24)
        {
            timeofday = 0;  
        }
        UpdateSunRotation();
        UpdateLighting();
    }

    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }
    private void UpdateSunRotation()
    {
        float currentTime = timeofday / 24;
        float sunRotation = Mathf.Lerp(-90, 270, currentTime);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }

    private void UpdateLighting()
    {
        float currentTime = timeofday / 24;
        sun.color = sunColor.Evaluate(currentTime);
        RenderSettings.ambientEquatorColor = equaterColor.Evaluate(currentTime);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(currentTime);

        RenderSettings.fogDensity = fogDensity.Evaluate(currentTime);
    }
}
