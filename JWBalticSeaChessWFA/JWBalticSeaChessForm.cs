using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Player.AI;
using JWBalticSeaChessLibrary.Player.Human;
using System.Reflection;

namespace JWBalticSeaChessWFA
{
    public partial class JWBalticSeaChessForm : Form
    {
        public bool LockUserInput { get; set; } = false;
        protected JWBalticSeaChessWFGame game = null;

        public JWBalticSeaChessForm()
        {
            InitializeComponent();
        }

        // reset-methods
        public void Reset()
        {
            LockUserInput = true;

            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                canvasPanel,
                new object[] { true }
            );

            playerOneNameLabel.Text = "?";
            playerOnePointsLabel.Text = "";
            playerTwoNameLabel.Text = "?";
            playerTwoPointsLabel.Text = "";

            playerOneToolStripComboBox.Items.Add("Human");
            playerOneToolStripComboBox.Items.Add("Random");
            playerOneToolStripComboBox.Items.Add("Advanced");
            playerOneToolStripComboBox.SelectedIndex = 0;

            playerTwoToolStripComboBox.Items.Add("Human");
            playerTwoToolStripComboBox.Items.Add("Random");
            playerTwoToolStripComboBox.Items.Add("Advanced");
            playerTwoToolStripComboBox.SelectedIndex = 1;

            //movesListView.Enabled = false;

            LockUserInput = false;

            JWBSActiveGame currentgame = new JWBSActiveGame();
            currentgame.AddPlayer(new JWBSRandomAIPlayer("Me"));
            currentgame.AddPlayer(new JWBSRandomAIPlayer("Rudi"));
            currentgame.Prepare();
            currentgame.Start();

            game = new JWBalticSeaChessWFGame(
                currentgame,
                new Label[] { playerOneNameLabel, playerTwoNameLabel },
                new Label[] { playerOnePointsLabel, playerTwoPointsLabel },
                canvasPanel,
                movesListView
            );
            game.Init();
            game.UpdateCanvasSize();

            playerOneToolStripComboBox.Enabled = true;
            playerTwoToolStripComboBox.Enabled = true;

            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
        }

        // event-methods
        private void PanelCanvasPaint(object sender, PaintEventArgs e)
        {
            game.UpdateCanvas();
        }

        private void CanvasPanelResize(object sender, EventArgs e)
        {
            game.UpdateCanvasSize();
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            game.UpdateCursor(new Tuple<int, int>(point.X, point.Y));
        }

        private void CanvasPanel_MouseClick(object sender, MouseEventArgs e)
        {
            game.UpdateSelection();
        }

        // event-file-methods
        private void FileNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void FileQuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // event-edit-methods
        private void PlayerOneToolStripComboBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PlayerTwoToolStripComboBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Start();
            playerOneToolStripComboBox.Enabled = false;
            playerTwoToolStripComboBox.Enabled = false;

