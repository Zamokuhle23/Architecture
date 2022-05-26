using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController
{
    public PlayerModel model { get; private set; }
    public PlayerView view { get; private set; }

    public playerController(Player model, PlayerView view)
    {
        this.model = model;
        this.view = view;

        this.model.OnPositionChanged += OnPositionChanged;
    }

    private void OnPositionChanged(Vector3 position)
    {
        // Sync
        Vector3 pos = this.model.position;

        // Unity call required here! (we lost portability)
        this.view.SetPosition(new UnityEngine.Vector3(pos.x, pos.y, pos.z));
    }

    // Calling this will fire the OnPositionChanged event 
    private void SetPosition(Vector3 position)
    {
        this.model.position = position;
    }
}
