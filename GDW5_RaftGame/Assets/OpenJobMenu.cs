using UnityEngine;

public class OpenJobMenu : MonoBehaviour
{
    private void Start()
    {
        print(transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat") || other.CompareTag("Player"))
        {
            if (!ShopManager.instance.isShown)
                ShopManager.instance.Show();

            JobSelectionSpawner.instance.CreateRandomQuantity();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boat") || other.CompareTag("Player"))
        {
            if (ShopManager.instance.isShown)
                ShopManager.instance.Hide();

            JobSelectionSpawner.instance.ClearCurrentOptions();
        }
    }

}
