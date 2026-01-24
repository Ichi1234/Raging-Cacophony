using System;
using UnityEngine;

public class Player_Berserk : MonoBehaviour
{
    [Header("Berserk Details")]
    [SerializeField] private BerserkerStage[] berserkerStages;
    [Range(0, 10)]
    [SerializeField] private float loundnessSensibility = 1;

    public AudioSource audioSource;
    public AudioLoundnessDetection detector;
    private float soundLevels;

    public static event Action OnStageChanged;
    public BerserkerStage curStage { get; private set; }


    private void Update()
    {
        soundLevels = detector.GetLoundnessFromMicrophone() * loundnessSensibility;

        if (soundLevels < 0)
        {
            soundLevels = 0;
        }
        Debug.Log(soundLevels);

        foreach (var stage in berserkerStages)
        {
            if (soundLevels >= stage.threshold)
            {
                ApplyStage(stage);
            }
        }
    }

    private void ApplyStage(BerserkerStage stage)
    {
        curStage = stage;
        OnStageChanged?.Invoke();
    }

}
