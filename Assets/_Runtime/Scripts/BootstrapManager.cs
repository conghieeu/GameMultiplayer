using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

namespace Hieu.Network
{
    public class BootstrapManager : NetworkBehaviour
    {
        [SerializeField] TextMeshProUGUI _txtIsConnectedClient;
        [SerializeField] Transform _spawnPoint;
        private bool pcAssigned;

        [SerializeField] TextMeshProUGUI ipAddressText;
        [SerializeField] TMP_InputField ip;

        [SerializeField] string ipAddress;
        [SerializeField] UnityTransport transport;

        void Start()
        {
            ipAddress = "0.0.0.0";
            SetIpAddress(); // Set the Ip to the above address
            pcAssigned = false;
            InvokeRepeating("assignPlayerController", 0.1f, 0.1f);
        }

        /* Gets the Ip Address of your connected network and
        shows on the screen in order to let other players join
        by inputing that Ip in the input field */
        // ONLY FOR HOST SIDE 
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddressText.text = ip.ToString();
                    ipAddress = ip.ToString();
                    return ip.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }

        /* Sets the Ip Address of the Connection Data in Unity Transport
        to the Ip Address which was input in the Input Field */
        // ONLY FOR CLIENT SIDE
        public void SetIpAddress()
        {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.ConnectionData.Address = ipAddress;
        }

        private void OnClientConnected(ulong clientId)
        {
            print("Client connected with id: " + clientId);

            NetworkManager.Singleton.LocalClient.PlayerObject.transform.position = _spawnPoint.position;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        public void OnClickStartClient()
        {
            Debug.Log(ip.text);
            ipAddress = ip.text;
            SetIpAddress();
            NetworkManager.Singleton.StartClient();
        }

        public void OnClickStartHost()
        {
            NetworkManager.Singleton.StartHost();
            GetLocalIPAddress();
        }

        public void OnClickStartServer()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void OnClickIsConnectedClient()
        {
            _txtIsConnectedClient.text = $"IsConnectedClient: {NetworkManager.Singleton.IsConnectedClient.ToString()}";
        }

    }

}

