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
        public static Form1 instance { get; private set; }

        public Form1()
        {
            InitializeComponent();

            if (instance != null) { 
                 throw new Exception("There should be only one Form1 instace");
            }

            instance = this;

            bm = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            g = Graphics.FromImage(bm);
            DrawingArea.Image = bm;
        }

        int selectedGraphId;
        Bitmap bm;
        Graphics g;
        Pen pen = new Pen(Color.Black);
        SolidBrush brush = new SolidBrush(Color.Black);

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
                MessageBox.Show("Graph with an ID: " + graphID + " could not be found");
                return null;
            }
            public Point promptVertexCoordinates(Vertex vertex)
            {
                int tempx = Prompt.ShowDialog("Enter the vertex " + vertex.GetVertexNumber() + "'s x coordinate: ", "OK");
                int tempy = Prompt.ShowDialog("Enter the vertex " + vertex.GetVertexNumber() + "'s y coordinate: ", "OK");
                return new Point(tempx, tempy);
            }
            public int promptVertexID(int graphID)
            { 
                int tempID = Prompt.ShowDialog("Enter the new vertex's ID: ", "OK");
                            
                foreach (Vertex vertex in GraphManager.getInstance().GetGraph(graphID).GetAllVertices())
                {
                    if(vertex.GetVertexNumber() == tempID)
                    {
                        MessageBox.Show("There is a vertex with the same ID in the graph with ID: " + graphID);
                        tempID = Prompt.ShowDialog("Enter the new vertex's ID: ", "OK");
                    }
                }

                return tempID;
            }
            public Edge promptEdge(int graphID)
            {
                Edge edge = new Edge();
                int edgeId = Prompt.ShowDialog("Enter the new edge's ID: ", "OK");

                foreach (Edge e in GraphManager.getInstance().GetGraph(graphID).GetAllEdges())
                {
                    if(e.GetEdgeNumber() == edgeId)
                    {
                        MessageBox.Show("There is a edge with the same ID in the graph with ID: " + graphID);
                        edgeId = Prompt.ShowDialog("Enter the new edge's ID: ", "OK");
                    }
                }

                edge.SetEdgeNumber(edgeId);
                promptEdgeVertices(edge, graphID);
                return edge;
            }
            private void promptEdgeVertices(Edge edge, int graphID)
            {
                int fromVertexID = Prompt.ShowDialog("From a vertex with an ID: ", "Continue");
                int toVertexID = Prompt.ShowDialog("To a vertex with an ID: ", "Finish");
                edge.SetFromVertex(getInstance().GetGraph(graphID).GetVertex(fromVertexID));
                edge.SetToVertex(getInstance().GetGraph(graphID).GetVertex(toVertexID));
            }
            public int promptGraphID()
            {
                return Prompt.ShowDialog("Enter the graphic ID for your new graph: ", "OK");
            }
            public void Create(int graphID)
            {
                Graph graph = new Graph();
                graph.SetId(graphID);
                _graphs.Add(graph);

                int howManyVertices, howManyEdges;
                howManyVertices = Prompt.ShowDialog("How many vertices do you want to draw?", "OK");
                howManyEdges = Prompt.ShowDialog("How many edges do you want to draw?", "OK");
                for (int i = 0; i < howManyVertices; i++)
                {
                    Vertex vertex = new Vertex();
                    vertex.SetVertexNumber(promptVertexID(graphID));
                    vertex.SetCoordinate(promptVertexCoordinates(vertex));
                    vertex.Draw(Form1.instance.g);
                    GraphManager.getInstance().GetGraph(graphID).AddVertex(vertex);
                }

                for (int i = 0; i < howManyEdges; i++)
                {
                    Edge edge = new Edge();
                    edge = promptEdge(graphID);
                    GraphManager.getInstance().GetGraph(graphID).AddEdge(edge);
                    edge.Draw(Form1.instance.g);
                }

                Form1.instance.graphsDropDown.Items.Add(graphID);
                Form1.instance.graphsDropDown.Text = graphID.ToString();

            }
            public void Revise(int graphID)
            {
                Graph graph = GraphManager.getInstance().GetGraph(graphID);
                Vertex chosenVertex;
                Edge chosenEdge;
                int chosenVertexNum = Prompt.ShowDialog("Enter the vertex number of the vertex you want to revise for graph " + graphID + ": " , "Done");
                try
                {
                    chosenVertex = graph.GetVertex(chosenVertexNum);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Vertex with an ID: " + chosenVertexNum +" could not be found");
                    return;
                }

                chosenVertex.SetCoordinate(promptVertexCoordinates(chosenVertex));
                chosenVertex.Draw(Form1.instance.g);
                int chosenEdgeNum = Prompt.ShowDialog("Enter the edge number of the edge you want to revise for graph " + graphID + ": " , "Done");

                try
                {
                    chosenEdge = graph.GetEdge(chosenEdgeNum);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Edge with an ID: " + chosenEdgeNum +" could not be found");
                    return;
                }
                promptEdgeVertices(chosenEdge, graphID);
                chosenEdge.Draw(Form1.instance.g);
            }
            public void Copy(int graphID)
            {
                Graph copyGraph = new Graph();
                GraphManager graphManager = GraphManager.getInstance();
                copyGraph = (Graph)graphManager.GetGraph(graphID).Clone();
                int newID = graphManager.promptGraphID();
                MessageBox.Show("Cloning graph with an ID: " + graphID);
                copyGraph.SetId(newID);
                Form1.instance.graphsDropDown.Items.Add(newID);
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
                g.FillRectangle(Form1.instance.brush, new Rectangle(_x_coordinate, _y_coordinate, 1, 1));
                Form1.instance.DrawingArea.Refresh();
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
                g.DrawLine(Form1.instance.pen, _from_vertex.GetCoordinate(), _to_vertex.GetCoordinate());
                Form1.instance.DrawingArea.Refresh();
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
            selectedGraphId = GraphManager.getInstance().promptGraphID();
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
                int result = 0;
                Form prompt = new Form()
                {
                    Width = 420,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = prompt.Width - 40 };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
                Button confirmation = new Button() { Text = "OK", Left = 150, Width = 100, Top = 100, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
               
                try
                {
                    if(prompt.ShowDialog() == DialogResult.OK){
                        result = Int32.Parse(textBox.Text);
                    }
                    else
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    ShowDialog(text,caption);
                }
               
                return result;
            }
        }
    }
}
