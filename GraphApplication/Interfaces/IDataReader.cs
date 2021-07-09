using GraphApplication.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Interfaces
{
    public  interface IDataReader<TVertex>
    {
        /// <summary>
        /// Read given file and returns Vertex list.
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <returns>Vertex list</returns>
        IList<TVertex> GetVertices(string fileName);
    }
}
