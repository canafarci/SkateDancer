using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class SequentialIKTrigger : MonoBehaviour, IActionTrigger
{
    private PlayerReferences playerReferences;
    private PlayerIKManager playerIKManager;
    public int stage = 0;
    public string AnimationToPlay;
    public GameObject[] dragSprites, Obstacles;
    public bool leftFoot, rightFoot, body, leftHand, rightHand;
    public bool skipFinalCam, DontAlignRotation = false;
    private float clipLength;
    private AnimationClip animationClip;

    /*  public Image ClockFillImage;
     public GameObject ClockHand, Clock;
     private Coroutine clockRoutine = null;
     public TextMeshProUGUI clockText; */

    public GameObject Player, DefaultCam, DefaultBulletTimeCam;
    private GifUIController gifController;
    private Recorder recorder;
    public float likesToGet = 100f;
    private int randomBulletCameraIndex, gifCameraIndex;
    private bool IsActive = false;
    private Vector3 initialPosition;
    public float animationSpeed;
    public bool isWallRide = false;

    [Range(1.0f, 30.0f)]
    public float SkeletonInterpSpeed = 1f;

    private void Awake()
    {
        playerReferences = FindObjectOfType<PlayerReferences>();
        playerIKManager = FindObjectOfType<PlayerIKManager>();
        gifController = FindObjectOfType<GifUIController>();
        recorder = FindObjectOfType<Recorder>();
    }

    private void Start()
    {
        animationClip = playerReferences.FindAnimation(AnimationToPlay);
        clipLength = animationClip.length;
        clipLength = clipLength / animationSpeed;
//        print(clipLength + gameObject.name);
        Player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    private void Update()
    {

        if (!IsActive) return;

        Vector3 _newPos = Player.transform.position;
        _newPos.y = initialPosition.y;

        transform.position = _newPos;

        if (!DontAlignRotation)
            transform.rotation = Player.transform.rotation;
    }

    public void ActionTrigger()
    {
        Time.timeScale = 1f;
        GetComponent<BoxCollider>().enabled = false;

        SequentialIK(stage);

        /* if (clockRoutine != null)
            StopCoroutine(clockRoutine);

        clockRoutine = StartCoroutine(ClockRoutine()); */

        if (Obstacles != null)
        {
            for (int i = 0; i < Obstacles.Length; i++)
            {
                Obstacles[i].transform.parent = null;
            }
        }
        IsActive = true;
    }



    private void SequentialIK(int __stage)
    {
        switch (__stage)
        {
            case (0):
                StartBulletTime();

                if (leftFoot)
                {
                    playerIKManager.HandleIKEnabled(true, false, false, false, false);

                    dragSprites[0].SetActive(true);
                    dragSprites[0].transform.parent.GetComponent<SilhouetteTrigger>().OnTriggerActive();
                }
                else
                    OnDragSuccessful();
                break;

            case (1):
                if (rightFoot)
                    playerIKManager.HandleIKEnabled(false, true, false, false, false);
                else
                    OnDragSuccessful();
                break;

            case (2):
                if (body)
                    playerIKManager.HandleIKEnabled(false, false, true, false, false);
                else
                    OnDragSuccessful();
                break;

            case (3):
                if (rightHand)
                    playerIKManager.HandleIKEnabled(false, false, false, true, false);
                else
                    OnDragSuccessful();
                break;

            case (4):
                if (leftHand)
                    playerIKManager.HandleIKEnabled(false, false, false, false, true);
                else
                    OnDragSuccessful();
                break;

            case (5):
                if (playerReferences.PlayerActionRoutine != null)
                    StopCoroutine(playerReferences.PlayerActionRoutine);

                playerReferences.PlayerActionRoutine = StartCoroutine(EndBulletTime());

                int rand = UnityEngine.Random.Range(0, AudioManager.Instance.CheerSFXs.Length);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CheerSFXs[rand]);
                break;

            default:
                break;
        }
    }

    private void StartBulletTime()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.SlowDownSFX,0.3f);
        playerReferences.timeManager.SlowMo();

        playerReferences.IKCamera.SetActive(true);

        playerIKManager.SetBoneWeights();

        playerIKManager.SetFirstTargets();
        playerIKManager.ResetOldTransforms();
    }

    IEnumerator EndBulletTime()
    {
        SetCameraIndexes();

        DollyTrackCameraController _dollyTrackControllerBulletTimeCam = playerReferences.BulletTimeCameras[randomBulletCameraIndex].GetComponent<DollyTrackCameraController>();
        DollyTrackCameraController _dollyTrackControllerGifCam = playerReferences.GifCameras[gifCameraIndex].GetComponent<DollyTrackCameraController>();

        //StartTimer();

        InstantiateVFX();

        if (!skipFinalCam)
        {
            playerReferences.IKCamera.SetActive(false);
            playerReferences.BulletTimeCameras[randomBulletCameraIndex].SetActive(true);
            playerReferences.GifCameras[gifCameraIndex].SetActive(true);
        }

        playerIKManager.EmptyTransformList();
        playerReferences.timeManager.SpeedUp(0.33f);

        if (isWallRide) 
        {
            StartCoroutine(StartWallRideSFX());
        }

        playerReferences.animator.Play(AnimationToPlay);

        StartCoroutine(SkeletonInterpRoutine());

        if (!skipFinalCam)
        {
            StartCoroutine(gifController.StartRecording());
            StartCoroutine(_dollyTrackControllerGifCam.AnimateCam(clipLength, 0.33f));
            yield return StartCoroutine(_dollyTrackControllerBulletTimeCam.AnimateCam(clipLength, 0.33f));
        }

        yield return new WaitForSecondsRealtime(1f);

        if (!skipFinalCam)
        {
            _dollyTrackControllerBulletTimeCam.DollyCam.m_PathPosition = 0f;
            _dollyTrackControllerGifCam.DollyCam.m_PathPosition = 0f;
            playerReferences.BulletTimeCameras[randomBulletCameraIndex].SetActive(false);
            playerReferences.GifCameras[gifCameraIndex].SetActive(false);
            StartCoroutine(gifController.PlayGIF((int)likesToGet));
        }

        this.enabled = false;

    }

    IEnumerator StartWallRideSFX()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.WallRideSFX, 0.7f);
    }

    IEnumerator SkeletonInterpRoutine()
    {
        yield return StartCoroutine(playerIKManager.LowerBoneWeightsRoutine(SkeletonInterpSpeed));

        playerIKManager.HandleIKDisabled();
    }

    private void InstantiateVFX()
    {
        int randomText = UnityEngine.Random.Range(0, playerReferences.successTexts.Length);
        GameObject winEffect = Instantiate(playerReferences.successTexts[randomText], new Vector3(playerReferences.transform.position.x, playerReferences.transform.position.y + 8, playerReferences.transform.position.z), Quaternion.identity, playerReferences.transform);
        winEffect.transform.LookAt(Camera.main.transform.position);
        Destroy(winEffect, 5f);
    }

    /* private void StartTimer()
    {
        if (clockRoutine != null)
            StopCoroutine(clockRoutine);

        ClockFillImage.fillAmount = 1f;
        ClockHand.transform.eulerAngles = Vector3.zero;
        Clock.SetActive(false);
    } */

    private void SetCameraIndexes()
    {
        if (DefaultCam == null)
        {
            randomBulletCameraIndex = UnityEngine.Random.Range(0, playerReferences.BulletTimeCameras.Length);
            RecursiveSetGifCamera();
        }
        else
        {
            for (int i = 0; i < playerReferences.BulletTimeCameras.Length; i++)
            {
                if (playerReferences.BulletTimeCameras[i] == DefaultCam)
                {
                    randomBulletCameraIndex = i;
                }
            }
            RecursiveSetGifCamera();
        }

    }

    private void RecursiveSetGifCamera()
    {
        if (DefaultBulletTimeCam == null)
        {
            gifCameraIndex = UnityEngine.Random.Range(0, playerReferences.GifCameras.Length);

            if (gifCameraIndex == randomBulletCameraIndex)
                gifCameraIndex = UnityEngine.Random.Range(0, playerReferences.GifCameras.Length);

            if (gifCameraIndex == randomBulletCameraIndex)
                gifCameraIndex = UnityEngine.Random.Range(0, playerReferences.GifCameras.Length);
        }
        else
        {
            for (int i = 0; i < playerReferences.GifCameras.Length; i++)
            {
                if (playerReferences.GifCameras[i] == DefaultBulletTimeCam)
                {
                    gifCameraIndex = i;
                }
            }
        }
    }

    public void OnDragSuccessful()
    {
        if (dragSprites[stage] != null)
        {
            dragSprites[stage].SetActive(false);

            Transform vfxPos = GameObject.FindGameObjectWithTag("VFXPos").transform;
            GameObject effect = Instantiate(playerReferences.DragSuccessfulVFX, vfxPos.position, vfxPos.rotation);
            Destroy(effect, 1f);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SuccessIKSFX,0.1f);
        }

        stage++;

        playerIKManager.EmptyTransformList();

        SequentialIK(stage);

        if (stage != 5 && dragSprites[stage] != null)
        {
            dragSprites[stage].SetActive(true);
            dragSprites[stage].transform.parent.GetComponent<SilhouetteTrigger>().OnTriggerActive();
        }

    }

    /* IEnumerator ClockRoutine()
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
    } */





}
