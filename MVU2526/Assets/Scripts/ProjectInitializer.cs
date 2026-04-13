using System;
using UnityEngine;
using Zenject;

public class ProjectInitializer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AudioSystem>().AsSingle();
        Container.Bind<LevelLoader>().AsSingle();
    }
}

public class AudioSystem
{
    public void EmitSound(string id)
    {
        Debug.Log($"Sound Emited {id}");
    }
}

public class LevelLoader
{
    public bool HasLoadingRequest => CurrentRequest != null;
    public LevelConfig CurrentRequest { get; private set; }
    public float CurrentProgress { get; private set; }

    public void LoadLevel(LevelConfig nextScene)
    {
        CurrentRequest = nextScene;
    }

    public void ConsumeRequest()
    {
        CurrentRequest = null;
    }

    public void SetProgress(float progress)
    {
        CurrentProgress = progress;
    }
}