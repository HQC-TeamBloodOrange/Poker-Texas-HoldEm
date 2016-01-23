﻿namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Contracts;
    using Poker.Core;
    using Poker.Models;

    public partial class PokerForm : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "BloodOrange Poker";
        }

        //Variables
        #region Variables

        public int Nm; //What the fuck is this ??

        private PokerDatabase database;

        //Constatns
        private const int CardsOnTable = 17 - 1;
        private readonly Panel playerPanel = new Panel();
        private readonly Panel bot1Panel = new Panel();
        private readonly Panel bot2Panel = new Panel();
        private readonly Panel bot3Panel = new Panel();
        private readonly Panel bot4Panel = new Panel();
        private readonly Panel bot5Panel = new Panel();

        private readonly List<bool?> bools = new List<bool?>();
        private List<Type> win = new List<Type>();
        private readonly List<string> CheckWinners = new List<string>();
        private readonly List<int> Pot = new List<int>();
        private int[] drawnCards = new int[17];
        private readonly Image[] Deck = new Image[52];
        private readonly PictureBox[] holder = new PictureBox[52];
        private readonly Timer timer = new Timer();
        private readonly Timer Updates = new Timer();

        //Fields

        private IPlayer player, bot1, bot2, bot3, bot4, bot5;
        private IPlayer[] players;

        private Label playerLabel, bot1Label, bot2Label, bot3Label, bot4Label, bot5Label;
        private ProgressBar progressBar = new ProgressBar();

        private int call = 500, foldedPlayers = 5;
        public int playerChips = 10000, bot1Chips = 10000, bot2Chips = 10000, bot3Chips = 10000, bot4Chips = 10000, bot5Chips = 10000;

        private double type, rounds, bot1Power, bot2Power, bot3Power, bot4Power, bot5Power, playerPower, playerType = -1, raise, bot1Type = -1, bot2Type = -1, bot3Type = -1, bot4Type = -1, bot5Type = -1;
        private bool isBot1Turn, isBot2Turn, isBot3Turn, isBot4Turn, isBot5Turn;
        private bool B1Fturn, B2Fturn, B3Fturn, B4Fturn, B5Fturn;
        private bool playerFolded, bot1Folded, bot2Folded, bot3Folded, bot4Folded, bot5Folded, intsadded, changed;
        private int playerCall, bot1Call, bot2Call, bot3Call, bot4Call, bot5Call, playerRaise, bot1Raise, bot2Raise, bot3Raise, bot4Raise, bot5Raise;
        private int height, width, winners, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;

        private int last = 123, raisedTurn = 1;

        private bool PFturn, isPlayerTurn = true, restart, raising;

        private Type sorted;

        private string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/



        private int time = 60, bigBlind = 500, smallBlind = 250, maxBlind = 10000000, turnCount; // TODO: Make them in constants;

        //Try hard mode :(
        public int i;

        #endregion

        public PokerForm()
        {
            ////bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);

            this.playerLabel = new Label();
            this.bot1Label = new Label();
            this.bot2Label = new Label();
            this.bot3Label = new Label();
            this.bot4Label = new Label();
            this.bot5Label = new Label();

            this.playerLabel.Text = string.Empty;
            this.bot1Label.Text = string.Empty;
            this.bot2Label.Text = string.Empty;
            this.bot3Label.Text = string.Empty;
            this.bot4Label.Text = string.Empty;
            this.bot5Label.Text = string.Empty;

            this.player = new Player(this.playerLabel);
            this.bot1 = new Player(this.bot1Label);
            this.bot2 = new Player(this.bot2Label);
            this.bot3 = new Player(this.bot3Label);
            this.bot4 = new Player(this.bot4Label);
            this.bot5 = new Player(this.bot5Label);

            this.players = new IPlayer[6];
            players[0] = this.player;
            players[1] = this.bot1;
            players[2] = this.bot2;
            players[3] = this.bot3;
            players[4] = this.bot4;
            players[5] = this.bot5;

            this.call = this.bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            this.InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            this.Shuffle();
            this.textBoxPot.Enabled = false;
            this.textBoxChips.Enabled = false;
            this.textBoxBot1Chips.Enabled = false;
            this.textBoxBot2Chips.Enabled = false;
            this.textBoxBot3Chips.Enabled = false;
            this.textBoxBot4Chips.Enabled = false;
            this.textBoxBot5Chips.Enabled = false;
            this.textBoxChips.Text = "Chips : " + this.playerChips;
            this.textBoxBot1Chips.Text = "Chips : " + this.bot1Chips;
            this.textBoxBot2Chips.Text = "Chips : " + this.bot2Chips;
            this.textBoxBot3Chips.Text = "Chips : " + this.bot3Chips;
            this.textBoxBot4Chips.Text = "Chips : " + this.bot4Chips;
            this.textBoxBot5Chips.Text = "Chips : " + this.bot5Chips;
            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += this.TimerTick;
            this.Updates.Interval = (1 * 1 * 100);
            this.Updates.Tick += this.UpdateTick;
            this.textBoxBB.Visible = true;
            this.textboxSB.Visible = true;
            this.buttonBB.Visible = true;
            this.buttonSB.Visible = true;
            this.textBoxBB.Visible = true;
            this.textboxSB.Visible = true;
            this.buttonBB.Visible = true;
            this.buttonSB.Visible = true;
            this.textBoxBB.Visible = false;
            this.textboxSB.Visible = false;
            this.buttonBB.Visible = false;
            this.buttonSB.Visible = false;
            this.tbRaise.Text = (this.bigBlind * 2).ToString();
        }

        public PokerDatabase Database { get; set; }

        public List<Type> Win
        {
            get
            {
                return this.win;
            }
        }

        public int[] DrawnCards
        {
            get
            {
                return this.drawnCards;
            }
        }

        public Type Sorted
        {
            get
            {
                return this.sorted;
            }

            set
            {
                this.sorted = value;
            }
        }

        public PictureBox[] Holder
        {
            get
            {
                return this.holder;
            }
        }

        private async Task Shuffle()
        {
            this.bools.Add(this.PFturn);
            this.bools.Add(this.B1Fturn);
            this.bools.Add(this.B2Fturn);
            this.bools.Add(this.B3Fturn);
            this.bools.Add(this.B4Fturn);
            this.bools.Add(this.B5Fturn);
            this.buttonCall.Enabled = false;
            this.bRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            var check = false;
            var backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            var r = new Random();

            for (this.i = this.ImgLocation.Length; this.i > 0; this.i--)
            {
                var j = r.Next(this.i);
                var k = this.ImgLocation[j];
                this.ImgLocation[j] = this.ImgLocation[this.i - 1];
                this.ImgLocation[this.i - 1] = k;
            }

            for (this.i = 0; this.i < 17; this.i++)
            {
                this.Deck[this.i] = Image.FromFile(this.ImgLocation[this.i]);
                var charsToRemove = new[]
                {
                    "Assets\\Cards\\", ".png"
                };

                foreach (var c in charsToRemove)
                {
                    this.ImgLocation[this.i] = this.ImgLocation[this.i].Replace(c, string.Empty);
                }

                this.DrawnCards[this.i] = int.Parse(this.ImgLocation[this.i]) - 1;
                this.Holder[this.i] = new PictureBox();
                this.Holder[this.i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Holder[this.i].Height = 130;
                this.Holder[this.i].Width = 80;
                this.Controls.Add(this.Holder[this.i]);
                this.Holder[this.i].Name = "pb" + this.i;
                await Task.Delay(200);

                #region Throwing Cards

                if (this.i < 2)
                {
                    if (this.Holder[0].Tag != null)
                    {
                        this.Holder[1].Tag = this.DrawnCards[1];
                    }

                    this.Holder[0].Tag = this.DrawnCards[0];
                    this.Holder[this.i].Image = this.Deck[this.i];
                    this.Holder[this.i].Anchor = (AnchorStyles.Bottom);

                    ////Holder[i].Dock = DockStyle.Top;
                    this.Holder[this.i].Location = new Point(horizontal, vertical);
                    horizontal += this.Holder[this.i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.players[0].CardsPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    //this.playerPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }

                if (this.bot1Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 2 && this.i < 4)
                    {
                        if (this.Holder[2].Tag != null)
                        {
                            this.Holder[3].Tag = this.DrawnCards[3];
                        }

                        this.Holder[2].Tag = this.DrawnCards[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot1Panel);
                        this.players[1].CardsPanel.Location = new Point(this.Holder[2].Left - 10, this.Holder[2].Top - 10);
                        //this.bot1Panel.Location = new Point(this.Holder[2].Left - 10, this.Holder[2].Top - 10);
                        this.bot1Panel.BackColor = Color.DarkBlue;
                        this.bot1Panel.Height = 150;
                        this.bot1Panel.Width = 180;
                        this.bot1Panel.Visible = false;
                        if (this.i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot2Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 4 && this.i < 6)
                    {
                        if (this.Holder[4].Tag != null)
                        {
                            this.Holder[5].Tag = this.DrawnCards[5];
                        }

                        this.Holder[4].Tag = this.DrawnCards[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot2Panel);
                        this.bot2Panel.Location = new Point(this.Holder[4].Left - 10, this.Holder[4].Top - 10);
                        this.bot2Panel.BackColor = Color.DarkBlue;
                        this.bot2Panel.Height = 150;
                        this.bot2Panel.Width = 180;
                        this.bot2Panel.Visible = false;
                        if (this.i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot3Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 6 && this.i < 8)
                    {
                        if (this.Holder[6].Tag != null)
                        {
                            this.Holder[7].Tag = this.DrawnCards[7];
                        }

                        this.Holder[6].Tag = this.DrawnCards[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot3Panel);
                        this.bot3Panel.Location = new Point(this.Holder[6].Left - 10, this.Holder[6].Top - 10);
                        this.bot3Panel.BackColor = Color.DarkBlue;
                        this.bot3Panel.Height = 150;
                        this.bot3Panel.Width = 180;
                        this.bot3Panel.Visible = false;
                        if (this.i == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot4Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 8 && this.i < 10)
                    {
                        if (this.Holder[8].Tag != null)
                        {
                            this.Holder[9].Tag = this.DrawnCards[9];
                        }

                        this.Holder[8].Tag = this.DrawnCards[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot4Panel);
                        this.bot4Panel.Location = new Point(this.Holder[8].Left - 10, this.Holder[8].Top - 10);
                        this.bot4Panel.BackColor = Color.DarkBlue;
                        this.bot4Panel.Height = 150;
                        this.bot4Panel.Width = 180;
                        this.bot4Panel.Visible = false;
                        if (this.i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot5Chips > 0)
                {
                    this.foldedPlayers--;
                    if (this.i >= 10 && this.i < 12)
                    {
                        if (this.Holder[10].Tag != null)
                        {
                            this.Holder[11].Tag = this.DrawnCards[11];
                        }

                        this.Holder[10].Tag = this.DrawnCards[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += this.Holder[this.i].Width;
                        this.Holder[this.i].Visible = true;
                        this.Controls.Add(this.bot5Panel);
                        this.bot5Panel.Location = new Point(this.Holder[10].Left - 10, this.Holder[10].Top - 10);
                        this.bot5Panel.BackColor = Color.DarkBlue;
                        this.bot5Panel.Height = 150;
                        this.bot5Panel.Width = 180;
                        this.bot5Panel.Visible = false;
                        if (this.i == 11)
                        {
                            check = false;
                        }
                    }
                }

                /// TODO: Refactor the list of if's. Replace the magic numbers.
                if (this.i >= 12)
                {
                    this.Holder[12].Tag = this.DrawnCards[12];

                    if (this.i > 12)
                    {
                        this.Holder[13].Tag = this.DrawnCards[13];
                    }

                    if (this.i > 13)
                    {
                        this.Holder[14].Tag = this.DrawnCards[14];
                    }

                    if (this.i > 14)
                    {
                        this.Holder[15].Tag = this.DrawnCards[15];
                    }

                    if (this.i > 15)
                    {
                        this.Holder[16].Tag = this.DrawnCards[16];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;
                    if (this.Holder[this.i] != null)
                    {
                        this.Holder[this.i].Anchor = AnchorStyles.None;
                        this.Holder[this.i].Image = backImage;

                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                #endregion

                if (this.bot1Chips <= 0)
                {
                    this.B1Fturn = true;
                    this.Holder[2].Visible = false;
                    this.Holder[3].Visible = false;
                }
                else
                {
                    this.B1Fturn = false;
                    if (this.i == 3)
                    {
                        if (this.Holder[3] != null)
                        {
                            this.Holder[2].Visible = true;
                            this.Holder[3].Visible = true;
                        }
                    }
                }

                if (this.bot2Chips <= 0)
                {
                    this.B2Fturn = true;
                    this.Holder[4].Visible = false;
                    this.Holder[5].Visible = false;
                }
                else
                {
                    this.B2Fturn = false;
                    if (this.i == 5)
                    {
                        if (this.Holder[5] != null)
                        {
                            this.Holder[4].Visible = true;
                            this.Holder[5].Visible = true;
                        }
                    }
                }

                if (this.bot3Chips <= 0)
                {
                    this.B3Fturn = true;
                    this.Holder[6].Visible = false;
                    this.Holder[7].Visible = false;
                }
                else
                {
                    this.B3Fturn = false;
                    if (this.i == 7)
                    {
                        if (this.Holder[7] != null)
                        {
                            this.Holder[6].Visible = true;
                            this.Holder[7].Visible = true;
                        }
                    }
                }

                if (this.bot4Chips <= 0)
                {
                    this.B4Fturn = true;
                    this.Holder[8].Visible = false;
                    this.Holder[9].Visible = false;
                }
                else
                {
                    this.B4Fturn = false;
                    if (this.i == 9)
                    {
                        if (this.Holder[9] != null)
                        {
                            this.Holder[8].Visible = true;
                            this.Holder[9].Visible = true;
                        }
                    }
                }

                if (this.bot5Chips <= 0)
                {
                    this.B5Fturn = true;
                    this.Holder[10].Visible = false;
                    this.Holder[11].Visible = false;
                }
                else
                {
                    this.B5Fturn = false;
                    if (this.i == 11)
                    {
                        if (this.Holder[11] != null)
                        {
                            this.Holder[10].Visible = true;
                            this.Holder[11].Visible = true;
                        }
                    }
                }

                if (this.i == 16)
                {
                    if (!this.restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }

                    this.timer.Start();
                }
            }

            if (this.foldedPlayers == 5)
            {
                var dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.foldedPlayers = 5;
            }

            if (this.i == 17)
            {
                this.bRaise.Enabled = true;
                this.buttonCall.Enabled = true;
                this.bRaise.Enabled = true;
                this.bRaise.Enabled = true;
                this.buttonFold.Enabled = true;
            }
        }

        private async Task Turns()
        {
            #region Rotating

            if (!this.PFturn)
            {
                if (this.isPlayerTurn)
                {
                    this.FixCall(this.playerStatus, ref this.playerCall, ref this.playerRaise, 1);
                    ////MessageBox.Show("Player's Turn");
                    this.progressBarTimer.Visible = true;
                    this.progressBarTimer.Value = 1000;
                    this.time = 60;
                    this.maxBlind = 10000000;
                    this.timer.Start();
                    this.bRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.playerStatus, ref this.playerCall, ref this.playerRaise, 2);
                }
            }

            if (this.PFturn || !this.isPlayerTurn)
            {
                await this.AllIn();
                if (this.PFturn && !this.playerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.playerFolded = true;
                    }
                }

                await this.CheckRaise(0, 0);
                this.progressBarTimer.Visible = false;
                this.bRaise.Enabled = false;
                this.buttonCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.bRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.timer.Stop();
                this.isBot1Turn = true;
                if (!this.B1Fturn)
                {
                    if (this.isBot1Turn)
                    {
                        this.FixCall(this.bot1Status, ref this.bot1Call, ref this.bot1Raise, 1);
                        this.FixCall(this.bot1Status, ref this.bot1Call, ref this.bot1Raise, 2);
                        this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
                        MessageBox.Show("Bot 1's Turn");
                        this.AI(2, 3, ref this.bot1Chips, ref this.isBot1Turn, ref this.B1Fturn, this.bot1Status, 0, this.bot1Power, this.bot1Type);
                        this.turnCount++;
                        this.last = 1;
                        this.isBot1Turn = false;
                        this.isBot2Turn = true;
                    }
                }

                if (this.B1Fturn && !this.bot1Folded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.maxLeft--;
                    this.bot1Folded = true;
                }

                if (this.B1Fturn || !this.isBot1Turn)
                {
                    await this.CheckRaise(1, 1);
                    this.isBot2Turn = true;
                }

                if (!this.B2Fturn)
                {
                    if (this.isBot2Turn)
                    {
                        this.FixCall(this.bot2Status, ref this.bot2Call, ref this.bot2Raise, 1);
                        this.FixCall(this.bot2Status, ref this.bot2Call, ref this.bot2Raise, 2);
                        this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
                        MessageBox.Show("Bot 2's Turn");
                        this.AI(4, 5, ref this.bot2Chips, ref this.isBot2Turn, ref this.B2Fturn, this.bot2Status, 1, this.bot2Power, this.bot2Type);
                        this.turnCount++;
                        this.last = 2;
                        this.isBot2Turn = false;
                        this.isBot3Turn = true;
                    }
                }

                if (this.B2Fturn && !this.bot2Folded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.bot2Folded = true;
                }

                if (this.B2Fturn || !this.isBot2Turn)
                {
                    await this.CheckRaise(2, 2);
                    this.isBot3Turn = true;
                }

                if (!this.B3Fturn)
                {
                    if (this.isBot3Turn)
                    {
                        this.FixCall(this.bot3Status, ref this.bot3Call, ref this.bot3Raise, 1);
                        this.FixCall(this.bot3Status, ref this.bot3Call, ref this.bot3Raise, 2);
                        this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
                        MessageBox.Show("Bot 3's Turn");
                        this.AI(6, 7, ref this.bot3Chips, ref this.isBot3Turn, ref this.B3Fturn, this.bot3Status, 2, this.bot3Power, this.bot3Type);
                        this.turnCount++;
                        this.last = 3;
                        this.isBot3Turn = false;
                        this.isBot4Turn = true;
                    }
                }

                if (this.B3Fturn && !this.bot3Folded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.bot3Folded = true;
                }

                if (this.B3Fturn || !this.isBot3Turn)
                {
                    await this.CheckRaise(3, 3);
                    this.isBot4Turn = true;
                }

                if (!this.B4Fturn)
                {
                    if (this.isBot4Turn)
                    {
                        this.FixCall(this.bot4Status, ref this.bot4Call, ref this.bot4Raise, 1);
                        this.FixCall(this.bot4Status, ref this.bot4Call, ref this.bot4Raise, 2);
                        this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
                        MessageBox.Show("Bot 4's Turn");
                        this.AI(8, 9, ref this.bot4Chips, ref this.isBot4Turn, ref this.B4Fturn, this.bot4Status, 3, this.bot4Power, this.bot4Type);
                        this.turnCount++;
                        this.last = 4;
                        this.isBot4Turn = false;
                        this.isBot5Turn = true;
                    }
                }

                if (this.B4Fturn && !this.bot4Folded)
                {
                    this.bools.RemoveAt(4);
                    this.bools.Insert(4, null);
                    this.maxLeft--;
                    this.bot4Folded = true;
                }

                if (this.B4Fturn || !this.isBot4Turn)
                {
                    await this.CheckRaise(4, 4);
                    this.isBot5Turn = true;
                }

                if (!this.B5Fturn)
                {
                    if (this.isBot5Turn)
                    {
                        this.FixCall(this.bot5Status, ref this.bot5Call, ref this.bot5Raise, 1);
                        this.FixCall(this.bot5Status, ref this.bot5Call, ref this.bot5Raise, 2);
                        this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
                        MessageBox.Show("Bot 5's Turn");
                        this.AI(10, 11, ref this.bot5Chips, ref this.isBot5Turn, ref this.B5Fturn, this.bot5Status, 4, this.bot5Power, this.bot5Type);
                        this.turnCount++;
                        this.last = 5;
                        this.isBot5Turn = false;
                    }
                }

                if (this.B5Fturn && !this.bot5Folded)
                {
                    this.bools.RemoveAt(5);
                    this.bools.Insert(5, null);
                    this.maxLeft--;
                    this.bot5Folded = true;
                }

                if (this.B5Fturn || !this.isBot5Turn)
                {
                    await this.CheckRaise(5, 5);
                    this.isPlayerTurn = true;
                }

                if (this.PFturn && !this.playerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.playerFolded = true;
                    }
                }

                #endregion

                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }

                this.restart = false;
            }
        }
        // ----------------------------------------------------------------------
//        Rules.Rules

        private void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            //if (c1 == 0 && c2 == 1)
            //{
            //}

            if (!foldedTurn || c1 == 0 && c2 == 1 && this.playerStatus.Text.Contains("Fold") == false)
            {

                for (this.i = 0; this.i < CardsOnTable; this.i++)
                {
                    if (this.DrawnCards[this.i] == int.Parse(this.Holder[c1].Tag.ToString()) && this.DrawnCards[this.i + 1] == int.Parse(this.Holder[c2].Tag.ToString()))
                    {
                        ////Pair from Hand current = 1
                        //this.rPairFromHand(ref current, ref Power);
                        Poker.Rules.RuleManager.FollowRules(
                            c1,
                            c2,
                            currentText,
                            ref current,
                            ref Power,
                            foldedTurn,
                            this.type,
                            this);


                        //CHANGES:

                        //this.PairTwoPairRule(ref current, ref Power);
                        //this.TwoPairRule(ref current, ref Power);
                        //this.rThreeOfAKind(ref current, ref Power, cardsOnTableWithPlayerCards);
                        //this.rStraight(ref current, ref Power, cardsOnTableWithPlayerCards);
                        //this.rFlush(ref current, ref Power, ref vf, cardsOnTable);
                        //this.rFullHouse(ref current, ref Power, ref done, cardsOnTableWithPlayerCards);
                        //this.rFourOfAKind(ref current, ref Power, cardsOnTableWithPlayerCards);
                        //this.rStraightFlush(ref current, ref Power, cardsOfClubs, cardsOfDiamonds, cardsOfHearts, cardsOfSpades);
                        //this.rHighCard(ref current, ref Power);






                    }
                }
            }
        }
        // TODO: RoyalFlush
        // ----------------------------------------------------------------------
        // TODO: Milen


        // TODO: Aleks


        // TODO: Tisho


        // TODO: Tedi - eddited



        // ----------------------------------------------------------------------

        private void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (var j = 0; j <= 16; j++)
            {
                ////await Task.Delay(5);
                if (this.Holder[j].Visible)
                {
                    this.Holder[j].Image = this.Deck[j];
                }
            }
            if (current == this.sorted.Current)
            {
                if (Power == this.sorted.Power)
                {
                    this.winners++;
                    this.CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly) ////lastfixed
            {
                if (this.winners > 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxChips.Text = this.playerChips.ToString();
                        ////pPanel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.bot1Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBot1Chips.Text = this.bot1Chips.ToString();
                        ////b1Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.bot2Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBot2Chips.Text = this.bot2Chips.ToString();
                        ////b2Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.bot3Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBot3Chips.Text = this.bot3Chips.ToString();
                        ////b3Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.bot4Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBot4Chips.Text = this.bot4Chips.ToString();
                        ////b4Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.bot5Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBot5Chips.Text = this.bot5Chips.ToString();
                        ////b5Panel.Visible = true;
                    }
                    ////await Finish(1);
                }
                if (this.winners == 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////pPanel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.bot1Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b1Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.bot2Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b2Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.bot3Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b3Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.bot4Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b4Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.bot5Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b5Panel.Visible = true;
                    }
                }
            }
        }

        private async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.raise = 0;
                        this.call = 0;
                        this.raisedTurn = 123;
                        this.rounds++;
                        if (!this.PFturn)
                        {
                            this.playerStatus.Text = "";
                        }
                        if (!this.B1Fturn)
                        {
                            this.bot1Status.Text = "";
                        }
                        if (!this.B2Fturn)
                        {
                            this.bot2Status.Text = "";
                        }
                        if (!this.B3Fturn)
                        {
                            this.bot3Status.Text = "";
                        }
                        if (!this.B4Fturn)
                        {
                            this.bot4Status.Text = "";
                        }
                        if (!this.B5Fturn)
                        {
                            this.bot5Status.Text = "";
                        }
                    }
                }
            }
            if (this.rounds == this.Flop)
            {
                for (var j = 12; j <= 14; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1Call = 0;
                        this.bot1Raise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (this.rounds == this.Turn)
            {
                for (var j = 14; j <= 15; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1Call = 0;
                        this.bot1Raise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (this.rounds == this.River)
            {
                for (var j = 15; j <= 16; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1Call = 0;
                        this.bot1Raise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (this.rounds == this.End && this.maxLeft == 6)
            {
                var fixedLast = "qwerty";
                if (!this.playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
                }
                if (!this.bot1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
                }
                if (!this.bot2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
                }
                if (!this.bot3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
                }
                if (!this.bot4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
                }
                if (!this.bot5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
                }
                this.Winner(this.playerType, this.playerPower, "Player", this.playerChips, fixedLast);
                this.Winner(this.bot1Type, this.bot1Power, "Bot 1", this.bot1Chips, fixedLast);
                this.Winner(this.bot2Type, this.bot2Power, "Bot 2", this.bot2Chips, fixedLast);
                this.Winner(this.bot3Type, this.bot3Power, "Bot 3", this.bot3Chips, fixedLast);
                this.Winner(this.bot4Type, this.bot4Power, "Bot 4", this.bot4Chips, fixedLast);
                this.Winner(this.bot5Type, this.bot5Power, "Bot 5", this.bot5Chips, fixedLast);
                this.restart = true;
                this.isPlayerTurn = true;
                this.PFturn = false;
                this.B1Fturn = false;
                this.B2Fturn = false;
                this.B3Fturn = false;
                this.B4Fturn = false;
                this.B5Fturn = false;
                if (this.playerChips <= 0)
                {
                    var f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.playerChips = f2.a;
                        this.bot1Chips += f2.a;
                        this.bot2Chips += f2.a;
                        this.bot3Chips += f2.a;
                        this.bot4Chips += f2.a;
                        this.bot5Chips += f2.a;
                        this.PFturn = false;
                        this.isPlayerTurn = true;
                        this.bRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.bRaise.Text = "Raise";
                    }
                }
                this.playerPanel.Visible = false;
                this.bot1Panel.Visible = false;
                this.bot2Panel.Visible = false;
                this.bot3Panel.Visible = false;
                this.bot4Panel.Visible = false;
                this.bot5Panel.Visible = false;
                this.playerCall = 0;
                this.playerRaise = 0;
                this.bot1Call = 0;
                this.bot1Raise = 0;
                this.bot2Call = 0;
                this.bot2Raise = 0;
                this.bot3Call = 0;
                this.bot3Raise = 0;
                this.bot4Call = 0;
                this.bot4Raise = 0;
                this.bot5Call = 0;
                this.bot5Raise = 0;
                this.last = 0;
                this.call = this.bigBlind;
                this.raise = 0;
                this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bools.Clear();
                this.rounds = 0;
                this.playerPower = 0;
                this.playerType = -1;
                this.type = 0;
                this.bot1Power = 0;
                this.bot2Power = 0;
                this.bot3Power = 0;
                this.bot4Power = 0;
                this.bot5Power = 0;
                this.bot1Type = -1;
                this.bot2Type = -1;
                this.bot3Type = -1;
                this.bot4Type = -1;
                this.bot5Type = -1;
                this.Pot.Clear();
                this.CheckWinners.Clear();
                this.winners = 0;
                this.Win.Clear();
                this.sorted.Current = 0;
                this.sorted.Power = 0;
                for (var os = 0; os < 17; os++)
                {
                    this.Holder[os].Image = null;
                    this.Holder[os].Invalidate();
                    this.Holder[os].Visible = false;
                }
                this.textBoxPot.Text = "0";
                this.playerStatus.Text = "";
                await this.Shuffle();
                await this.Turns();
            }
        }

        private void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != this.raise && cRaise <= this.raise)
                    {
                        this.call = Convert.ToInt32(this.raise) - cRaise;
                    }
                    if (cCall != this.call || cCall <= this.call)
                    {
                        this.call = this.call - cCall;
                    }
                    if (cRaise == this.raise && this.raise > 0)
                    {
                        this.call = 0;
                        this.buttonCall.Enabled = false;
                        this.buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        private async Task AllIn()
        {
            #region All in

            if (this.playerChips <= 0 && !this.intsadded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.Pot.Add(this.playerChips);
                    this.intsadded = true;
                }
                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.Pot.Add(this.playerChips);
                    this.intsadded = true;
                }
            }
            this.intsadded = false;
            if (this.bot1Chips <= 0 && !this.B1Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.bot1Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot2Chips <= 0 && !this.B2Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.bot2Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot3Chips <= 0 && !this.B3Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.bot3Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot4Chips <= 0 && !this.B4Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.bot4Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.bot5Chips <= 0 && !this.B5Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.bot5Chips);
                    this.intsadded = true;
                }
            }
            if (this.Pot.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.Pot.Clear();
            }

            #endregion

            var abc = this.bools.Count(x => x == false);

            #region LastManStanding

            if (abc == 1)
            {
                var index = this.bools.IndexOf(false);
                if (index == 0)
                {
                    this.playerChips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.playerChips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    this.bot1Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.bot1Chips.ToString();
                    this.bot1Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    this.bot2Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.bot2Chips.ToString();
                    this.bot2Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    this.bot3Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.bot3Chips.ToString();
                    this.bot3Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    this.bot4Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.bot4Chips.ToString();
                    this.bot4Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    this.bot5Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.bot5Chips.ToString();
                    this.bot5Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (var j = 0; j <= 16; j++)
                {
                    this.Holder[j].Visible = false;
                }
                await this.Finish(1);
            }
            this.intsadded = false;

            #endregion

            #region FiveOrLessLeft

            if (abc < 6 && abc > 1 && this.rounds >= this.End)
            {
                await this.Finish(2);
            }

            #endregion
        }

        private async Task Finish(int n)
        {
            if (n == 2)
            {
                this.FixWinners();
            }
            this.playerPanel.Visible = false;
            this.bot1Panel.Visible = false;
            this.bot2Panel.Visible = false;
            this.bot3Panel.Visible = false;
            this.bot4Panel.Visible = false;
            this.bot5Panel.Visible = false;
            this.call = this.bigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.rounds = 0;
            this.bot1Power = 0;
            this.bot2Power = 0;
            this.bot3Power = 0;
            this.bot4Power = 0;
            this.bot5Power = 0;
            this.playerPower = 0;
            this.playerType = -1;
            this.raise = 0;
            this.bot1Type = -1;
            this.bot2Type = -1;
            this.bot3Type = -1;
            this.bot4Type = -1;
            this.bot5Type = -1;
            this.isBot1Turn = false;
            this.isBot2Turn = false;
            this.isBot3Turn = false;
            this.isBot4Turn = false;
            this.isBot5Turn = false;
            this.B1Fturn = false;
            this.B2Fturn = false;
            this.B3Fturn = false;
            this.B4Fturn = false;
            this.B5Fturn = false;
            this.playerFolded = false;
            this.bot1Folded = false;
            this.bot2Folded = false;
            this.bot3Folded = false;
            this.bot4Folded = false;
            this.bot5Folded = false;
            this.PFturn = false;
            this.isPlayerTurn = true;
            this.restart = false;
            this.raising = false;
            this.playerCall = 0;
            this.bot1Call = 0;
            this.bot2Call = 0;
            this.bot3Call = 0;
            this.bot4Call = 0;
            this.bot5Call = 0;
            this.playerRaise = 0;
            this.bot1Raise = 0;
            this.bot2Raise = 0;
            this.bot3Raise = 0;
            this.bot4Raise = 0;
            this.bot5Raise = 0;
            this.height = 0;
            this.width = 0;
            this.winners = 0;
            this.Flop = 1;
            this.Turn = 2;
            this.River = 3;
            this.End = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.bools.Clear();
            this.CheckWinners.Clear();
            this.Pot.Clear();
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.textBoxPot.Text = "0";
            this.time = 60;
            this.maxBlind = 10000000;
            this.turnCount = 0;
            this.playerStatus.Text = "";
            this.bot1Status.Text = "";
            this.bot2Status.Text = "";
            this.bot3Status.Text = "";
            this.bot4Status.Text = "";
            this.bot5Status.Text = "";
            if (this.playerChips <= 0)
            {
                var f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.playerChips = f2.a;
                    this.bot1Chips += f2.a;
                    this.bot2Chips += f2.a;
                    this.bot3Chips += f2.a;
                    this.bot4Chips += f2.a;
                    this.bot5Chips += f2.a;
                    this.PFturn = false;
                    this.isPlayerTurn = true;
                    this.bRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.bRaise.Text = "Raise";
                }
            }
            this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (var os = 0; os < 17; os++)
            {
                this.Holder[os].Image = null;
                this.Holder[os].Invalidate();
                this.Holder[os].Visible = false;
            }
            await this.Shuffle();
            ////await Turns();
        }

        private void FixWinners()
        {
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            var fixedLast = "qwerty";
            if (!this.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            }
            if (!this.bot1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
            }
            if (!this.bot2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
            }
            if (!this.bot3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
            }
            if (!this.bot4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
            }
            if (!this.bot5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
            }
            this.Winner(this.playerType, this.playerPower, "Player", this.playerChips, fixedLast);
            this.Winner(this.bot1Type, this.bot1Power, "Bot 1", this.bot1Chips, fixedLast);
            this.Winner(this.bot2Type, this.bot2Power, "Bot 2", this.bot2Chips, fixedLast);
            this.Winner(this.bot3Type, this.bot3Power, "Bot 3", this.bot3Chips, fixedLast);
            this.Winner(this.bot4Type, this.bot4Power, "Bot 4", this.bot4Chips, fixedLast);
            this.Winner(this.bot5Type, this.bot5Power, "Bot 5", this.bot5Chips, fixedLast);
        }
        // ----------------------------------------------------------------------
        private void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            // TODO: make it with switch case.
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    this.HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    this.PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    this.PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    this.TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    this.ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 4)
                {
                    this.Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    this.Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 6)
                {
                    this.FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 7)
                {
                    this.FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    this.StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                this.Holder[c1].Visible = false;
                this.Holder[c2].Visible = false;
            }
        }

        // TODO: Stoqn
        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            this.HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            this.HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }

        // TODO: Tedi - edited
        private void PairHand(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            var pairHand = new Random();
            var randomCall = pairHand.Next(10, 16);
            var randomRaise = pairHand.Next(10, 13);

            if (botPower <= 199 && botPower >= 140)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 6, randomRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 7, randomRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 9, randomRaise);
            }
        }

        private void TwoPair(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            var twoPair = new Random();
            var randomCall = twoPair.Next(6, 11);
            var randomRaise = twoPair.Next(6, 11);

            if (botPower <= 290 && botPower >= 246)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 3, randomRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 4, randomRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 4, randomRaise);
            }
        }

        // TODO: Tisho
        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var tk = new Random();
            var tCall = tk.Next(3, 7);
            var tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower <= 327 && botPower >= 321) ////10  8
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303) ////7 2
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }

        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var str = new Random();
            var sCall = str.Next(3, 6);
            var sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower <= 409 && botPower >= 407) ////10  8
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower < 407 && botPower >= 404)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }

        // TODO: Aleks
        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var fsh = new Random();
            var fCall = fsh.Next(2, 6);
            var fRaise = fsh.Next(3, 7);
            this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }

        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var flh = new Random();
            var fhCall = flh.Next(1, 5);
            var fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
            if (botPower < 620 && botPower >= 602)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }

        // TODO: Milen
        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var fk = new Random();
            var fkCall = fk.Next(1, 4);
            var fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }

        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            var sf = new Random();
            var sfCall = sf.Next(1, 3);
            var sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                this.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }
        //-----------------------------------------------------------------------------------------
        // TODO: Add to namespace Statuses.
        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            this.raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            this.raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            this.raising = false;
            sTurn = false;
            sChips -= this.call;
            sStatus.Text = "Call " + this.call;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "Raise " + this.raise;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
            this.call = Convert.ToInt32(this.raise);
            this.raising = true;
            sTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            var a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 4);
            if (this.call <= 0)
            {
                this.Check(ref sTurn, sStatus);
            }
            if (this.call > 0)
            {
                if (rnd == 1)
                {
                    if (this.call <= RoundN(sChips, n))
                    {
                        this.Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
                if (rnd == 2)
                {
                    if (this.call <= RoundN(sChips, n1))
                    {
                        this.Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (rnd == 3)
            {
                if (this.raise == 0)
                {
                    this.raise = this.call * 2;
                    this.Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (this.raise <= RoundN(sChips, n))
                    {
                        this.raise = this.call * 2;
                        this.Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int randomCall, int n1, int randomRaise)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.call <= 0)
                {
                    this.Check(ref sTurn, sStatus);
                }
                if (this.call > 0)
                {
                    if (this.call >= RoundN(sChips, n1))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (this.raise > RoundN(sChips, randomCall))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (this.call >= RoundN(sChips, randomCall) && this.call <= RoundN(sChips, n1))
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= RoundN(sChips, randomCall) && this.raise >= (RoundN(sChips, randomCall)) / 2)
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= (RoundN(sChips, randomCall)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, randomCall);
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
            }
            if (this.rounds >= 2)
            {
                if (this.call > 0)
                {
                    if (this.call >= RoundN(sChips, n1 - rnd))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (this.raise > RoundN(sChips, randomCall - rnd))
                    {
                        this.Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (this.call >= RoundN(sChips, randomCall - rnd) && this.call <= RoundN(sChips, n1 - rnd))
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= RoundN(sChips, randomCall - rnd) && this.raise >= (RoundN(sChips, randomCall - rnd)) / 2)
                        {
                            this.Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= (RoundN(sChips, randomCall - rnd)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, randomCall - rnd);
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (this.call <= 0)
                {
                    this.raise = RoundN(sChips, randomRaise - rnd);
                    this.Raised(ref sChips, ref sTurn, sStatus);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.call <= 0)
            {
                this.Check(ref botTurn, botStatus);
            }
            else
            {
                if (this.call >= RoundN(botChips, n))
                {
                    if (botChips > this.call)
                    {
                        this.Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= this.call)
                    {
                        this.raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (botChips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            this.Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            this.Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.raise = this.call * 2;
                        this.Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }


        #region UI
        //------------------------------------------------------------------
        // TODO: Add to namespace Events.
        private async void TimerTick(object sender, object e)
        {
            if (this.progressBarTimer.Value <= 0)
            {
                this.PFturn = true;
                await this.Turns();
            }
            if (this.time > 0)
            {
                this.time--;
                this.progressBarTimer.Value = (this.time / 6) * 100;
            }
        }

        private void UpdateTick(object sender, object e)
        {
            if (this.playerChips <= 0)
            {
                this.textBoxChips.Text = "Chips : 0";
            }
            if (this.bot1Chips <= 0)
            {
                this.textBoxBot1Chips.Text = "Chips : 0";
            }
            if (this.bot2Chips <= 0)
            {
                this.textBoxBot2Chips.Text = "Chips : 0";
            }
            if (this.bot3Chips <= 0)
            {
                this.textBoxBot3Chips.Text = "Chips : 0";
            }
            if (this.bot4Chips <= 0)
            {
                this.textBoxBot4Chips.Text = "Chips : 0";
            }
            if (this.bot5Chips <= 0)
            {
                this.textBoxBot5Chips.Text = "Chips : 0";
            }
            this.textBoxChips.Text = "Chips : " + this.playerChips;
            this.textBoxBot1Chips.Text = "Chips : " + this.bot1Chips;
            this.textBoxBot2Chips.Text = "Chips : " + this.bot2Chips;
            this.textBoxBot3Chips.Text = "Chips : " + this.bot3Chips;
            this.textBoxBot4Chips.Text = "Chips : " + this.bot4Chips;
            this.textBoxBot5Chips.Text = "Chips : " + this.bot5Chips;
            if (this.playerChips <= 0)
            {
                this.isPlayerTurn = false;
                this.PFturn = true;
                this.buttonCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }
            if (this.maxBlind > 0)
            {
                this.maxBlind--;
            }
            if (this.playerChips >= this.call)
            {
                this.buttonCall.Text = "Call " + this.call;
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.bRaise.Enabled = false;
            }
            if (this.call > 0)
            {
                this.buttonCheck.Enabled = false;
            }
            if (this.call <= 0)
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }
            if (this.playerChips <= 0)
            {
                this.bRaise.Enabled = false;
            }
            int parsedValue;

            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.playerChips <= int.Parse(this.tbRaise.Text))
                {
                    this.bRaise.Text = "All in";
                }
                else
                {
                    this.bRaise.Text = "Raise";
                }
            }
            if (this.playerChips < this.call)
            {
                this.bRaise.Enabled = false;
            }
        }

        private async void ButtonFoldClick(object sender, EventArgs e)
        {
            this.playerStatus.Text = "Fold";
            this.isPlayerTurn = false;
            this.PFturn = true;
            await this.Turns();
        }

        private async void ButtonCheckClick(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.isPlayerTurn = false;
                this.playerStatus.Text = "Check";
            }
            else
            {
                ////pStatus.Text = "All in " + Chips;

                this.buttonCheck.Enabled = false;
            }
            await this.Turns();
        }

        private async void ButtonCallClick(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            if (this.playerChips >= this.call)
            {
                this.playerChips -= this.call;
                this.textBoxChips.Text = "Chips : " + this.playerChips;
                if (this.textBoxPot.Text != "")
                {
                    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                }
                else
                {
                    this.textBoxPot.Text = this.call.ToString();
                }
                this.isPlayerTurn = false;
                this.playerStatus.Text = "Call " + this.call;
                this.playerCall = this.call;
            }
            else if (this.playerChips <= this.call && this.call > 0)
            {
                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.playerChips).ToString();
                this.playerStatus.Text = "All in " + this.playerChips;
                this.playerChips = 0;
                this.textBoxChips.Text = "Chips : " + this.playerChips;
                this.isPlayerTurn = false;
                this.buttonFold.Enabled = false;
                this.playerCall = this.playerChips;
            }
            await this.Turns();
        }

        private async void ButtonRaiseClick(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            int parsedValue;
            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.playerChips > this.call)
                {
                    if (this.raise * 2 > int.Parse(this.tbRaise.Text))
                    {
                        this.tbRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    if (this.playerChips >= int.Parse(this.tbRaise.Text))
                    {
                        this.call = int.Parse(this.tbRaise.Text);
                        this.raise = int.Parse(this.tbRaise.Text);
                        this.playerStatus.Text = "Raise " + this.call;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                        this.buttonCall.Text = "Call";
                        this.playerChips -= int.Parse(this.tbRaise.Text);
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                    else
                    {
                        this.call = this.playerChips;
                        this.raise = this.playerChips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.playerChips).ToString();
                        this.playerStatus.Text = "Raise " + this.call;
                        this.playerChips = 0;
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.isPlayerTurn = false;
            await this.Turns();
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            if (this.textBoxAdd.Text == "")
            {
            }
            else
            {
                this.playerChips += int.Parse(this.textBoxAdd.Text);
                this.bot1Chips += int.Parse(this.textBoxAdd.Text);
                this.bot2Chips += int.Parse(this.textBoxAdd.Text);
                this.bot3Chips += int.Parse(this.textBoxAdd.Text);
                this.bot4Chips += int.Parse(this.textBoxAdd.Text);
                this.bot5Chips += int.Parse(this.textBoxAdd.Text);
            }
            this.textBoxChips.Text = "Chips : " + this.playerChips;
        }

        private void ButtonOptionsClick(object sender, EventArgs e)
        {
            this.textBoxBB.Text = this.bigBlind.ToString();
            this.textboxSB.Text = this.smallBlind.ToString();
            if (this.textBoxBB.Visible == false)
            {
                this.textBoxBB.Visible = true;
                this.textboxSB.Visible = true;
                this.buttonBB.Visible = true;
                this.buttonSB.Visible = true;
            }
            else
            {
                this.textBoxBB.Visible = false;
                this.textboxSB.Visible = false;
                this.buttonBB.Visible = false;
                this.buttonSB.Visible = false;
            }
        }

        private void ButtonSBClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textboxSB.Text.Contains(",") || this.textboxSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.textboxSB.Text = this.smallBlind.ToString();
                return;
            }
            if (!int.TryParse(this.textboxSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textboxSB.Text = this.smallBlind.ToString();
                return;
            }
            if (int.Parse(this.textboxSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.textboxSB.Text = this.smallBlind.ToString();
            }
            if (int.Parse(this.textboxSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(this.textboxSB.Text) >= 250 && int.Parse(this.textboxSB.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.textboxSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void ButtonBBClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxBB.Text.Contains(",") || this.textBoxBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.textBoxBB.Text = this.bigBlind.ToString();
                return;
            }
            if (!int.TryParse(this.textboxSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textboxSB.Text = this.bigBlind.ToString();
                return;
            }
            if (int.Parse(this.textBoxBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.textBoxBB.Text = this.bigBlind.ToString();
            }
            if (int.Parse(this.textBoxBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(this.textBoxBB.Text) >= 500 && int.Parse(this.textBoxBB.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.textBoxBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void LayoutChange(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }

        #endregion
    }
}