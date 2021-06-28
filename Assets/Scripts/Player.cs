using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour, IDamageble, IDestroyable, IBonusable
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 100;
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody2D shootBody;
    private IDestroyable destroyable;
    private IBonusable bonusable;
    private bool activeDamage = true;
    private bool activeDestroy = true;
    private int activatedBonuses = 0;
    private Rigidbody2D playerBody;
    private SceneController sceneController;
    public event Action<GameObject, Collider2D> OnEnter;
    public event Action<Collider2D> OnExit;
    

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDrag()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(mousePos, shootBody.position) > maxDistance)
        {
            playerBody.position = shootBody.position + (mousePos - shootBody.position).normalized * maxDistance;
        }
        else
        {
            playerBody.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        playerBody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        playerBody.isKinematic = false;
        StartCoroutine(Fly());
    }
    private void OnTriggerEnter2D(Collider2D currentCollider)
    {
        OnEnter?.Invoke(gameObject, currentCollider);
    }
    private void OnTriggerExit2D(Collider2D currentCollider)
    {
        OnExit?.Invoke(currentCollider);
    }

    IEnumerator Fly()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
    }
    public void SetHealth(int health)
    {
        this.health = health;
    }
    public int GetHealth()
    {
        return health;
    }
    public int Damage()
    {
        return this.damage;
    }
    public bool SetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            return true; // set death
        }
        else
        {
            return false; // take damage
        }
    }
    public bool GetActiveDamage()
    {
        return activeDamage;
    }
    public void SetActiveDamage(bool active)
    {
        activeDamage = active;
    }

    public bool GetActiveDestroy()
    {
        return activeDestroy;
    }
    public void SetActiveDestroy(bool active)
    {
        activeDestroy = active;
    }
    public int GetActivatedBonuses()
    {
        return activatedBonuses;
    }

}
