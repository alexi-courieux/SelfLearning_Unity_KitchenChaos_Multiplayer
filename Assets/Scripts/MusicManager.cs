using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PlayerPrefsMusicVolume = "MusicVolume";
    public static MusicManager Instance { get; private set; }
    
    public float Volume { get; private set; }
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        
        Volume = PlayerPrefs.GetFloat(PlayerPrefsMusicVolume, .3f);
        _audioSource.volume = Volume;
    }
    
    public void ChangeVolume()
    {
        Volume += .1f;
        if (Volume >= 1.1f)
        {
            Debug.Log(Volume);
            Volume = 0f;
        }

        _audioSource.volume = Volume;
        PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, Volume);
        PlayerPrefs.Save();
    }
}
