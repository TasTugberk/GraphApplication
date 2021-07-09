using GraphApplication.Interfaces;
using GraphApplication.Node;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication
{
    public class IntegerWeightedGraphDataReader : IDataWithCostReader<int>
    {
        /// <summary>
        /// Read given file and returns adjacents.
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <returns>Source Vertex index, Destination Vertex index and cost of adjacents</returns>
        public IList<Tuple<int, int, double>> GetNodes(string fileName)
        {
            IList<Tuple<int, int, double>> result = new List<Tuple<int, int, double>>();
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] indexes = line.Split(" ");
                    int sourceVertexIndex = int.Parse(indexes[0]);
                    int destinationVertexIndex = int.Parse(indexes[1]);
                    double cost = double.Parse(indexes[2]);

                    result.Add(new Tuple<int, int, double>(sourceVertexIndex, destinationVertexIndex, cost));
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occured while reading file:" + exception.Message);
            }

            return result;
        }

        public IList<int> GetVertices(string fileName)
        {
            IList<int> result = new List<int>();
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    result.Add(int.Parse(line));
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occured while reading file:" + exception.Message);
            }

            return result;
        }
    }
}
