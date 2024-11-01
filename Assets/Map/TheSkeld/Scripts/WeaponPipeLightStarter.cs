using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPipeLightStarter : MonoBehaviour
{
    private WaitForSeconds wait = new WaitForSeconds(1f);

    private List<WeaponPipeLight> lights = new List<WeaponPipeLight>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<WeaponPipeLight>();
            if(child)
            {
                lights.Add(child);
            }
        }

        StartCoroutine(TurnOnPipeLight());
    }

    private IEnumerator TurnOnPipeLight()
    {
        while(true)
        {
            yield return wait;

            foreach(var child in lights)
            {
                child.TurnOnLight();
            }
        }
    }
}
