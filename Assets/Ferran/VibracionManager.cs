using UnityEngine;

public class VibracionManager : MonoBehaviour
{

    public static void vibracion(int iteration, int frequency, int strength, OVRInput.Controller controlador)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for (int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }

        if (controlador == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        else if (controlador == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }

    public static void vibracionAudio(AudioClip audio, OVRInput.Controller controlador)
    {
        OVRHapticsClip clip = new OVRHapticsClip(audio);

        if (controlador == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        else if (controlador == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
}
