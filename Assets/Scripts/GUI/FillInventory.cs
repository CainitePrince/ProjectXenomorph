using UnityEngine;

namespace DuneRunner.GUI
{
    public class FillInventory : MonoBehaviour
    {
        public InventoryItem Vehicle;
        public GameObject Content;
        public InventoryItem InventoryItemPrefab;

        private Inventory playerInventory;
        private Sellable playerVehicle;

        void OnEnable()
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            playerInventory = playerGO.GetComponent<Inventory>();
            playerVehicle = playerGO.GetComponentInChildren<Vehicle>().GetComponent<Sellable>();

            Fill();
        }

        private void Fill()
        {
            Vehicle.ItemName.text = playerVehicle.Name;
            Vehicle.ItemDescription.text = playerVehicle.Description;

            int count = 0;
            foreach (var item in playerInventory.Items)
            {
                InventoryItem guiItem = Instantiate(InventoryItemPrefab, Content.transform);
                var sellable = item.GetComponent<Sellable>();
                guiItem.ItemName.text = sellable.Name;
                guiItem.ItemDescription.text = sellable.Description;
                guiItem.transform.position -= new Vector3(0, count * 60, 0);
                ++count;
            }
        }
    }
}
