using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public void TakeDamage()
    {
        Die();
    }


    private void Die()
    {
        gameObject.SetActive(false);
    }

}
