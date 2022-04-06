using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : Grabbable
{
    public int ammo = 8;

    public float bulletVelocity = 20.0f;
    public AudioClip sndFire, sndFireEmpty;
    public GameObject bulletObj;
    public Transform bulletStartMarker;

    public AudioSource sndSrc { get; private set; }


    // Start is called before the first frame update
    public override void DoAwake()
    {
        sndSrc = GetComponent<AudioSource>();
    }

    public override void OnActivate()
    {
        if (ammo > 0)
        {
            Vector3 bulletStart = bulletStartMarker.position;
            GameObject bulPrefab = Instantiate(bulletObj, bulletStart, bulletStartMarker.rotation * Quaternion.Euler(90f, 0f, 0f));
            GameObject bul = bulPrefab.transform.GetChild(0).gameObject;

            bul.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity);

            Destroy(bulPrefab, 3.0f);

            sndSrc.PlayOneShot(sndFire);
            ammo--;
        }
        else
        {
            sndSrc.PlayOneShot(sndFireEmpty);
        }
    }
}
