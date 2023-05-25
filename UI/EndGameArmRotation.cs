using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class EndGameArmRotation : MonoBehaviour
{
    [SerializeField] float angleModifier = 90f;
    [SerializeField] float speed = 2f;

    [SerializeField] List<Ramp> endGameRamps = new List<Ramp>();
    TimeManager timeManager;
    PathFollower pathFollower;

    bool moving = true;
    bool called = false;
    float angle;
    bool playerSpeedingUp;
    public GameObject EndGameCam;
    PlayerReferences playerReferences;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
        pathFollower = FindObjectOfType<PathFollower>();
        playerReferences = FindObjectOfType<PlayerReferences>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) 
        {
            angle = Mathf.Sin(Time.time * speed) * angleModifier; //tweak this to change frequency
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (playerSpeedingUp)
        {
            pathFollower.speed += 0.04f;
            if (pathFollower.speed >= 20f)
            {
                playerSpeedingUp = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && !called)
        {
            moving = false;
            called = true;

            timeManager.SpeedUp();
            playerSpeedingUp = true;
            if (angle <= 20 && angle >= -20)
            {
                //endGameRamps[0].gameObject.SetActive(true);
                StartCoroutine(StartMovingToPos(endGameRamps.Count));
            }
            else if ((angle > 20 && angle <= 60) || (angle < -20 && angle >= -60)) 
            {
                StartCoroutine(StartMovingToPos(endGameRamps.Count));

            }
            else if ((angle > 60 && angle <= 90) || (angle < -60 && angle >= -90)) 
            {
                StartCoroutine(StartMovingToPos(endGameRamps.Count));
            }
        }
    }

    IEnumerator StartMovingToPos(int platformNum) 
    {
        StartCoroutine(playerReferences.EndGameCamRoutine());
        for (int i = 0; i < platformNum; i++) 
        {
            endGameRamps[i].gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.rampEffectSFX,5f);
            yield return new WaitForSeconds(0.4f);
        }
        transform.parent.gameObject.SetActive(false);
    }

    
}
