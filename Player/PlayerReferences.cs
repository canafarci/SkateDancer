using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public TimeManager timeManager;
    public FullBodyBipedIK fullBodyBipedIK;
    public Animator animator;
    public GameObject DragSuccessfulVFX;
    public float currentLikes = 0;
    public GameObject MainCamera, IKCamera, SpeedUpFX, EndGameCinematicCam, EndGameCam;
    public GameObject[] BulletTimeCameras, GifCameras;
    public Coroutine PlayerActionRoutine;

    public GameObject[] successTexts;

    public GameObject[] SmokeFX;

    public AnimationClip FindAnimation(string name)
    {
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            if (animator.runtimeAnimatorController.animationClips[i].name == name)
                return animator.runtimeAnimatorController.animationClips[i];
        }

        Debug.LogError("Animation clip: " + name + " not found");

        return null;
    }

    public IEnumerator EndGameCamRoutine()
    {
        EndGameCam.SetActive(true);
        yield return new WaitForSeconds(5f);
        EndGameCam.SetActive(false);
    }
}
