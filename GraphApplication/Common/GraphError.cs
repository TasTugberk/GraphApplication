using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Common
{
    /// <summary>
    /// Hatalar için base sınıf.
    /// </summary>
    public class GraphError : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="errorCode">Error Code</param>
        public GraphError(ErrorCode errorCode) : base(errorCode.GetDescription())
        {
            this.ErrorCode = errorCode;
        }
    }

    /// <summary>
    /// Error Codes.
    /// </summary>
    public enum ErrorCode
    {
        [Resource("Error_CannotAddNullVertex")]
        CannotAddNullVertex = 1000,
        [Resource("Error_CannotAddNullNode")]
        CannotAddNullNode,
        [Resource("Error_NullSourceVertex")]
        NullSourceVertex,
        [Resource("Error_NullDestinationVertex")]
        NullDestinationVertex,
        [Resource("Error_SourceVertexNotExist")]
        SourceVertexNotExist,
        [Resource("Error_DestinationVertexNotExist")]
        DestinationVertexNotExist,
        [Resource("Error_InvalidCost")]
        InvalidCost,
        [Resource("Error_DuplicateVertex")]
        DuplicateVertex,
        [Resource("Error_DuplicateNode")]
        DuplicateNode,
        [Resource("Error_CannotTraverseNullOrEmptyGraph")]
        CannotTraverseNullOrEmptyGraph,
        [Resource("Error_CannotTraverseGraphWithoutNode")]
        CannotTraverseGraphWithoutNode,
        [Resource("Error_NullStartVertex")]
        NullStartVertex,
        [Resource("Error_GraphNotContainsStartVertex")]
        GraphNotContainsStartVertex
    }

    public static class GraphErrorEnumExtension
    {
        public static string GetDescription(this ErrorCode errorCode)
        {
            var memInfo = typeof(ErrorCode).GetMember(errorCode.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(ResourceAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                var description = ((ResourceAttribute)attributes[0]).ResourceKey;
                ResourceManager resourceManager = new ResourceManager(typeof(GraphResource));
                var message = resourceManager.GetString(description);
                return message;
            }
            else
            {
                return errorCode.ToString();
            }
        }
    }
}
