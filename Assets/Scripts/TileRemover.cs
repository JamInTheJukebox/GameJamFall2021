using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRemover : MonoBehaviour
{

    public List<GameObject> Platforms = new List<GameObject>();
    public float TimeForPlatformsToReturn = 3;
    Coroutine currentCoroutine;

    public void RemoveTiles()
    {
        if(currentCoroutine != null) { return; }
        var m_HitDetect = Physics2D.BoxCastAll(transform.position, new Vector2(8, 100), 0,Vector2.down);
        if (m_HitDetect.Length != 0)
        {
            foreach(var block in m_HitDetect)
            {
                if(block.collider.gameObject.tag == Tags.TILE_REMOVER)
                {
                    Platforms.Add(block.collider.gameObject);
                    currentCoroutine = StartCoroutine(RemoveBlocks());
                }

            }
            //Output the name of the Collider your Box hit
        }
    }

    public IEnumerator RemoveBlocks()
    {
        foreach (GameObject plat in Platforms)
        {
            plat.SetActive(false);
        }
        yield return new WaitForSeconds(TimeForPlatformsToReturn);
        foreach(GameObject plat in Platforms)
        {
            plat.SetActive(true);
        }
        Platforms.Clear();
        currentCoroutine = null;
    }
}
