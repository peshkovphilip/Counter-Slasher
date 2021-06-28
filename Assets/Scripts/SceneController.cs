using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    private Starter starter;

    private void OnEnable()
    {
        starter = FindObjectOfType<Starter>();
        starter.Restart += RestartLevel;
    }

    private void OnDisable()
    {
        starter = FindObjectOfType<Starter>();
        if (starter)
        {
            starter.Restart -= RestartLevel;
        }
    }
    public void RestartLevel(bool isRestart)
    {
        if (isRestart)
            StartCoroutine(Retry());
    }

    private IEnumerator Retry()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }
}
