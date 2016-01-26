namespace Poker
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

        private IPlayer player, botOne, botTwo, botThree, botFour, botFive;
        private IPlayer[] players;

        private Label playerLabel, bot1Label, bot2Label, bot3Label, bot4Label, bot5Label;
        private ProgressBar progressBar = new ProgressBar();

        private int call = 500;
        private int foldedPlayers = 5;
        // public int players[0].Chips = 10000, players[1].Chips = 10000, players[2].Chips = 10000, players[3].Chips = 10000, players[4].Chips = 10000, players[5].Chips = 10000;

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
            this.botOne = new Bot("Bot1", this.bot1Label);
            this.botTwo = new Bot("Bot2", this.bot2Label);
            this.botThree = new Bot("Bot3", this.bot3Label);
            this.botFour = new Bot("Bot4", this.bot4Label);
            this.botFive = new Bot("Bot5", this.bot5Label);

            this.players = new IPlayer[6];
            players[0] = this.player;
            players[1] = this.botOne;
            players[2] = this.botTwo;
            players[3] = this.botThree;
            players[4] = this.botFour;
            players[5] = this.botFive;

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
            this.textBoxBotOneChips.Enabled = false;
            this.textBoxBotTwoChips.Enabled = false;
            this.textBoxBotThreeChips.Enabled = false;
            this.textBoxBotFourChips.Enabled = false;
            this.textBoxBotFiveChips.Enabled = false;
            this.textBoxChips.Text = "Chips : " + this.players[0].Chips;
            this.textBoxBotOneChips.Text = "Chips : " + this.players[1].Chips;
            this.textBoxBotTwoChips.Text = "Chips : " + this.players[2].Chips;
            this.textBoxBotThreeChips.Text = "Chips : " + this.players[3].Chips;
            this.textBoxBotFourChips.Text = "Chips : " + this.players[4].Chips;
            this.textBoxBotFiveChips.Text = "Chips : " + this.players[5].Chips;
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
            int playerCardsZPosition = 580;
            int playerCardsYPosition = 480;
            var random = new Random();

            for (this.i = this.ImgLocation.Length; this.i > 0; this.i--)
            {
                var j = random.Next(this.i);
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
                    this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                    playerCardsZPosition += this.Holder[this.i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.players[0].CardsPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    //this.playerPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }

                if (this.players[1].Chips > 0)
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
                            playerCardsZPosition = 15;
                            playerCardsYPosition = 420;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                        playerCardsZPosition += this.Holder[this.i].Width;
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

                if (this.players[2].Chips > 0)
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
                            playerCardsZPosition = 75;
                            playerCardsYPosition = 65;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                        playerCardsZPosition += this.Holder[this.i].Width;
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

                if (this.players[3].Chips > 0)
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
                            playerCardsZPosition = 590;
                            playerCardsYPosition = 25;
                        }
                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                        playerCardsZPosition += this.Holder[this.i].Width;
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

                if (this.players[4].Chips > 0)
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
                            playerCardsZPosition = 1115;
                            playerCardsYPosition = 65;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                        playerCardsZPosition += this.Holder[this.i].Width;
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

                if (this.players[5].Chips > 0)
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
                            playerCardsZPosition = 1160;
                            playerCardsYPosition = 420;
                        }

                        check = true;
                        this.Holder[this.i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.Holder[this.i].Image = backImage;
                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                        playerCardsZPosition += this.Holder[this.i].Width;
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
                        playerCardsZPosition = 410;
                        playerCardsYPosition = 265;
                    }

                    check = true;
                    if (this.Holder[this.i] != null)
                    {
                        this.Holder[this.i].Anchor = AnchorStyles.None;
                        this.Holder[this.i].Image = backImage;

                        ////Holder[i].Image = Deck[i];
                        this.Holder[this.i].Location = new Point(playerCardsZPosition, playerCardsYPosition);
                        playerCardsZPosition += 110;
                    }
                }

                #endregion

                if (this.players[1].Chips <= 0)
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

                if (this.players[2].Chips <= 0)
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

                if (this.players[3].Chips <= 0)
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

                if (this.players[4].Chips <= 0)
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

                if (this.players[5].Chips <= 0)
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

            
            //Player Turn
            if (!this.players[0].FTurn)
            {
                if (this.players[0].Turn)
                {
                    this.FixCall(this.players[0], 1);
                    ////MessageBox.Show("Player's Turn");
                    this.progressBarTimer.Visible = true;
                    this.progressBarTimer.Value = 1000;
                    this.time = 60;
                    this.maxBlind = 10000000;
                    this.timer.Start();
                    this.bRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.players[0], 2);
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
                //this.isBot1Turn = true;


                var bots = players.Skip(1);
                int counter = 0;

                foreach (IPlayer bot in bots)
                {
                    counter++;
                    bot.Turn = true;
                    this.RotateTurns(bot,1 ,1, counter);
                    bot.Turn = false;

                }

                //Bot1 Turn
                //if (!this.B1Fturn)
                //{
                //    if (this.isBot1Turn)
                //    {
                //        this.FixCall(this.bot1Status, ref this.bot1Call, ref this.bot1Raise, 1);
                //        this.FixCall(this.bot1Status, ref this.bot1Call, ref this.bot1Raise, 2);
                //        this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.B1Fturn);
                //        MessageBox.Show("Bot 1's Turn");
                //        //ArtificialIntelligence(2, 3, this.players[1].Chips, ref this.isBot1Turn, ref this.B1Fturn, this.bot1Status, 0, this.bot1Power, this.bot1Type);
                //        this.ArtificialIntelligence(2, 3, this.players[1], 0);
                //        this.turnCount++;
                //        this.last = 1;
                //        this.isBot1Turn = false;
                //        this.isBot2Turn = true;

                //    }
                //}

                //if (this.B1Fturn && !this.bot1Folded)
                //{
                //    this.bools.RemoveAt(1);
                //    this.bools.Insert(1, null);
                //    this.maxLeft--;
                //    this.bot1Folded = true;
                //}

                //if (this.B1Fturn || !this.isBot1Turn)
                //{
                //    await this.CheckRaise(1, 1);
                //    this.isBot2Turn = true;
                //}

                ////Bot2 Turn
                //if (!this.B2Fturn)
                //{
                //    if (this.isBot2Turn)
                //    {
                //        this.FixCall(this.bot2Status, ref this.bot2Call, ref this.bot2Raise, 1);
                //        this.FixCall(this.bot2Status, ref this.bot2Call, ref this.bot2Raise, 2);
                //        this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.B2Fturn);
                //        MessageBox.Show("Bot 2's Turn");
                //        //this.ArtificialIntelligence(4, 5, this.players[2].Chips, ref this.isBot2Turn, ref this.B2Fturn, this.bot2Status, 1, this.bot2Power, this.bot2Type);
                //        this.ArtificialIntelligence(4, 5, this.players[2], 1);
                //        this.turnCount++;
                //        this.last = 2;
                //        this.isBot2Turn = false;
                //        this.isBot3Turn = true;
                //    }
                //}

                //if (this.B2Fturn && !this.bot2Folded)
                //{
                //    this.bools.RemoveAt(2);
                //    this.bools.Insert(2, null);
                //    this.maxLeft--;
                //    this.bot2Folded = true;
                //}

                //if (this.B2Fturn || !this.isBot2Turn)
                //{
                //    await this.CheckRaise(2, 2);
                //    this.isBot3Turn = true;
                //}

                ////Bot3 Turn
                //if (!this.B3Fturn)
                //{
                //    if (this.isBot3Turn)
                //    {
                //        this.FixCall(this.bot3Status, ref this.bot3Call, ref this.bot3Raise, 1);
                //        this.FixCall(this.bot3Status, ref this.bot3Call, ref this.bot3Raise, 2);
                //        this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.B3Fturn);
                //        MessageBox.Show("Bot 3's Turn");
                //        //this.ArtificialIntelligence(6, 7, this.players[3].Chips, ref this.isBot3Turn, ref this.B3Fturn, this.bot3Status, 2, this.bot3Power, this.bot3Type);
                //        this.ArtificialIntelligence(6, 7, this.players[3], 2);
                //        this.turnCount++;
                //        this.last = 3;
                //        this.isBot3Turn = false;
                //        this.isBot4Turn = true;
                //    }
                //}

                //if (this.B3Fturn && !this.bot3Folded)
                //{
                //    this.bools.RemoveAt(3);
                //    this.bools.Insert(3, null);
                //    this.maxLeft--;
                //    this.bot3Folded = true;
                //}

                //if (this.B3Fturn || !this.isBot3Turn)
                //{
                //    await this.CheckRaise(3, 3);
                //    this.isBot4Turn = true;
                //}

                ////Bot4 Turn
                //if (!this.B4Fturn)
                //{
                //    if (this.isBot4Turn)
                //    {
                //        this.FixCall(this.bot4Status, ref this.bot4Call, ref this.bot4Raise, 1);
                //        this.FixCall(this.bot4Status, ref this.bot4Call, ref this.bot4Raise, 2);
                //        this.Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.B4Fturn);
                //        MessageBox.Show("Bot 4's Turn");
                //        //this.ArtificialIntelligence(8, 9, this.players[4].Chips, ref this.isBot4Turn, ref this.B4Fturn, this.bot4Status, 3, this.bot4Power, this.bot4Type);
                //        this.ArtificialIntelligence(8, 9, this.players[4], 3);
                //        this.turnCount++;
                //        this.last = 4;
                //        this.isBot4Turn = false;
                //        this.isBot5Turn = true;
                //    }
                //}

                //if (this.B4Fturn && !this.bot4Folded)
                //{
                //    this.bools.RemoveAt(4);
                //    this.bools.Insert(4, null);
                //    this.maxLeft--;
                //    this.bot4Folded = true;
                //}

                //if (this.B4Fturn || !this.isBot4Turn)
                //{
                //    await this.CheckRaise(4, 4);
                //    this.isBot5Turn = true;
                //}

                ////Bot5 Turn
                //if (!this.B5Fturn)
                //{
                //    if (this.isBot5Turn)
                //    {
                //        this.FixCall(this.bot5Status, ref this.bot5Call, ref this.bot5Raise, 1);
                //        this.FixCall(this.bot5Status, ref this.bot5Call, ref this.bot5Raise, 2);
                //        this.Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.B5Fturn);
                //        MessageBox.Show("Bot 5's Turn");
                //        //this.ArtificialIntelligence(10, 11, this.players[5].Chips, ref this.isBot5Turn, ref this.B5Fturn, this.bot5Status, 4, this.bot5Power, this.bot5Type);
                //        this.ArtificialIntelligence(10, 11, this.players[5], 4);
                //        this.turnCount++;
                //        this.last = 5;
                //        this.isBot5Turn = false;
                //    }
                //}

                //if (this.B5Fturn && !this.bot5Folded)
                //{
                //    this.bools.RemoveAt(5);
                //    this.bools.Insert(5, null);
                //    this.maxLeft--;
                //    this.bot5Folded = true;
                //}

                //if (this.B5Fturn || !this.isBot5Turn)
                //{
                //    await this.CheckRaise(5, 5);
                //    this.isPlayerTurn = true;
                //}

                //if (this.PFturn && !this.playerFolded)
                //{
                //    if (this.buttonCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                //    {
                //        this.bools.RemoveAt(0);
                //        this.bools.Insert(0, null);
                //        this.maxLeft--;
                //        this.playerFolded = true;
                //    }
                //}

                #endregion

                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }

                this.restart = false;
            }
        }

        private async void RotateTurns(IPlayer botPlayer, int card1, int card2, int index)
        {
            if (!botPlayer.FTurn)
            {
                if (botPlayer.Turn)
                {
                    this.FixCall(botPlayer, 1);
                    this.FixCall(botPlayer, 2);
                    this.Rules(card1, card2, botPlayer.Name,ref botPlayer.Type, ref botPlayer.Power, botPlayer.FTurn);
                    MessageBox.Show(botPlayer.Name + "'s Turn");
                    //ArtificialIntelligence(2, 3, this.players[1].Chips, ref this.isBot1Turn, ref this.B1Fturn, this.bot1Status, 0, this.bot1Power, this.bot1Type);
                    this.ArtificialIntelligence(card1, card2, botPlayer);
                    this.turnCount++;
                    this.last = index;
                    //this.isBot1Turn = false;
                    //this.isBot2Turn = true;
                }
            }

            if (botPlayer.FTurn && !botPlayer.Folded)
            {
                this.bools.RemoveAt(index);
                this.bools.Insert(index, null);
                this.maxLeft--;
                botPlayer.Folded = true;
            }

            if (botPlayer.FTurn || !botPlayer.Turn)
            {
                await this.CheckRaise(index, index);
                //this.isBot2Turn = true;
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
                        this.players[0].Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxChips.Text = this.players[0].Chips.ToString();
                        ////pPanel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.players[1].Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotOneChips.Text = this.players[1].Chips.ToString();
                        ////b1Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.players[2].Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotTwoChips.Text = this.players[2].Chips.ToString();
                        ////b2Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.players[3].Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotThreeChips.Text = this.players[3].Chips.ToString();
                        ////b3Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.players[4].Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotFourChips.Text = this.players[4].Chips.ToString();
                        ////b4Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.players[5].Chips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotFiveChips.Text = this.players[5].Chips.ToString();
                        ////b5Panel.Visible = true;
                    }
                    ////await Finish(1);
                }
                if (this.winners == 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.players[0].Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////pPanel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.players[1].Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b1Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.players[2].Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b2Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.players[3].Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b3Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.players[4].Chips += int.Parse(this.textBoxPot.Text);
                        ////await Finish(1);
                        ////b4Panel.Visible = true;
                    }
                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.players[5].Chips += int.Parse(this.textBoxPot.Text);
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
                this.Winner(this.playerType, this.playerPower, "Player", this.players[0].Chips, fixedLast);
                this.Winner(this.bot1Type, this.bot1Power, "Bot 1", this.players[1].Chips, fixedLast);
                this.Winner(this.bot2Type, this.bot2Power, "Bot 2", this.players[2].Chips, fixedLast);
                this.Winner(this.bot3Type, this.bot3Power, "Bot 3", this.players[3].Chips, fixedLast);
                this.Winner(this.bot4Type, this.bot4Power, "Bot 4", this.players[4].Chips, fixedLast);
                this.Winner(this.bot5Type, this.bot5Power, "Bot 5", this.players[5].Chips, fixedLast);
                this.restart = true;
                this.isPlayerTurn = true;
                this.PFturn = false;
                this.B1Fturn = false;
                this.B2Fturn = false;
                this.B3Fturn = false;
                this.B4Fturn = false;
                this.B5Fturn = false;
                if (this.players[0].Chips <= 0)
                {
                    var f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.players[0].Chips = f2.a;
                        this.players[1].Chips += f2.a;
                        this.players[2].Chips += f2.a;
                        this.players[3].Chips += f2.a;
                        this.players[4].Chips += f2.a;
                        this.players[5].Chips += f2.a;
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

        private void FixCall(IPlayer botPlayer, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (botPlayer.Label.Text.Contains("Raise"))
                    {
                        var changeRaise = botPlayer.Label.Text.Substring(6);
                        botPlayer.Raise = int.Parse(changeRaise);
                    }
                    if (botPlayer.Label.Text.Contains("Call"))
                    {
                        var changeCall = botPlayer.Label.Text.Substring(5);
                        botPlayer.Call = int.Parse(changeCall);
                    }
                    if (botPlayer.Label.Text.Contains("Check"))
                    {
                        botPlayer.Raise = 0;
                        botPlayer.Call = 0;
                    }
                }
                if (options == 2)
                {
                    if (botPlayer.Raise != this.raise && botPlayer.Raise <= this.raise)
                    {
                        this.call = Convert.ToInt32(this.raise) - botPlayer.Raise;
                    }
                    if (botPlayer.Call != this.call || botPlayer.Call <= this.call)
                    {
                        this.call = this.call - botPlayer.Call;
                    }
                    if (botPlayer.Raise == this.raise && this.raise > 0)
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

            if (this.players[0].Chips <= 0 && !this.intsadded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.Pot.Add(this.players[0].Chips);
                    this.intsadded = true;
                }
                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.Pot.Add(this.players[0].Chips);
                    this.intsadded = true;
                }
            }
            this.intsadded = false;
            if (this.players[1].Chips <= 0 && !this.B1Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.players[1].Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.players[2].Chips <= 0 && !this.B2Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.players[2].Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.players[3].Chips <= 0 && !this.B3Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.players[3].Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.players[4].Chips <= 0 && !this.B4Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.players[4].Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }
            if (this.players[5].Chips <= 0 && !this.B5Fturn)
            {
                if (!this.intsadded)
                {
                    this.Pot.Add(this.players[5].Chips);
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
                    this.players[0].Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.players[0].Chips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    this.players[1].Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.players[1].Chips.ToString();
                    this.bot1Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    this.players[2].Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.players[2].Chips.ToString();
                    this.bot2Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    this.players[3].Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.players[3].Chips.ToString();
                    this.bot3Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    this.players[4].Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.players[4].Chips.ToString();
                    this.bot4Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    this.players[5].Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.players[5].Chips.ToString();
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
            if (this.players[0].Chips <= 0)
            {
                var f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.players[0].Chips = f2.a;
                    this.players[1].Chips += f2.a;
                    this.players[2].Chips += f2.a;
                    this.players[3].Chips += f2.a;
                    this.players[4].Chips += f2.a;
                    this.players[5].Chips += f2.a;
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
            var fixedLast = string.Empty;
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
            this.Winner(this.playerType, this.playerPower, "Player", this.players[0].Chips, fixedLast);
            this.Winner(this.bot1Type, this.bot1Power, "Bot 1", this.players[1].Chips, fixedLast);
            this.Winner(this.bot2Type, this.bot2Power, "Bot 2", this.players[2].Chips, fixedLast);
            this.Winner(this.bot3Type, this.bot3Power, "Bot 3", this.players[3].Chips, fixedLast);
            this.Winner(this.bot4Type, this.bot4Power, "Bot 4", this.players[4].Chips, fixedLast);
            this.Winner(this.bot5Type, this.bot5Power, "Bot 5", this.players[5].Chips, fixedLast);
        }
        // ----------------------------------------------------------------------
        private void ArtificialIntelligence(
        //int c1, 
        //int c2, 
        ////ref int botChips,
        //int botChips,
        //ref bool botTurn,
        //ref bool botPlayer.FTurn,
        //Label botStatus,
        //,
        //double botPower,
        //double botPlayer.PokerHandMultiplier)

        int c1,
        int c2,
        IPlayer botPlayer
        //
        )
        {
            // TODO: make it with switch case.
            if (!botPlayer.FTurn)
            {
                if (botPlayer.PokerHandMultiplier == -1)
                {
                    //HighCard(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.HighCard(botPlayer);
                }
                //Switch bro
                if (botPlayer.PokerHandMultiplier == 0)
                {
                    //PairTable(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.PairTable(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 1)
                {
                    //PairHand(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.PairHand(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 2)
                {
                    //TwoPair(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.TwoPair(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 3)
                {
                    //ThreeOfAKind(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.ThreeOfAKind(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 4)
                {
                    //Straight(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Straight(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 5 || botPlayer.PokerHandMultiplier == 5.5)
                {
                    //Flush(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Flush(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 6)
                {
                    //FullHouse(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.FullHouse(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 7)
                {
                    //FourOfAKind(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.FourOfAKind(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 8 || botPlayer.PokerHandMultiplier == 9)
                {
                    //StraightFlush(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.StraightFlush(botPlayer);

                }
            }
            if (botPlayer.FTurn)
            {
                this.Holder[c1].Visible = false;
                this.Holder[c2].Visible = false;
            }
        }

        // Moved to class CombinationManager (renamed there). 
        // TODO: Implement logic
        #region CombinationManager
        private void HighCard(IPlayer botPlayer)
        {
            //this.HP(ref botChips, ref botTurn, ref botFTurn, botStatus, botPower, 20, 25);
            this.HP(botPlayer, 20, 25);
        }

        private void PairTable(IPlayer botPlayer)
        {
            //this.HP(ref botChips, ref botTurn, ref botFTurn, botStatus, botPower, 16, 25);
            this.HP(botPlayer, 16, 25);
        }

        //private void PairHand(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        private void PairHand(IPlayer botPlayer)
        {
            var pairHand = new Random();
            var randomCall = pairHand.Next(10, 16);
            var randomRaise = pairHand.Next(10, 13);

            if (botPlayer.Power <= 199 && botPlayer.Power >= 140)
            {
                //this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 6, randomRaise);
                this.PH(botPlayer, randomCall, 6, randomRaise);
            }

            if (botPlayer.Power <= 139 && botPlayer.Power >= 128)
            {
                //this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 7, randomRaise);
                this.PH(botPlayer, randomCall, 7, randomRaise);
            }

            if (botPlayer.Power < 128 && botPlayer.Power >= 101)
            {
                //this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 9, randomRaise);
                this.PH(botPlayer, randomCall, 9, randomRaise);
            }
        }

        private void TwoPair(IPlayer botPlayer)
        {
            var twoPair = new Random();
            var randomCall = twoPair.Next(6, 11);
            var randomRaise = twoPair.Next(6, 11);

            if (botPlayer.Power <= 290 && botPlayer.Power >= 246)
            {
                //this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 3, randomRaise);
                this.PH(botPlayer, randomCall, 3, randomRaise);
            }

            if (botPlayer.Power <= 244 && botPlayer.Power >= 234)
            {
                //this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 4, randomRaise);
                this.PH(botPlayer, randomCall, 4, randomRaise);
            }

            if (botPlayer.Power < 234 && botPlayer.Power >= 201)
            {
                //this.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, randomCall, 4, randomRaise);
                this.PH(botPlayer, randomCall, 4, randomRaise);
            }
        }

        private void ThreeOfAKind(IPlayer botPlayer)
        {
            var tk = new Random();
            var tCall = tk.Next(3, 7);
            var tRaise = tk.Next(4, 8);
            if (botPlayer.Power <= 390 && botPlayer.Power >= 330)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, tCall, tRaise);
                this.Smooth(botPlayer, tCall, tRaise);
            }
            if (botPlayer.Power <= 327 && botPlayer.Power >= 321) ////10  8
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, tCall, tRaise);
                this.Smooth(botPlayer, tCall, tRaise);
            }
            if (botPlayer.Power < 321 && botPlayer.Power >= 303) ////7 2
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, tCall, tRaise);
                this.Smooth(botPlayer, tCall, tRaise);
            }
        }

        private void Straight(IPlayer botPlayer)
        {
            var str = new Random();
            var sCall = str.Next(3, 6);
            var sRaise = str.Next(3, 8);
            if (botPlayer.Power <= 480 && botPlayer.Power >= 410)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, sCall, sRaise);
                this.Smooth(botPlayer, sCall, sRaise);
            }
            if (botPlayer.Power <= 409 && botPlayer.Power >= 407) ////10  8
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, sCall, sRaise);
                this.Smooth(botPlayer, sCall, sRaise);
            }
            if (botPlayer.Power < 407 && botPlayer.Power >= 404)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, sCall, sRaise);
                this.Smooth(botPlayer, sCall, sRaise);
            }
        }

        private void Flush(IPlayer botPlayer)
        {
            var fsh = new Random();
            var fCall = fsh.Next(2, 6);
            var fRaise = fsh.Next(3, 7);
            this.Smooth(botPlayer, fCall, fRaise);
        }

        private void FullHouse(IPlayer botPlayer)
        {
            var flh = new Random();
            var fhCall = flh.Next(1, 5);
            var fhRaise = flh.Next(2, 6);
            if (botPlayer.Power <= 626 && botPlayer.Power >= 620)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, fhCall, fhRaise);
                this.Smooth(botPlayer, fhCall, fhRaise);
            }
            if (botPlayer.Power < 620 && botPlayer.Power >= 602)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, fhCall, fhRaise);
                this.Smooth(botPlayer, fhCall, fhRaise);
            }
        }

        private void FourOfAKind(IPlayer botPlayer)
        {
            var fk = new Random();
            var fkCall = fk.Next(1, 4);
            var fkRaise = fk.Next(2, 5);
            if (botPlayer.Power <= 752 && botPlayer.Power >= 704)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, fkCall, fkRaise);
                this.Smooth(botPlayer, fkCall, fkRaise);
            }
        }

        private void StraightFlush(IPlayer botPlayer)
        {
            var sf = new Random();
            var sfCall = sf.Next(1, 3);
            var sfRaise = sf.Next(1, 3);
            if (botPlayer.Power <= 913 && botPlayer.Power >= 804)
            {
                //this.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, sfCall, sfRaise);
                this.Smooth(botPlayer, sfCall, sfRaise);
            }
        }
        #endregion CombinationManager
        //-----------------------------------------------------------------------------------------
        // TODO: Add to namespace Statuses.
        private void Fold(bool sTurn, bool sFTurn, Label sStatus)
        {
            this.raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        //private void Check(ref bool cTurn, Label cStatus)
        private void Check(bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            this.raising = false;
        }

        private void Call(int sChips, bool sTurn, Label sStatus)
        {
            this.raising = false;
            sTurn = false;
            sChips -= this.call;
            sStatus.Text = "Call " + this.call;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
        }

        private void Raised(IPlayer bot, bool sTurn, Label sStatus)
        {
            bot.Chips -= Convert.ToInt32(this.raise);
            //sChips -= Convert.ToInt32(this.raise);
            bot.Label.Text = "Raise " + this.raise;
            //sStatus.Text = "Raise " + this.raise;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
            this.call = Convert.ToInt32(this.raise);
            this.raising = true;
            //sTurn = false;
            bot.Turn = false;
        }

        //private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        //{
        //    sChips -= Convert.ToInt32(this.raise);
        //    sStatus.Text = "Raise " + this.raise;
        //    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
        //    this.call = Convert.ToInt32(this.raise);
        //    this.raising = true;
        //    sTurn = false;
        //}

        private static double RoundN(int sChips, int n)
        {
            var a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        // make static
        //public void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        public void HP(IPlayer botPlayer, int n, int n1)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 4);
            if (this.call <= 0)
            {
                this.Check(botPlayer.Turn, botPlayer.Label);
            }
            if (this.call > 0)
            {
                if (rnd == 1)
                {
                    if (this.call <= RoundN(botPlayer.Chips, n))
                    {
                        this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                    }
                    else
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                }
                if (rnd == 2)
                {
                    if (this.call <= RoundN(botPlayer.Chips, n1))
                    {
                        this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                    }
                    else
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                }
            }
            if (rnd == 3)
            {
                if (this.raise == 0)
                {
                    this.raise = this.call * 2;
                    this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                }
                else
                {
                    if (this.raise <= RoundN(botPlayer.Chips, n))
                    {
                        this.raise = this.call * 2;
                        this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                    }
                    else
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                }
            }
            if (botPlayer.Chips <= 0)
            {
                botPlayer.FTurn = true;
            }
        }

        // make static
        //public void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int randomCall, int n1, int randomRaise)
        public void PH(IPlayer botPlayer, int randomCall, int n1, int randomRaise)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.call <= 0)
                {
                    this.Check(botPlayer.Turn, botPlayer.Label);
                }
                if (this.call > 0)
                {
                    if (this.call >= RoundN(botPlayer.Chips, n1))
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                    if (this.raise > RoundN(botPlayer.Chips, randomCall))
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                    if (!botPlayer.FTurn)
                    {
                        if (this.call >= RoundN(botPlayer.Chips, randomCall) && this.call <= RoundN(botPlayer.Chips, n1))
                        {
                            this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                        }
                        if (this.raise <= RoundN(botPlayer.Chips, randomCall) && this.raise >= (RoundN(botPlayer.Chips, randomCall)) / 2)
                        {
                            this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                        }
                        if (this.raise <= (RoundN(botPlayer.Chips, randomCall)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(botPlayer.Chips, randomCall);
                                this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                            }
                        }
                    }
                }
            }
            if (this.rounds >= 2)
            {
                if (this.call > 0)
                {
                    if (this.call >= RoundN(botPlayer.Chips, n1 - rnd))
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                    if (this.raise > RoundN(botPlayer.Chips, randomCall - rnd))
                    {
                        this.Fold(botPlayer.Turn, botPlayer.FTurn, botPlayer.Label);
                    }
                    if (!botPlayer.FTurn)
                    {
                        if (this.call >= RoundN(botPlayer.Chips, randomCall - rnd) && this.call <= RoundN(botPlayer.Chips, n1 - rnd))
                        {
                            this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                        }
                        if (this.raise <= RoundN(botPlayer.Chips, randomCall - rnd) && this.raise >= (RoundN(botPlayer.Chips, randomCall - rnd)) / 2)
                        {
                            this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                        }
                        if (this.raise <= (RoundN(botPlayer.Chips, randomCall - rnd)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(botPlayer.Chips, randomCall - rnd);
                                this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                            }
                        }
                    }
                }
                if (this.call <= 0)
                {
                    this.raise = RoundN(botPlayer.Chips, randomRaise - rnd);
                    this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                }
            }
            if (botPlayer.Chips <= 0)
            {
                botPlayer.FTurn = true;
            }
        }

        // make static
        //public void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, , int n, int r)
        //public void Smooth(IPlayer botPlayer, , int call, int raise)
        public void Smooth(IPlayer botPlayer, int call, int raise)
        {
            var rand = new Random();
            var rnd = rand.Next(1, 3);
            if (this.call <= 0)
            {
                this.Check(botPlayer.Turn, botPlayer.Label);
            }
            else
            {
                if (this.call >= RoundN(botPlayer.Chips, call))
                {
                    if (botPlayer.Chips > this.call)
                    {
                        this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                    }
                    else if (botPlayer.Chips <= this.call)
                    {
                        this.raising = false;
                        botPlayer.Turn = false;
                        botPlayer.Chips = 0;
                        botPlayer.Label.Text = "Call " + botPlayer.Chips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + botPlayer.Chips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (botPlayer.Chips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                        }
                        else
                        {
                            this.Call(botPlayer.Chips, botPlayer.Turn, botPlayer.Label);
                        }
                    }
                    else
                    {
                        this.raise = this.call * 2;
                        this.Raised(botPlayer, botPlayer.Turn, botPlayer.Label);
                    }
                }
            }
            if (botPlayer.Chips <= 0)
            {
                botPlayer.FTurn = true;
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
            if (this.players[0].Chips <= 0)
            {
                this.textBoxChips.Text = "Chips : 0";
            }
            if (this.players[1].Chips <= 0)
            {
                this.textBoxBotOneChips.Text = "Chips : 0";
            }
            if (this.players[2].Chips <= 0)
            {
                this.textBoxBotTwoChips.Text = "Chips : 0";
            }
            if (this.players[3].Chips <= 0)
            {
                this.textBoxBotThreeChips.Text = "Chips : 0";
            }
            if (this.players[4].Chips <= 0)
            {
                this.textBoxBotFourChips.Text = "Chips : 0";
            }
            if (this.players[5].Chips <= 0)
            {
                this.textBoxBotFiveChips.Text = "Chips : 0";
            }
            this.textBoxChips.Text = "Chips : " + this.players[0].Chips;
            this.textBoxBotOneChips.Text = "Chips : " + this.players[1].Chips;
            this.textBoxBotTwoChips.Text = "Chips : " + this.players[2].Chips;
            this.textBoxBotThreeChips.Text = "Chips : " + this.players[3].Chips;
            this.textBoxBotFourChips.Text = "Chips : " + this.players[4].Chips;
            this.textBoxBotFiveChips.Text = "Chips : " + this.players[5].Chips;
            if (this.players[0].Chips <= 0)
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
            if (this.players[0].Chips >= this.call)
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
            if (this.players[0].Chips <= 0)
            {
                this.bRaise.Enabled = false;
            }
            int parsedValue;

            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.players[0].Chips <= int.Parse(this.tbRaise.Text))
                {
                    this.bRaise.Text = "All in";
                }
                else
                {
                    this.bRaise.Text = "Raise";
                }
            }
            if (this.players[0].Chips < this.call)
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
            if (this.players[0].Chips >= this.call)
            {
                this.players[0].Chips -= this.call;
                this.textBoxChips.Text = "Chips : " + this.players[0].Chips;
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
            else if (this.players[0].Chips <= this.call && this.call > 0)
            {
                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.players[0].Chips).ToString();
                this.playerStatus.Text = "All in " + this.players[0].Chips;
                this.players[0].Chips = 0;
                this.textBoxChips.Text = "Chips : " + this.players[0].Chips;
                this.isPlayerTurn = false;
                this.buttonFold.Enabled = false;
                this.playerCall = this.players[0].Chips;
            }
            await this.Turns();
        }

        private async void ButtonRaiseClick(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            int parsedValue;
            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.players[0].Chips > this.call)
                {
                    if (this.raise * 2 > int.Parse(this.tbRaise.Text))
                    {
                        this.tbRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    if (this.players[0].Chips >= int.Parse(this.tbRaise.Text))
                    {
                        this.call = int.Parse(this.tbRaise.Text);
                        this.raise = int.Parse(this.tbRaise.Text);
                        this.playerStatus.Text = "Raise " + this.call;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                        this.buttonCall.Text = "Call";
                        this.players[0].Chips -= int.Parse(this.tbRaise.Text);
                        this.raising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.raise);
                    }
                    else
                    {
                        this.call = this.players[0].Chips;
                        this.raise = this.players[0].Chips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.players[0].Chips).ToString();
                        this.playerStatus.Text = "Raise " + this.call;
                        this.players[0].Chips = 0;
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
                this.players[0].Chips += int.Parse(this.textBoxAdd.Text);
                this.players[1].Chips += int.Parse(this.textBoxAdd.Text);
                this.players[2].Chips += int.Parse(this.textBoxAdd.Text);
                this.players[3].Chips += int.Parse(this.textBoxAdd.Text);
                this.players[4].Chips += int.Parse(this.textBoxAdd.Text);
                this.players[5].Chips += int.Parse(this.textBoxAdd.Text);
            }
            this.textBoxChips.Text = "Chips : " + this.players[0].Chips;
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