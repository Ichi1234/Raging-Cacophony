using System;
using UnityEngine;

public class Player_Berserk : MonoBehaviour
{
    [Header("Berserk Details")]
    [SerializeField] private BerserkerStage[] berserkerStages;
    [Range(0, 10)]
    [SerializeField] public float loundnessSensibility = 1;
    [SerializeField] private UI_RageBar rageBar;

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

        rageBar.UpdateRageBar(soundLevels);

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
