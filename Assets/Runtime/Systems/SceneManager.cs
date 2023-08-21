using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using U = UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

public class SceneManager {

    public static Scene activeScene => GetActiveScene();

    public static event UnityAction<Scene, Scene> activeSceneChanged {
        add    => U.SceneManager.activeSceneChanged += value;
        remove => U.SceneManager.activeSceneChanged -= value;
    }

    public static void ReloadScene() {
        LoadScene(activeScene.name);
    }

    public static void LoadScene(string name) {
        LoadScene(name);
    }
}
