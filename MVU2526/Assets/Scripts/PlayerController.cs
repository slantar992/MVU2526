using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelConfig nextScene;

    private AudioSystem audioSystem;
    private LevelLoader levelLoader;

    [Inject]
    public void SetDependencies(
        AudioSystem audioSystem,
        LevelLoader levelLoader)
    {
        this.audioSystem = audioSystem;
        this.levelLoader = levelLoader;
    }

    public void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            audioSystem.EmitSound("Jump sfx");
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            levelLoader.LoadLevel(nextScene);
        }
    }
}
