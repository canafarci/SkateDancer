using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EasyMobile;
using TMPro;
using UnityEngine;

public class GifUIController : MonoBehaviour
{

    //public
    public GameObject GifUI;
    public AnimatedClip recordedClip;
    public ClipPlayerUI clipPlayer;
    public int LikeCount = 0;
    public TextMeshProUGUI likeText, heartText;
    public RectTransform HeartIcon;
    public float heartPulseScale = 0.4f;

    //private
    private bool IsAnimating = false;
    private Recorder recorder;
    private PlayerReferences playerReferences;
    private Coroutine shakeRoutine, gifRoutine = null;
    

    private void Awake()
    {
        recorder = FindObjectOfType<Recorder>();
        playerReferences = FindObjectOfType<PlayerReferences>();
    }

    public IEnumerator StartRecording()
    {
        // Dispose the old clip
        if (recordedClip != null)
            recordedClip.Dispose();

        yield return new WaitForSecondsRealtime(0.25f);

        Gif.StartRecording(recorder);

        yield return new WaitForSecondsRealtime(recorder.Length);

        recordedClip = Gif.StopRecording(recorder);
//        print(recordedClip);
    }

    public IEnumerator PlayGIF(int __likeIncrease)
    {
        playerReferences.timeManager.SpeedUp();
           
        if (recordedClip != null)
        {
            IsAnimating = true;
            if (shakeRoutine != null)
                StopCoroutine(shakeRoutine);

            shakeRoutine = StartCoroutine(ShakeRoutine());
//            print("palying gif");
            GifUI.SetActive(true);
            StartCoroutine(OnLikeCountIncreased(__likeIncrease));
            Gif.PlayClip(clipPlayer, recordedClip);
        }

        /* ExportMyGif(); */

        yield return new WaitForSecondsRealtime(recorder.Length + 0.5f);

        IsAnimating = false;
        clipPlayer.Stop();
        GifUI.SetActive(false);
    }

    public IEnumerator OnLikeCountIncreased(int __increaseAmount)
    {
        int _oldLikeCount = LikeCount;
        int _endLikeCount = LikeCount + __increaseAmount;
        int _phoneLikeAmount = 0;
        float _time = recorder.Length;

//        print(_endLikeCount);

        for (float t = 0f; t <= _time - 0.2f; t += Time.unscaledDeltaTime)
        {
            LikeCount = (int)Mathf.Lerp((float)LikeCount, (float)(_endLikeCount + 1), t / (_time - 0.2f));

            _phoneLikeAmount = (int)Mathf.Lerp((float)_phoneLikeAmount, (float)(__increaseAmount + 1), t / (_time - 0.2f));

            likeText.text = _phoneLikeAmount.ToString();
            heartText.text = LikeCount.ToString();

            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
//            print("loooooopingg");
        }

        yield return new WaitForSecondsRealtime(0.1f);
        playerReferences.currentLikes = LikeCount;

    }

    IEnumerator ShakeRoutine()
    {
        while(IsAnimating)
        {
            HeartIcon.DOPunchScale(new Vector3(heartPulseScale,heartPulseScale, heartPulseScale), 0.65f, 1);
            yield return new WaitForSeconds(0.65f);
            HeartIcon.localScale = Vector3.one;
        }
    }

    /* void ExportMyGif()
    {
        // Parameter setup
        string filename = "myGif";    // filename, no need the ".gif" extension
        int loop = 0;                 // -1: no loop, 0: loop indefinitely, >0: loop a set number of times
        int quality = 80;             // 80 is a good value in terms of time-quality balance
        System.Threading.ThreadPriority tPriority = System.Threading.ThreadPriority.Normal; // exporting thread priority

        Gif.ExportGif(recordedClip,
                    filename,
                    loop,
                    quality,
                    tPriority,
                    OnGifExportProgress,
                    OnGifExportCompleted);
    }

    void OnGifExportProgress(AnimatedClip clip, float progress)
    {
        Debug.Log(string.Format("Export progress: {0:P0}", progress));
    }


    // This callback is called once the GIF exporting has completed.
    // It receives a reference to the original clip and the filepath of the generated image.
    void OnGifExportCompleted(AnimatedClip clip, string path)
    {
        Debug.Log("A GIF image has been created at " + path);
    } */
}
