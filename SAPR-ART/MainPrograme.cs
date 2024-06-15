using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;

namespace TestTaskSAPR_ART
{
    public class MainForm : Form
    {
        public static Rectangle MainRectangle;
        public static ObjectId MainPolylineId;
        public static Dictionary<string, int> colorDictionary = new Dictionary<string, int>
        {{"чёрный",250},{"красный",1},{"жёлтый",2},{"зелёный",3},{"голубой",4},{"синий",5},{"фиолетовый",6},{"белый",7},{"серый",8},{"светло-серый",9}};

        private Label labelMainRectangle;
        private Label labelMainRectCoordRightX;
        private Label labelMainRectCoordRightY;
        private Button buttonCrMainRect;
        private TextBox textBoxMainRectCoordRightX;
        private Label labelSecondaryRectangle;
        private Label labelSecondRectCoordRightX;
        private Label labelSecondRectCoordRightY;
        private TextBox textBoxSecondRectCoordRightX;
        private TextBox textBoxSecondRectCoordRightY;
        private Button buttonCrSecondRect;
        private Button buttonOK;
        private Button buttonCancel;
        private Label labelMainRectCoordLeftX;
        private TextBox textBoxMainRectCoordLeftX;
        private TextBox textBoxMainRectCoordLeftY;
        private Label labelMainRectCoordLeftY;
        private TextBox textBoxSecondRectCoordLeftX;
        private TextBox textBoxSecondRectCoordLeftY;
        private Label labelSecondRectCoordLeftY;
        private Label labelSecondRectCoordLeftX;
        private ComboBox comboBoxChoiseColor;
        private CheckedListBox checkedListBoxColor;
        private Button buttonDeleteRectangles;
        private Button buttonPointMainCoord;
        private Label labelMainRectCoordLeft;
        private Label labelMainRectCoordRight;
        private Label labelSecondRectCoordLeft;
        private Label labelSecondRectCoordRight;
        private Button buttonPointSecondCoord;
        private Label labelFilterColor;
        private Label labelChoiceColor;
        private CheckBox checkBoxBorders;
        private TextBox textBoxMainRectCoordRightY;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.labelMainRectangle = new System.Windows.Forms.Label();
            this.labelMainRectCoordRightX = new System.Windows.Forms.Label();
            this.labelMainRectCoordRightY = new System.Windows.Forms.Label();
            this.buttonCrMainRect = new System.Windows.Forms.Button();
            this.textBoxMainRectCoordRightX = new System.Windows.Forms.TextBox();
            this.textBoxMainRectCoordRightY = new System.Windows.Forms.TextBox();
            this.labelSecondaryRectangle = new System.Windows.Forms.Label();
            this.labelSecondRectCoordRightX = new System.Windows.Forms.Label();
            this.labelSecondRectCoordRightY = new System.Windows.Forms.Label();
            this.textBoxSecondRectCoordRightX = new System.Windows.Forms.TextBox();
            this.textBoxSecondRectCoordRightY = new System.Windows.Forms.TextBox();
            this.buttonCrSecondRect = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelMainRectCoordLeftX = new System.Windows.Forms.Label();
            this.textBoxMainRectCoordLeftX = new System.Windows.Forms.TextBox();
            this.textBoxMainRectCoordLeftY = new System.Windows.Forms.TextBox();
            this.labelMainRectCoordLeftY = new System.Windows.Forms.Label();
            this.textBoxSecondRectCoordLeftX = new System.Windows.Forms.TextBox();
            this.textBoxSecondRectCoordLeftY = new System.Windows.Forms.TextBox();
            this.labelSecondRectCoordLeftY = new System.Windows.Forms.Label();
            this.labelSecondRectCoordLeftX = new System.Windows.Forms.Label();
            this.comboBoxChoiseColor = new System.Windows.Forms.ComboBox();
            this.checkedListBoxColor = new System.Windows.Forms.CheckedListBox();
            this.buttonDeleteRectangles = new System.Windows.Forms.Button();
            this.buttonPointMainCoord = new System.Windows.Forms.Button();
            this.labelMainRectCoordLeft = new System.Windows.Forms.Label();
            this.labelMainRectCoordRight = new System.Windows.Forms.Label();
            this.labelSecondRectCoordLeft = new System.Windows.Forms.Label();
            this.labelSecondRectCoordRight = new System.Windows.Forms.Label();
            this.buttonPointSecondCoord = new System.Windows.Forms.Button();
            this.labelFilterColor = new System.Windows.Forms.Label();
            this.labelChoiceColor = new System.Windows.Forms.Label();
            this.checkBoxBorders = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelMainRectangle
            // 
            this.labelMainRectangle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMainRectangle.Location = new System.Drawing.Point(25, 25);
            this.labelMainRectangle.Name = "labelMainRectangle";
            this.labelMainRectangle.Size = new System.Drawing.Size(220, 140);
            this.labelMainRectangle.TabIndex = 0;
            this.labelMainRectangle.Text = "Главный прямоугольник";
            // 
            // labelMainRectCoordRightX
            // 
            this.labelMainRectCoordRightX.AutoSize = true;
            this.labelMainRectCoordRightX.Location = new System.Drawing.Point(37, 113);
            this.labelMainRectCoordRightX.Name = "labelMainRectCoordRightX";
            this.labelMainRectCoordRightX.Size = new System.Drawing.Size(17, 13);
            this.labelMainRectCoordRightX.TabIndex = 1;
            this.labelMainRectCoordRightX.Text = "X:";
            // 
            // labelMainRectCoordRightY
            // 
            this.labelMainRectCoordRightY.AutoSize = true;
            this.labelMainRectCoordRightY.Location = new System.Drawing.Point(127, 113);
            this.labelMainRectCoordRightY.Name = "labelMainRectCoordRightY";
            this.labelMainRectCoordRightY.Size = new System.Drawing.Size(17, 13);
            this.labelMainRectCoordRightY.TabIndex = 2;
            this.labelMainRectCoordRightY.Text = "Y:";
            // 
            // buttonCrMainRect
            // 
            this.buttonCrMainRect.Location = new System.Drawing.Point(160, 136);
            this.buttonCrMainRect.Name = "buttonCrMainRect";
            this.buttonCrMainRect.Size = new System.Drawing.Size(75, 23);
            this.buttonCrMainRect.TabIndex = 3;
            this.buttonCrMainRect.Text = "Создать";
            this.buttonCrMainRect.UseVisualStyleBackColor = true;
            this.buttonCrMainRect.Click += new System.EventHandler(this.buttonCrMainRect_Click);
            // 
            // textBoxMainRectCoordRightX
            // 
            this.textBoxMainRectCoordRightX.Location = new System.Drawing.Point(60, 110);
            this.textBoxMainRectCoordRightX.Name = "textBoxMainRectCoordRightX";
            this.textBoxMainRectCoordRightX.Size = new System.Drawing.Size(50, 20);
            this.textBoxMainRectCoordRightX.TabIndex = 4;
            // 
            // textBoxMainRectCoordRightY
            // 
            this.textBoxMainRectCoordRightY.Location = new System.Drawing.Point(150, 110);
            this.textBoxMainRectCoordRightY.Name = "textBoxMainRectCoordRightY";
            this.textBoxMainRectCoordRightY.Size = new System.Drawing.Size(50, 20);
            this.textBoxMainRectCoordRightY.TabIndex = 5;
            // 
            // labelSecondaryRectangle
            // 
            this.labelSecondaryRectangle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSecondaryRectangle.Location = new System.Drawing.Point(25, 180);
            this.labelSecondaryRectangle.Name = "labelSecondaryRectangle";
            this.labelSecondaryRectangle.Size = new System.Drawing.Size(245, 250);
            this.labelSecondaryRectangle.TabIndex = 6;
            this.labelSecondaryRectangle.Text = "Второстепенный прямоугольник";
            // 
            // labelSecondRectCoordRightX
            // 
            this.labelSecondRectCoordRightX.AutoSize = true;
            this.labelSecondRectCoordRightX.Location = new System.Drawing.Point(40, 268);
            this.labelSecondRectCoordRightX.Name = "labelSecondRectCoordRightX";
            this.labelSecondRectCoordRightX.Size = new System.Drawing.Size(17, 13);
            this.labelSecondRectCoordRightX.TabIndex = 7;
            this.labelSecondRectCoordRightX.Text = "X:";
            // 
            // labelSecondRectCoordRightY
            // 
            this.labelSecondRectCoordRightY.AutoSize = true;
            this.labelSecondRectCoordRightY.Location = new System.Drawing.Point(128, 268);
            this.labelSecondRectCoordRightY.Name = "labelSecondRectCoordRightY";
            this.labelSecondRectCoordRightY.Size = new System.Drawing.Size(17, 13);
            this.labelSecondRectCoordRightY.TabIndex = 8;
            this.labelSecondRectCoordRightY.Text = "Y:";
            // 
            // textBoxSecondRectCoordRightX
            // 
            this.textBoxSecondRectCoordRightX.Location = new System.Drawing.Point(60, 265);
            this.textBoxSecondRectCoordRightX.Name = "textBoxSecondRectCoordRightX";
            this.textBoxSecondRectCoordRightX.Size = new System.Drawing.Size(50, 20);
            this.textBoxSecondRectCoordRightX.TabIndex = 9;
            // 
            // textBoxSecondRectCoordRightY
            // 
            this.textBoxSecondRectCoordRightY.Location = new System.Drawing.Point(150, 265);
            this.textBoxSecondRectCoordRightY.Name = "textBoxSecondRectCoordRightY";
            this.textBoxSecondRectCoordRightY.Size = new System.Drawing.Size(50, 20);
            this.textBoxSecondRectCoordRightY.TabIndex = 10;
            // 
            // buttonCrSecondRect
            // 
            this.buttonCrSecondRect.Location = new System.Drawing.Point(160, 400);
            this.buttonCrSecondRect.Name = "buttonCrSecondRect";
            this.buttonCrSecondRect.Size = new System.Drawing.Size(75, 23);
            this.buttonCrSecondRect.TabIndex = 11;
            this.buttonCrSecondRect.Text = "Создать";
            this.buttonCrSecondRect.UseVisualStyleBackColor = true;
            this.buttonCrSecondRect.Click += new System.EventHandler(this.buttonCrSecondRect_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(25, 542);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(194, 542);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelMainRectCoordLeftX
            // 
            this.labelMainRectCoordLeftX.AutoSize = true;
            this.labelMainRectCoordLeftX.Location = new System.Drawing.Point(40, 68);
            this.labelMainRectCoordLeftX.Name = "labelMainRectCoordLeftX";
            this.labelMainRectCoordLeftX.Size = new System.Drawing.Size(17, 13);
            this.labelMainRectCoordLeftX.TabIndex = 14;
            this.labelMainRectCoordLeftX.Text = "X:";
            // 
            // textBoxMainRectCoordLeftX
            // 
            this.textBoxMainRectCoordLeftX.Location = new System.Drawing.Point(60, 65);
            this.textBoxMainRectCoordLeftX.Name = "textBoxMainRectCoordLeftX";
            this.textBoxMainRectCoordLeftX.Size = new System.Drawing.Size(50, 20);
            this.textBoxMainRectCoordLeftX.TabIndex = 15;
            // 
            // textBoxMainRectCoordLeftY
            // 
            this.textBoxMainRectCoordLeftY.Location = new System.Drawing.Point(150, 65);
            this.textBoxMainRectCoordLeftY.Name = "textBoxMainRectCoordLeftY";
            this.textBoxMainRectCoordLeftY.Size = new System.Drawing.Size(50, 20);
            this.textBoxMainRectCoordLeftY.TabIndex = 16;
            // 
            // labelMainRectCoordLeftY
            // 
            this.labelMainRectCoordLeftY.AutoSize = true;
            this.labelMainRectCoordLeftY.Location = new System.Drawing.Point(128, 68);
            this.labelMainRectCoordLeftY.Name = "labelMainRectCoordLeftY";
            this.labelMainRectCoordLeftY.Size = new System.Drawing.Size(17, 13);
            this.labelMainRectCoordLeftY.TabIndex = 17;
            this.labelMainRectCoordLeftY.Text = "Y:";
            // 
            // textBoxSecondRectCoordLeftX
            // 
            this.textBoxSecondRectCoordLeftX.Location = new System.Drawing.Point(60, 220);
            this.textBoxSecondRectCoordLeftX.Name = "textBoxSecondRectCoordLeftX";
            this.textBoxSecondRectCoordLeftX.Size = new System.Drawing.Size(50, 20);
            this.textBoxSecondRectCoordLeftX.TabIndex = 18;
            // 
            // textBoxSecondRectCoordLeftY
            // 
            this.textBoxSecondRectCoordLeftY.Location = new System.Drawing.Point(150, 220);
            this.textBoxSecondRectCoordLeftY.Name = "textBoxSecondRectCoordLeftY";
            this.textBoxSecondRectCoordLeftY.Size = new System.Drawing.Size(50, 20);
            this.textBoxSecondRectCoordLeftY.TabIndex = 19;
            // 
            // labelSecondRectCoordLeftY
            // 
            this.labelSecondRectCoordLeftY.AutoSize = true;
            this.labelSecondRectCoordLeftY.Location = new System.Drawing.Point(128, 223);
            this.labelSecondRectCoordLeftY.Name = "labelSecondRectCoordLeftY";
            this.labelSecondRectCoordLeftY.Size = new System.Drawing.Size(17, 13);
            this.labelSecondRectCoordLeftY.TabIndex = 20;
            this.labelSecondRectCoordLeftY.Text = "Y:";
            // 
            // labelSecondRectCoordLeftX
            // 
            this.labelSecondRectCoordLeftX.AutoSize = true;
            this.labelSecondRectCoordLeftX.Location = new System.Drawing.Point(40, 223);
            this.labelSecondRectCoordLeftX.Name = "labelSecondRectCoordLeftX";
            this.labelSecondRectCoordLeftX.Size = new System.Drawing.Size(17, 13);
            this.labelSecondRectCoordLeftX.TabIndex = 21;
            this.labelSecondRectCoordLeftX.Text = "X:";
            // 
            // comboBoxChoiseColor
            // 
            this.comboBoxChoiseColor.FormattingEnabled = true;
            this.comboBoxChoiseColor.Items.AddRange(new object[] {
            "чёрный",
            "красный",
            "жёлтый",
            "зелёный",
            "голубой",
            "синий",
            "фиолетовый",
            "белый",
            "серый",
            "светло-серый"});
            this.comboBoxChoiseColor.Location = new System.Drawing.Point(40, 370);
            this.comboBoxChoiseColor.Name = "comboBoxChoiseColor";
            this.comboBoxChoiseColor.Size = new System.Drawing.Size(75, 21);
            this.comboBoxChoiseColor.TabIndex = 22;
            // 
            // checkedListBoxColor
            // 
            this.checkedListBoxColor.FormattingEnabled = true;
            this.checkedListBoxColor.Items.AddRange(new object[] {
            "чёрный",
            "красный",
            "жёлтый",
            "зелёный",
            "голубой",
            "синий",
            "фиолетовый",
            "белый",
            "серый",
            "светло-серый"});
            this.checkedListBoxColor.Location = new System.Drawing.Point(40, 295);
            this.checkedListBoxColor.Name = "checkedListBoxColor";
            this.checkedListBoxColor.Size = new System.Drawing.Size(120, 64);
            this.checkedListBoxColor.TabIndex = 23;
            // 
            // buttonDeleteRectangles
            // 
            this.buttonDeleteRectangles.Location = new System.Drawing.Point(71, 513);
            this.buttonDeleteRectangles.Name = "buttonDeleteRectangles";
            this.buttonDeleteRectangles.Size = new System.Drawing.Size(143, 23);
            this.buttonDeleteRectangles.TabIndex = 24;
            this.buttonDeleteRectangles.Text = "удалить прямоугольники";
            this.buttonDeleteRectangles.UseVisualStyleBackColor = true;
            this.buttonDeleteRectangles.Click += new System.EventHandler(this.buttonDeleteRectangles_Click);
            // 
            // buttonPointMainCoord
            // 
            this.buttonPointMainCoord.Location = new System.Drawing.Point(43, 136);
            this.buttonPointMainCoord.Name = "buttonPointMainCoord";
            this.buttonPointMainCoord.Size = new System.Drawing.Size(111, 23);
            this.buttonPointMainCoord.TabIndex = 25;
            this.buttonPointMainCoord.Text = "Указать мышью ->";
            this.buttonPointMainCoord.UseVisualStyleBackColor = true;
            this.buttonPointMainCoord.Click += new System.EventHandler(this.buttonPointMainCoord_Click);
            // 
            // labelMainRectCoordLeft
            // 
            this.labelMainRectCoordLeft.AutoSize = true;
            this.labelMainRectCoordLeft.Location = new System.Drawing.Point(40, 45);
            this.labelMainRectCoordLeft.Name = "labelMainRectCoordLeft";
            this.labelMainRectCoordLeft.Size = new System.Drawing.Size(92, 13);
            this.labelMainRectCoordLeft.TabIndex = 26;
            this.labelMainRectCoordLeft.Text = "Первая вершина";
            // 
            // labelMainRectCoordRight
            // 
            this.labelMainRectCoordRight.AutoSize = true;
            this.labelMainRectCoordRight.Location = new System.Drawing.Point(40, 90);
            this.labelMainRectCoordRight.Name = "labelMainRectCoordRight";
            this.labelMainRectCoordRight.Size = new System.Drawing.Size(90, 13);
            this.labelMainRectCoordRight.TabIndex = 27;
            this.labelMainRectCoordRight.Text = "Вторая вершина";
            // 
            // labelSecondRectCoordLeft
            // 
            this.labelSecondRectCoordLeft.AutoSize = true;
            this.labelSecondRectCoordLeft.Location = new System.Drawing.Point(40, 200);
            this.labelSecondRectCoordLeft.Name = "labelSecondRectCoordLeft";
            this.labelSecondRectCoordLeft.Size = new System.Drawing.Size(92, 13);
            this.labelSecondRectCoordLeft.TabIndex = 28;
            this.labelSecondRectCoordLeft.Text = "Первая вершина";
            // 
            // labelSecondRectCoordRight
            // 
            this.labelSecondRectCoordRight.AutoSize = true;
            this.labelSecondRectCoordRight.Location = new System.Drawing.Point(40, 245);
            this.labelSecondRectCoordRight.Name = "labelSecondRectCoordRight";
            this.labelSecondRectCoordRight.Size = new System.Drawing.Size(90, 13);
            this.labelSecondRectCoordRight.TabIndex = 29;
            this.labelSecondRectCoordRight.Text = "Вторая вершина";
            // 
            // buttonPointSecondCoord
            // 
            this.buttonPointSecondCoord.Location = new System.Drawing.Point(33, 400);
            this.buttonPointSecondCoord.Name = "buttonPointSecondCoord";
            this.buttonPointSecondCoord.Size = new System.Drawing.Size(111, 23);
            this.buttonPointSecondCoord.TabIndex = 30;
            this.buttonPointSecondCoord.Text = "Указать мышью ->";
            this.buttonPointSecondCoord.UseVisualStyleBackColor = true;
            this.buttonPointSecondCoord.Click += new System.EventHandler(this.buttonPointSecondCoord_Click);
            // 
            // labelFilterColor
            // 
            this.labelFilterColor.Location = new System.Drawing.Point(166, 295);
            this.labelFilterColor.Name = "labelFilterColor";
            this.labelFilterColor.Size = new System.Drawing.Size(103, 44);
            this.labelFilterColor.TabIndex = 31;
            this.labelFilterColor.Text = "<- Потсавить галочку ,если цвет учитывается";
            // 
            // labelChoiceColor
            // 
            this.labelChoiceColor.AutoSize = true;
            this.labelChoiceColor.Location = new System.Drawing.Point(127, 371);
            this.labelChoiceColor.Name = "labelChoiceColor";
            this.labelChoiceColor.Size = new System.Drawing.Size(131, 13);
            this.labelChoiceColor.TabIndex = 32;
            this.labelChoiceColor.Text = "<- Цвет прямоугольника";
            // 
            // checkBoxBorders
            // 
            this.checkBoxBorders.Location = new System.Drawing.Point(25, 433);
            this.checkBoxBorders.Name = "checkBoxBorders";
            this.checkBoxBorders.Size = new System.Drawing.Size(244, 74);
            this.checkBoxBorders.TabIndex = 33;
            this.checkBoxBorders.Text = "Не учитывать вершины второстепенных прямоугольников, вышедших за границу главного" +
    " прямоугольника";
            this.checkBoxBorders.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 581);
            this.Controls.Add(this.checkBoxBorders);
            this.Controls.Add(this.labelChoiceColor);
            this.Controls.Add(this.labelFilterColor);
            this.Controls.Add(this.buttonPointSecondCoord);
            this.Controls.Add(this.labelSecondRectCoordRight);
            this.Controls.Add(this.labelSecondRectCoordLeft);
            this.Controls.Add(this.labelMainRectCoordRight);
            this.Controls.Add(this.labelMainRectCoordLeft);
            this.Controls.Add(this.buttonPointMainCoord);
            this.Controls.Add(this.buttonDeleteRectangles);
            this.Controls.Add(this.checkedListBoxColor);
            this.Controls.Add(this.comboBoxChoiseColor);
            this.Controls.Add(this.labelSecondRectCoordLeftX);
            this.Controls.Add(this.labelSecondRectCoordLeftY);
            this.Controls.Add(this.textBoxSecondRectCoordLeftY);
            this.Controls.Add(this.textBoxSecondRectCoordLeftX);
            this.Controls.Add(this.labelMainRectCoordLeftY);
            this.Controls.Add(this.textBoxMainRectCoordLeftY);
            this.Controls.Add(this.textBoxMainRectCoordLeftX);
            this.Controls.Add(this.labelMainRectCoordLeftX);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCrSecondRect);
            this.Controls.Add(this.textBoxSecondRectCoordRightY);
            this.Controls.Add(this.textBoxSecondRectCoordRightX);
            this.Controls.Add(this.labelSecondRectCoordRightY);
            this.Controls.Add(this.labelSecondRectCoordRightX);
            this.Controls.Add(this.labelSecondaryRectangle);
            this.Controls.Add(this.textBoxMainRectCoordRightY);
            this.Controls.Add(this.textBoxMainRectCoordRightX);
            this.Controls.Add(this.buttonCrMainRect);
            this.Controls.Add(this.labelMainRectCoordRightY);
            this.Controls.Add(this.labelMainRectCoordRightX);
            this.Controls.Add(this.labelMainRectangle);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonCrMainRect_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxMainRectCoordLeftX.Text, out double botLeftX) ||
                !double.TryParse(textBoxMainRectCoordLeftY.Text, out double botLeftY) ||
                !double.TryParse(textBoxMainRectCoordRightX.Text, out double topRightX) ||
                !double.TryParse(textBoxMainRectCoordRightY.Text, out double topRightY))
            {
                MessageBox.Show("Please enter valid numerical values for the coordinates.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Point botLeft = new Point(botLeftX, botLeftY);
            Point topRight = new Point(topRightX, topRightY);
            MainRectangle = new Rectangle("main", botLeft, topRight);

            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Polyline polyline = new Polyline(4);

                polyline.AddVertexAt(0, new Point2d(MainRectangle.BotLeft.X, MainRectangle.BotLeft.Y), 0, 5, 5);
                polyline.AddVertexAt(1, new Point2d(MainRectangle.BotLeft.X, MainRectangle.TopRight.Y), 0, 5, 5);
                polyline.AddVertexAt(2, new Point2d(MainRectangle.TopRight.X, MainRectangle.TopRight.Y), 0, 5, 5);
                polyline.AddVertexAt(3, new Point2d(MainRectangle.TopRight.X, MainRectangle.BotLeft.Y), 0, 5, 5);
                polyline.Closed = true;

                polyline.ColorIndex = 23;

                btr.AppendEntity(polyline);
                tr.AddNewlyCreatedDBObject(polyline, true);
                MainPolylineId = polyline.ObjectId;
                tr.Commit();
            }

            MessageBox.Show($"Main Rectangle created from ({MainRectangle.BotLeft.X}, {MainRectangle.BotLeft.Y}) to ({MainRectangle.TopRight.X}, {MainRectangle.TopRight.Y})");
        }

        private void buttonCrSecondRect_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxSecondRectCoordLeftX.Text, out double botLeftX) ||
                !double.TryParse(textBoxSecondRectCoordLeftY.Text, out double botLeftY) ||
                !double.TryParse(textBoxSecondRectCoordRightX.Text, out double topRightX) ||
                !double.TryParse(textBoxSecondRectCoordRightY.Text, out double topRightY))
            {
                MessageBox.Show("Please enter valid numerical values for the coordinates.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Point botLeft = new Point(botLeftX, botLeftY);
            Point topRight = new Point(topRightX, topRightY);
            Rectangle secondRect = new Rectangle("main", botLeft, topRight);

            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Polyline polyline = new Polyline(4);
                polyline.AddVertexAt(0, new Point2d(secondRect.BotLeft.X, secondRect.BotLeft.Y), 0, 0, 0);
                polyline.AddVertexAt(1, new Point2d(secondRect.BotLeft.X, secondRect.TopRight.Y), 0, 0, 0);
                polyline.AddVertexAt(2, new Point2d(secondRect.TopRight.X, secondRect.TopRight.Y), 0, 0, 0);
                polyline.AddVertexAt(3, new Point2d(secondRect.TopRight.X, secondRect.BotLeft.Y), 0, 0, 0);
                polyline.Closed = true;

                Hatch hatch = new Hatch();
                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");              
                btr.AppendEntity(hatch);
                tr.AddNewlyCreatedDBObject(hatch, true);

                string selectedColorName = comboBoxChoiseColor.SelectedItem.ToString();
                int colorIndex = colorDictionary[selectedColorName];
                polyline.ColorIndex = colorIndex;
                hatch.ColorIndex = colorIndex;

                btr.AppendEntity(polyline);
                tr.AddNewlyCreatedDBObject(polyline, true);
                hatch.AppendLoop(HatchLoopTypes.Default, new ObjectIdCollection { polyline.ObjectId });
                hatch.EvaluateHatch(true);

                tr.Commit();
            }

            MessageBox.Show($"Main Rectangle created from ({secondRect.BotLeft.X}, {secondRect.BotLeft.Y}) to ({secondRect.TopRight.X}, {secondRect.TopRight.Y})");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            if (MainPolylineId == ObjectId.Null)
            {
                MessageBox.Show("Main rectangle not found.");
                return;
            }

            List<Point> points = GetSecondaryRectanglePoints();

            if (points.Count == 0)
            {
                MessageBox.Show("No secondary rectangles found.");
                return;
            }

            double botLeftX = double.MaxValue;
            double botLeftY = double.MaxValue;
            double topRightX = double.MinValue;
            double topRightY = double.MinValue;

            foreach (var point in points)
            {
                if (checkBoxBorders.Checked)
                {
                    if (point.X > MainRectangle.BotLeft.X && point.X < botLeftX) botLeftX = point.X;
                    if (point.Y > MainRectangle.BotLeft.Y && point.Y < botLeftY) botLeftY = point.Y;
                    if (point.X < MainRectangle.TopRight.X && point.X > topRightX) topRightX = point.X;
                    if (point.Y < MainRectangle.TopRight.Y && point.Y > topRightY) topRightY = point.Y;
                }
                
                if (!checkBoxBorders.Checked)
                {
                    if (point.X < botLeftX) botLeftX = point.X;
                    if (point.Y < botLeftY) botLeftY = point.Y;
                    if (point.X > topRightX) topRightX = point.X;
                    if (point.Y > topRightY) topRightY = point.Y;
                }
            }

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                Polyline polyline = (Polyline)tr.GetObject(MainPolylineId, OpenMode.ForWrite);

                if (polyline != null)
                {
                    polyline.SetPointAt(0, new Point2d(botLeftX, botLeftY));
                    polyline.SetPointAt(1, new Point2d(botLeftX, topRightY));
                    polyline.SetPointAt(2, new Point2d(topRightX, topRightY));
                    polyline.SetPointAt(3, new Point2d(topRightX, botLeftY));
                }

                tr.Commit();
            }
        }

        private List<Point> GetSecondaryRectanglePoints()
        {
            List<Point> points = new List<Point>();

            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            List<int> selectedColorIndices = new List<int>();

            for (int i = 0; i < checkedListBoxColor.Items.Count; i++)
            {
                string color = checkedListBoxColor.Items[i].ToString();
                if (checkedListBoxColor.GetItemChecked(i) && colorDictionary.ContainsKey(color))
                {
                    selectedColorIndices.Add(colorDictionary[color]);
                }
            }

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);

                foreach (ObjectId objId in btr)
                {
                    Entity entity = tr.GetObject(objId, OpenMode.ForRead) as Entity;

                    for (int i = 0; i < 10; i++)
                    {
                        if (entity is Polyline polyline && polyline.NumberOfVertices == 4 && polyline.Closed && polyline.ObjectId != MainPolylineId && selectedColorIndices.Contains(polyline.ColorIndex))
                        {
                            double minX = double.MaxValue;
                            double minY = double.MaxValue;
                            double maxX = double.MinValue;
                            double maxY = double.MinValue;

                            for (int j = 0; j < polyline.NumberOfVertices; j++)
                            {
                                Point2d pt = polyline.GetPoint2dAt(j);

                                if (pt.X < minX) minX = pt.X;
                                if (pt.Y < minY) minY = pt.Y;
                                if (pt.X > maxX) maxX = pt.X;
                                if (pt.Y > maxY) maxY = pt.Y;
                            }

                            points.Add(new Point(minX, minY));
                            points.Add(new Point(maxX, maxY));
                        }
                    }
                }

                tr.Commit();
            }

            return points;
        }

        private void buttonDeleteRectangles_Click(object sender, EventArgs e)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                List<ObjectId> entitiesToDelete = new List<ObjectId>();

                foreach (ObjectId objId in acBlkTblRec)
                {
                    Entity ent = acTrans.GetObject(objId, OpenMode.ForRead) as Entity;
                    if (ent != null)
                    {
                        if (ent.GetType() == typeof(Polyline) || ent.GetType() == typeof(Hatch))
                        {
                            entitiesToDelete.Add(objId);
                        }
                    }
                }

                foreach (ObjectId entityId in entitiesToDelete)
                {
                    Entity ent = acTrans.GetObject(entityId, OpenMode.ForWrite) as Entity;
                    ent.Erase();
                }

                acTrans.Commit();
            }
        }

        private void buttonPointMainCoord_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += Editor_PointMonitor;
            Hide();

            PromptPointResult mainLeftBotCoord = ed.GetPoint("Укажите первую вершину: ");
            if (mainLeftBotCoord.Status == PromptStatus.OK)
            {
                textBoxMainRectCoordLeftX.Text = mainLeftBotCoord.Value.X.ToString();
                textBoxMainRectCoordLeftY.Text = mainLeftBotCoord.Value.Y.ToString();
            }

            PromptPointResult mainRightTopCoord = ed.GetPoint("Укажите вторую вершину: ");
            if (mainRightTopCoord.Status == PromptStatus.OK)
            {
                textBoxMainRectCoordRightX.Text = mainRightTopCoord.Value.X.ToString();
                textBoxMainRectCoordRightY.Text = mainRightTopCoord.Value.Y.ToString();
            }

            ed.PointMonitor -= Editor_PointMonitor;
            Show();
        }

        private void buttonPointSecondCoord_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += Editor_PointMonitor;
            Hide();

            PromptPointResult secondLeftBotCoord = ed.GetPoint("Укажите первую вершину: ");
            if (secondLeftBotCoord.Status == PromptStatus.OK)
            {
                textBoxSecondRectCoordLeftX.Text = secondLeftBotCoord.Value.X.ToString();
                textBoxSecondRectCoordLeftY.Text = secondLeftBotCoord.Value.Y.ToString();
            }

            PromptPointResult secondRightTopCoord = ed.GetPoint("Укажите вторую вершину: ");
            if (secondRightTopCoord.Status == PromptStatus.OK)
            {
                textBoxSecondRectCoordRightX.Text = secondRightTopCoord.Value.X.ToString();
                textBoxSecondRectCoordRightY.Text = secondRightTopCoord.Value.Y.ToString();
            }

            ed.PointMonitor -= Editor_PointMonitor;
            Show();
        }

        private void Editor_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            // Обработка для закрытия диалогового окна
        }
    }

    public class MainPrograme : IExtensionApplication
    {
        [CommandMethod("RectAppCommand")]
        public void MyCommand()
        {
            System.Windows.Forms.Application.Run(new MainForm());
        }

        public void Initialize()
        {
            var editor = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("\nВведите \"RectAppCommand\" для запуска приложения...\n" +
            Environment.NewLine);
        }

        public void Terminate()
        {
        }
    }
}
