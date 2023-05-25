using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DollyTrackCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    [HideInInspector] public CinemachineTrackedDolly DollyCam;
    public bool isEndGameCam = false;

    private void Awake() 
    {
        CinemachineVirtualCamera= GetComponent<CinemachineVirtualCamera>();
        DollyCam = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void OnEnable() {
        if (isEndGameCam)
        {
            StartCoroutine(EndGameCamAnimation());
            StartCoroutine(DelayedLoadNextSecene());
        }
    }
    
    private IEnumerator EndGameCamAnimation()
    {
        while (DollyCam.m_PathPosition < 1f)
        {
            DollyCam.m_PathPosition += 0.005f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return new WaitForSecondsRealtime(1f);
        DollyCam.m_PathPosition = 0f;
        StartCoroutine(EndGameCamAnimation());
    }

    public IEnumerator AnimateCam(float __animationDuration = 4.5f, float __slowDownFactor = 0.02f)
    {   
        float timeStep =__animationDuration / (1f / 0.005f);

        while (DollyCam.m_PathPosition < 1f)
        {
            DollyCam.m_PathPosition += 0.01f;
            yield return new WaitForSecondsRealtime(timeStep);
        }        
    }

    IEnumerator DelayedLoadNextSecene()
    {
        yield return new WaitForSeconds(5f);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex != 4)
            sceneIndex += 1;
        else
            sceneIndex = 0;

        SceneManager.LoadScene(sceneIndex);
    }
}