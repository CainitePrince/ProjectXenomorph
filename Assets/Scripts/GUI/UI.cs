using UnityEngine;
using UnityEngine.UI;

namespace DuneRunner.GUI
{
    public class UI : MonoBehaviour
    {
        public Image PowerBar;
        public Image ShieldBar;
        public Image ArmourBar;

        public Text CreditsValue;

        public GameObject Player;

        private Generator playerGenerator;
        private Shield playerShields;
        private Armour playerArmour;

        private Inventory playerInventory;

        void Update()
        {
            if (Player)
            {
                if (playerArmour == null)
                {
                    playerArmour = Player.GetComponentInChildren<Armour>();
                    playerShields = Player.GetComponentInChildren<Shield>();
                    playerGenerator = Player.GetComponentInChildren<Generator>();
                    playerInventory = Player.GetComponent<Inventory>();
                }

                PowerBar.fillAmount = playerGenerator.AvailablePower / playerGenerator.Capacity;
                ArmourBar.fillAmount = playerArmour.Buffer / playerArmour.Capacity;
                if (playerShields)
                {
                    ShieldBar.fillAmount = playerShields.Buffer / playerShields.Capacity;
                }
                else
                {
                    ShieldBar.fillAmount = 0;
                }

                // This allocates memory
                CreditsValue.text = playerInventory.Credits.ToString();
            }
        }
    }
}
