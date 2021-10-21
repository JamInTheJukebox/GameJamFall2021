using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // instantiate the game manager interface
        }
    }
#endif
}
