using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject]
    private AudioSystem audioSystem;


    public void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            audioSystem.EmitSound("Jump sfx");
        }
    }
}
