using Raylib_cs;
using System.Text;

class Program
    {

        static void Main()
        {
            //initialized RayLib window 
            Raylib.InitWindow(400, 400, "Raylib Test Window");
            //create input window 
            StringBuilder userInput = new StringBuilder();
            int maxLength = 20;
            Rectangle inputBox = new Rectangle(100, 250, 200, 40);
            // start ball position
            float circleX = 400;
            float circleY = 400;
            float radius = 30;
            //target fps in window 
            Raylib.SetTargetFPS(60);
            //loop for raylib window 
            while (!Raylib.WindowShouldClose())
            {
                //handle keyboard input
                int key = Raylib.GetKeyPressed();
                //handle wrong input
                if (key != 0 && userInput.Length < maxLength)
                {
                    userInput.Append((char)key);
                }
                //for backspace 
                if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && userInput.Length > 0)
                {
                    userInput.Remove(userInput.Length - 1, 1);
                }
                //get cursor postition for ball position
                circleX = Raylib.GetMouseX();
                circleY = Raylib.GetMouseY();
                //draw
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                //input box
                Raylib.DrawRectangleRec(inputBox, Color.Blue);
                Raylib.DrawText(userInput.ToString(), (int)inputBox.X+10, (int)inputBox.Y+10, 20, Color.Black);
                //ball
                Raylib.DrawCircle((int)circleX, (int)circleY, radius, color:Color.Black);
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }


