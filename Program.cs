using System;
using SFML.Learning;

     class Program : Game
    {

    static uint applicationHeight = 1005;
    static uint applicationWidth = 1200;

    static int gameWidth = 1200;
    static int gameHeight = 1005;
   

    static string backgroundTexture = LoadTexture("resources/background.png");
    static string playerTexture = LoadTexture("resources/player.png");
    static string foodTexture = LoadTexture("resources/food.png");

    static string meowSound = LoadSound("resources/meow_sound.wav");
    static string crashSound = LoadSound("resources/cat_crash_sound.wav");
    //static string bgMusic = LoadSound("bg.wav"); 

    static float playerX = 30;
    static float playerY = 500;
    static int playerSize = 60;

    static float playerSpeed = 400;
    static int playerDirection = 1;

    static int playerScore = 0;

    static float foodX;
    static float foodY;
    static int foodSize = 32;


    static void playerMove()
    {
        if (GetKey(SFML.Window.Keyboard.Key.W) == true) playerDirection = 0; // UP
        if (GetKey(SFML.Window.Keyboard.Key.S) == true) playerDirection = 1; // DOWN
        if (GetKey(SFML.Window.Keyboard.Key.A) == true) playerDirection = 2; // LEFT
        if (GetKey(SFML.Window.Keyboard.Key.D) == true) playerDirection = 3; // RIGHT

        if (playerDirection == 0) playerY -= playerSpeed * DeltaTime;
        if (playerDirection == 1) playerY += playerSpeed * DeltaTime;
        if (playerDirection == 2) playerX -= playerSpeed * DeltaTime;
        if (playerDirection == 3) playerX += playerSpeed * DeltaTime;
    }
    
    static void DrawPlayer()
    {
        if (playerDirection == 0) DrawSprite(playerTexture, playerX, playerY, 64, 64, playerSize, playerSize);
        if (playerDirection == 1) DrawSprite(playerTexture, playerX, playerY, 0, 64, playerSize, playerSize);
        if (playerDirection == 2) DrawSprite(playerTexture, playerX, playerY, 64, 0, playerSize, playerSize);
        if (playerDirection == 3) DrawSprite(playerTexture, playerX, playerY, 0, 0, playerSize, playerSize);
    }
    
        static void Main()
        {
        InitWindow(applicationWidth, applicationHeight, "Kitched crazy cat 1.0");

        SetFont("resources/arial.ttf");

        Random rnd = new Random();
        foodX = rnd.Next(0, gameWidth - foodSize);
        foodY = rnd.Next(200, gameHeight - foodSize);

        Boolean isLost = false;
        //PlayMusic(bgMusic, 10);

        
        while (true)
            {
                DispatchEvents();
            
            if (isLost == false)
            {
                playerMove();

                Boolean foodCollisionCondition = (playerX + playerSize > foodX
                && playerX < foodX + foodSize 
                && playerY + playerSize > foodY
                && playerY < foodY + foodSize);

                if (foodCollisionCondition)
                {
                    foodX = rnd.Next(0, gameWidth - foodSize);
                    foodY = rnd.Next(200, gameHeight - foodSize);

                    playerScore++;
                    playerSpeed += 10;
                    PlaySound(meowSound);
                }

                Boolean defeatCondition = (playerX + playerSize > gameWidth
                || playerX < 0
                || playerY + playerSize > gameHeight
                || playerY < 240);

                if (defeatCondition)
                {                   
                    isLost = true;
                    PlaySound(crashSound);
                }
            }
            ClearWindow(100,100,100);

            DrawSprite(backgroundTexture, 0, 0);

            if (isLost)
            {
                SetFillColor(255, 0, 0);
                DrawText(300, 400, "Why are you running around the kitchen?!", 36);

                SetFillColor(70, 70, 70);
                DrawText(450, 700, "Press \"R\" to restart the game", 24);

                if (GetKeyDown(SFML.Window.Keyboard.Key.R))
                {
                    isLost = false;
                    playerX = 30;
                    playerY = 500;
                    playerSize = 60;
                    playerSpeed = 400;
                    playerDirection = 1;
                    playerScore = 0;
                    SetFillColor(SFML.Graphics.Color.White);
                }
            }
            DrawPlayer();            
            DrawSprite(foodTexture, foodX, foodY);

            SetFillColor(70, 70, 70);
            DrawText(20, 8, "Food eaten: " + playerScore.ToString(), 18);

            // Console.SetCursorPosition(0, 0);
           // Console.Write("                   ");
            //Console.SetCursorPosition(0, 0);
            // Console.Write("Score: "+playerScore);
             
            DisplayWindow();
            Delay(1);
            }            
        // Console.ReadLine();
        }
    }

