using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    public string url = "wss://192.168.0.16:8080/realtime";
    WebSocket ws;

    void Start()
    {
        // 建立 WebSocket client
        ws = new WebSocket(url);
        // 若 url 是 https 一定要加這行，設置 WebSocket 連接的 SSL 協議版本為 TLS 1.2
        // 這可能是因為伺服器只接受 TLS 1.2 及以上版本的安全連線，因此客戶端要明確指定使用 TLS 1.2 協議版本
        ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

        // 先寫 EventListener 再 connect() ，OnOpen 才不會被忽略
        ws.OnOpen += (sender, e) => Debug.Log("WebSocket Connect!");
        ws.OnError += (sender, e) => Debug.Log("WebSocket Error!");
        ws.OnClose += (sender, e) => Debug.Log("WebSocket Close!");
        ws.OnMessage += (sender, e) => Debug.Log(e.Data);
        
        // 連接 WebSocket server
        ws.Connect();
    }

    // 若結束 app 記得 websocket 連線，因為它並不會自動斷線
    void OnApplicationQuit()
    {
        if (ws != null && ws.ReadyState == WebSocketState.Open)
            ws.Close();
    }
}
