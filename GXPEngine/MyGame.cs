using GXPEngine;                                // GXPEngine contains the engine

static class Globals
{
    public static int health;
}

public class MyGame : Game
{
    private StartMenu _menu;
    private CreditsMenu _creditsmenu;
    private Level1 _level1;
    private GameOver _gameover;

    Sound _quitSound;

    public static float timeSince;

    public bool quitIsPressed;
    public bool level1IsActive;

    bool quitsoundHasPlayed;
    bool gameIsPaused;
    bool unpauseIsPressed;

    Button_Credits _buttonunpause;
    Button_Back _button_backmain;

    Sprite pause_menu;

    Sprite unpause_button_normal;
    Sprite unpause_button_hover;

    Sprite back_main_button_normal;
    Sprite back_main_button_hover;

    public MyGame() : base(1440, 1080, false, false)    // Create a window that's 1440x1080 and NOT fullscreen and V-Sync turned OFF
    {
        targetFps = 60;

        _quitSound = new Sound("QuitSound.mp3", false, true);

        quitIsPressed = false;
        quitsoundHasPlayed = false;
        level1IsActive = false;
        gameIsPaused = false;

        CreateMenu();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateMenu()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateMenu()
    {
        _menu = new StartMenu();
        AddChild(_menu);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreatePauseMenu()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreatePauseMenu()
    {
        pause_menu = new Sprite("PauseMenu.png");
        AddChild(pause_menu);
        pause_menu.visible = false;

        _buttonunpause = new Button_Credits();
        AddChild(_buttonunpause);

        _button_backmain = new Button_Back();
        AddChild(_button_backmain);

        unpause_button_normal = new Sprite("unpause_button_normal.png");
        AddChild(unpause_button_normal);
        unpause_button_normal.visible = false;

        unpause_button_hover = new Sprite("unpause_button_hover.png");
        AddChild(unpause_button_hover);
        unpause_button_hover.visible = false;

        back_main_button_normal = new Sprite("back_main_button_normal.png");
        AddChild(back_main_button_normal);
        back_main_button_normal.visible = false;

        back_main_button_hover = new Sprite("back_main_button_hover.png");
        AddChild(back_main_button_hover);
        back_main_button_hover.visible = false;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateCreditsMenu()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateCreditsMenu()
    {
        _creditsmenu = new CreditsMenu();
        AddChild(_creditsmenu);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        CreateGame()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CreateGame()
    {
        _level1 = new Level1();
        AddChild(_level1);
        level1IsActive = true;

        CreatePauseMenu();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        GameOver()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GameOver()
    {
        _level1.Destroy();
        _level1.Remove();

        _gameover = new GameOver();
        AddChild(_gameover);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandleQuitButton()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void HandleQuitButton()
    {
        if (quitIsPressed == true)
        {
            if (quitsoundHasPlayed == false)
            {
                _quitSound.Play();
                quitsoundHasPlayed = true;
            }

            if (Time.now >= timeSince + 950f)
            {
                Destroy();
                quitIsPressed = false;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandlePause()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandlePause()
    {
        if (Input.GetKeyDown(Key.P) && gameIsPaused == false)
        {
            if (level1IsActive == true)
            {
                _level1.Pause();
                gameIsPaused = true;
                pause_menu.visible = true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandleUnpause()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandleUnpause()
    {
        if (unpauseIsPressed == true || Input.GetKeyDown(Key.U))
        {
            _level1.Unpause();
            gameIsPaused = false;

            pause_menu.visible = false;

            unpause_button_normal.visible = false;
            unpause_button_hover.visible = false;

            back_main_button_normal.visible = false;
            back_main_button_hover.visible = false;

            unpauseIsPressed = false;
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        HandlePauseButton()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandlePauseButton()
    {
        if (gameIsPaused == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_buttonunpause.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    unpauseIsPressed = true;
                }

                if (_button_backmain.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    unpauseIsPressed = true;

                    _level1.Destroy();
                    _level1.Remove();

                    CreateMenu();
                }
            }

            if (_buttonunpause.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                unpause_button_normal.visible = false;
                unpause_button_hover.visible = true;
            }
            else
            {
                unpause_button_normal.visible = true;
                unpause_button_hover.visible = false;
            }

            if (_button_backmain.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                back_main_button_normal.visible = false;
                back_main_button_hover.visible = true;
            }
            else
            {
                back_main_button_normal.visible = true;
                back_main_button_hover.visible = false;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Update()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        HandleQuitButton();

        HandlePause();
        HandleUnpause();

        HandlePauseButton();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Main()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static void Main()                  // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}
