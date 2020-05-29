using Godot;
using System;

public class Game : Node2D {    
	//INSTANCE VARIABLES
	[Export] public float gameTimer;

	public static int p1score, p2score;

	private bool gameReady, gameOver;
	private float timer = 5;

	//runs when the program starts
	public override void _Ready() {
		timer = gameTimer;

		((Label)FindNode("P1Score")).Text = $"0{p1score}";
		((Label)FindNode("P2Score")).Text = $"0{p2score}";

		if(p1score >= 7) WinGame(Players.Player1);
		if(p2score >= 7) WinGame(Players.Player2);
	}

	//run every frame
	public override void _Process(float delta) {
		//update timer
		if(!gameReady && !gameOver) timer -= delta;

		//react to timer finish
		if(timer <= 1) {
			if (!gameOver)
				Ball.StartGame();
			else
				GetTree().Quit();
			gameReady = true;
			timer = gameTimer;
		}

		//if gameover then show text
		if (!gameOver) {
			if(!gameReady)
				((Label)FindNode("InfoLabel")).Text = Mathf.RoundToInt(timer).ToString();
			else
				((Label)FindNode("InfoLabel")).Text = "";
		}
	}

	//set player score to amount
	public static void SetScore(Players player, int amount) {
		switch(player) {
			case Players.Player1:
			p1score = amount;
			break;
			default:
			p2score = amount;
			break;
		}
	}

	//get player score
	public static int GetScore(Players player) {
		var score = 0;
		switch(player) {
			case Players.Player1:
			score = p1score;
			break;
			default:
			score = p2score;
			break;
		}

		return score;
	}

	//win game for player
	private void WinGame(Players player) {
		gameOver = true;
		timer = 5;

		switch(player) {
			case Players.Player1:
			((Label)FindNode("InfoLabel")).Text = "Player1 Won!";
			break;
			default:
			((Label)FindNode("InfoLabel")).Text = "Player2 Won!";
			break;
		}
	}
}
