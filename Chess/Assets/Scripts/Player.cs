using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
    White,
    Black
}

public class Player
{
    private List<ChessPiece> army;
    public PlayerColor Color { get; private set; }

    public Player(PlayerColor color)
    {
        army = new List<ChessPiece>();
        Color = color;
    }

    public void AddPiece(ChessPiece piece)
    {
        army.Add(piece);
    }

    public void LoseMaterial(ChessPiece piece)
    {
        army.Remove(piece);
    }

    // Additional methods and properties specific to the player can be added here
}
