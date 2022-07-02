using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UniRx;
using UnityEngine.UI;

public class ServerExample : MonoBehaviour
{
   private UdpClient udpClient;
   private Subject<string> subject = new Subject<string>();
   [SerializeField] Text message;


   void Start() {
       udpClient = new UdpClient(9000);
       udpClient.BeginReceive(OnReceived, udpClient);

   }

   private void OnReceived(System.IAsyncResult result) {
       UdpClient getUdp = (UdpClient) result.AsyncState;
       IPEndPoint ipEnd = null;

       byte[] getByte = getUdp.EndReceive(result, ref ipEnd);

       var message = Encoding.UTF8.GetString(getByte);
       Debug.Log(message);

       getUdp.BeginReceive(OnReceived, getUdp);
   }

   private void OnDestroy() {
       udpClient.Close();
   }
}