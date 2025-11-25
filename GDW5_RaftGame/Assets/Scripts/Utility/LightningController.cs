using UnityEngine;

public class LightningController : MonoBehaviour
{
    public GameObject lightningOne;
    public GameObject lightningTwo;
    public GameObject lightningThree;

    public GameObject audioOne;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightningOne.SetActive(false);
        lightningTwo.SetActive(false);
        lightningThree.SetActive(false);

        //audioOne.SetActive(false);

        Invoke("CallLightning", 1.75f);
    }

    void CallLightning()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            lightningOne.SetActive(true);
            Invoke("EndLightning", 0.125f);
            //Invoke("CallThunder", 0.395f);
        }
        else if (r == 1)
        {
            lightningTwo.SetActive(true);
            Invoke("EndLightning", 0.105f);
            //Invoke("CallThunder", 0.195f);
        }
        else
        {
            lightningThree.SetActive(true);
            Invoke("EndLightning", 0.75f);
            //CallThunder();
        }
    }

    void EndLightning()
    {
        lightningOne.SetActive(false);
        lightningTwo.SetActive(false);
        lightningThree.SetActive(false);

        float rand = Random.Range(3.5f, 7.7f);
        Invoke("CallLightning", rand);
    }

    void CallThunder()
    {
        audioOne.SetActive(true);
        Invoke("EndThunder", 3.4f);
    }

    void EndThunder()
    {
        audioOne.SetActive(false);
    }
}
