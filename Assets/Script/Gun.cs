using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator bulletAnimator;
    [SerializeField] private AudioClip bulletSFX;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private Transform muzzlePosition;

    private AudioSource bulletAudioSource;

    private RaycastHit hit;

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

        //play muzzle effect
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, muzzlePosition.position, muzzlePosition.rotation);
        muzzleFlash.transform.parent = muzzlePosition;
        Destroy(muzzleFlash, 0.2f);

        //raycast
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 800f))
        {
            if (hit.transform.GetComponent<EnemyHit>() != null)
            {
                hit.transform.GetComponent<EnemyHit>().EnemyKilled();
            }
            else if(hit.transform.GetComponent<UIController>() != null)
            {
                hit.transform.GetComponent<UIController>().HitByRaycast();
            }
        }
    }
}