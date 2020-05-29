using Godot;
using System;

public enum Players {Player1, Player2}
public class Paddle : KinematicBody2D {
	//INSTANCE VARIABLES
	[Export]
	public Players player;
	[Export]
	public float speed, acceleration;

	private float yVelocity;

	//run every frame
	public override void _Process(float delta) {
		//react to player input
		switch(player) {
			case Players.Player1:
			var p1Input = Input.GetActionStrength("P1_Down") - Input.GetActionStrength("P1_Up");
			yVelocity = Mathf.Lerp(yVelocity, p1Input * speed, acceleration * delta);
			break;
			default:
			var p2Input = Input.GetActionStrength("P2_Down") - Input.GetActionStrength("P2_Up");
			yVelocity = Mathf.Lerp(yVelocity, p2Input * speed, acceleration * delta);
			break;
		}

		//move paddle
		MoveAndSlide(new Vector2(0, yVelocity));
	}
}
