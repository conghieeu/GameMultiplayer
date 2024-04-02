using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

namespace Hieu.Network
{
    public class BootstrapManager : NetworkBehaviour
    {
        [SerializeField] Text _text;

        private void OnClientConnected(ulong clientId)
        {
            print("Client connected with id: " + clientId);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            if (IsClient)
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsClient)
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        public void OnClickStartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void OnClickStartHost()
        {
            NetworkManager.Singleton.StartHost();

        }

        public void OnClickStartServer()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void OnClickIsConnectedClient()
        {
            _text.text = $"IsConnectedClient: {NetworkManager.Singleton.IsConnectedClient.ToString()}";
        }

    }

}

