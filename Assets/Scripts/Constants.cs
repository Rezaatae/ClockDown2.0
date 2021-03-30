public static class Constants
{
    public static class Scenes
    {

        public const string NameSelection = "Name Selection";

        public static class Game
        {

            public const string MainMenu = "Main Menu";

            public static class Menu 
            {

                public const string About = "About";
                public const string Instructions = "Instructions";
                public const string NewGame = "New Game";
                public const string JoinGame = "Join Game";
        
            }

            public static class Levels 
            {

                public const string GameLobby = "Game Lobby";
                public const string Level1 = "Level 1";
                public const string Level2 = "Level 2";
                public const string Level3 = "Level 3";
                public const string Level4 = "Level 4";
                public const string Level5 = "Level 5";
            
            }

            public static class Objects
            {
                public const int ToiletRoll = 9;
                public const int Enemy = 12;
                public const string PlayerId = "player_id";

                public const string PlayerCurrentLifeRemaining = "current_life_remaining";

                public const string PlayerCurrentScore = "current_score";

                public const string Timer = "Timer";

                public const string LifeRemainingText = "Life Remaining Text";

                public const string ScoreText = "Score Text";

                public const string TargetTransform = "Target Transform";
            }
        }

    }    
    

    public static class GameControls
    {
        public static class Movement
        {
            public const string Horizontal = "Horizontal";
            public const string Jump = "Jump";
            public const string Fire = "Fire1";
        }
        
    }

    public static class RPC
    {
        public const string Destroy = "Destroy";
        public const string DestroyEnemy = "DestroyEnemy";
    }

    public static class Prefabs
    {
        public const string Player = "Player";
        public const string PlayerPhotonClone = "Player(Clone)";
    }

    public static class PhotonErrorCodes
    {
        public const int OK = 0;
        public const int GameAlreadyExists = 32766;
        public const int GameDoesNotExist = 32758;
        public const int GameFull = 32765;
        public const int GameClosed = 32764;        

    }

}