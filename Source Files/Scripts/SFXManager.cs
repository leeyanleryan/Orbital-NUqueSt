using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> audioClips = new List<AudioClip>();

    public static SFXManager instance;

    /*
     * Useful Indexes:
     * audioClips[3] - Clock ticktock
     * audioClips[9] - Loud impact
     * audioClips[11] - Acquiring something / completing something
     * audioClips[17] - Starting war 
     * audioClips[20] - Water drop
     * audioClips[22] - Something strange / mystifying or thinking about something
     * audioClips[23] - Light warning
     * audioClips[24] - Death sound (?)
     * audioClips[25] - Acquiring something / completing something
     * audioClips[35] - Funny sound
     * audioClips[45] - Something moving super fast
     * audioClips[46] - Something moving fast
     */

    public bool hasPlayedWarning;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
