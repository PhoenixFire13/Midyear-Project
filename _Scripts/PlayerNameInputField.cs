using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Midyear.PlayerNameInputField
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {


        #region Private Variables


        // Store the PlayerPref Key to avoid typos
        private static string playerNamePrefKey = "PlayerName";
        private string playerName;


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            string defaultName = "";
            playerName = "";
            InputField _inputField = GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }


            PhotonNetwork.playerName = defaultName;
        }


        #endregion


        #region Public Methods


        //Sets the player's name to a private var, will not update player preferences until the Join Lobby button is pressed
        public void SetPlayerName(string value)
        {
            playerName = value;
        }

        /// <summary>
        /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName()
        {
            // #Important
            PhotonNetwork.playerName = playerName + " "; // force a trailing space string in case value is an empty string, else playerName would not be updated
            PlayerPrefs.SetString(playerNamePrefKey, playerName);
        }


        #endregion
    }
}