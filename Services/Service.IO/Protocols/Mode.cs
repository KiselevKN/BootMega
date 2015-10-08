namespace Service.IO.Protocols
{
    public enum Mode : byte
    {
        Connection = 1,
        ReadFlashPage = 2,
        WriteFlashPage = 3,
        IsEmptyFlashPage = 4,
        ReadFuse = 5,
        ReadLock = 6,
        Reset = 7,
        EraseFlashPage = 8,
        ReadEepromPage = 9,
        WriteEepromPage = 10,
        EraseEepromPage = 11,
        IsEmptyEepromPage = 12
    }
}
