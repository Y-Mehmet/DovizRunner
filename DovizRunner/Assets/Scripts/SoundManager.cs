using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] soundClips;
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayGameSound(SoundType soundType, float volume=0.1f)
    {
        AudioClip clipToPlay = null;

        clipToPlay = soundClips[(int)soundType];

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay, volume);
        }
    }
}
public enum SoundType
{
    AddPika,
    RedDor,
    Fire,
    Water,


}
