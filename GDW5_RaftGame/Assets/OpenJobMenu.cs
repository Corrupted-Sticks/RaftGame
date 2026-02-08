using SDS_Jobs;
using SDS_Locations;
using UnityEngine;

public class OpenJobMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat") || other.CompareTag("Player"))
        {
            if (!ShopManager.instance.isShown) ShopManager.instance.Show();
            JobSelectionSpawner.instance.CreateRandomQuantity();

            JobManager.instance.CheckJobComplete(Locations.GetClosestIsland(transform.position));

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boat") || other.CompareTag("Player"))
        {
            if (ShopManager.instance.isShown)ShopManager.instance.Hide();

            JobSelectionSpawner.instance.ClearCurrentOptions();
        }
    }

}
