using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    public double ChanceOfRandomSound = 0.001;
    public List<AudioClip> sndList = new List<AudioClip>();

    public Vector3 bounds1 = Vector3.zero;
    public Vector3 bounds2 = Vector3.zero;
    public AudioSource sndSrc { get; private set; }

    private void RandomTeleport()
    {
        float min, max;
        if (bounds1.x < bounds2.x) { min = bounds1.x; max = bounds2.x; } else { min = bounds2.x; max = bounds1.x; }
        int randX = RandomNumGen.Instance.Next((int)min, (int)max + 1);
        if (bounds1.y < bounds2.y) { min = bounds1.y; max = bounds2.y; } else { min = bounds2.y; max = bounds1.y; }
        int randY = RandomNumGen.Instance.Next((int)min, (int)max + 1);
        if (bounds1.z < bounds2.z) { min = bounds1.z; max = bounds2.z; } else { min = bounds2.z; max = bounds1.z; }
        int randZ = RandomNumGen.Instance.Next((int)min, (int)max + 1);

        Vector3 pos = new Vector3(randX, randY, randZ);
        transform.position = pos;
    }

    private AudioClip RandomGetSound()
    {
        if (sndList.Count > 0)
        {
            int randIndex = RandomNumGen.Instance.Next(0, sndList.Count);
            return sndList[randIndex];
        }
        return null;
    }

    private void RandomDoSound(double chance)
    {
        double randNum = RandomNumGen.Instance.NextDouble();
        if (randNum <= chance)
        {
            RandomTeleport();
            sndSrc.PlayOneShot(RandomGetSound());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sndSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sndSrc.isPlaying)
        {
            RandomDoSound(ChanceOfRandomSound);
        }
    }
}
