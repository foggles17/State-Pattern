using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace State_Pattern
{
    public partial class Game : Form
    {
        int width;
        int height;
        bool[] birthConditions;
        bool[] survivalConditions;
        List<List<CheckBox>> checkBoxes;
        List<List<State>> stateField;
        Button stepButton;
        Button startStopButton;
        Button clearButton;
        bool running;
        public Game(int width, int height, bool[] birth, bool[] survival)
        {
            this.width = width;
            this.height = height;
            birthConditions = birth;
            survivalConditions = survival;
            checkBoxes = new List<List<CheckBox>>();
            stateField = new List<List<State>>();
            initializeComponent();
            createStates();

            running = false;
        }
        private void initializeComponent()
        {
            createButtons();
            createCheckBoxes();
            createTimer();
            this.SuspendLayout();

            placeButtons();
            placeCheckBoxes();
            placeTimer();
            
            initializeComponentFooter();
        }
        private void createTimer()
        {
            this.components = new System.ComponentModel.Container();
            stepTimer = new System.Windows.Forms.Timer(this.components);
        }
        private void placeTimer()
        {
            // 
            // stepTimer
            // 
            this.stepTimer.Tick += new System.EventHandler(this.stepTimer_Tick);
        }
        private void createButtons()
        {
            stepButton = new Button();
            startStopButton = new Button();
            clearButton = new Button();
        }
        private void createCheckBoxes()
        {
            for (int i = 0; i < width; i++)
            {
                checkBoxes.Add(new List<CheckBox>());
                for (int j = 0; j < height; j++)
                {
                    checkBoxes[i].Add(new CheckBox());
                }
            }
        }
        private void placeButtons()
        {
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(12, 41);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 23);
            this.stepButton.TabIndex = 1;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // startStopButton
            // 
            this.startStopButton.Location = new System.Drawing.Point(12, 12);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(75, 23);
            this.startStopButton.TabIndex = 0;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(12, 70);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
        }
        private void placeCheckBoxes()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    placeCheckBox(checkBoxes[i][j], i, j);
                }
            }
        }
        private void placeCheckBox(CheckBox cB, int horiz, int vert)
        {
            cB.AutoSize = true;
            cB.Location = new System.Drawing.Point(112 + 12 * horiz, 12 + 12 * vert);
            cB.Name = "checkBox" + horiz + "" + vert;
            cB.Size = new System.Drawing.Size(15, 14);
            cB.TabIndex = 0;
            cB.UseVisualStyleBackColor = true;
        }
        private void initializeComponentFooter()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(width * 12 + 124, height * 12 + 24);
            for (int i = width - 1; i >= 0; i--)
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    this.Controls.Add(checkBoxes[i][j]);
                }
            }
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.clearButton);
            this.Name = "Game";
            this.Text = "Life Game";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void createStates()
        {
            for (int i = 0; i < width; i++)
            {
                stateField.Add(new List<State>());
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    stateField[i].Add(new OffState(0, birthConditions, survivalConditions));
                }
            }
        }
        private void updateStateField()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    stateField[i][j] = stateField[i][j].setOn(checkBoxes[i][j].Checked);
                }
            }
        }
        private void updateCheckBoxes()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    checkBoxes[i][j].Checked = (stateField[i][j] is OnState);
                }
            }
        }

        private void step()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int placement = 0;
                    List<State> neighbors = new List<State>();

                    if (i == 0)
                        placement += 0;
                    else if (i < width - 1)
                        placement += 3;
                    else
                        placement += 6;
                    if (j == 0)
                        placement += 0;
                    else if (j < height - 1)
                        placement += 1;
                    else
                        placement += 2;

                    if (placement / 3 > 0)
                    {
                        neighbors.Add(stateField[i - 1][j]);
                        if (placement % 3 > 0)
                        {
                            neighbors.Add(stateField[i - 1][j - 1]);
                            neighbors.Add(stateField[i][j - 1]);
                        }
                        if (placement % 3 < 2)
                        {
                            neighbors.Add(stateField[i - 1][j + 1]);
                            neighbors.Add(stateField[i][j + 1]);
                        }
                    }
                    if(placement / 3 < 2)
                    {
                        neighbors.Add(stateField[i + 1][j]);
                        if (placement % 3 > 0)
                        {
                            neighbors.Add(stateField[i + 1][j - 1]);
                            if(placement / 3 == 0)
                                neighbors.Add(stateField[i][j - 1]);
                        }
                        if (placement % 3 < 2)
                        {
                            neighbors.Add(stateField[i + 1][j + 1]);
                            if (placement / 3 == 0)
                                neighbors.Add(stateField[i][j + 1]);
                        }
                    }
                    stateField[i][j].setOnNeighbors(neighbors);
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    stateField[i][j] = stateField[i][j].update();
                }
            }
            updateCheckBoxes();
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            updateStateField();
            step();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    checkBoxes[i][j].Checked = false;
                }
            }
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if(running)
            {
                stepTimer.Enabled = false;
                startStopButton.Text = "Start";
                foreach(List<CheckBox> currentList in checkBoxes)
                {
                    foreach(CheckBox currentCheckBox in currentList)
                    {
                        currentCheckBox.Enabled = true;
                    }
                }
                clearButton.Enabled = true;
                stepButton.Enabled = true;
            }
            else
            {
                updateStateField();
                clearButton.Enabled = false;
                stepButton.Enabled = false;
                startStopButton.Text = "Stop";
                foreach (List<CheckBox> currentList in checkBoxes)
                {
                    foreach (CheckBox currentCheckBox in currentList)
                    {
                        currentCheckBox.Enabled = false;
                    }
                }
                stepTimer.Enabled = true;
            }
            running = !running;
        }

        private void stepTimer_Tick(object sender, EventArgs e)
        {
            step();
        }
    }
}
