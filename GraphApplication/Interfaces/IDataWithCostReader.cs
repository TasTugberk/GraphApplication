using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Interfaces
{
    interface IDataWithCostReader<TVertex> : IDataReader<TVertex>
    {
        /// <summary>
        /// Read given file and returns adjacents.
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <returns>Source Vertex index, Destination Vertex index and cost of adjacents</returns>
        IList<Tuple<int, int, double>> GetNodes(string fileName);
    }
}
