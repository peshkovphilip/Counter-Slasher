using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : IStarter, IDestroyer
{
    private GameStarter starter;
    private Damage damage;

    public void Starter()
    {
        damage = new Damage();
        Debug.Log("start controller");
        starter = Object.FindObjectOfType<GameStarter>();
        damage.Restart += RestartLevel;
    }

    public void Destroyer()
    {
        starter = Object.FindObjectOfType<GameStarter>();
        if (starter)
        {
            damage.Restart -= RestartLevel;
        }
    }
    public void RestartLevel(bool isRestart)
    {
        if (isRestart)
            SceneManager.LoadScene("Main");
        //UnityEngine.Object.StartCoroutine(Retry());
    }

    private IEnumerator Retry()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }
}
