using Godot;
using System;

public class Ball : KinematicBody2D {
	//INSTANCE VARIABLES
	public static Vector2 velocity;

	//run when program starts
	public override void _Ready() {
		velocity = Vector2.Zero;
	}

	//run every frame
	public override void _Process(float delta) {
		//react to win or loss
		for (int i = 0; i < GetSlideCount(); i++) {
			if(((Node2D)GetSlideCollision(i).GetCollider()).IsInGroup("outofbounds")) {
				switch(GetSlideCollision(i).GetNormal().x) {
					case 1:
					Game.SetScore(Players.Player2, Game.GetScore(Players.Player2) + 1);
					break;
					case -1:
					Game.SetScore(Players.Player1, Game.GetScore(Players.Player1) + 1);
					break;
				}
				GetTree().ReloadCurrentScene();
			}

			//bounce off side wall
			switch(GetSlideCollision(i).GetNormal().x) {
				case 1:
				velocity.x = Mathf.Abs(velocity.x);
				break;
				case -1:
				velocity.x = -Mathf.Abs(velocity.x);
				break;
			}

			//bounce off floor & ceiling
			switch(GetSlideCollision(i).GetNormal().y) {
				case 1:
				velocity.y = Mathf.Abs(velocity.y);
				break;
				case -1:
				velocity.y = -Mathf.Abs(velocity.y);
				break;
			}
		}

		//move ball
		MoveAndSlide(velocity, Vector2.Up);
	}

	//randomizes velocity
	public static void StartGame() {
		var rand = new Random();
		velocity = new Vector2(rand.Next(-350, 350), rand.Next(-350, 350));
	}
}
