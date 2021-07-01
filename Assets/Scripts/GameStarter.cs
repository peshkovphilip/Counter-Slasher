
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private Controllers controllers;

    private void Start()
    {
        controllers = new Controllers();
        controllers.Add(new SceneController());
        controllers.Add(new PlayerController());
        controllers.Add(new EnemyController());
        controllers.Starter();
    }

    private void OnDestroy()
    {
        controllers.Destroyer();
    }

}
