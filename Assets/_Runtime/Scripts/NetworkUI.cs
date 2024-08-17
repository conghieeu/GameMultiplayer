using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

namespace Hieu.Network
{
    public class NetworkUI : NetworkBehaviour
    {
        [SerializeField] Button hostButton;
        [SerializeField] Button clientButton;
        [SerializeField] TextMeshProUGUI playerCountText;

        NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

        private void Awake()
        {
            hostButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });
            hostButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            });
        }

        private void Update()
        {
            playerCountText.text = "Players: " + playersNum.Value.ToString();
            if (!IsServer) return;
            playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
        }
    }

}