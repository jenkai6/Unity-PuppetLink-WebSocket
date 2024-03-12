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
        // �إ� WebSocket client
        ws = new WebSocket(url);
        // �Y url �O https �@�w�n�[�o��A�]�m WebSocket �s���� SSL ��ĳ������ TLS 1.2
        // �o�i��O�]�����A���u���� TLS 1.2 �ΥH�W�������w���s�u�A�]���Ȥ�ݭn���T���w�ϥ� TLS 1.2 ��ĳ����
        ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

        // ���g EventListener �A connect() �AOnOpen �~���|�Q����
        ws.OnOpen += (sender, e) => Debug.Log("WebSocket Connect!");
        ws.OnError += (sender, e) => Debug.Log("WebSocket Error!");
        ws.OnClose += (sender, e) => Debug.Log("WebSocket Close!");
        ws.OnMessage += (sender, e) => Debug.Log(e.Data);
        
        // �s�� WebSocket server
        ws.Connect();
    }

    // �Y���� app �O�o websocket �s�u�A�]�����ä��|�۰��_�u
    void OnApplicationQuit()
    {
        if (ws != null && ws.ReadyState == WebSocketState.Open)
            ws.Close();
    }
}
