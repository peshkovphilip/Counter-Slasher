using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageble, IDestroyable
{
    [SerializeField] private bool drag = false;
    [SerializeField] private int health = 100;
    [SerializeField] public Rigidbody2D shootBody;
    [SerializeField] private float maxDistance = 2f;
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (drag)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePos, shootBody.position) > maxDistance)
            {
                rbody.position = shootBody.position + (mousePos - shootBody.position).normalized * maxDistance;
            }
            else
            {
                rbody.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        drag = true;
        rbody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        drag = false;
        rbody.isKinematic = false;
        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
    }

    public int Damage()
    {
        return this.health;
    }

    public bool SetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Retry());
            return true; // set death
        }
        else
        {
            return false; // take damage
        }
    }

    private IEnumerator Retry()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }
}
