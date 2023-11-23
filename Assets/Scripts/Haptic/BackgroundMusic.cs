using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    private Music _music;

    private void Awake()
    {
        _music = new Music();
        _audioSource = GetComponent<AudioSource>();

        if (_music.IsEnable)
        {
            _audioSource.Play();
        }
    }
}
