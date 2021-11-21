using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_State_Machine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    private Transform playerTransform;

    void Awake()
    {
        StartCoroutine(Test_Projectiles());
        playerTransform = FindObjectOfType<CharacterMovement>().transform;
    }
    
    private IEnumerator Test_Projectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Projectile projPrefab = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projPrefab.MoveProjectile(playerTransform.position);
        }
    }
}