using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bntFX : MonoBehaviour
{
    public AudioSource soundFX;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound()
    {
        soundFX.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        soundFX.PlayOneShot(clickSound);
    }
}
