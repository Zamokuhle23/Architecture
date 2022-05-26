using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : ButtonElement
{
    // Handles the ball hit event
    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ButtonNotification.BallHitGround:
                app.model.bounces++;
                Debug.Log(“Bounce ”+app.model.bounce);
                if (app.model.buttonNo >= app.model.pressCondition)
                {
                    app.view.ball.enabled = false;
                    app.view.ball.GetComponent<RigidBody>().isKinematic = true; // stops the ball
                                                                                // Notify itself and other controllers possibly interested in the event
                    app.Notify(ButtonNotification.GameComplete, this);
                }
                break;

            case ButtonNotification.GameComplete:
                Debug.Log(“pressed!!”);
                break;
        }
    }
}
