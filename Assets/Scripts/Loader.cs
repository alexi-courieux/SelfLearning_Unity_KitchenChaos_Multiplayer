using Unity.Netcode;
using UnityEngine.SceneManagement;

public static class Loader {


    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene,
        LobbyScene,
        CharacterSelectScene
    }


    private static Scene targetScene;



    public static void Load(Scene newTargetScene) {
        targetScene = newTargetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    
    public static void LoadNetwork(Scene newTargetScene) {
        targetScene = newTargetScene;

        NetworkManager.Singleton.SceneManager.LoadScene(newTargetScene.ToString(), LoadSceneMode.Single);
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}