using UnityEngine;
using System.Collections.Generic;

public class BinDetector : MonoBehaviour
{
    public enum BinType { Association, Enregistrement }
    [Header("Configuration")]
    public BinType binType = BinType.Association;
    public GameObject validObject;
    public AudioClip successSound;
    public AudioClip errorSound;

    private AudioSource audioSource;
    private HashSet<GameObject> validatedObjects = new HashSet<GameObject>();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("bullet")) return;

        if (validatedObjects.Contains(other.gameObject)) return;

        if (other.gameObject == validObject)
        {
            PlaySound(successSound);
            validatedObjects.Add(other.gameObject);

            switch (binType)
            {
                case BinType.Association:
                    GameManager.Instance?.AddLinkScore();
                    break;
                case BinType.Enregistrement:
                    GameManager.Instance?.AddPropScore();
                    break;
            }
        }
        else
        {
            PlaySound(errorSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
