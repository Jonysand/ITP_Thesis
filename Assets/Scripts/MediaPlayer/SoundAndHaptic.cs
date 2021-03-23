// haptic json generator: https://composer.lofelt.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

[RequireComponent(typeof(AudioSource))]
public class SoundAndHaptic : MonoBehaviour
{
    [SerializeField]
    TextAsset AHAPFile;

    AudioSource audioSouse;

    enum AudioType{
        Environment,
        Impulse
    }

    [SerializeField]
    AudioType type;

    // Start is called before the first frame update
    void Start()
    {
        audioSouse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case AudioType.Environment:
            {
                if (!audioSouse.isPlaying && AHAPFile!=null)
                {
                    audioSouse.Play();
                    MMVibrationManager.AdvancedHapticPattern(AHAPFile.text, null, null, -1, null, null, null, -1, HapticTypes.LightImpact); 
                }
                break;
            }
            case AudioType.Impulse:
            {
                break;
            }
        }
    }
}