            undoToolStripMenuItem.Enabled = true;
            undoToolStripMenuItem.Enabled = true;
        }

        private void EditUndoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void EditRedoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void EditCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // event-view-methods
        private void OrientationToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            if (!LockUserInput)
            {
                LockUserInput = true;

                if (Orientation.TOP.ToString().Trim().ToLower() == orientationToolStripComboBox.Text)
                {
                    game.ViewOrientation = Orientation.TOP;
                    orientationToolStripComboBox.Text = game.ViewOrientation.ToString().ToLower();
                }
                else if (Orientation.RIGHT.ToString().Trim().ToLower() == orientationToolStripComboBox.Text)
                {
                    game.ViewOrientation = Orientation.RIGHT;
                    orientationToolStripComboBox.Text = game.ViewOrientation.ToString().ToLower();
                }
                else if (Orientation.BOTTOM.ToString().Trim().ToLower() == orientationToolStripComboBox.Text)
                {
                    game.ViewOrientation = Orientation.BOTTOM;
                    orientationToolStripComboBox.Text = game.ViewOrientation.ToString().ToLower();
                }
                else if (Orientation.LEFT.ToString().Trim().ToLower() == orientationToolStripComboBox.Text)
                {
                    game.ViewOrientation = Orientation.LEFT;
                    orientationToolStripComboBox.Text = game.ViewOrientation.ToString().ToLower();
                }
                else
                {

                }

                LockUserInput = false;
            }
        }

        private void EyesToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            if (!LockUserInput)
            {
                LockUserInput = true;

                if (eyesToolStripComboBox.Text.Trim().ToLower() == "true")
                {
                    game.ViewEyes = true;
                    eyesToolStripComboBox.Text = "true";
                    game.UpdateCanvasSize();
                }
                else if (eyesToolStripComboBox.Text.Trim().ToLower() == "false")
                {
                    game.ViewEyes = false;
                    eyesToolStripComboBox.Text = "false";
                    game.UpdateCanvasSize();
                }
                else
                {

                }

                LockUserInput = false;
            }
        }

        // event-settings-methods
        private void SettingAutomaticAIsStripComboBox_TextChanged(object sender, EventArgs e)
        {
            if (!LockUserInput)
            {
                LockUserInput = true;
                game.AIPlayerStepping = (settingAutomaticAIsStripComboBox.Text != "true");
                LockUserInput = false;
            }
        }

        // event-info-methods
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Baltic Sea Chess - Rules:\n\n" +
                "You win the game by scoring the most points within 60 moves.\n" +
                "You get a point when\n" +
                "1. getting a SHELL, SEAGULL or STARFISH to the opponents baseline or \n" +
                "2. building a tower more than 2 pieces high.\n" +
                "Towers in general are created by jumping onto an opponents piece.\n" +
                "A tower belongs to the player of the top piece and can be moved\n" +
                "like any other piece.\n" +
                "Any point winning piece is removed from the game.\n\n" +
                "SHELL - symbol v - 2 possible moves - light piece:\n" +
                "    Can move to its front left or right fields.\n" +
                "SEAGULL - symbol + - 4 possible moves - light piece:\n" +
                "    Can move to every edge connected neighbour field.\n" +
                "SHELL - symbol * - 5 possible moves - light piece:\n" +
                "    Can move to every corner connected neighbour and its front field.\n" +
                "SEAL - symbol L - 8 possible moves - strong piece:\n" +
                "    Can move 2 steps top/right/bottom/left followed by 2 steps in any\n" +
                "    90° angled direction like an L-shape."
            );
        }

        // event-form-methods
        private void JWBalticSeaChessForm_Load(object sender, EventArgs e)
        {
            LockUserInput = true;

            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                canvasPanel,
                new object[] { true }
            );

            playerOneNameLabel.Text = "?";
            playerOnePointsLabel.Text = "";
            playerTwoNameLabel.Text = "?";
            playerTwoPointsLabel.Text = "";

            playerOneToolStripComboBox.Items.Add("Human");
            playerOneToolStripComboBox.Items.Add("Random");
            playerOneToolStripComboBox.Items.Add("Advanced");
            playerOneToolStripComboBox.SelectedIndex = 0;

            playerTwoToolStripComboBox.Items.Add("Human");
            playerTwoToolStripComboBox.Items.Add("Random");
            playerTwoToolStripComboBox.Items.Add("Advanced");
            playerTwoToolStripComboBox.SelectedIndex = 1;

            //movesListView.Enabled = false;

            LockUserInput = false;

            JWBSActiveGame currentgame = new JWBSActiveGame();
            currentgame.AddPlayer(new JWBSRandomAIPlayer("Me"));
            currentgame.AddPlayer(new JWBSRandomAIPlayer("Rudi"));
            currentgame.Prepare();
            currentgame.Start();

            game = new JWBalticSeaChessWFGame(
                currentgame,
                new Label[] { playerOneNameLabel, playerTwoNameLabel },
                new Label[] { playerOnePointsLabel, playerTwoPointsLabel },
                canvasPanel,
                movesListView
            );
            game.Init();
            game.UpdateCanvasSize();
        }
    }
}