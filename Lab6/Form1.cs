using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        int selectedGraphId;
        public static Graphics g;
        public static Bitmap bg;
        public Form1()
        {
            InitializeComponent();

            bg = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            g = Graphics.FromImage(bg);
            DrawingArea.Image = bg;
        }

        public class GraphManager
        {
            private static GraphManager _instance = new GraphManager();
            private ArrayList _graphs = new ArrayList();

            private GraphManager() { }
            public static GraphManager getInstance()
            {
                return _instance;
            }
            public Graph GetGraph(int graphID)
            {
                foreach (Graph graph in _graphs)
                {
                    if (graph.GetId() == graphID)
                    {
                        return graph;
                    }
                }
                return null;
            }
            private Point promptVertexCoordinates(Vertex vertex)
            {
                int tempx = Prompt.ShowDialog("Enter the vertex " + vertex.GetVertexNumber() + "'s x coordinate: ", "OK");
                int tempy = Prompt.ShowDialog("Enter the vertex " + vertex.GetVertexNumber() + "'s y coordinate: ", "OK");
                return new Point(tempx, tempy);
            }
            private int promptVertexID()
            {
                int tempId = Prompt.ShowDialog("Enter the vertex's ID: ", "OK");
                return tempId;
            }
            private Edge promptEdge(int graphID)
            {
                Edge edge = new Edge();
                int edgeId = Prompt.ShowDialog("Enter the edge ID: ", "OK");
                edge.SetEdgeNumber(edgeId);
                int fromVertexID = Prompt.ShowDialog("From a vertex with an ID: ", "Continue");
                int toVertexID = Prompt.ShowDialog("To a vertex with an ID: ", "Finish");
                
                edge.SetFromVertex(getInstance().GetGraph(graphID).GetVertex(fromVertexID));
                edge.SetToVertex(getInstance().GetGraph(graphID).GetVertex(toVertexID));
                return edge;
            }

            public void Create(int graphID)
            {
                Graph graph = new Graph();
                graph.SetId(graphID);
                int howManyVertices, howManyEdges;
                howManyVertices = Prompt.ShowDialog("How many vertices do you want to draw?", "OK");
                howManyEdges = Prompt.ShowDialog("How many edges do you want to draw?", "OK");
                for (int i = 0; i < howManyVertices; i++)
                {
                    Vertex vertex = new Vertex();
                    vertex.SetVertexNumber(promptVertexID());
                    vertex.SetCoordinate(promptVertexCoordinates(vertex));
                    vertex.Draw(g);
                    graph.AddVertex(vertex);
                }

                for (int i = 0; i < howManyEdges; i++)
                {
                    Edge edge = new Edge();
                    edge = promptEdge(graphID);
                    edge.Draw(g);
                }
                _graphs.Add(graph);

            }
            public void Revise(int graphID)
            {
                Graph graph = getInstance().GetGraph(graphID);
                int chosenVertexNum = Prompt.ShowDialog("Enter the vertex number: ", "Done");
                int chosenEdgeNum = Prompt.ShowDialog("Enter the edge number: ", "Done");
                Vertex chosenVertex = graph.GetVertex(chosenVertexNum);
                chosenVertex.SetCoordinate(promptVertexCoordinates(chosenVertex));
                Edge chosenEdge = graph.GetEdge(chosenEdgeNum);
                chosenEdge = promptEdge(graphID);
            }
            public void Copy(int graphID)
            {
                Graph copyGraph = new Graph();
                _graphs.Add(copyGraph);
            }
        }
        public class Graph : ICloneable
        {
            private List<Vertex> _vertices = new List<Vertex>();
            private List<Edge> _edges = new List<Edge>();
            private int _id;
            public Vertex GetVertex(int vertexID)
            {
                return _vertices.Find(vertex => vertex.GetVertexNumber() == vertexID);
            }
            public void AddVertex(Vertex vertex)
            {
                _vertices.Add(vertex);
            }

            public Edge GetEdge(int edgeID)
            {
                return _edges.Find(edge => edge.GetEdgeNumber() == edgeID);
            }

            public void AddEdge(Edge edge)
            {
                _edges.Add(edge);
            }
            public List<Vertex> GetAllVertices()
            {
                return _vertices;
            }
            public void SetVerticees(List<Vertex> vertices)
            {
                _vertices = vertices;
            }
            public List<Edge> GetAllEdges()
            {
                return _edges;
            }
            public void SetEdges(List<Edge> edges)
            {
                _edges = edges;
            }
            public int GetId()
            {
                return _id;
            }
            public void SetId(int id)
            {
                _id = id;
            }

            public object Clone()
            {
                Graph tempG = new Graph();
                tempG.SetVerticees(_vertices.Select(a => a.Clone()).Cast<Vertex>().ToList());
                tempG.SetEdges(_edges.Select(a => a.Clone()).Cast<Edge>().ToList());
                tempG.SetId(_id);
                return tempG;
            }
        }
        public class Vertex
        {
            private int _vertex_number;
            private int _x_coordinate;
            private int _y_coordinate;
            public int GetVertexNumber()
            {
                return _vertex_number;
            }
            public void SetVertexNumber(int id)
            {
                _vertex_number = id;
            }
            public Point GetCoordinate()
            {
                return new Point(_x_coordinate, _y_coordinate);
            }
            public void SetCoordinate(Point point)
            {
                _x_coordinate = point.X;
                _y_coordinate = point.Y;
            }
            public void Draw(Graphics g)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                g.FillRectangle(brush, new Rectangle(_x_coordinate, _y_coordinate, 1, 1));
            }
            public object Clone()
            {
                Vertex tempV = new Vertex();
                tempV.SetCoordinate(new Point(_x_coordinate, _y_coordinate));
                tempV.SetVertexNumber(_vertex_number);
                return (Vertex)tempV;
            }
        }
        public class Edge
        {
            private int _edge_number;
            private Vertex _from_vertex;
            private Vertex _to_vertex;
            public int GetEdgeNumber()
            {
                return _edge_number;
            }
            public void SetEdgeNumber(int eNum)
            {
                _edge_number = eNum;
            }
            public Vertex GetFromVertex()
            {
                return _from_vertex;
            }
            public void SetFromVertex(Vertex vertex)
            {
                _from_vertex = vertex;
            }
            public Vertex GetToVertex()
            {
                return _to_vertex;
            }
            public void SetToVertex(Vertex vertex)
            {
                _to_vertex = vertex;
            }

            public void Draw(Graphics g)
            {
                Pen pen = new Pen(Color.Black);
                g.DrawLine(pen, _from_vertex.GetCoordinate(), _to_vertex.GetCoordinate());
            }
            public object Clone()
            {
                Edge tempE = new Edge();
                tempE.SetFromVertex(_from_vertex);
                tempE.SetToVertex(_to_vertex);
                tempE.SetEdgeNumber(_edge_number);
                return (Edge)tempE;
            }
        }
        private void graphsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGraphId = int.Parse(graphsDropDown.SelectedItem.ToString());
        }

        private void create_Click(object sender, EventArgs e)
        {
            selectedGraphId = Prompt.ShowDialog("Enter the graphic ID for your new graph: ", "OK");
            graphsDropDown.Items.Add(selectedGraphId);
            GraphManager.getInstance().Create(selectedGraphId);
        }

        private void revise_Click(object sender, EventArgs e)
        {
            GraphManager.getInstance().Revise(selectedGraphId);
        }

        private void copy_Click(object sender, EventArgs e)
        {
            GraphManager.getInstance().Copy(selectedGraphId);
        }

        public static class Prompt
        {
            public static int ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = prompt.Width - 100 };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 50 };
                Button confirmation = new Button() { Text = "Ok", Left = 120, Width = 100, Top = 50, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;


                return prompt.ShowDialog() == DialogResult.OK ? Int32.Parse(textBox.Text) : 0;
            }
        }
    }
}
