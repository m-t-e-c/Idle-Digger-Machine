using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [Header("===Audio Clips===")]
    public AudioClip buttonSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip errorSFX;
    public AudioClip ambient;
    public AudioClip[] bgm;

    // Audio Sources
    List<AudioSource> sourceSFX = new List<AudioSource>();
    List<AudioSource> sourceBGM = new List<AudioSource>();
    AudioSource sourceAmbient;

    [Header("===Playback Settings===")]
    public float timeToFade = 2.5f;

    // Playback Variables
    float volumeLevel;
    bool firstSourcePlaying = false;
    bool faded;
    int trackCount;

    // Vitals
    [HideInInspector] public UIControl uIControl;

#region Initializers
    public void InitializeAudio()
    {
        SetAudioSources();
        GetVolume();
        SetVolume();
        uIControl.settings.CustomDebug("System Event : Initialized Audio");
    }
    void SetAudioSources()
    {
        foreach(AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource>())
        {
            if(audioSource.tag == "SFX") sourceSFX.Add(audioSource);
            if(audioSource.tag == "BGM") sourceBGM.Add(audioSource);
            if(audioSource.tag == "Ambient") sourceAmbient = audioSource;
        }
    }
    void CheckBGM()
    {
        if(!uIControl.settings.audioEnabled) return;
        trackCount = Random.Range(0,bgm.Length);
        sourceBGM[0].clip = bgm[trackCount];
        sourceBGM[0].Play();
        uIControl.settings.CustomDebug("System Event : BGM Started Playing");
    }
#endregion

#region Audio Variables
    public void SetVolume()
    {
        // SFX
        foreach(AudioSource x in sourceSFX) x.volume = uIControl.sfxSlider.value;
        // Ambient
        sourceAmbient.volume = uIControl.sfxSlider.value/3;
        // BGM
        volumeLevel = uIControl.bgmSlider.value/3;
        sourceBGM[0].volume = uIControl.bgmSlider.value/3;
        sourceBGM[1].volume = uIControl.bgmSlider.value/3;
        PlayerPrefs.SetFloat("bgmVolume", uIControl.bgmSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", uIControl.sfxSlider.value);
        uIControl.settings.CustomDebug("Game Event : Volume Changed");
    }
    void GetVolume()
    {
        uIControl.bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume", 0.5f);
        uIControl.sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
    }
#endregion

#region Audio Functions
    public void SwapTrackNoFade(AudioClip newClip)
    {
        if(firstSourcePlaying)
        {
            sourceBGM[1].clip = newClip;
            sourceBGM[1].Play();
            sourceBGM[0].Stop();
        }
        else
        {
            sourceBGM[0].clip = newClip;
            sourceBGM[0].Play();
            sourceBGM[1].Stop();
        }
        firstSourcePlaying = !firstSourcePlaying;
    }
    public void FadeTrack()
    {
        if(faded) return;
        if(sourceBGM[0].isPlaying && sourceBGM[0].time >= sourceBGM[0].clip.length - timeToFade) ShuffleBeforeFade();
        if(sourceBGM[1].isPlaying && sourceBGM[1].time >= sourceBGM[1].clip.length - timeToFade) ShuffleBeforeFade();
    }
    public void PlaySFX(AudioClip x)
    {
        if(sourceSFX != null)
        {
            foreach(AudioSource y in sourceSFX)
            {
                if(y.isPlaying == false) { y.PlayOneShot(x); return; }
            }
        }
    }
#endregion

#region Audio Fade Functions
    void ShuffleBeforeFade()
    {
        float x = trackCount;
        trackCount = Random.Range(0,bgm.Length);
        if(x==trackCount) trackCount = Random.Range(0,bgm.Length);
        faded = true;
        CheckFadeSource(bgm[trackCount]);
    }
    void CheckFadeSource(AudioClip newClip)
    {
        float timeElapsed = 0;
        if(firstSourcePlaying) StartCoroutine(CrossFade(sourceBGM[1], sourceBGM[0], newClip, timeElapsed));
        else StartCoroutine(CrossFade(sourceBGM[0], sourceBGM[1], newClip, timeElapsed));
    }
    IEnumerator CrossFade(AudioSource x, AudioSource y, AudioClip newClip, float t)
    {
        x.clip = newClip;
        x.Play();
        while(t < timeToFade)
        {
            x.volume = Mathf.Lerp(0, volumeLevel, t/timeToFade);
            y.volume = Mathf.Lerp(volumeLevel, 0, t/timeToFade);
            t += Time.deltaTime;
            yield return null;
        }
        y.Stop();
        faded = false;
        firstSourcePlaying = x.isPlaying;
        uIControl.settings.CustomDebug("Game Event : Audio crossfaded from " + x.name + " to " + y.name);
    }
#endregion

}
