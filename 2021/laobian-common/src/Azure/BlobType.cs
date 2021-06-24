namespace Laobian.Common.Azure
{
    /// <summary>
    /// Content type of blob
    /// </summary>
    public enum BlobType
    {
        /// <summary>
        /// JSON format
        /// </summary>
        Json,

        /// <summary>
        /// ProtoBuf format
        /// </summary>
        ProtoBuf,

        /// <summary>
        /// Other formats which is neither JSON nor ProtoBuf
        /// </summary>
        Other
    }
}
