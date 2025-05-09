using UnityEngine;

public class PlayText : MonoBehaviour
{
    public AudioClip textSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = textSound;
        audioSource.playOnAwake = false;
    }

    public void PlaySoundText()
    {
        audioSource.Play();
    }
}
