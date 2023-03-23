using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayersFinding : NetworkBehaviour
{
    [SerializeField] private NetMatchManager _matchManager;

    private static NetworkObject _player0;
    private static NetworkObject _player1;

    void Start()
    {
        StartCoroutine(FindPlayers());
    }

    private IEnumerator FindPlayers()
    {
        yield return new WaitUntil(() => { Debug.Log(IsServer); return IsServer; });

        yield return new WaitUntil(() => { Debug.Log(NetworkManager.Singleton.ConnectedClients.Count); return NetworkManager.Singleton.ConnectedClients.Count > 1; });

        Debug.Log(NetworkManager.Singleton.ConnectedClients[1].PlayerObject.name + " -1");
        _player0 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;

        Debug.Log(NetworkManager.Singleton.ConnectedClients[2].PlayerObject + " -2");
        _player1 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;

        _matchManager.SetPlayers(_player0, _player1);
        _matchManager.ReloadMatch(3);
    }
}
