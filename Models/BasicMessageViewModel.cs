namespace rook_aoc_2016.Models;

public enum BasicMessageType {
    Success,
    Warning,
    Error
}

public class BasicMessageViewModel
{
    public string Message { get; set; } = "";

    public BasicMessageType MessageType { get; set; } = BasicMessageType.Success;

    public string GetCSSClass()
    {
        switch (this.MessageType) {
            case BasicMessageType.Success:
                return "status-message-success";
            case BasicMessageType.Warning:
                return "status-message-warning";
            case BasicMessageType.Error:
                return "status-message-error";
            default:
                return "status-message-warning";
        }
    }
}