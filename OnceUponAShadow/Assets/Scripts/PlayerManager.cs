using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

public class PlayerManager : NetworkManager
{
    public Button narratorButton;
    public Button dragonButton;
    public Button knightButton;
    public Button princessButton;

    int avatarIndex = 0;

    public Canvas characterSelectionCanvas;
	private NetworkManager networkManager;
    // Use this for initialization
    void Start()
    {
        narratorButton.onClick.AddListener(delegate { AvatarPicker(narratorButton.name); });
        dragonButton.onClick.AddListener(delegate { AvatarPicker(dragonButton.name); });
        knightButton.onClick.AddListener(delegate { AvatarPicker(knightButton.name); });
        princessButton.onClick.AddListener(delegate { AvatarPicker(princessButton.name); });
		networkManager = GetComponent<NetworkManager> ();
    }

    void AvatarPicker(string buttonName)
    {
        switch (buttonName)
        {
            case "Narrator":
                avatarIndex = 0;
                break;
            case "Dragon":
                avatarIndex = 1;
                break;
            case "Knight":
                avatarIndex = 2;
                break;
            case "Princess":
                avatarIndex = 3;
                break;
        }

        playerPrefab = spawnPrefabs[avatarIndex];
		if(avatarIndex == 0) networkManager.StartHost();
		else networkManager.StartClient();
    }

    /// Copied from Unity's original NetworkManager script except where noted
    public override void OnClientConnect(NetworkConnection conn)
    {
        /// ***
        /// This is added:
        /// First, turn off the canvas...
        characterSelectionCanvas.enabled = false;
        /// Can't directly send an int variable to 'addPlayer()' so you have to use a message service...
        IntegerMessage msg = new IntegerMessage(avatarIndex);
        /// ***

        if (!clientLoadedScene)
        {
            // Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {
                ///***
                /// This is changed - the original calls a differnet version of addPlayer
                /// this calls a version that allows a message to be sent
                ClientScene.AddPlayer(conn, 0, msg);
            }
        }

    }

    /// Copied from Unity's original NetworkManager 'OnServerAddPlayerInternal' script except where noted
    /// Since OnServerAddPlayer calls OnServerAddPlayerInternal and needs to pass the message - just add it all into one.
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        /// *** additions
        /// I skipped all the debug messages...
        /// This is added to recieve the message from addPlayer()...
        int id = 0;

        if (extraMessageReader != null)
        {
            IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
            id = i.value;
        }

        /// using the sent message - pick the correct prefab
        GameObject playerPrefab = spawnPrefabs[id];
        /// *** end of additions

        GameObject player;

        // if princess spawn her on the tower
        if (id == 3)
        {
            player = (GameObject)Instantiate(playerPrefab, new Vector3(-20, 0, 0), Quaternion.identity);
        }
        else
        {
            Transform startPos = GetStartPosition();

            if (startPos != null)
            {
                player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
            }
            else
            {
                player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }
        }
        
        

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}