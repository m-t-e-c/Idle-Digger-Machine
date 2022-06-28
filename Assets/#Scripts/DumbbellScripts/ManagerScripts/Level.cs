using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(fileName = "Level", menuName = "Create Level/Level", order = 1)]
public class Level : ScriptableObject 
{
    [Header("===Required Variables===")]
    public GameObject levelPrefab;

    [Header("===Level Identity===")]
    public string levelName;
    [HideInInspector] public GameObject currentLevel;

    [Header("===Rendering Variables===")]
    public Material skybox;
    public Color32 skyboxColor = Color.white;
    public Color32 fogColor = Color.white;
    public bool isFogEnabled;

    [Header("===Camera Variables===")]
    [Range(10,120)] public float fov;

    [Header("===Post Processing Variables===")]
    public bool postProcessEnabled;
    public PostProcessProfile postProcessProfile;
    public bool randomizeValues;
    [Range(-180,180)] public float hue;
    [Range(-100,100)] public float saturation;
    [Range(-100,100)] public float contrast;
    ColorGrading colorGrading;

    [Header("===Collectibles===")]
    public GameObject[] beneficial;
    public GameObject[] detrimental;

    [Header("===Procedural Generation Variables===")]
    [Range(0,1000)] public float seed;

    public void Load()
    {
        if(levelPrefab != null) currentLevel = Instantiate(levelPrefab) as GameObject;
        if(skybox != null) RenderSettings.skybox = skybox;
    }

    public void ChangeRenderSettings()
    {
        if(skybox != null) RenderSettings.skybox = skybox;
        RenderSettings.skybox.color = skyboxColor;
        RenderSettings.fog = isFogEnabled;
        RenderSettings.fogColor = fogColor;
    }

    public void RandomizePostProcessValues()
    {
        hue = Random.Range(-180,180);
        saturation = Random.Range(-100,100);
        contrast = Random.Range(-100,100);
    }

    public void ChangePostProcessingValues()
    {
        PostProcessVolume x = Camera.main.GetComponent<PostProcessVolume>();
        if(postProcessProfile != null) x.profile = postProcessProfile;
        PostProcessProfile xProfile = x.profile;
        if(randomizeValues) RandomizePostProcessValues();
        xProfile.TryGetSettings(out colorGrading);
        colorGrading.hueShift.value = hue;
        colorGrading.saturation.value = saturation;
        colorGrading.contrast.value = contrast;
    }

    public void ChangeMainCameraFov()
    {
        Camera.main.fieldOfView = fov;
    }
}
