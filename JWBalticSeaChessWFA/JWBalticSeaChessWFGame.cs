using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Piece.Passive;
using JWBalticSeaChessLibrary.Player;
using JWBalticSeaChessLibrary.Player.AI;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace JWBalticSeaChessWFA
{
    /// <summary>
    /// JWBalticSeaChess-WinForm-Game contains all data for the GUI
    /// to draw the current state image, list the moves and update user 
    /// </summary>
    public class JWBalticSeaChessWFGame
    {
        public static readonly string ICON_SHEET_ORIGINAL_FILE_PATH = "D:\\users\\all\\source\\JWBalticSeaChess\\reduced\\baltic_see_chess_sheet_32x32_reduced.png";
        public static readonly string ICON_SHEET_FILE_PATH_EYELESS = "bsc_sheet_32x32_eyeless.png";
        public static readonly string ICON_SHEET_FILE_PATH = "bsc_sheet_32x32.png";
        public static readonly Color[] COLORS = new Color[]{
            Color.FromArgb(255, 100, 50),
            Color.FromArgb(50, 100, 255),
            Color.FromArgb(255, 200, 50),
            Color.FromArgb(255, 50, 200)
        };
        public static readonly PixelFormat BITMAP_PIXEL_FORMAT = PixelFormat.Format16bppRgb555;
        /// <summary>
        /// Original pixel size of square tiles.
        /// </summary>
        public static readonly int TILE_SIZE = 32;
        public static readonly int TOWER_TOP_SHIFT_FACTOR_NUMERATOR = 2;
        public static readonly int TOWER_TOP_SHIFT_FACTOR_DENOMINATOR = 5;
        public static readonly double NUMBERING_FRAME_SIZE_FACTOR = 0.5;
        public static readonly double NUMBERING_FRAME_FONT_SIZE_FACTOR = 0.6;
        /// <summary>
        /// <see cref="JWBSPieceType"/>
        /// </summary>
        public static readonly int PIECE_TYPES = 4;
        /// <summary>
        /// Minimal tile size. If _tileSize is lower it collapse to zero and the GUI will paint a default image.
        /// </summary>
        public static readonly int TILE_SIZE_MIN = 10;
        /// <summary>
        /// 
        /// </summary>
        public static readonly TimeSpan PLAYER_AI_RESPOND_TIME_MIN = new TimeSpan(0, 0, 1);
        public static readonly TimeSpan PLAYER_AI_RESPOND_TIME_MAX = new TimeSpan(0, 0, 15);

        #region components
        public Label[] PlayerNames { get; set; }
        public Label[] PlayerPoints { get; set; }
        public Panel Canvas { get; set; }
        public ListView MovesList { get; set; }
        private BackgroundWorker _updateAIPlayers;
        #endregion
        #region settings
        public bool AIPlayerStepping { get; set; } = false;
        private bool _updatingAIPlayers = false;
        #endregion
        #region canvas styles
        public Orientation ViewOrientation
        {
            get
            {
                return viewOrientation;
            }
            set
            {
                if (ViewOrientation != value)
                {
                    viewOrientation = value;
                    CalculateCanvasOrientationMeasures();
                }
            }
        }
        protected Orientation viewOrientation = Orientation.TOP;
        public bool ViewEyes { get; set; } = true;
        public Color BoardFrameColorBackground { get; set; } = Color.FromArgb(255, 139, 69, 18);
        public Color BoardFrameColorFont { get; set; } = Color.FromArgb(255, 255, 255, 255);
        public Color BoardFrameColorMark { get; set; } = Color.FromArgb(100, 255, 255, 255);
        public FontFamily BoardFrameFontFamily { get; set; } = new FontFamily("Courier New");
        public Color BoardColorBackground { get; set; } = Color.FromArgb(255, 0, 0, 0);

        private int _paddingMin = 10;
        private int _tileGap = 8;
        #endregion
        #region calculated canvas properties
        private int _tilesPerWidth = 0;
        private int _tilesPerHeight = 0;
        private int _tileSize = 0;
        private int _bufferNumberingFrameSize = 0;
        private int _bufferNumberingFrameFontSize = 0;
        private int _bufferNumberingFrameSizeShift = 0;
        private int _bufferNumberingFrameTileShift = 0;
        private int _bufferGapTileSize = 0;
        private int _towerTopShift = 0;
        private Rectangle _boardFrameEdges = new Rectangle(0, 0, 0, 0);
        private Rectangle _boardEdges = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// Numbering of frame per <see cref="Orientation"/>.
        /// </summary>
        private string[][] _bufferGeneralCanvasFrameNumberingTexts = null;
        private string[][] _bufferCanvasFrameNumberingTexts = null;

        private Tuple<int, int>? _chosenTile = null;
        private IJWBSPiece _chosenPiece = null;
        #endregion
        #region user selection
        private Tuple<int, int> _currentCanvasPosition = null;
        private Tuple<int, int> _currentBoardPosition = null;
        private Tuple<int, int> _selectedCanvasSourcePosition = null;
        private Tuple<int, int> _selectedBoardSourcePosition = null;
        private IJWBSPiece _preselectedPiece = null;
        private Tuple<int, int>[] _allowedCanvasTargetPositions = null;
        #endregion
        #region data
        private JWBSActiveGame _game = null;
        private Bitmap _boardBitmapBuffer = null;
        private Bitmap _marksBitmapBuffer = null;
        #endregion

        public JWBalticSeaChessWFGame(JWBSActiveGame game, Label[] playerNames, Label[] playerPoints, Panel canvas, ListView movesList)
        {
            _game = game;
            PlayerNames = playerNames;
            PlayerPoints = playerPoints;
            Canvas = canvas;
            MovesList = movesList;

            _updateAIPlayers = new BackgroundWorker();
            _updateAIPlayers.DoWork += new DoWorkEventHandler(UpdatingAIPlayers);
            _updateAIPlayers.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdatedAIPlayers);
        }

        // init-methods
        public void Init()
        {
            UpdateExpandedIconSheet(COLORS);
            Reset();
        }

        // static-get-methods
        public static bool AreEqualPositions(Tuple<int, int> a, Tuple<int, int> b)
        {
            return (
                (a == null && b == null) ||
                (a != null && b != null && a.Item1 == b.Item1 && a.Item2 == b.Item2)
            );
        }

        public static bool AreDifferentPositions(Tuple<int, int> a, Tuple<int, int> b)
        {
            return (
                (a == null && b != null) ||
                (b == null && a != null) ||
                (a != null && b != null && (a.Item1 != b.Item1 || a.Item2 != b.Item2))
            );
        }

        // methods
        public void Start()
        {
            _game.Start();
            UpdateAIPlayers();
        }

        // update-methods
        public void UpdateAIPlayers()
        {
            _updatingAIPlayers = true;
            _updateAIPlayers.RunWorkerAsync();
        }

        protected void UpdatingAIPlayers(object sender, DoWorkEventArgs e)
        {
            while (!AIPlayerStepping && _game.CurrentPlayer is IJWBSAIPlayer)
            {
                DateTime start = DateTime.Now;
                JWBSMove move = ((IJWBSAIPlayer)_game.CurrentPlayer).GetMove();
                move.Name = JWBSMove.GetMoveName(
                    _game.Board,
                    move.SourcePosition, _game.Board.GetPieceAt(move.SourcePosition),
                    move.TargetPosition, _game.Board.GetPieceAt(move.TargetPosition)
                );
                while(DateTime.Now - start < PLAYER_AI_RESPOND_TIME_MIN)
                {
                    // wait PLAYER_AI_RESPOND_TIME_MIN
                }
                AddMove(move);
                UpdateBoardBitmap();
            }
        }

        protected void UpdatedAIPlayers(object sender, RunWorkerCompletedEventArgs e)
        {
            _updatingAIPlayers = false;
        }

        public void UpdatePoints()
        {
            //PlayerPoints[(int)JWBSPlayerType.ONE].Text = _game.PlayerPoints[(int)JWBSPlayerType.ONE].Count.ToString();
            //PlayerPoints[(int)JWBSPlayerType.TWO].Text = _game.PlayerPoints[(int)JWBSPlayerType.TWO].Count.ToString();
        }

        /// <summary>
        /// Expand reduced sheet of a 32x32 grid 4x3
        ///     circle    empty       empty        empty
        ///     shell/top seagull/top starfish/top seal/top
        ///     eyes/top  eyes/top    eyes/top     eyes/top
        /// to sheets where every oriented colored piece exists of a 32x32 grid 16xn
        ///     shell/top seagull/top starfish/top seal/top shell/right seagull/right ...
        ///     ...
        /// </summary>
        /// <param name="colors">Color of every line.</param>
        public void UpdateExpandedIconSheet(params Color[] colors)
        {
            int PIECE_CIRCLE_Y = 0 * TILE_SIZE;
            int PIECE_TYPE_Y = 1 * TILE_SIZE;
            int PIECE_EYES_Y = 2 * TILE_SIZE;

            using (Bitmap sourceBitmap = (Bitmap)Bitmap.FromFile(ICON_SHEET_ORIGINAL_FILE_PATH))
            {
                using (Bitmap targetBitmap = new Bitmap(TILE_SIZE * PIECE_TYPES * 4, TILE_SIZE * colors.Length, PixelFormat.Format32bppArgb))
                {
                    void SetTargetBitmapAt(int x, int y, Color templateColor, Color playerColor)
                    {
                        if (templateColor.A != 0)
                        {
                            if (templateColor.R == 0 && templateColor.G == 0 && templateColor.B == 0)
                            {
                                targetBitmap.SetPixel(x, y, Color.White);
                            }
                            else
                            {
                                targetBitmap.SetPixel(x, y, Color.FromArgb(
                                    (int)Math.Floor((double)templateColor.A * playerColor.A / 255),
                                    (int)Math.Floor((double)templateColor.R * playerColor.R / 255),
                                    (int)Math.Floor((double)templateColor.G * playerColor.G / 255),
                                    (int)Math.Floor((double)templateColor.B * playerColor.B / 255)
                                ));
                            }
                        }
                    }
                    Color GetSourceColor(int sourceXBase, int sourceYBase, Orientation targetOrientation, int x, int y, int sourceWidth, int sourceHeight)
                    {
                        switch (targetOrientation)
                        {
                            case Orientation.RIGHT: return sourceBitmap.GetPixel(sourceXBase+y, sourceYBase+sourceWidth - 1 - x);
                            case Orientation.BOTTOM: return sourceBitmap.GetPixel(sourceXBase+sourceWidth - 1 - x, sourceYBase+sourceHeight - 1 - y);
                            case Orientation.LEFT: return sourceBitmap.GetPixel(sourceXBase + sourceHeight - 1 - y, sourceYBase + x);
                            default: return sourceBitmap.GetPixel(sourceXBase + x, sourceYBase + y);
                        }
                    }

                    int targetY = 0;
                    foreach (Color color in colors)
                    {
                        int targetX = 0;
                        for (int o = 0; o < 4; o++)
                        {
                            for (int t = 0; t < PIECE_TYPES; t++)
                            {
                                // draw circle
                                for (int i = 0; i < TILE_SIZE; i++)
                                {
                                    for (int j = 0; j < TILE_SIZE; j++)
                                    {
                                        SetTargetBitmapAt(targetX + j, targetY + i, sourceBitmap.GetPixel(j, PIECE_CIRCLE_Y + i), color);
                                    }
                                }

                                // map pixels and colorate icons
                                int sourceXBase = TILE_SIZE * t;
                                int sourceYBase = PIECE_TYPE_Y;
                                for (int i = 0; i < TILE_SIZE; i++)
                                {
                                    for (int j = 0; j < TILE_SIZE; j++)
                                    {
                                        SetTargetBitmapAt(targetX + j, targetY + i, GetSourceColor(sourceXBase, sourceYBase, (Orientation)o, j, i, TILE_SIZE, TILE_SIZE), color);
                                    }
                                }
                                targetX += TILE_SIZE;
                            }
                        }
                        targetY += TILE_SIZE;
                    }

                    targetBitmap.Save(ICON_SHEET_FILE_PATH_EYELESS);

                    targetY = 0;
                    foreach (Color color in colors)
                    {
                        int targetX = 0;
                        for (int o = 0; o < 4; o++)
                        {
                            for (int t = 0; t < PIECE_TYPES; t++)
                            {
                                // draw eyes
                                int sourceXBase = TILE_SIZE * t;
                                int sourceYBase = PIECE_EYES_Y;
                                for (int i = 0; i < TILE_SIZE; i++)
                                {
                                    for (int j = 0; j < TILE_SIZE; j++)
                                    {
                                        Color currentColor = GetSourceColor(sourceXBase, sourceYBase, (Orientation)o, j, i, TILE_SIZE, TILE_SIZE);
                                        if (currentColor.A != 0)
                                        {
                                            targetBitmap.SetPixel(targetX + j, targetY + i, currentColor);
                                        }
                                    }
                                }
                                targetX += TILE_SIZE;
                            }
                        }
                        targetY += TILE_SIZE;
                    }

                    targetBitmap.Save(ICON_SHEET_FILE_PATH);
                }
            }
        }

        public void UpdateCanvas()
        {
            Canvas.BackgroundImage = _marksBitmapBuffer;
            PlayerPoints[(int)JWBSPlayerType.ONE].Text = _game.PlayerPoints[(int)JWBSPlayerType.ONE].Count.ToString();
            PlayerPoints[(int)JWBSPlayerType.TWO].Text = _game.PlayerPoints[(int)JWBSPlayerType.TWO].Count.ToString();
        }

        public void UpdateCanvasSize()
        {
            CalculateCanvasMeasures();
        }

        public void UpdateSelection()
        {
            if (_game.CurrentPlayer is IJWBSAIPlayer)
            {
                if (AIPlayerStepping)
                {
                    AddMove(((IJWBSAIPlayer)_game.CurrentPlayer).GetMove());
                    UpdateBoardBitmap();
                }
            }
            else
            {
                if (HasSelectedPiece())
                {
                    if (_currentCanvasPosition == null)
                    {
                        ResetSelection();
                        UpdateMarksBitmap();
                    }
                    else
                    {
                        // check if selected target position is an allowed one
                        bool isAllowedPosition = false;
                        foreach (Tuple<int, int> allowedPosition in _allowedCanvasTargetPositions)
                        {
                            if (AreEqualPositions(_currentCanvasPosition, allowedPosition))
                            {
                                isAllowedPosition = true;
                                break;
                            }
                        }

                        // execute move
                        if (isAllowedPosition)
                        {
                            IJWBSPiece targetPiece = _game.Board.GetPieceAt(_currentBoardPosition);

                            AddMove(new JWBSMove(
                                _selectedCanvasSourcePosition,
                                _currentCanvasPosition,
                                JWBSMove.GetMoveName(_game.Board,
                                    _selectedBoardSourcePosition, _preselectedPiece,
                                    _currentBoardPosition, _game.Board.GetPieceAt(_currentBoardPosition)
                                )
                            ));
                        }

                        // reset selection and update _preselectedPiece and even select _currentCanvasPosition if no move was executed
                        ResetSelection();
                        if (!(_game.CurrentPlayerType is IJWBSAIPlayer))
                        {
                            UpdatePreselectedPiece();
                            if (!isAllowedPosition)
                            {
                                UpdateSelection();
                            }
                        }
                    }
                }
                else if (_preselectedPiece != null && _preselectedPiece.PlayerType == _game.CurrentPlayerType)
                {
                    _selectedCanvasSourcePosition = _currentCanvasPosition;
                    _selectedBoardSourcePosition = _currentBoardPosition;
                    UpdateMarksBitmap();
                }
            }
        }

        public void UpdateCursor(Tuple<int, int> position)
        {
            Tuple<int, int> currentCanvasPosition = null;
            // calculate currentTile
            if (
                HasVisibleBoard() && position != null &&
                _boardEdges.Left <= position.Item1 && position.Item1 <= _boardEdges.Right &&
                _boardEdges.Top <= position.Item2 && position.Item2 <= _boardEdges.Bottom
            )
            {
                currentCanvasPosition = new Tuple<int, int>(
                    (int)Math.Floor((double)(position.Item1 - _boardEdges.Left - _tileGap) / _bufferGapTileSize),
                    (int)Math.Floor((double)(position.Item2 - _boardEdges.Top - _tileGap) / _bufferGapTileSize)
                );
                if (
                    currentCanvasPosition.Item1 < 0 || currentCanvasPosition.Item1 >= _tilesPerWidth ||
                    currentCanvasPosition.Item2 < 0 || currentCanvasPosition.Item2 >= _tilesPerHeight ||
                    position.Item1 > _boardEdges.Left + _bufferGapTileSize * (currentCanvasPosition.Item1 + 1) ||
                    position.Item2 > _boardEdges.Top + _bufferGapTileSize * (currentCanvasPosition.Item2 + 1)
                )
                {
                    // inside tile range _bufferGapTileSize but inside first board gap or gap after tile content
                    currentCanvasPosition = null;
                }
                else
                {
                    // inside tile content
                }
            }
            else
            {
                // outside board
            }

            // update _currentTile if and only if it is not the same as before
            if (AreDifferentPositions(currentCanvasPosition, _currentCanvasPosition))
            {
                _currentCanvasPosition = currentCanvasPosition;
                _currentBoardPosition = GetBoardPositionFromCanvasPosition(_currentCanvasPosition);

                // change _preselectedPiece only when no selection is already done
                if (!HasSelectedPiece())
                {
                    UpdatePreselectedPiece();
                }
                UpdateMarksBitmap();
            }
        }

        protected void UpdatePreselectedPiece()
        {
            _preselectedPiece = (_currentCanvasPosition == null ? null : _game.Board.GetPieceAt(_currentBoardPosition));
            if (_preselectedPiece == null)
            {
                _allowedCanvasTargetPositions = null;
            }
            else
            {
                // buffer and calculate _allowedCanvasTargetPositions
                JWBSPassivePiece preselectedPassivePiece = (_preselectedPiece is JWBSPassivePiece ? (JWBSPassivePiece)_preselectedPiece : ((JWBSPassivePieceTower)_preselectedPiece).TopPiece);
                IList<Tuple<int, int>> allowedBoardTargetPosition = _game.Board.GetAllowedMoves(
                    _preselectedPiece,
                    preselectedPassivePiece.GetPossiblePositions(_currentBoardPosition.Item1, _currentBoardPosition.Item2)
                );
                _allowedCanvasTargetPositions = new Tuple<int, int>[allowedBoardTargetPosition.Count];
                for (int a = 0; a < _allowedCanvasTargetPositions.Length; a++)
                {
                    _allowedCanvasTargetPositions[a] = GetCanvasPositionFromBoardPosition(allowedBoardTargetPosition[a]);
                }
            }
        }

        public void UpdateBoardBitmap()
        {
            if (HasVisibleBoard())
            {
                Image PIECE_ICONS = Image.FromFile(ViewEyes ? ICON_SHEET_FILE_PATH : ICON_SHEET_FILE_PATH_EYELESS);
                Graphics iconGraphics = Graphics.FromImage(PIECE_ICONS);

                _boardBitmapBuffer = new Bitmap(Canvas.Width, Canvas.Height, BITMAP_PIXEL_FORMAT);
                Graphics graphics = Graphics.FromImage(_boardBitmapBuffer);
                graphics.Clear(Color.Green);

                Color BOARD_FRAME_BACKGROUND_COLOR = Color.SaddleBrown;
                Color BOARD_FRAME_NUMBERING_FONT_COLOR = Color.White;
                SolidBrush BOARD_FRAME_NUMBERING_FONT_SOLID_BRUSH = new SolidBrush(BoardFrameColorFont);
                Font BOARD_FRAME_NUMBERING_FONT = new Font(BoardFrameFontFamily, _bufferNumberingFrameFontSize, FontStyle.Regular, GraphicsUnit.Pixel);

                graphics.FillRectangle(new SolidBrush(BoardFrameColorBackground), _boardFrameEdges);
                graphics.FillRectangle(new SolidBrush(BoardColorBackground), _boardEdges);

                SolidBrush[] sbRedTile = new SolidBrush[]{
                    new SolidBrush(Color.FromArgb(255, 220, 220)),
                    new SolidBrush(Color.FromArgb(255, 120, 120))
                };
                SolidBrush[] sbBlueTile = new SolidBrush[]{
                    new SolidBrush(Color.FromArgb(220, 220, 255)),
                    new SolidBrush(Color.FromArgb(120, 120, 255))
                };
                SolidBrush[] sbTile = new SolidBrush[]{
                    new SolidBrush(Color.FromArgb(220, 255, 200)),
                    new SolidBrush(Color.FromArgb(120, 255, 200))
                };

                int bufferTileSizeMark = _bufferGapTileSize + _tileGap;
                void DrawFrameNumber(int x, int y, Orientation orientation, int index)
                {
                    graphics.DrawString(
                        _bufferCanvasFrameNumberingTexts[(int)orientation][index],
                        BOARD_FRAME_NUMBERING_FONT, BOARD_FRAME_NUMBERING_FONT_SOLID_BRUSH,
                        x, y
                    );
                }
                void DrawPiece(int targetCanvasTileX, int targetCanvasTileY, IJWBSPiece piece)
                {
                    int o = (((int)(piece.PlayerType == JWBSPlayerType.ONE ? Orientation.TOP : Orientation.BOTTOM)) + ((int)ViewOrientation)) % 4;
                    int iconSourceX = o * PIECE_TYPES * TILE_SIZE + TILE_SIZE * (int)piece.PieceType;
                    int iconSourceY = (piece.PlayerType == JWBSPlayerType.ONE ? 0 : TILE_SIZE);
                    graphics.DrawImage(
                        PIECE_ICONS,
                        new Rectangle(targetCanvasTileX, targetCanvasTileY, _tileSize, _tileSize),
                        iconSourceX, iconSourceY, TILE_SIZE, TILE_SIZE, GraphicsUnit.Pixel
                    );
                }

                int bufferCanvasBoardPositionX = _boardEdges.Left + _tileGap;

                int canvasPositionX = _boardEdges.Left + _tileGap;
                int canvasPositionY = _boardFrameEdges.Top + _bufferNumberingFrameSizeShift;
                //frame numbering
                for (int j = 0; j < _game.Board.Width; j++)
                {
                    DrawFrameNumber(canvasPositionX + _bufferNumberingFrameTileShift, canvasPositionY, Orientation.TOP, j);
                    canvasPositionX += _bufferGapTileSize;
                }
                canvasPositionY = _boardEdges.Top + _tileGap;
                for (int i = 0; i < _game.Board.Height; i++)
                {
                    //frame numbering
                    canvasPositionX = _boardFrameEdges.Left + _bufferNumberingFrameSizeShift;
                    DrawFrameNumber(canvasPositionX, canvasPositionY + _bufferNumberingFrameTileShift, Orientation.LEFT, i);

                    canvasPositionX = bufferCanvasBoardPositionX;
                    for (int j = 0; j < _game.Board.Width; j++)
                    {
                        Tuple<int, int> canvasPosition = new Tuple<int, int>(j, i);
                        Tuple<int, int> boardPosition = GetBoardPositionFromCanvasPosition(canvasPosition);

                        // draw tile
                        {
                            SolidBrush sb;
                            if (
                                (i == _game.Board.Height - 1 && ViewOrientation == Orientation.TOP) ||
                                (j == 0 && ViewOrientation == Orientation.RIGHT) ||
                                (i == 0 && ViewOrientation == Orientation.BOTTOM) ||
                                (j == _game.Board.Width - 1 && ViewOrientation == Orientation.LEFT)
                            )
                            {
                                sb = sbRedTile[(j + i) % 2];
                            }
                            else if (
                                (i == 0 && ViewOrientation == Orientation.TOP) ||
                                (j == _game.Board.Width - 1 && ViewOrientation == Orientation.RIGHT) ||
                                (i == _game.Board.Height - 1 && ViewOrientation == Orientation.BOTTOM) ||
                                (j == 0 && ViewOrientation == Orientation.LEFT)
                            )
                            {
                                sb = sbBlueTile[(j + i) % 2];
                            }
                            else
                            {
                                sb = sbTile[(j + i) % 2];
                            }
                            graphics.FillRectangle(sb, canvasPositionX, canvasPositionY, _tileSize, _tileSize);
                        }

                        IJWBSPiece piece = _game.Board.GetPieceAt(boardPosition);
                        if (piece != null)
                        {
                            int o = (((int)(piece.PlayerType == JWBSPlayerType.ONE ? Orientation.TOP : Orientation.BOTTOM)) - ((int)ViewOrientation)) % 4;
                            int px = o * PIECE_TYPES * TILE_SIZE + TILE_SIZE * (int)piece.PieceType;
                            int py = (piece.PlayerType == JWBSPlayerType.ONE ? 0 : TILE_SIZE);
                            if(piece is IJWBSPieceTower<JWBSPassivePiece, JWBSPassivePiece>)
                            {
                                IJWBSPieceTower<JWBSPassivePiece, JWBSPassivePiece> pieceTower = (IJWBSPieceTower<JWBSPassivePiece, JWBSPassivePiece>)piece;
                                DrawPiece(canvasPositionX, canvasPositionY, pieceTower.Pieces[pieceTower.Pieces.Count - 2]);
                                DrawPiece(canvasPositionX, canvasPositionY - _towerTopShift, pieceTower.Pieces.Last());
                            }
                            else
                            {
                                DrawPiece(canvasPositionX, canvasPositionY, piece);
                            }
                        }

                        canvasPositionX += _bufferGapTileSize;
                    }

                    //frame numbering
                    canvasPositionX += _bufferNumberingFrameSizeShift;
                    DrawFrameNumber(canvasPositionX, canvasPositionY + _bufferNumberingFrameTileShift, Orientation.RIGHT, i);

                    canvasPositionY += _bufferGapTileSize;
                }

                //frame numbering
                canvasPositionX = _boardEdges.Left + _tileGap;
                for (int j = 0; j < _game.Board.Width; j++)
                {
                    DrawFrameNumber(canvasPositionX + _bufferNumberingFrameTileShift, canvasPositionY, Orientation.BOTTOM, j);
                    canvasPositionX += _bufferGapTileSize;
                }
            }
            else
            {
                ResetCanvas();
            }
            UpdateMarksBitmap();
        }

        public void UpdateMarksBitmap()
        {
            if (HasVisibleBoard())
            {
                _marksBitmapBuffer = new Bitmap(_boardBitmapBuffer);
                Graphics graphics = Graphics.FromImage(_marksBitmapBuffer);

                SolidBrush[] sbRedMarks = new SolidBrush[]{
                    new SolidBrush(Color.FromArgb(255, 120, 120)),
                    new SolidBrush(Color.FromArgb(155, 120, 120))
                };
                SolidBrush[] sbBlueMarks = new SolidBrush[]{
                    new SolidBrush(Color.FromArgb(120, 120, 255)),
                    new SolidBrush(Color.FromArgb(120, 120, 155))
                };

                // mark current position
                int bufferTileSizeMark = _bufferGapTileSize + _tileGap;

                void DrawMark(SolidBrush sb, int sourceX, int sourceY)
                {
                    graphics.FillRectangle(sb, sourceX, sourceY, bufferTileSizeMark, _tileGap);
                    graphics.FillRectangle(sb, sourceX, sourceY + _tileGap, _tileGap, bufferTileSizeMark - 2*_tileGap);
                    graphics.FillRectangle(sb, sourceX + bufferTileSizeMark - _tileGap, sourceY + _tileGap, _tileGap, bufferTileSizeMark - 2 * _tileGap);
                    graphics.FillRectangle(sb, sourceX, sourceY + bufferTileSizeMark - _tileGap, bufferTileSizeMark, _tileGap);
                }
                SolidBrush GetMarkBrush()
                {
                    if (
                        _preselectedPiece == null ||
                        (!(_game.CurrentPlayer is IJWBSAIPlayer) && _game.CurrentPlayerType == _preselectedPiece.PlayerType)
                    )
                    {
                        return (_game.CurrentPlayerType == JWBSPlayerType.ONE ? sbRedMarks : sbBlueMarks)[0];
                    }
                    else
                    {
                        return (_preselectedPiece.PlayerType == JWBSPlayerType.ONE ? sbRedMarks : sbBlueMarks)[1];
                    }
                }

                // mark current position
                if (_currentCanvasPosition != null)
                {
                    DrawMark(GetMarkBrush(),
                        _boardEdges.Left + _bufferGapTileSize * _currentCanvasPosition.Item1,
                        _boardEdges.Top + _bufferGapTileSize * _currentCanvasPosition.Item2
                    );

                    SolidBrush sb = new SolidBrush(BoardFrameColorMark);
                    graphics.FillRectangle(sb,
                        _boardEdges.Left+_tileGap+_bufferGapTileSize*_currentCanvasPosition.Item1,
                        _boardFrameEdges.Top,
                        _tileSize,
                        _bufferNumberingFrameSize
                    );
                    graphics.FillRectangle(sb,
                        _boardFrameEdges.Right - _bufferNumberingFrameSize,
                        _boardEdges.Top + _tileGap + _bufferGapTileSize * _currentCanvasPosition.Item2,
                        _bufferNumberingFrameSize,
                        _tileSize
                    );
                    graphics.FillRectangle(sb,
                        _boardEdges.Left + _tileGap + _bufferGapTileSize * _currentCanvasPosition.Item1,
                        _boardFrameEdges.Bottom-_bufferNumberingFrameSize,
                        _tileSize,
                        _bufferNumberingFrameSize
                    );
                    graphics.FillRectangle(sb,
                        _boardFrameEdges.Left,
                        _boardEdges.Top + _tileGap + _bufferGapTileSize * _currentCanvasPosition.Item2,
                        _bufferNumberingFrameSize,
                        _tileSize
                    );
                }

                // mark _preselectedPiece only when already selected cause otherwise it is the current position
                if (HasSelectedPiece())
                {
                    DrawMark(GetMarkBrush(),
                        (int)_boardEdges.Left + _bufferGapTileSize * GetPreselectedPieceCanvasPosition().Item1,
                        (int)_boardEdges.Top + _bufferGapTileSize * GetPreselectedPieceCanvasPosition().Item2
                    );
                }

                // mark selected pieces allowed moving tiles
                if (_preselectedPiece != null)
                {
                    SolidBrush sb = GetMarkBrush();
                    foreach (Tuple<int, int> allowedPosition in _allowedCanvasTargetPositions)
                    {
                        DrawMark(sb,
                            (int)_boardEdges.Left + _bufferGapTileSize * allowedPosition.Item1,
                            (int)_boardEdges.Top + _bufferGapTileSize * allowedPosition.Item2
                        );
                    }
                }
            }
            Canvas.Invalidate();
        }

        // calculate-methods
        protected void CalculateCanvasMeasures()
        {
            _tileSize = 0;

            if (Canvas.Width != 0 && Canvas.Height != 0)
            {
                // calculate _tileSize
                {
                    // p + fx + g+t*(x+g) + fx + p = w              <=>
                    // 2p + 2f*x + g+t*x+t*g       = w              <=>
                    // 2f*x + t*x + 2p + (t+1)*g   = w              <=>
                    // x*(2f + t)                  = w - 2p -(t+1)g <=>
                    // x                           = (w - 2p -(t+1)g) / (2f + t)
                    int bufferTileWidth = (int)Math.Floor((
                            Canvas.Width
                            - 2 * _paddingMin
                            - (_tilesPerWidth + 1) * _tileGap
                        ) / (2* NUMBERING_FRAME_SIZE_FACTOR + _tilesPerWidth)
                    );
                    // p + fx + (n/d)*x + g+t*(x+g) + fx + p = h              <=>
                    // (n/d)*x + tx + 2fx + 2p + (t+1)*g     = h              <=>
                    // x * ((n/d) + t + 2f)                  = h - 2p -(t+1)g <=>
                    // x                                     = (h - 2p -(t+1)g) / ((n/d) + t + 2f) 
                    int bufferTileHeight = (int)Math.Floor((
                            Canvas.Height
                            - 2 * _paddingMin
                            - (_tilesPerWidth + 1) * _tileGap
                        ) / (
                            TOWER_TOP_SHIFT_FACTOR_NUMERATOR / TOWER_TOP_SHIFT_FACTOR_DENOMINATOR
                            + _tilesPerHeight
                            + 2* NUMBERING_FRAME_SIZE_FACTOR
                        )
                    );
                    _tileSize = Math.Min(bufferTileWidth, bufferTileHeight);
                }
                if (_tileSize < TILE_SIZE_MIN)
                {
                    _tileSize = 0;
                }
                else
                {
                    _bufferNumberingFrameSize = (int)Math.Floor(NUMBERING_FRAME_SIZE_FACTOR*_tileSize);
                    _bufferNumberingFrameFontSize = (int)Math.Floor(NUMBERING_FRAME_FONT_SIZE_FACTOR * _bufferNumberingFrameSize);
                    _bufferNumberingFrameSizeShift = (int)Math.Floor((_bufferNumberingFrameSize - _bufferNumberingFrameFontSize) / 2.0);
                    _bufferNumberingFrameTileShift = (int)Math.Floor((_tileSize - _bufferNumberingFrameFontSize) / 2.0);
                    _bufferGapTileSize = (_tileGap + _tileSize);

                    int bufferPaddingX = (int)Math.Floor((
                            Canvas.Width
                            - 2 * _bufferNumberingFrameSize
                            - _tileGap - _tilesPerWidth * _bufferGapTileSize
                        ) / 2.0
                    );
                    _towerTopShift = (int)Math.Floor((TOWER_TOP_SHIFT_FACTOR_NUMERATOR * _tileSize) / (double)TOWER_TOP_SHIFT_FACTOR_DENOMINATOR);
                    // p + fx + (n/d)*x + g+t*(x+g) + fx + p = h                           <=>
                    // (n/d)*x + tx + 2fx + 2p + (t+1)*g     = h                           <=>
                    // 2p + x*((n/d)+t+2f) + (t+1)g          = h                           <=>
                    // 2p                                    = h - x*((n/d)+t+2f) - (t+1)g <=>
                    // p                                     = (h - x*((n/d)+t+2f) - (t+1)g)/2
                    int bufferPaddingY = (int)Math.Floor((
                            Canvas.Height
                            - _tileSize * (
                                TOWER_TOP_SHIFT_FACTOR_NUMERATOR / TOWER_TOP_SHIFT_FACTOR_DENOMINATOR
                                + _tilesPerHeight
                                + 2 * NUMBERING_FRAME_SIZE_FACTOR
                            )
                            - (_tilesPerWidth + 1) * _tileGap
                        ) / 2.0
                    );
                    if (bufferPaddingX < _paddingMin || bufferPaddingY < _paddingMin)
                    {
                        _tileSize = 0;
                    }
                    else
                    {
                        int bufferBoardFrameX = bufferPaddingX;
                        int bufferBoardFrameY = bufferPaddingY;
                        _boardFrameEdges = new Rectangle(
                            bufferBoardFrameX,
                            bufferBoardFrameY,
                            2 * _bufferNumberingFrameSize + _tileGap + _bufferGapTileSize * _tilesPerWidth,
                            2 * _bufferNumberingFrameSize + _tileGap + _bufferGapTileSize * _tilesPerHeight
                        );
                        _boardEdges = new Rectangle(
                            bufferBoardFrameX+_bufferNumberingFrameSize,
                            bufferBoardFrameY+_bufferNumberingFrameSize,
                            _tileGap + _bufferGapTileSize * _tilesPerWidth,
                            _tileGap + _bufferGapTileSize * _tilesPerHeight
                        );
                    }
                }
            }
            UpdateBoardBitmap();
        }

        protected void CalculateCanvasOrientationMeasures()
        {
            _tilesPerWidth = (ViewOrientation == Orientation.TOP || ViewOrientation == Orientation.BOTTOM ? _game.Board.Width : _game.Board.Height);
            _tilesPerHeight = (ViewOrientation == Orientation.RIGHT || ViewOrientation == Orientation.LEFT ? _game.Board.Height : _game.Board.Width);
            switch (ViewOrientation)
            {
                case Orientation.RIGHT:
                    _bufferCanvasFrameNumberingTexts = new string[][]
                    {
                        _bufferGeneralCanvasFrameNumberingTexts[3].Reverse().ToArray(),
                        _bufferGeneralCanvasFrameNumberingTexts[0],
                        _bufferGeneralCanvasFrameNumberingTexts[1].Reverse().ToArray(),
                        _bufferGeneralCanvasFrameNumberingTexts[2]
                    };
                    break;

                case Orientation.BOTTOM:
                    _bufferCanvasFrameNumberingTexts = new string[][]
                    {
                        _bufferGeneralCanvasFrameNumberingTexts[2].Reverse().ToArray(),
                        _bufferGeneralCanvasFrameNumberingTexts[3].Reverse().ToArray(),
                        _bufferGeneralCanvasFrameNumberingTexts[0].Reverse().ToArray(),
                        _bufferGeneralCanvasFrameNumberingTexts[1].Reverse().ToArray()
                    };
                    break;

                case Orientation.LEFT:
                    _bufferCanvasFrameNumberingTexts = new string[][]
                    {
                        _bufferGeneralCanvasFrameNumberingTexts[1],
                        _bufferGeneralCanvasFrameNumberingTexts[2].Reverse().ToArray(),
                        _bufferGeneralCanvasFrameNumberingTexts[3],
                        _bufferGeneralCanvasFrameNumberingTexts[0].Reverse().ToArray()
                    };
                    break;

                default:
                    _bufferCanvasFrameNumberingTexts = _bufferGeneralCanvasFrameNumberingTexts;
                    break;
            }
            _bufferCanvasFrameNumberingTexts[(int)Orientation.RIGHT] = _bufferCanvasFrameNumberingTexts[(int)Orientation.LEFT];
            _bufferCanvasFrameNumberingTexts[(int)Orientation.BOTTOM] = _bufferCanvasFrameNumberingTexts[(int)Orientation.TOP];

            CalculateCanvasMeasures();
        }

        // reset-methods
        public void Reset()
        {
            _updatingAIPlayers = false;

            #region reset generel canvas numbering
            _bufferGeneralCanvasFrameNumberingTexts = new string[][]
            {
                new string[_game.Board.Width],
                new string[_game.Board.Height],
                new string[_game.Board.Width],
                new string[_game.Board.Height]
            };
            for (int j = 0; j < _game.Board.Width; j++)
            {
                _bufferGeneralCanvasFrameNumberingTexts[(int)Orientation.TOP][j] = JWBSMove.COLUMN_NAMES[j].ToString();
            }
            for (int i = 0; i < _game.Board.Height; i++)
            {
                _bufferGeneralCanvasFrameNumberingTexts[(int)Orientation.LEFT][i] = (_game.Board.Height - 1 - i).ToString();
            }
            _bufferGeneralCanvasFrameNumberingTexts[(int)Orientation.RIGHT] = _bufferGeneralCanvasFrameNumberingTexts[(int)Orientation.LEFT];
            _bufferGeneralCanvasFrameNumberingTexts[(int)Orientation.BOTTOM] = _bufferGeneralCanvasFrameNumberingTexts[(int)Orientation.TOP];
            #endregion

            PlayerNames[0].Text = _game.Players[0].Name;
            PlayerNames[1].Text = _game.Players[1].Name;

            ResetSelection();

            UpdatePoints();

            CalculateCanvasOrientationMeasures();
        }

        public void ResetCanvas()
        {
            if (Canvas.Width > 0 || Canvas.Height > 0)
            {
                _currentCanvasPosition = null;
                _currentBoardPosition = null;
                ResetSelection();
                _boardBitmapBuffer = new Bitmap(Canvas.Width, Canvas.Height, BITMAP_PIXEL_FORMAT);
                _marksBitmapBuffer = new Bitmap(Canvas.Width, Canvas.Height, BITMAP_PIXEL_FORMAT);
            }
        }

        protected void ResetSelection()
        {
            _preselectedPiece = null;
            _selectedCanvasSourcePosition = null;
            _selectedBoardSourcePosition = null;
        }

        // add-methods
        protected void AddMove(JWBSMove move)
        {
            if (_game.Move(move))
            {
                //MovesList.Items.Add(move.Name);
                //MovesList.Items[MovesList.Items.Count - 1].BackColor = COLORS[((int)_game.CurrentPlayerType + 1)];
                UpdatePoints();
                UpdateBoardBitmap();
            }
        }

        // get-methods
        public Tuple<int, int> GetPreselectedPieceCanvasPosition()
        {
            return (HasSelectedPiece() ? _selectedCanvasSourcePosition : _currentCanvasPosition );
        }

        public Tuple<int, int> GetPreselectedPieceBoardPosition()
        {
            return (HasSelectedPiece() ? _selectedBoardSourcePosition : _currentBoardPosition);
        }

        public Tuple<int, int> GetCanvasPositionFromBoardPosition(Tuple<int, int> position)
        {
            if (position == null)
            {
                return null;
            }
            switch (ViewOrientation)
            {
                case Orientation.RIGHT: return new Tuple<int, int>(_game.Board.Height-1-position.Item2, position.Item1);
                case Orientation.BOTTOM: return new Tuple<int, int>(_game.Board.Width - 1 - position.Item1, _game.Board.Height - 1 - position.Item2);
                case Orientation.LEFT: return new Tuple<int, int>(position.Item2, _game.Board.Width - 1 - position.Item1);
                default: return position;
            }
        }

        public Tuple<int, int> GetBoardPositionFromCanvasPosition(Tuple<int, int> position)
        {
            if (position == null)
            {
                return null;
            }
            switch (ViewOrientation)
            {
                case Orientation.RIGHT: return new Tuple<int, int>(position.Item2, _game.Board.Width-1-position.Item1);
                case Orientation.BOTTOM: return new Tuple<int, int>(_game.Board.Width - 1 - position.Item1, _game.Board.Height - 1 - position.Item2);
                case Orientation.LEFT: return new Tuple<int, int>(_game.Board.Height - 1 - position.Item2, position.Item1);
                default: return position;
            }
        }

        public bool HasSelectedPiece()
        {
            return (_selectedCanvasSourcePosition != null);
        }

        public bool HasVisibleBoard()
        {
            return (_game != null && _tileSize != 0);
        }
    }
}