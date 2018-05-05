using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            start = new Bitmap(panel1.BackgroundImage);
            end = new Bitmap(panel1.Width, panel1.Height);
            for (int i = 0; i < start.Size.Width; i++)
            {
                for (int j = 0; j < start.Height; j++)
                {
                    if (start.GetPixel(i, j).G == 0)
                        start.SetPixel(i, j, Color.Black);
                    end.SetPixel(i, j, Color.GreenYellow);
                }
            }
            movingUp = false;
            movingDown = false;
            movingLeft = false;
            movingRight = true;
        }
        Bitmap start;
        Bitmap end;
        bool movingUp, movingDown, movingLeft, movingRight;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isExit())
            {
                panel1.BackgroundImage = end;
                timer1.Stop();
            }


            if (wallRight())
            {
                moveForward();
                if (wallBefore())
                    turnLeft();
            }
            else
            {
                turnRight();
            }
        }
        /// <summary>
        /// Поворот вправо
        /// </summary>
        private void turnRight()
        {
            if (movingUp)
            {
                movingUp = false;
                movingDown = false;
                movingLeft = false;
                movingRight = true;
                return;
            }
   
            if (movingDown)
            {
                movingUp = false;
                movingDown = false;
                movingLeft = true;
                movingRight = false;
                return;
            }
         
            if (movingLeft)
            {
                movingUp = true;
                movingDown = false;
                movingLeft = false;
                movingRight = false;
                return;
             }
                    
            if (movingRight)
            {
                movingUp = false;
                movingDown = true;
                movingLeft = false;
                movingRight = false;
                return;
            }
                    
        }
        /// <summary>
        /// Поворот налево
        /// </summary>
        private void turnLeft()
        {
            if (movingUp)
            {
                movingUp = false;
                movingDown = false;
                movingLeft = true;
                movingRight = false;
                return;
            }
           
            if (movingDown)
            {
                movingUp = false;
                movingDown = false;
                movingLeft = false;
                movingRight = true;
                return;
            }
               
            if (movingLeft)
            {
                movingUp = false;
                movingDown = true;
                movingLeft = false;
                movingRight = false;
                return;
            }

            if (movingRight)
            {
                movingUp = true;
                movingDown = false;
                movingLeft = false;
                movingRight = false;
                return;
            }
                    
        }
        private bool isExit()
        {
            if ((mouse.Location.X > 210 && mouse.Location.X < 225) && (mouse.Location.Y >= 210 && mouse.Location.Y < 220))
                return true;
            return false;
        }
        private bool rightPass()
        {
            if ((start.GetPixel(mouse.Location.X + 1, mouse.Location.Y + 1).R >= 0 && movingDown) ||
                (start.GetPixel(mouse.Location.X + 1, mouse.Location.Y - 1).R >= 0 && movingUp) ||
                (start.GetPixel(mouse.Location.X + 1, mouse.Location.Y - 1).R >= 0 && movingRight) ||
                (start.GetPixel(mouse.Location.X - 1, mouse.Location.Y + 1).R >= 0 && movingUp))
                return true;
            return false;

        }
        /// <summary>
        /// Препятствие впереди 
        /// </summary>
        /// <returns></returns>
        private bool wallBefore()
        {
            if (start.GetPixel(mouse.Location.X + 1, mouse.Location.Y).G == 0 && movingUp)
            { return true; }
            if (start.GetPixel(mouse.Location.X + 1, mouse.Location.Y + 3).G == 0 && movingDown)
            { return true; }
            if (start.GetPixel(mouse.Location.X, mouse.Location.Y + 1).G == 0 && movingLeft)
            { return true; }
            if (start.GetPixel(mouse.Location.X + 3, mouse.Location.Y + 1).G == 0 && movingRight)
            { return true; }

            return false;
        }
        /// <summary>
        /// Шаг назад
        /// </summary>
        private void moveBack()
        {
            if (movingUp)
                moveDown();
            if (movingDown)
                moveUp();
            if (movingLeft)
                moveRight();
            if (movingRight)
                moveLeft();
        }
        //стенка справа
        private bool wallRight()
        {
            if (start.GetPixel(mouse.Location.X + 3, mouse.Location.Y).G == 0 && movingUp)
            { return true; }
            if (start.GetPixel(mouse.Location.X, mouse.Location.Y + 3).G == 0 && movingDown)
            { return true; }
            if (start.GetPixel(mouse.Location.X, mouse.Location.Y).G == 0 && movingLeft)
            { return true; }
            if (start.GetPixel(mouse.Location.X + 3, mouse.Location.Y + 3).G == 0 && movingRight)
            { return true; }

            return false;
        }
        /// <summary>
        /// Шаг вперед
        /// </summary>
        private void moveForward()
        {
                if (movingUp)
                    moveUp();
                if (movingDown)
                    moveDown();
                if (movingLeft)
                    moveLeft();
                if (movingRight)
                    moveRight();

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void moveUp()
        {
            movingUp = true;
            movingDown = false;
            movingLeft = false;
            movingRight = false;
            mouse.Location = new Point(mouse.Location.X, mouse.Location.Y - 1);
        }

        private void moveDown()
        {
            movingUp = false;
            movingDown = true;
            movingLeft = false;
            movingRight = false;
            mouse.Location = new Point(mouse.Location.X, mouse.Location.Y + 1);
        }
        private void moveLeft()
        {
            movingUp = false;
            movingDown = false;
            movingLeft = true;
            movingRight = false;
            mouse.Location = new Point(mouse.Location.X -1 , mouse.Location.Y);
        }
        private void moveRight()
        {
            movingUp = false;
            movingDown = false;
            movingLeft = false;
            movingRight = true;
            mouse.Location = new Point(mouse.Location.X + 1, mouse.Location.Y);
        }
    }
}
