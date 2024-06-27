using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SAPR_ART
{
    public class MainPresenter : Form
    {
        public MainForm _mainForm;
        public static Rectangle MainRectangle;
        public static ObjectId MainPolylineId;
        private readonly IRectangleView _view;
        public static Dictionary<string, int> colorDictionary = new Dictionary<string, int>
        {{"чёрный",250},{"красный",1},{"жёлтый",2},{"зелёный",3},{"голубой",4},{"синий",5},{"фиолетовый",6},{"белый",7},{"серый",8},{"светло-серый",9}};

        public MainPresenter(IRectangleView view, MainForm mainForm)
        {
            _view = view;
            _view.CreateMainRectangle += OnCreateMainRectangle;
            _view.CreateSecondRectangle += OnCreateSecondRectangle;
            _view.UpdateMainRectangle += OnUpdateMainRectangle;
            _view.DeleteRectangles += OnDeleteRectangles;
            _view.SelectMainRectangleCoordinates += OnSelectMainRectangleCoordinates;
            _view.SelectSecondRectangleCoordinates += OnSelectSecondRectangleCoordinates;

            _mainForm = mainForm;
        }

        private void OnCreateMainRectangle(object sender, EventArgs e)
        {
            if (!double.TryParse(_view.MainRectCoordLeftX, out double botLeftX) ||
                !double.TryParse(_view.MainRectCoordLeftY, out double botLeftY) ||
                !double.TryParse(_view.MainRectCoordRightX, out double topRightX) ||
                !double.TryParse(_view.MainRectCoordRightY, out double topRightY))
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

        private void OnCreateSecondRectangle(object sender, EventArgs e)
        {
            if (!double.TryParse(_view.SecondRectCoordLeftX, out double botLeftX) ||
                !double.TryParse(_view.SecondRectCoordLeftY, out double botLeftY) ||
                !double.TryParse(_view.SecondRectCoordRightX, out double topRightX) ||
                !double.TryParse(_view.SecondRectCoordRightY, out double topRightY))
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

                string selectedColorName = _view.SecondRectchoiceColor;
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

        private void OnUpdateMainRectangle(object sender, EventArgs e)
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
                if (_view.CheckBorders)
                {
                    if (point.X > MainRectangle.BotLeft.X && point.X < botLeftX) botLeftX = point.X;
                    if (point.Y > MainRectangle.BotLeft.Y && point.Y < botLeftY) botLeftY = point.Y;
                    if (point.X < MainRectangle.TopRight.X && point.X > topRightX) topRightX = point.X;
                    if (point.Y < MainRectangle.TopRight.Y && point.Y > topRightY) topRightY = point.Y;
                }

                if (!_view.CheckBorders)
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

            for (int i = 0; i < _view.ColorList.Items.Count; i++)
            {
                string color = _view.ColorList.Items[i].ToString();
                if (_view.ColorList.GetItemChecked(i) && colorDictionary.ContainsKey(color))
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

        private void OnDeleteRectangles(object sender, EventArgs e)
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

        private void OnSelectMainRectangleCoordinates(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += Editor_PointMonitor;
            _mainForm.Hide();

            PromptPointResult mainLeftBotCoord = ed.GetPoint("Укажите первую вершину: ");
            if (mainLeftBotCoord.Status == PromptStatus.OK)
            {
                _view.MainRectCoordLeftX = mainLeftBotCoord.Value.X.ToString();
                _view.MainRectCoordLeftY = mainLeftBotCoord.Value.Y.ToString();
            }

            PromptPointResult mainRightTopCoord = ed.GetPoint("Укажите вторую вершину: ");
            if (mainRightTopCoord.Status == PromptStatus.OK)
            {
                _view.MainRectCoordRightX = mainRightTopCoord.Value.X.ToString();
                _view.MainRectCoordRightY = mainRightTopCoord.Value.Y.ToString();
            }

            ed.PointMonitor -= Editor_PointMonitor;
            _mainForm.Show();
        }

        private void OnSelectSecondRectangleCoordinates(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += Editor_PointMonitor;
            _mainForm.Hide();

            PromptPointResult secondLeftBotCoord = ed.GetPoint("Укажите первую вершину: ");
            if (secondLeftBotCoord.Status == PromptStatus.OK)
            {
                _view.SecondRectCoordLeftX = secondLeftBotCoord.Value.X.ToString();
                _view.SecondRectCoordLeftY = secondLeftBotCoord.Value.Y.ToString();
            }

            PromptPointResult secondRightTopCoord = ed.GetPoint("Укажите вторую вершину: ");
            if (secondRightTopCoord.Status == PromptStatus.OK)
            {
                _view.SecondRectCoordRightX = secondRightTopCoord.Value.X.ToString();
                _view.SecondRectCoordRightY = secondRightTopCoord.Value.Y.ToString();
            }

            ed.PointMonitor -= Editor_PointMonitor;
            _mainForm.Show();
        }

        private void Editor_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            // Обработка для закрытия диалогового окна
        }
    }

}
