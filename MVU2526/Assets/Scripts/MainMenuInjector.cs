using UnityEngine;

public class MainMenuInjector : MonoBehaviour
{
    private void Start()
    {
        var noesis = GetComponent<NoesisView>();

        noesis.Content.DataContext = new MainMenuViewModel();
    }

}

public class MainMenuViewModel
{
    public float Number => 4;
}
