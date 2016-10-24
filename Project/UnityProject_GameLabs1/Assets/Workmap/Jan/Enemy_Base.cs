using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy_Base : MonoBehaviour {

    protected virtual GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void OnDeath()
    {
        Destroy(transform.gameObject);
    }

    protected virtual void KilledPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected virtual bool ReceiveDamage(int damage, int remainingHP)
    {
        if (remainingHP - damage < 0)
            return true;
        return false;
    }
}
