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
    public void LoadLevel(LevelConfig nextScene)
    {
        
    }
}