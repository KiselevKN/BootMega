namespace Service.HexFile.Record
{
    /// <summary>
    /// Hex record type
    /// </summary>
    public enum HexRecordType : byte
    {
        /// <summary>
        /// The record contains data
        /// </summary>
        Data = 0x00,

        /// <summary>
        /// End of file
        /// </summary>
        EOF = 0x01,

        /// <summary>
        /// Extended segment address
        /// </summary>
        ExtendedSegmentAddress = 0x02,

        /// <summary>
        /// Start segment address (reserve)
        /// </summary>
        StartSegmentAddress = 0x03,

        /// <summary>
        /// Extended linear address
        /// </summary>
        ExtendedLinearAddress = 0x04,

        /// <summary>
        /// Start linear address (reserve)
        /// </summary>
        StartLinearAddress = 0x05
    }
}
