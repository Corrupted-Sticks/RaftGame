using UnityEngine;

public class OpenJobMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat") || other.CompareTag("Player"))
        {
            if (!ShopManager.instance.isShown)
                ShopManager.instance.Show();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boat") || other.CompareTag("Player"))
        {
            if (ShopManager.instance.isShown)
                ShopManager.instance.Hide();
        }
    }

}
