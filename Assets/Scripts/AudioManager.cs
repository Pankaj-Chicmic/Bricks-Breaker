using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bombed;
    [SerializeField] private AudioSource bombHit;
    [SerializeField] private AudioSource brickHit;
    [SerializeField] private AudioSource brickBroken;
    public void PlaybombHit()
    {
        if (bombHit.isPlaying) return;
        bombHit.Play();
    }
    public void PlayBombed()
    {
        if (bombed.isPlaying) return;
        bombed.Play();
    }
    public void PlayBrickHit()
    {
        if (brickHit.isPlaying) return;
        brickHit.Play();
    }
    public void PlayBrickBroken()
    {
        if (brickBroken.isPlaying) return;
        brickBroken.Play();
    }
}
