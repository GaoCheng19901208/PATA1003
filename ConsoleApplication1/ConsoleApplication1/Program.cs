using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /// <summary>
    /// ShortestPathByDFS 20180101
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ShortestPath shortestPath = new ShortestPath();
            Console.ReadKey();
        }
    }

    class ShortestPath
    {
        private List<Node>[] adjList;
        private int[] teamsInfos;
        private int start;
        private int end;
        private int best = int.MaxValue;
        private List<int> visited = new List<int>();
        private int count;
        private int bestT;

        public ShortestPath()
        {
            Init();
            if(start == end)
            {
                count = 1;
                bestT = teamsInfos[start];
            }
            else
                DFS(adjList[start], start, 0);

            Console.WriteLine("{0} {1}", count, bestT);
        }

        void Init()
        {
            string basic = Console.ReadLine();
            string teams = Console.ReadLine();
            string[] basicInfos = basic.Split(' ');
            teamsInfos = teams.Split(' ').Select(s => int.Parse(s)).ToArray();

            int citys = int.Parse(basicInfos[0]);
            int roads = int.Parse(basicInfos[1]);
            start = int.Parse(basicInfos[2]);
            end = int.Parse(basicInfos[3]);
            adjList = new List<Node>[citys];
            for (int i = 0; i < roads; i++)
            {
                string path = Console.ReadLine();
                string[] paths = path.Split(' ');
                int city1 = int.Parse(paths[0]);
                int city2 = int.Parse(paths[1]);
                int weight = int.Parse(paths[2]);

                if (adjList[city1] == null)
                    adjList[city1] = new List<Node>();
                adjList[city1].Add(new Node(city2, weight));

                if (adjList[city2] == null)
                    adjList[city2] = new List<Node>();
                adjList[city2].Add(new Node(city1, weight));
            }
        }

        void DFS(List<Node> nodeList, int sta, int dis)
        {
            if (nodeList == null)
                return;

            visited.Add(sta);
            for (int i = 0; i < nodeList.Count; i++)
            {
                int curDis = dis + nodeList[i].Weight;
                if (nodeList[i].Next == end)
                {
                    if (curDis <= best)
                    {
                        if (curDis < best)
                        {
                            best = dis + nodeList[i].Weight;
                            count = 1;
                            bestT = 0;
                        }
                        else if (curDis == best)
                            count++;

                        int curBestT = teamsInfos.Where((s, index) => visited.Contains(index)).Sum() + teamsInfos[end];
                        if (curBestT > bestT)
                            bestT = curBestT;
                    }
                }
                else if (!visited.Contains(nodeList[i].Next))
                {
                    if (curDis < best)
                        DFS(adjList[nodeList[i].Next], nodeList[i].Next, curDis);
                }
            }
            visited.Remove(sta);
        }
    }

    class Node
    {
        public Node(int next, int weight)
        {
            Next = next;
            Weight = weight;
        }

        public int Next
        {
            get;set;
        }

        public int Weight
        {
            get;set;
        }
    }
}
