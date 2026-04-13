using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingSceneUILogic : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    private LevelLoader levelLoader;

    [Inject]
    private void SetDependencies(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    public void Update()
    {
        loadingBar.fillAmount = levelLoader.CurrentProgress;
    }
}
