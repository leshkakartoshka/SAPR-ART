using System;
using System.Windows.Forms;

namespace SAPR_ART
{
    public interface IRectangleView
    {
        event EventHandler CreateMainRectangle;
        event EventHandler CreateSecondRectangle;
        event EventHandler UpdateMainRectangle;
        event EventHandler DeleteRectangles;
        event EventHandler SelectMainRectangleCoordinates;
        event EventHandler SelectSecondRectangleCoordinates;

        string MainRectCoordLeftX { get; set; }
        string MainRectCoordLeftY { get; set; }
        string MainRectCoordRightX { get; set; }
        string MainRectCoordRightY { get; set; }

        string SecondRectCoordLeftX { get; set; }
        string SecondRectCoordLeftY { get; set; }
        string SecondRectCoordRightX { get; set; }
        string SecondRectCoordRightY { get; set; }

        string SecondRectchoiceColor { get; }
        bool CheckBorders { get; }
        CheckedListBox ColorList { get; }

        void ShowMessage(string message);
    }

    public partial class MainForm : Form, IRectangleView
    {
        private MainPresenter presenter;

        public event EventHandler CreateMainRectangle;
        public event EventHandler CreateSecondRectangle;
        public event EventHandler UpdateMainRectangle;
        public event EventHandler DeleteRectangles;
        public event EventHandler SelectMainRectangleCoordinates;
        public event EventHandler SelectSecondRectangleCoordinates;

        public string MainRectCoordLeftX
        {
            get => textBoxMainRectCoordLeftX.Text;
            set => textBoxMainRectCoordLeftX.Text = value;
        }

        public string MainRectCoordLeftY
        {
            get => textBoxMainRectCoordLeftY.Text;
            set => textBoxMainRectCoordLeftY.Text = value;
        }
        public string MainRectCoordRightX
        {
            get => textBoxMainRectCoordRightX.Text;
            set => textBoxMainRectCoordRightX.Text = value;
        }
        public string MainRectCoordRightY
        {
            get => textBoxMainRectCoordRightY.Text;
            set => textBoxMainRectCoordRightY.Text= value;
        }

        public string SecondRectCoordLeftX
        {
            get => textBoxSecondRectCoordLeftX.Text;
            set => textBoxSecondRectCoordLeftX.Text = value;
        }
        public string SecondRectCoordLeftY
        {
            get => textBoxSecondRectCoordLeftY.Text;
            set => textBoxSecondRectCoordLeftY.Text = value;
        }
        public string SecondRectCoordRightX
        {
            get => textBoxSecondRectCoordRightX.Text;
            set => textBoxSecondRectCoordRightX.Text = value;
        }
        public string SecondRectCoordRightY
        {
            get => textBoxSecondRectCoordRightY.Text;
            set => textBoxSecondRectCoordRightY.Text = value;
        }

        public string SecondRectchoiceColor => comboBoxChoiseColor.SelectedItem.ToString();
        public bool CheckBorders => checkBoxBorders.Checked;
        public CheckedListBox ColorList => checkedListBoxColor;


        public MainForm()
        {
            InitializeComponent();
            presenter = new MainPresenter(this, this);

            buttonCrMainRect.Click += (sender, e) => CreateMainRectangle?.Invoke(sender, e);
            buttonCrSecondRect.Click += (sender, e) => CreateSecondRectangle?.Invoke(sender, e);
            buttonOK.Click += (sender, e) => UpdateMainRectangle?.Invoke(sender, e);
            buttonDeleteRectangles.Click += (sender, e) => DeleteRectangles?.Invoke(sender, e);
            buttonPointMainCoord.Click += (sender, e) => SelectMainRectangleCoordinates?.Invoke(sender, e);
            buttonPointSecondCoord.Click += (sender, e) => SelectSecondRectangleCoordinates?.Invoke(sender, e);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
