using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Steamworks;
using Steamworks.Data;
using FishNet.Object.Synchronizing;
using FishNet;
using FishNet.Object;

public class LeaderboardItem : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI playerNameText;

    [SyncVar(OnChange = nameof(SyncScore))]
    private int _score;
    public int score { get { return _score; } }

    private Friend playerData;

    public void Initialize(Friend _player)
    {
        playerData = _player;

        playerNameText.text = _player.Name + " | " + score;
    }

    public void ChangeScore(int _upDown)
    {
        if (!InstanceFinder.IsServer) return;

        _score += _upDown;
    }

    private void SyncScore(int prev, int next, bool asServer)
    {
        _score = next;
        playerNameText.text = playerData.Name + " | " + score;
    }
}
