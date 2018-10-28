using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public GameObject[] lights;
    int contador = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPC>())
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }

            contador++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        contador--;

        if (other.gameObject.GetComponent<NPC>())
        {
            if(contador <= 0)
            {
                foreach (GameObject light in lights)
                {
                    light.SetActive(false);
                }
            }
        }
    }


}
