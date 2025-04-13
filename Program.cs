using Raylib_cs;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Raylib.InitWindow(1000, 800, "Endless Ball Bouncing Game");
            Raylib.SetTargetFPS(60);

            int numCircles = 4;
            float[] circleRadii = { 150, 200, 250, 300 };
            float rotationAngle = 0; 
            float rotationSpeed = 2; 
            float gapWidth = MathF.PI / 6; 

            float ballX = 500, ballY = 400;
            float ballRadius = 20;
            float ballSpeedX = 3, ballSpeedY = 5; 
            float gravity = 0.2f; 
            float groundY = 780; 
            Random random = new Random(); 
            int currentCircle = 0; 

            while (!Raylib.WindowShouldClose())
            {
                rotationAngle += rotationSpeed * (MathF.PI / 180); 
                if (rotationAngle > MathF.PI * 2) rotationAngle -= MathF.PI * 2; 

                ballSpeedY += gravity;

                ballX += ballSpeedX;
                ballY += ballSpeedY;

                if (ballY + ballRadius >= groundY)
                {
                    ballY = groundY - ballRadius; 
                    ballSpeedY = -5;
                    ballSpeedX = random.Next(0, 2) == 0 ? 3 : -3; 
                }

                if (ballY - ballRadius <= 0)
                {
                    ballY = ballRadius; 
                    ballSpeedY = 5;
                }

                if (ballX - ballRadius <= 0 || ballX + ballRadius >= 1000)
                {
                    ballSpeedX = -ballSpeedX; 
                }

                float circleX = 500, circleY = 400;

                float distX = ballX - circleX;
                float distY = ballY - circleY;
                float distance = MathF.Sqrt(distX * distX + distY * distY);

                if (distance + ballRadius > circleRadii[currentCircle])
                {
                    float ballAngle = MathF.Atan2(distY, distX);
                    if (ballAngle < 0) ballAngle += MathF.PI * 2; 

                    float gapStart = rotationAngle;
                    float gapEnd = rotationAngle + gapWidth;

                    if (ballAngle >= gapStart && ballAngle <= gapEnd)
                    {
                        if (currentCircle < circleRadii.Length - 1)
                        {
                            currentCircle++; 
                        }
                        else
                        {
                            currentCircle = 0; 
                            ballX = 500;
                            ballY = 400;
                            ballSpeedX = random.Next(-3, 3); 
                            ballSpeedY = -5; 
                        }
                    }
                    else
                    {
                        ballSpeedY = -ballSpeedY; 
                        ballSpeedX = -ballSpeedX; 
                    }
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                for (int i = currentCircle; i < numCircles; i++)
                {
                    float gapStartAngle = rotationAngle;
                    float gapEndAngle = rotationAngle + gapWidth;

                    for (float angle = 0; angle < MathF.PI * 2; angle += 0.01f) 
                    {
                        if (angle < gapStartAngle || angle > gapEndAngle) 
                        {
                            float startX = circleX + circleRadii[i] * MathF.Cos(angle);
                            float startY = circleY + circleRadii[i] * MathF.Sin(angle);
                            float endX = circleX + circleRadii[i] * MathF.Cos(angle + 0.01f);
                            float endY = circleY + circleRadii[i] * MathF.Sin(angle + 0.01f);

                            Raylib.DrawLine((int)startX, (int)startY, (int)endX, (int)endY, Color.Red);
                        }
                    }
                }

                Raylib.DrawCircle((int)ballX, (int)ballY, ballRadius, Color.Blue);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
