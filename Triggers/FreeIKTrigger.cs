/* using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeIKTrigger : MonoBehaviour, IActionTrigger
{
    PlayerReferences playerReferences;
    PlayerIKManager playerIKManager;
    bool inBulletTime = false;
    public float BulletTimeDuration;

    public Image ClockFillImage;
    public GameObject ClockHand, Clock;
    private Coroutine clockRoutine = null;
    public Coroutine FreeIKRoutine = null;
    public TextMeshProUGUI clockText;

    private void Awake() 
    {
        playerReferences = FindObjectOfType<PlayerReferences>();
        playerIKManager = FindObjectOfType<PlayerIKManager>();
    }

    public void ActionTrigger()
    {
        GetComponent<BoxCollider>().enabled = false;

        FreeIKRoutine = StartCoroutine(TimeManagerForFreeIKObstacle());

        playerIKManager.SetBoneWeights();

        if (clockRoutine != null)
            StopCoroutine(clockRoutine);

        clockRoutine = StartCoroutine(ClockRoutine());
    }

    IEnumerator ClockRoutine()
    {
        float t = 10f;
        Clock.SetActive(true);

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            ClockHand.transform.RotateAround(ClockHand.transform.position, Vector3.back, 36 * Time.unscaledDeltaTime);
            ClockFillImage.fillAmount -= (Time.unscaledDeltaTime) / 10f;
            clockText.text = Mathf.Round(t).ToString();
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }

        ClockFillImage.fillAmount = 1f;
        ClockHand.transform.eulerAngles = Vector3.zero;
        Clock.SetActive(false);
    }

    IEnumerator TimeManagerForFreeIKObstacle()
    {

        DollyTrackCameraController _dollyTrackController = playerReferences.BulletTimeCamera.GetComponent<DollyTrackCameraController>();
        //berke
        playerReferences.timeManager.SlowMo();

        //SetFirstTargets();
        //realtimeIKRaycaster.StartIK();
        if (!inBulletTime)
        {
            playerReferences.IKCamera.SetActive(true);
            inBulletTime = true;

            playerIKManager.HandleIKEnabled(false, false);
        }

        yield return new WaitForSecondsRealtime(BulletTimeDuration);

        StartCoroutine(FreeIKSpeedUp());
    }

    public IEnumerator FreeIKSpeedUp() 
    {
        DollyTrackCameraController _dollyTrackController = playerReferences.BulletTimeCamera.GetComponent<DollyTrackCameraController>();

        if (clockRoutine != null)
            StopCoroutine(clockRoutine);

        ClockFillImage.fillAmount = 1f;
        ClockHand.transform.eulerAngles = Vector3.zero;
        Clock.SetActive(false);

        playerReferences.IKCamera.SetActive(false);

        foreach (IKTarget _fit in FindObjectsOfType<IKTarget>())
        {
            _fit.transform.GetChild(0).transform.gameObject.SetActive(false);
        }        

        playerReferences.BulletTimeCamera.SetActive(true);

        yield return StartCoroutine(_dollyTrackController.AnimateCam(10f));

        playerReferences.timeManager.SpeedUp();

        yield return new WaitForSecondsRealtime(1f);

        _dollyTrackController.DollyCam.m_PathPosition = 0f;

        playerReferences.BulletTimeCamera.SetActive(false);
        playerIKManager.EmptyTransformList();
        playerIKManager.LowerBoneWeights();
        yield return new WaitForSecondsRealtime(1f);
        playerIKManager.HandleIKDisabled();
        //berke
        

        inBulletTime = false;
        //realtimeIKRaycaster.StopIK();
    }
}
 */