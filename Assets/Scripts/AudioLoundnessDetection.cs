using UnityEngine;

public class AudioLoundnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;

    private void Start()
    {
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip()
    {
        string microPhoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microPhoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoundnessFromMicrophone()
    {
        return GetLoundnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    public float GetLoundnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoundness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoundness += Mathf.Abs(waveData[i]);
        }

        return totalLoundness / sampleWindow;
    }
}
