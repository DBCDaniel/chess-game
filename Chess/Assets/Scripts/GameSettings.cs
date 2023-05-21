public class GameSettings
{
    private static GameSettings instance;
    public static GameSettings Instance { get => instance ?? (instance = new GameSettings()); }

    public float PieceMovementSpeed = 5f;

    private GameSettings()
    {
        // Private constructor to enforce singleton pattern
    }
}
