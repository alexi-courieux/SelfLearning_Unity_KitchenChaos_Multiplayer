using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        
        optionsButton.onClick.AddListener(() =>
        {
            Debug.Log("Not implemented");
        });
        
        quitButton.onClick.AddListener(Application.Quit);
    }
}
