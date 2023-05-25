using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    //public
    [Space(10)]
    [Header("THEME SFXs")]
    //public AudioClip[] AmbianceSFX;
    public AudioClip[] PlayableLevelMusic;
    public AudioClip[] MainMenuMusic;
    public AudioClip[] AmbianceSFX;

    [Space(10)]
    [Header("GAMEPLAY SFXs")]
    public AudioClip DiamondSFX;
    public AudioClip WindSFX;
    public AudioClip SlowDownSFX;
    public AudioClip WallRideSFX;
    public AudioClip rampEffectSFX;
    public AudioClip[] JumpSFXs;
    public AudioClip[] CheerSFXs;

    [Space(10)]
    [Header("UI SFXs")]
    public AudioClip SuccessIKSFX;


    public AudioClip EndGameSFX;

    //private
    public AudioSource audioSourceTheme, audioSourceAmbiance;
    public bool isMainMenu;
    private bool isFirstTimeMusicPlaying = true;
    private int randomMusicIndex, previousMusicIndex;

    //singleton
    public static AudioManager Instance { get; private set; }
    void Awake()
    {
        //Singleton Method
        if (Instance)
        {
            Destroy(gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }

        audioSourceTheme = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        CheckAndPlayMusic();
    }

    public void PlaySFX(AudioClip __audio, float __volume = 1f, bool __loop = false)
    {
        AudioSource[] _audioSources = GetComponents<AudioSource>();

        for (int i = 0; i < _audioSources.Length; i++)
        {
            if (!_audioSources[i].isPlaying)
            {
                _audioSources[i].clip = __audio;
                _audioSources[i].loop = __loop;
                if (__volume != 1f)
                    _audioSources[i].volume = __volume;
                _audioSources[i].Play();
                break;
            }
            
            if (i == _audioSources.Length - 1)
            {
                AudioSource _as = transform.gameObject.AddComponent<AudioSource>();
                //print(_as.gameObject.name);
                _as.loop = false;
                _as.playOnAwake = false;
                _as.clip = __audio;
                if (__volume != 1f)
                    _as.volume = __volume;
                _as.Play();
                break;
            }

        }
    }

    private void PlayMusic(AudioClip[] music, bool loop = true)
    {
        if (isFirstTimeMusicPlaying)
        {
            audioSourceTheme.clip = music[0];
            audioSourceTheme.Play();
            isFirstTimeMusicPlaying = false;
            previousMusicIndex = 0;
        }
        else
        {
            PlayRandomMusic(music, loop);
        }
    }

    public void StopMusic() 
    {
        audioSourceTheme.Stop();
    }

    private void PlayRandomMusic(AudioClip[] music, bool loop = true)
    {
        randomMusicIndex = Random.Range(0, music.Length);

        if (randomMusicIndex == previousMusicIndex)
        {
            PlayRandomMusic(music, loop);
        }

        audioSourceTheme.clip = music[randomMusicIndex];
        previousMusicIndex = randomMusicIndex;

        audioSourceTheme.Play();
    }

    private void CheckAndPlayMusic()
    {
        //check if audiosource is playing, if not loop music
        if (!audioSourceTheme.isPlaying)
        {
            //Check if main menu and play music accordingly
            if (isMainMenu)
            {
                audioSourceTheme.clip = MainMenuMusic[0];
                audioSourceTheme.Play();
            }
            if (!isMainMenu)
            {
                if (PlayableLevelMusic.Length > 0) 
                {
                    PlayMusic(PlayableLevelMusic);
                }
            }
        }
    }

    private void Start()
    {
        
         //audioSourceTheme.Stop();

        int __randIndex = Random.Range(0, AmbianceSFX.Length);
        audioSourceAmbiance.clip = AmbianceSFX[__randIndex];
        audioSourceAmbiance.Play();
        
    }
    
}
