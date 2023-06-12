using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator bulletAnimator;
    [SerializeField] private AudioClip bulletSFX;

    private AudioSource bulletAudioSource;

    private void Awake()
    {
        bulletAudioSource = GetComponent<AudioSource>();
    }

    public void GunFired()
    {
        // animate the gun
        bulletAnimator.SetTrigger("Shot1");

        //play laser gun sfx
        bulletAudioSource.PlayOneShot(bulletSFX);
    }
}