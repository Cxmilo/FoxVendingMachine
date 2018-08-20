using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class ComManager : MonoBehaviour
{
    public static SerialPort serialPort = null;
    private static string text = "";
    private static bool isDataReceived = true;
    public Text label;
    public bool showSelectCom = true;

    private static ComManager instance;

    public static ComManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (!ComManager.instance)
        {
            ComManager.instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {

        // AddPort("Puerto_1");
        // StartCoroutine(Check());
    }

    public static void InitReadingPort()
    {
        isDataReceived = false;
    }

    private void AddPort(string port)
    {
        try
        {
            SerialPort serialPort = new SerialPort(port
                                       , 9600
                                       , Parity.None
                                       , 8
                                       , StopBits.One);

            serialPort.Open();
            serialPort.ReadTimeout = 50;
        }
        catch
        {
            Debug.LogWarning(port + " don't open");
        }
    }

    public void End()
    {
        serialPort.Close();
    }

    public void Send(string s)
    {
        if (serialPort != null)
        {
            Debug.Log("Send Values");
            serialPort.Write(s);
        }
        else
        {

            Debug.Log("Send Values Failed");
        }
    }

    public void OnGUI()
    {

#if UNITY_EDITOR
        GUI.Label(new Rect(0, 0, 100, 20), "Hello World!");
#endif

        GUI.Label(new Rect(0, 0, 300, Screen.height), text);

        if (showSelectCom)
        {
            for (int i = 0; i < SerialPort.GetPortNames().Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width - 300, 60 * i, 100, 50), SerialPort.GetPortNames()[i]))
                {
                    serialPort = new SerialPort(SerialPort.GetPortNames()[i], 9600, Parity.None, 8, StopBits.One);
                    OpenConnection(serialPort);
                    // Application.LoadLevel("Database");
                    showSelectCom = false;
                }
            }
        }
    }

    public static int CheckPort()
    {
        int data = RecieveByte();

        if (data == 99 && !isDataReceived)
        {
            isDataReceived = true;

        }
        else if (data == 98 && !isDataReceived)
        {
            isDataReceived = true;
            Debug.Log(data);
        }

        return data;
    }




    IEnumerator Check()
    {
        while (true)
        {
            if (serialPort != null)
            {
                CheckPort();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private static int RecieveByte()
    {
        int i = 0;

        try
        {
            i = serialPort.ReadByte();
            Debug.Log(serialPort.ReadLine());
        }
        catch (TimeoutException)
        { }

        return i;
    }



    public string ListenPort()
    {
        string data = RecieveChar();

        if (data != "Error")
        {
            return data;
        }
        return string.Empty;

        /* if (data == 99 && !isDataReceived)
         {
             isDataReceived = true;
             Debug.Log(data);
         }
         else if (data == 98 && !isDataReceived)
         {
             isDataReceived = true;
             Debug.Log(data);
         }*/
    }

    private static string RecieveChar()
    {
        string i = "";

        try
        {
            i = serialPort.ReadLine();
        }
        catch (TimeoutException)
        {
            Debug.Log("Error!!!!! port TimeoutExeption");
            i = "Error";
        }

        return i;
    }


    public void OpenConnection(SerialPort sp)
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                Debug.Log("Closing port, because it was already open!");
            }
            else
            {
                sp.Open();  // opens the connection
                sp.ReadTimeout = 100;  // sets the timeout value before reporting error
                Debug.Log("Port Opened!");
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                Debug.Log("Port is already open");
            }
            else
            {
                Debug.Log("Port == null");
            }
        }
    }

    public bool SerialPortEnable()
    {
        if (serialPort != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}