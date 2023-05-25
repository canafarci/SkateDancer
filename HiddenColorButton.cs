using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boxophobic.StyledGUI;
using UnityEngine.Rendering;

public class HiddenColorButton : MonoBehaviour
{
    public Material material1, material2, material3, material4, material5, material6, material7, material8, material9, material10, material11, material12, skybox;
    public Material[] object1Materials, object2Materials, object3Materials, object4Materials, object5Materials, object6Materials, object7Materials,
                         object8Materials, object9Materials, object10Materials, object11Materials, object12Materials, skyboxMaterials;

    public VolumeProfile[] postProfiles;
    public VolumeProfile currentPostProfile;
    public Color[] FogColors;
    public Color fogColor;
    public static int currentColorIndex = 0;
    private PolyverseSkies polyverseSkies;
    private Volume post;
    private GameObject _waterObject;

    private void Awake()
    {
        polyverseSkies = FindObjectOfType<PolyverseSkies>();
        post = FindObjectOfType<Volume>();
    }

    private void Start()
    {
        foreach (Renderer _rnd in FindObjectsOfType<Renderer>())
        {
            if (_rnd.sharedMaterial == material6)
                _waterObject = _rnd.gameObject;
        }

        OnColorButtonClicked(currentColorIndex);
    }

    private void SetStartReferenceMaterials()
    {
        material1 = object1Materials[currentColorIndex];
        material2 = object2Materials[currentColorIndex];
        material3 = object3Materials[currentColorIndex];
        material4 = object4Materials[currentColorIndex];
        material5 = object5Materials[currentColorIndex];
        if (material6 != null)
            material6 = object6Materials[currentColorIndex];
        if (material7 != null)
            material7 = object7Materials[currentColorIndex];
        if (material8 != null)
            material8 = object8Materials[currentColorIndex];
        if (material9 != null)
            material9 = object9Materials[currentColorIndex];
        if (material10 != null)
            material10 = object10Materials[currentColorIndex];
        if (material11 != null)
            material11 = object11Materials[currentColorIndex];
        if (material12 != null)
            material12 = object12Materials[currentColorIndex];

        skybox = skyboxMaterials[currentColorIndex];
        fogColor = FogColors[currentColorIndex];
        currentPostProfile = postProfiles[currentColorIndex];
    }

    public void OnHiddenSwitch(GameObject __go)
    {
        if (__go.activeSelf)
            __go.SetActive(false);
        else
            __go.SetActive(true);
    }

    public void OnColorButtonClicked(int __colorIndex)
    {
        foreach (Renderer _rnd in FindObjectsOfType<Renderer>())
        {
            if (_rnd.sharedMaterial == material1)
            {
                _rnd.sharedMaterial = object1Materials[__colorIndex];
                print("found mat");
            }


            else if (_rnd.sharedMaterial == material2)
                _rnd.sharedMaterial = object2Materials[__colorIndex];

            else if (_rnd.sharedMaterial == material3)
                _rnd.sharedMaterial = object3Materials[__colorIndex];

            else if (_rnd.sharedMaterial == material4)
                _rnd.sharedMaterial = object4Materials[__colorIndex];

            else if (_rnd.sharedMaterial == material5)
                _rnd.sharedMaterial = object5Materials[__colorIndex];

            else if (material6 != null && _rnd.sharedMaterial == material6)
                _rnd.sharedMaterial = object6Materials[__colorIndex];

            else if (material7 != null && _rnd.sharedMaterial == material7)
                _rnd.sharedMaterial = object7Materials[__colorIndex];

            else if (material8 != null && _rnd.sharedMaterial == material8)
                _rnd.sharedMaterial = object8Materials[__colorIndex];

            else if (material9 != null && _rnd.sharedMaterial == material9)
                _rnd.sharedMaterial = object9Materials[__colorIndex];

            else if (material10 != null && _rnd.sharedMaterial == material10)
                _rnd.sharedMaterial = object10Materials[__colorIndex];

            else if (material11 != null && _rnd.sharedMaterial == material11)
                _rnd.sharedMaterial = object11Materials[__colorIndex];

            else if (material12 != null && _rnd.sharedMaterial == material12)
                _rnd.sharedMaterial = object12Materials[__colorIndex];



            print("looping colors");
        }


        RenderSettings.fogColor = FogColors[__colorIndex];
        RenderSettings.fog = true;

        polyverseSkies.skyboxDay = skyboxMaterials[__colorIndex];
        polyverseSkies.skyboxNight = skyboxMaterials[__colorIndex];
        post.profile = postProfiles[__colorIndex];

        currentColorIndex = __colorIndex;

        if (currentColorIndex == 2)
            _waterObject.SetActive(false);
        else
            _waterObject.SetActive(true);

        SetStartReferenceMaterials();
    }









    /* public GameObject[] Material1Objects, Material2Objects, Color3Objects;
    public Material[] Material1, Material2, Material3;

    public static int currentColorIndex = 0;

    public void OnColorButtonClicked()
    {
        currentColorIndex++;
        
        for (int i = 0; i < Material1Objects.Length; i++)
        {
            Material1Objects[i].GetComponent<Renderer>().material = Material1[currentColorIndex];
        }

        for (int i = 0; i < Material2Objects.Length; i++)
        {
            Material2Objects[i].GetComponent<Renderer>().material = Material2[currentColorIndex];
        }

        for (int i = 0; i < Color3Objects.Length; i++)
        {
            Color3Objects[i].GetComponent<Renderer>().material = Material3[currentColorIndex];
        }
    } */
}
