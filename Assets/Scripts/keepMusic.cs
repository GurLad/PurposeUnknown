using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepMusic : MonoBehaviour {
    public AudioClip[] musics;
    public float FadeTime;
    private int CurrentlyPlaying = 99;
    private AudioClip ChangeToMusic;
    private int Fading = 0;
    private float BaseVolume;
    private float count;
    private static keepMusic instance = null;
    public static keepMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }
    }
    void Update()
    {
        BaseVolume = PlayerPrefs.GetFloat("MusicVolume");
        if (Fading == 0) gameObject.GetComponent<AudioSource>().volume = BaseVolume;
        if (CurrentlyPlaying != PlayerPrefs.GetInt("Music"))
        {
            count = 0;
            Fading = -1;
            ChangeToMusic = musics[PlayerPrefs.GetInt("Music")];
            CurrentlyPlaying = PlayerPrefs.GetInt("Music");
        }
        if (GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().Play();
        }
        if (Fading < 0)
        {
            count += Time.deltaTime;
            gameObject.GetComponent<AudioSource>().volume = BaseVolume - (count / FadeTime) * BaseVolume;
        }
        if (Fading > 0)
        {
            count += Time.deltaTime;
            gameObject.GetComponent<AudioSource>().volume = (count / FadeTime) * BaseVolume;
        }
        if ((gameObject.GetComponent<AudioSource>().volume <= 0) && Fading < 0)
        {
            count = 0;
            Fading = 1;
            GetComponent<AudioSource>().clip = ChangeToMusic;
        }
        if (gameObject.GetComponent<AudioSource>().volume >= BaseVolume && Fading > 0)
        {
            count = 0;
            Fading = 0;
        }
        if (gameObject.GetComponent<AudioSource>().volume > BaseVolume) gameObject.GetComponent<AudioSource>().volume = BaseVolume;
        //Debug.Log("Fading: " + Fading + ", Count: " + count + ", BaseVolume: " + BaseVolume + ", CurrentlyPlaying: " + CurrentlyPlaying);
    }
}
