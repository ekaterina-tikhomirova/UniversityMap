using Infrastructure.Services.Models;
using System.Collections.Generic;

namespace Infrastructure.Services.PathResolver
{
    class ShortestPathResolverService
    {
        public Graph Graph;
        ShortestPathModel result;

        public ShortestPathResolverService()
        {
            result = new ShortestPathModel();
            result.FinalDistance = 0;
        }

        public ShortestPathModel FindShortestPath(MapModel map, int idStart, int idFinish)
        {
            Graph = new Graph();
            foreach (var room in map.rooms)
            {
                Graph.AddVertex(room);
            }

            foreach (var path in map.paths)
            {
                Graph.AddEdge(path.FirstRoomId, path.SecondRoomId, path.Distance);
            }

            return FindShortestPath(Graph.FindVertex(idStart), Graph.FindVertex(idFinish));
        }
        public ShortestPathModel FindShortestPath(Graph graph, int idStart, int idFinish)
        {
            Graph = new Graph();
            foreach (var vertex in graph.Vertices)
            {
                Graph.AddVertex(vertex.Id);
            }
            foreach (var edge in graph.Edges)
            {
                Graph.AddEdge(edge.FirstVertex.Id, edge.SecondVertex.Id, edge.EdgeWeight);
            }
            foreach (var vertex in Graph.Vertices)
            {
                vertex.EdgesWeightSum = int.MaxValue;
                vertex.IsUnvisited = true;
                vertex.NextVertices = new List<GraphVertex>();
                vertex.PreviousVertex = null;
            }
            return FindShortestPath(Graph.FindVertex(idStart), Graph.FindVertex(idFinish));
        }

