using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public VideoPlayer videoPlayer;

    private bool isTeleporting = false;
    private Collider playerToTeleport;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;
            playerToTeleport = other;
            videoPlayer.Play();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (playerToTeleport != null)
        {
            TeleportPlayer(playerToTeleport);
            playerToTeleport = null;
        }
    }

    private void TeleportPlayer(Collider player)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();

        if (characterController != null)
        {
            characterController.enabled = false;

            Vector3 newPosition = destination.position;
            newPosition.z += 3;
            player.transform.position = newPosition;

            characterController.enabled = true;
        }

        isTeleporting = false;
    }
}