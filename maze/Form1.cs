﻿using System;
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
            start = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < start.Size.Width; i++)
            {
                for (int j = 0; j < start.Height; j++)
                {
                    if (start.GetPixel(i, j).G == 0)
                        start.SetPixel(i, j, Color.Black);
                }
            }
            movingUp = true;
            movingDown = false;
            movingLeft = false;
            movingRight = false;
            pictureBox1.Image = start;
        }
        Bitmap start;
        bool movingUp, movingDown, movingLeft, movingRight;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (wallBefore())
            {
                richTextBox1.Text += "\nСТЕНА ВПЕРЕДИ - ПОВОРАЧИВАЮ НАПРАВО - ИДУ ВПЕРЕД\n";
                turnRight();
                moveForward();
                if (isExit())
                {
                    //УсПЕХ
                    pictureBox1.BackColor = Color.Green;
                    pictureBox1.Size = new Size(50, 50);
                }
                else if (wallBefore())
                {
                    richTextBox1.Text += "\nСТЕНА ВПЕРЕДИ - ИДУ НАЗАД - ПОВОРАЧИВАЮ НАЛЕВО - ИДУ ВПЕРЕД\n";
                    moveBack();
                    turnLeft();
                    moveForward();
                    if (wallBefore())
                    {
                        richTextBox1.Text += "\nСТЕНА ВПЕРЕДИ - ИДУ НАЗАД - ПОВОРАЧИВАЮ НАЛЕВО\n";
                        moveBack();
                        turnLeft();
                    }
                }
            }
            else
            {
                richTextBox1.Text += "\n БЛЯ СТЕНЫ НЕТ - ИДУ ВПЕРЕД\n";
                moveForward();
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
            }
            else
            {
                if (movingDown)
                {
                    movingUp = false;
                    movingDown = false;
                    movingLeft = true;
                    movingRight = false;
                }
                else
                {
                    if (movingLeft)
                    {
                        movingUp = true;
                        movingDown = false;
                        movingLeft = false;
                        movingRight = false;
                    }
                    else
                    {
                        if (movingRight)
                        {
                            movingUp = false;
                            movingDown = true;
                            movingLeft = false;
                            movingRight = false;
                        }
                    }
                }
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
            }
            else
            {
                if (movingDown)
                {
                    movingUp = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = true;
                }
                else
                {
                    if (movingLeft)
                    {
                        movingUp = false;
                        movingDown = true;
                        movingLeft = false;
                        movingRight = false;
                    }
                    else
                    {
                        if (movingRight)
                        {
                            movingUp = true;
                            movingDown = false;
                            movingLeft = false;
                            movingRight = false;
                        }
                    }
                }
            }
        }
        private bool isExit()
        {
            if ((mouse.Location.X > 147 && mouse.Location.X < 157) && (mouse.Location.Y >= 0 && mouse.Location.Y < 10))
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
            if (start.GetPixel(mouse.Location.X, mouse.Location.Y - 2).G == 0 && movingUp)
                return true;
            if (start.GetPixel(mouse.Location.X, mouse.Location.Y + 2).G == 0 && movingDown)
                return true;
            if (start.GetPixel(mouse.Location.X - 2, mouse.Location.Y).G == 0 && movingLeft)
                return true;
            if (start.GetPixel(mouse.Location.X + 2, mouse.Location.Y).G == 0 && movingRight)
                return true;
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
        private void moveUp()
        {
            movingUp = true;
            movingDown = false;
            movingLeft = false;
            movingRight = false;
            mouse.Location = new Point(mouse.Location.X, mouse.Location.Y - 1);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
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