        public ShortestPathModel FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            startVertex.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }
        public GraphVertex FindUnvisitedVertexWithMinSum()
        {
            var minValue = double.MaxValue;
            GraphVertex minVertexInfo = null;
            foreach (var Vertex in Graph.Vertices)
            {
                if (Vertex.IsUnvisited && Vertex.EdgesWeightSum < minValue)
                {
                    minVertexInfo = Vertex;
                    minValue = Vertex.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }
        void SetSumToNextVertex(GraphVertex info)
        {
            info.IsUnvisited = false;
            foreach (var Edge in info.Edges)
            {
                var nextInfo = Edge.ConnectedVertex;
                var sum = info.EdgesWeightSum + Edge.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info;
                }
            }
        }

        ShortestPathModel GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            List<int> ResultList = new List<int>();
            List<RoomModel> RoomModels = new List<RoomModel>();
             result.FinalDistance = endVertex.EdgesWeightSum;
            while (startVertex != endVertex)
            {
                if (endVertex.PreviousVertex == null)
                {
                    return null;
                }
                RoomModels.Add(new RoomModel
                {
                    Id = endVertex.Id,
                    X = endVertex.X,
                    Y = endVertex.Y,
                    Floor = endVertex.Floor,
                    Number = endVertex.Number
                });

                ResultList.Add(endVertex.Id);
                endVertex = endVertex.PreviousVertex;
            }
            RoomModels.Add(new RoomModel
            {
                Id = startVertex.Id,
                X = startVertex.X,
                Y = startVertex.Y,
                Floor = startVertex.Floor,
                Number = startVertex.Number
            });

            ResultList.Add(startVertex.Id);
            ResultList.Reverse();
            result.Path = ResultList;
            result.Rooms = RoomModels;
            foreach (var vertex in Graph.Vertices)
            {
                vertex.EdgesWeightSum = int.MaxValue;
                vertex.IsUnvisited = true;
                vertex.NextVertices = new List<GraphVertex>();
                vertex.PreviousVertex = null;
            }
            return result;
        }
    }

    public class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public List<GraphEdge> Edges { get; }
        public Graph()
        {
            Vertices = new List<GraphVertex>();
            Edges = new List<GraphEdge>();
        }

        public void AddVertex(int id)
        {
            Vertices.Add(new GraphVertex(id));
        }

        public void AddVertex(RoomModel room)
        {
            Vertices.Add(new GraphVertex(room));
        }

        public GraphEdge GetEdge(string FirstVertexName, string SecondVertexName)
        {
            foreach (var item in Edges)
            {
                if (FirstVertexName.Equals(item.FirstVertex.Id) && SecondVertexName.Equals(item.SecondVertex.Id)) return item;
            }
            return null;
        }
        public GraphVertex FindVertex(int id)
        {
            foreach (var Vertex in Vertices)
            {
                if (Vertex.Id.Equals(id))
                {
                    return Vertex;
                }
            }

            return null;
        }

        public void AddEdge(int firstId, int secondId, double weight)
        {
            Edges.Add(new GraphEdge(FindVertex(firstId), FindVertex(secondId), weight));
            Edges.Add(new GraphEdge(FindVertex(secondId), FindVertex(firstId), weight));
            var FirstVertex = FindVertex(firstId);
            var SecondVertex = FindVertex(secondId);
            if (SecondVertex != null && FirstVertex != null)
            {
                FirstVertex.AddEdge(SecondVertex, weight);
                SecondVertex.AddEdge(FirstVertex, weight);
            }
        }
    }
    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }

        public double EdgeWeight { get; }
        public GraphVertex FirstVertex { get; set; }
        public GraphVertex SecondVertex { get; set; }
        public GraphEdge(GraphVertex ConnectedVertex, double Weight)
        {
            this.ConnectedVertex = ConnectedVertex;
            EdgeWeight = Weight;
        }
        public GraphEdge(GraphVertex FirstVert, GraphVertex SecondVert, double Weight)
        {
            ConnectedVertex = FirstVert;
            FirstVertex = FirstVert;
            SecondVertex = SecondVert;
            EdgeWeight = Weight;
        }
    }

    public class GraphVertex
    {
        public int Id { get; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }


        public List<GraphEdge> Edges { get; }
        public bool IsUnvisited { get; set; }
        public double EdgesWeightSum { get; set; }
        public GraphVertex PreviousVertex { get; set; }

        public List<GraphVertex> NextVertices { get; set; }

        public GraphVertex(int id)
        {
            Id = id;
            Edges = new List<GraphEdge>();
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
            NextVertices = new List<GraphVertex>();
        }

        public GraphVertex(RoomModel room)
        {
            Id = room.Id;
            X = room.X;
            Y = room.Y;
            Floor = room.Floor;
            Number = room.Number;
            Edges = new List<GraphEdge>();
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
            NextVertices = new List<GraphVertex>();
        }

        public void AddNextVertex(GraphVertex secondVertex)
        {
            secondVertex.PreviousVertex = this;
            NextVertices.Add(secondVertex);
        }
        public void AddEdge(GraphEdge NewEdge)
        {
            Edges.Add(NewEdge);
        }
        public void AddEdge(GraphVertex Vertex, double EdgeWeight)
        {
            if (!FindEdge(Vertex.Id))
            {
                AddEdge(new GraphEdge(Vertex, EdgeWeight));
            }
        }
        public void AddEdge(GraphVertex firstVertex, GraphVertex secondVertex, int weight)
        {
            Edges.Add(new GraphEdge(firstVertex, secondVertex, weight));
        }
        public GraphEdge GetEdge(GraphVertex firstVertex, GraphVertex secondVertex)
        {
            foreach (var edge in Edges)
            {
                if (edge.FirstVertex == null) continue;
                else
                {
                    if (edge.FirstVertex == firstVertex && edge.SecondVertex == secondVertex) return edge;
                }
            }
            return default;
        }



        public bool FindEdge(int id)
        {
            foreach (var Edge in Edges)
            {
                if (Edge.ConnectedVertex.Id.Equals(id))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
